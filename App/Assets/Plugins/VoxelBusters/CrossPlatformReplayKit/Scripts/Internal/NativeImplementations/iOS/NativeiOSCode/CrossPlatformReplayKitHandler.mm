//
//  Cross Platform Replay Kit
//
//  Created by Ayyappa Reddy on 03/06/19.
//  Copyright (c) 2019 Voxel Busters Interactive LLP. All rights reserved.
//

#include "CrossPlatformReplayKitHandler.h"
#include "UnityReplayKit.h"
#include "ReplayKit+FileAccess.h"
#import <UIKit/UIKit.h>
#import "UnityAppController.h"
#import <ReplayKit/ReplayKit.h>

#define kReplayKitNativeGameObject              "ReplayKitInternal"
#define kSavingToGalleryFinished                "OnReplayKitSaveToGalleryFinished"
#define kVideoRecordingStarted                  "OnReplayKitRecordingStarted"
#define kVideoRecordingStopped                  "OnReplayKitRecordingStopped"
#define kRecordingVideoAvailable                "OnReplayKitRecordingAvailable"
#define kRecordingVideoFailed                   "OnReplayKitRecordingFailed"


@implementation CrossPlatformReplayKitHandler

#pragma mark - Singleton Instance
+ (id)sharedInstance
{
    static CrossPlatformReplayKitHandler *sharedInstance = nil;
    @synchronized(self) {
        if (sharedInstance == nil)
        {
            sharedInstance = [[self alloc] init];
        }
    }
    return sharedInstance;
}

- (id) init
{
    self = [super init];
    self.sessionQueue = dispatch_queue_create("Replay Kit Session Queue", DISPATCH_QUEUE_SERIAL);
    
    return self;
}

#pragma mark - Query Methods

- (BOOL) isCurrentlyRecording
{
    return _isRecording;
}


- (BOOL) isPreviewAvailable
{
    return (!_isRecording) && (_recordingPath != NULL);
}

- (NSString*) getPreviewFilePath
{
    if(!_isRecording)
    {
        return _recordingPath;
    }
    else
    {
        return NULL;
    }
}




#pragma mark - Microphone Capture Setup

- (void) createCaptureSession
{
    // Set this to avoid stuttering
    [[AVAudioSession sharedInstance] setCategory:AVAudioSessionCategoryPlayAndRecord
                                     withOptions:AVAudioSessionCategoryOptionMixWithOthers | AVAudioSessionCategoryOptionDefaultToSpeaker
                                           error:nil];
    
    self.session = [[AVCaptureSession alloc] init];
    
    // Get microphone capture device
    AVCaptureDevice *captureDevice = [self getCaptureDevice:AVCaptureDeviceTypeBuiltInMicrophone];
    
    self.micCaptureInput    = [self createCaptureDeviceInput: captureDevice];
    self.micCaptureOutput   = [self createCaptureDeviceOutput];
    
    // Set the delegate
    [self.micCaptureOutput setSampleBufferDelegate:self queue:dispatch_get_main_queue()];
    
    
    if ([self.session canAddInput:self.micCaptureInput])
    {
        [self.session addInput:self.micCaptureInput];
    }
    
    if ([self.session canAddOutput:self.micCaptureOutput])
    {
        [self.session addOutput:self.micCaptureOutput];
    }
}

- (AVCaptureDevice*) getCaptureDevice:(AVCaptureDeviceType)type
{
    AVCaptureDeviceDiscoverySession *discoverySession = [AVCaptureDeviceDiscoverySession discoverySessionWithDeviceTypes:@[type]
                                                                                                               mediaType:AVMediaTypeAudio
                                                                                                                position:AVCaptureDevicePositionUnspecified];
    
    NSArray *devices = discoverySession.devices;
    for (AVCaptureDevice *device in devices)
    {
        if (device.deviceType == type)
        {
            return device;
        }
    }
    
    return NULL;
}

- (AVCaptureDeviceInput*) createCaptureDeviceInput:(AVCaptureDevice*) device
{
    NSError *error = nil;
    AVCaptureDeviceInput *input = [[AVCaptureDeviceInput alloc] initWithDevice:device error:&error];
    if(error != nil)
    {
        NSLog(@"Error creating AVCaptureDeviceInput : %@", error);
        return NULL;
    }
    else
    {
        return input;
    }
}

- (AVCaptureAudioDataOutput*) createCaptureDeviceOutput
{
    AVCaptureAudioDataOutput *output = [[AVCaptureAudioDataOutput alloc] init];
    return output;
}

#pragma mark - AVCaptureAudioDataOutputSampleBufferDelegate Implementation

- (void)captureOutput:(AVCaptureOutput *)captureOutput didOutputSampleBuffer:(CMSampleBufferRef)sampleBuffer fromConnection:(AVCaptureConnection *)connection {
    
    if (self.writer.status != AVAssetWriterStatusWriting)
    {
        return;
    }

    if(_initialisedWriter && _mic.isReadyForMoreMediaData)
    {
        [_mic appendSampleBuffer:sampleBuffer];
    }
}

#pragma mark - Recording Methods

- (void) startRecording: (BOOL) isMicrophoneEnabled
{
    // Register for pause events
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(applicationDidEnterBackground:) name:UIApplicationDidEnterBackgroundNotification object:nil];
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(captureError:) name:AVCaptureSessionRuntimeErrorNotification object:nil];
    
    // Delete if any file exists
    [self discardRecording];
    
    // Request for microphone if microphone is enabled
    if(isMicrophoneEnabled)
    {
        AVAudioSession *audioSession = [AVAudioSession sharedInstance];
        [audioSession requestRecordPermission:^(BOOL granted) {
            if(!granted)
                NSLog(@"User denied microphone permission. Recording without microphone input now.");
            
            [self startRecordingInternal:granted];
        }];
    }
    else
    {
        [self startRecordingInternal:FALSE];
    }
}

- (void) startRecordingInternal :(BOOL) isMicrophoneEnabled
{
    RPScreenRecorder *recorder = [RPScreenRecorder sharedRecorder];
    
    // Due to bug in Replaykit, not use replay kit's capture for recording mic data. So forcefully setting to FALSE.
    recorder.microphoneEnabled = FALSE;
    
    // Set flags
    _initialisedWriter  = FALSE;
    _microphoneEnabled  = isMicrophoneEnabled;
    
    dispatch_async(self.sessionQueue, ^{
        
        // Add capture session if microphone is required
        if(isMicrophoneEnabled)
        {
            [self createCaptureSession];
            [self.session startRunning];
        }
        
        // Create and setup Asset Writer
        [self setupAssetWriter];
    });
        
    if (@available(iOS 11.0, *))
    {
        [recorder startCaptureWithHandler:^(CMSampleBufferRef  _Nonnull sampleBuffer, RPSampleBufferType bufferType, NSError * _Nullable error)
         {
             if (error != nil)
             {
                 NSLog(@"Sample Buffer Type : %d", (int)bufferType);
                 NSLog(@"Writer Status : %d", (int)_writer.status);
                 NSLog(@"Error  : %@", error);
                 return;
             }
             
             if (CMSampleBufferDataIsReady(sampleBuffer))
             {
                 if (self.writer.status != AVAssetWriterStatusWriting)
                     return;
                 
                 if (!_initialisedWriter)
                 {
                     _initialisedWriter = TRUE;
                     [_writer startSessionAtSourceTime:CMSampleBufferGetPresentationTimeStamp(sampleBuffer)];
                     UnitySendMessage(kReplayKitNativeGameObject, kVideoRecordingStarted, "true");
                 }
                 
                 if (_writer.status == AVAssetWriterStatusFailed)
                 {
                     NSLog(@"Error : Writer status =  AVAssetWriterStatusFailed : %@ %@", _writer.error.localizedFailureReason, _writer.error.localizedRecoverySuggestion);
                     [self cleanup:TRUE];
                     return;
                 }
                 switch (bufferType)
                 {
                     case RPSampleBufferTypeVideo:
                         
                         if(!_videoDataStarted)
                         {
                             _videoDataStarted = TRUE;
                             [_writer startSessionAtSourceTime:CMSampleBufferGetPresentationTimeStamp(sampleBuffer)];
                         }
                         
                         if(_video.isReadyForMoreMediaData)
                         {
                             [_video appendSampleBuffer:sampleBuffer];
                         }
                         break;
                     case RPSampleBufferTypeAudioApp:
                         
                         /*if(!_audioDataStarted)
                          {
                          _audioDataStarted = TRUE;
                          [_writer startSessionAtSourceTime:CMSampleBufferGetPresentationTimeStamp(sampleBuffer)];
                          }*/
                         if(_audio.isReadyForMoreMediaData)
                         {
                             [_audio appendSampleBuffer:sampleBuffer];
                         }
                         break;
                     default:
                         break;
                 }
             }
         } completionHandler:^(NSError * _Nullable error)
         {
             if(error == nil)
             {
                 NSLog(@"Screen capturing successfully started!");
                 _isRecording  = TRUE;
             }
             else
             {
                 NSLog(@"Screen capturing start failed with error : %@", error.localizedDescription);
                 [self cleanup : TRUE];
             }
             
         }];
    }
    else
    {
        // Fallback on earlier versions
        NSLog(@"[ReplayKit] : This plugin supports only from iOS 11 devices");
    }
}


- (void) stopRecording
{
    if(_isRecording)
    {

        [self.session stopRunning];
        if (@available(iOS 11.0, *)) {
                [[RPScreenRecorder sharedRecorder] stopCaptureWithHandler:^(NSError * _Nullable error) {
                    
                    if(error != NULL)
                    {
                        NSLog(@"Failed to stop capture : %@ ", error);
                        UnitySendMessage(kReplayKitNativeGameObject, kRecordingVideoFailed, "Failed to stop capture");
                    }
                    else
                    {
                        UnitySendMessage(kReplayKitNativeGameObject, kVideoRecordingStopped, "true");
                    }
                    
                    [_video markAsFinished];
                    [_audio markAsFinished];
                    
                    if(_microphoneEnabled)
                        [_mic markAsFinished];
                    
                    if(_writer.status == AVAssetWriterStatusWriting)
                    {
                        [_writer finishWritingWithCompletionHandler:^{
                            NSLog(@"Finished stopping recording!");
                            int status = _writer.status;
                            if (status == AVAssetWriterStatusFailed)
                            {
                                NSLog(@"Error : Writer status =  AVAssetWriterStatusFailed : %@ %@", _writer.error.localizedFailureReason, _writer.error.localizedRecoverySuggestion);
                                [self cleanup: TRUE];
                                
                                if(error == NULL)
                                {
                                    UnitySendMessage(kReplayKitNativeGameObject, kRecordingVideoFailed, "Failed stopping recording with status : AVAssetWriterStatusFailed");
                                }
                            }
                            else
                            {
                                UnitySendMessage(kReplayKitNativeGameObject, kRecordingVideoAvailable, "true");
                            }
                        }];
                    }
                    [self cleanup];
                }];
        } else {
            // Fallback on earlier versions
            NSLog(@"[ReplayKit] : This plugin supports only from iOS 11 devices");
        }
    }
    
    [[NSNotificationCenter defaultCenter] removeObserver:self name:UIApplicationDidEnterBackgroundNotification object:nil];
    [[NSNotificationCenter defaultCenter] removeObserver:self name:AVCaptureSessionRuntimeErrorNotification object:nil];
}

- (BOOL) previewRecording;
{
        NSString* filePath = [self getPreviewFilePath];
    
        if(filePath == nil)
            return FALSE;
    
        NSURL * url = [NSURL URLWithString:[@"file://" stringByAppendingString:filePath]];
    
        // Stop playing video
        if (_moviePlayerVC != nil) {
            [[_moviePlayerVC player] pause];
            _moviePlayerVC = nil;
        }
    
        _moviePlayerVC = [[AVPlayerViewController alloc] init];
        AVPlayer *player = [[AVPlayer alloc] initWithURL:url];
    
        _moviePlayerVC.player = player;
        [player play];
    
        [UnityGetGLViewController() presentViewController:_moviePlayerVC animated:TRUE completion:nil];

        return TRUE;
}

- (BOOL) discardRecording
{
    NSFileManager *fileManager = [NSFileManager defaultManager];
    NSString *filePath = [self getPreviewFilePath];
    BOOL success  = TRUE;
    if(filePath != NULL)
    {
       NSError *error;
       success = [fileManager removeItemAtPath:filePath error:&error];
       if (!success)
       {
           NSLog(@"Failed deleting the file : %@ ",[error localizedDescription]);
       }
    }
    // Setting to null as we don't need this file anymore
    _recordingPath = NULL;

    return success;
}

- (void)applicationDidEnterBackground:(id)sender
{
    [self stopRecording];
}

- (void)captureError:(id)sender
{
    NSLog(@"Info : %@ : ", sender);
}


- (void) cleanup
{
    [self cleanup : FALSE];
}


- (void) cleanup :(BOOL) writeFailure
{
    _isRecording = FALSE;
    
    if(writeFailure)
    {
        _recordingPath = NULL;
    }
}

- (void) setupAssetWriter
{
    NSError *writerError;
    NSURL *url = [self recordingURL];
    
    CGFloat contentScaleFactor = [[UIScreen mainScreen] scale];

    int width   = floor([UIScreen mainScreen].bounds.size.width/16) * 16;
    int height  = floor([UIScreen mainScreen].bounds.size.height/16) * 16;
    
    NSDictionary <NSString *, id> *videoSettings = @{
                                                     AVVideoCodecKey: AVVideoCodecTypeH264,
                                                     AVVideoWidthKey: @(width * contentScaleFactor),
                                                     AVVideoHeightKey: @(height * contentScaleFactor),
                                                     AVVideoScalingModeKey: AVVideoScalingModeResizeAspectFill
                                                     };
    
    NSDictionary <NSString *, id> *appAudioSettings = @{
                                                        AVFormatIDKey: @(kAudioFormatMPEG4AAC),
                                                        AVNumberOfChannelsKey: @(2),
                                                        AVSampleRateKey: @(44100.0),
                                                        AVEncoderBitRateKey: @(128000)
                                                        };
    
    NSDictionary <NSString *, id> *microphoneSettings = @{
                                                          AVFormatIDKey: @(kAudioFormatMPEG4AAC),
                                                          AVNumberOfChannelsKey: @(2),
                                                          AVSampleRateKey: @(44100.0),
                                                          AVEncoderBitRateKey: @(128000)
                                                          };
    
    
    _recordingPath = [url path];
    
    
    self.writer = [AVAssetWriter assetWriterWithURL:url fileType:AVFileTypeMPEG4 error:&writerError];
    
    
    self.video  = [AVAssetWriterInput assetWriterInputWithMediaType:AVMediaTypeVideo outputSettings:videoSettings];
    self.audio  = [AVAssetWriterInput assetWriterInputWithMediaType:AVMediaTypeAudio outputSettings:appAudioSettings];
    
    if(_microphoneEnabled)
        self.mic    = [AVAssetWriterInput assetWriterInputWithMediaType:AVMediaTypeAudio outputSettings:microphoneSettings];
    
    
    self.video.expectsMediaDataInRealTime   = YES;
    self.audio.expectsMediaDataInRealTime   = YES;
    
    if(_microphoneEnabled)
        self.mic.expectsMediaDataInRealTime     = YES;
    
    [self.writer addInput:self.video];
    
    if(_microphoneEnabled) //Order Imp
        [self.writer addInput:self.mic];
    
    [self.writer addInput:self.audio];

    BOOL isWriting = [_writer startWriting];
    NSLog(@"Is Writing %d",(int)isWriting);
}

- (NSURL *)recordingURL
{
    NSString *basePath = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES).firstObject;
    NSString *path = [basePath stringByAppendingPathComponent:@"Recordings"];
    
    if (![[NSFileManager defaultManager] fileExistsAtPath:path]) {
        [[NSFileManager defaultManager] createDirectoryAtPath:path withIntermediateDirectories:YES attributes:nil error:nil];
    }
    
    NSString *filename = [NSString stringWithFormat:@"Recording_%.0f.mp4", [NSDate date].timeIntervalSince1970];
    
    NSURL *url = [NSURL fileURLWithPath:[NSString pathWithComponents:@[path, filename]]];
    
#ifdef DEBUG
    NSLog(@"Recording Output URL: %@", url);
#endif
    
    return url;
}


#pragma mark - Utility Methods

-(void) savePreview:(NSString*) path
{
    NSURL *url = [NSURL URLWithString:path];
    
    BOOL compatible = UIVideoAtPathIsCompatibleWithSavedPhotosAlbum([url path]);
    if (compatible) {
        UISaveVideoAtPathToSavedPhotosAlbum([url path], self, @selector(savePreviewFinished:didFinishSavingWithError:contextInfo:), nil);
    }
    else
    {
        NSLog(@"Unable to save video to camera roll!");
        UnitySendMessage(kReplayKitNativeGameObject, kSavingToGalleryFinished, "UNKNOWN");
    }
}

-(void) sharePreview
{
    NSString *path = [self getPreviewFilePath];
    if(path != NULL)
    {
        NSURL       *url        = [NSURL fileURLWithPath:path];
        
        NSArray *activityItems = [NSArray arrayWithObjects:url, nil];
        
        UIActivityViewController *controller = [[UIActivityViewController alloc] initWithActivityItems:activityItems applicationActivities:nil];
        
        [GetAppController().rootViewController presentViewController:controller animated:YES completion:nil];
    }
    else
    {
        NSLog(@"No preview recording to share!");
    }
}

#pragma mark - Callback Methods

- (void)savePreviewFinished:(NSString *)videoPath didFinishSavingWithError:(NSError *)error contextInfo:(void *)contextInfo
{
    NSLog(@"Finished Saving to Gallery! [Error : %@]", error);
    UnitySendMessage(kReplayKitNativeGameObject, kSavingToGalleryFinished, (error == NULL) ? "" : ([self isPreviewAvailable] ? "PERMISSION_UNAVAILABLE" : "PREVIEW_UNAVAILABLE"));
}

@end


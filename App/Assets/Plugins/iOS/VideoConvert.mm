#import <AVFoundation/AVFoundation.h>


extern "C" {
  void ConvertVideo(const char* sourcePath,const char* desiredOutputPath){
	  NSString* sourcePathNs=[NSString stringWithUTF8String: sourcePath];
      NSString* resultPathNs=[NSString stringWithUTF8String: desiredOutputPath];
      AVURLAsset *avAsset = [AVURLAsset URLAssetWithURL:[NSURL fileURLWithPath:sourcePathNs] options:nil];
      NSString* preset = AVAssetExportPresetMediumQuality;
       AVAssetExportSession *exportSession = [[AVAssetExportSession alloc]initWithAsset:avAsset presetName:preset];
      exportSession.outputURL = [NSURL fileURLWithPath:resultPathNs];
      exportSession.outputFileType = AVFileTypeMPEG4;
      [exportSession exportAsynchronouslyWithCompletionHandler:^{
          const char *cOutputPath = [resultPathNs UTF8String];
          const char *cInputPath = [sourcePathNs UTF8String];
          if([exportSession status] == AVAssetExportSessionStatusCompleted){
              UnitySendMessage("VideoConvertListener", "OnVideoConvertSuccessHandler", cOutputPath);
          }
          else{
              if([exportSession status] == AVAssetExportSessionStatusFailed){
                  NSLog(@"Export failed: %@", [[exportSession error] localizedDescription]);
              }
              UnitySendMessage("VideoConvertListener", "OnVideoConvertFailHandler", cInputPath);
          }
      }];
  }
}

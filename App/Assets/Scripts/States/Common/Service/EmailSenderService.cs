using Assets.Scripts.Common.Thread;
using System;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Assets.Scripts.States.Common.Service
{
    public class EmailSenderService
    {
        private class EmailSendJob : ThreadedJob
        {
            public string sendStatus;
            private string _userEmail;
            private string[] _videoLinks;
            private string _htmlBody;

            public EmailSendJob(string userEmail, string[] videoLinks)
            {
                _userEmail = userEmail;
                _videoLinks = videoLinks;

                TextAsset htmlBodyAsset = Resources.Load<TextAsset>("EmailTemplate/New email template 2020-03-03");
                _htmlBody = htmlBodyAsset.text;
            }

            public void Send()
            {
                ThreadFunction();
            }

            protected override void ThreadFunction()
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("xgrowring.test@gmail.com");
                    mail.To.Add(_userEmail);
                    mail.Subject = "XGrowring Awesome Videos";
                    mail.IsBodyHtml = true;
                    _htmlBody = _htmlBody.Replace("%User_Name%", "Dear Friend");
                    for (int i = 0; i < _videoLinks.Length; i++)
                    {
                        _htmlBody = _htmlBody.Replace(string.Format("%link_{0}%", i + 1), _videoLinks[i]);
                    }
                    mail.Body = _htmlBody;
                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                    smtpServer.Port = 587;
                    smtpServer.Credentials = new NetworkCredential("xgrowring.test@gmail.com", "U96yukeEqgvTVTbL") as ICredentialsByHost;
                    smtpServer.EnableSsl = true;
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    { return true; };
                    sendStatus = "sending";
                    smtpServer.Send(mail);
                    sendStatus = "success";
                }
                catch (Exception ex)
                {
                    sendStatus = "error";
                    Debug.LogError(ex.ToString());
                }
            }
        }

        public string SendStatus;

        public IEnumerator AsyncSendEmail(string userEmail, string userName, string[] videoLinks)
        {
            EmailSendJob emailSendJob = new EmailSendJob(userEmail, videoLinks);
            emailSendJob.Start();
            yield return emailSendJob.WaitFor();
            SendStatus = emailSendJob.sendStatus;
        }

        public void SendEmail(string userEmail, string[] videoLinks)
        {
            EmailSendJob emailSendJob = new EmailSendJob(userEmail, videoLinks);
            emailSendJob.Send();
            SendStatus = emailSendJob.sendStatus;
        }
    }
}

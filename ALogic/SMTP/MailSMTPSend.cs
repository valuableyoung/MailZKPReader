using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALogic.DBConnector;
using System.Net.Mail;
using ALogic.Logic.Old;
using System.Net;

namespace ALogic.SMTP
{
    public class MailSMTPSend
    {

        public static void SendEmail(string mailFrom, string passWord, string mailTo, string subject, string body)
        {
            SmtpClient smtp = new SmtpClient(ProjectProperty.SmtpServer, ProjectProperty.SmtpPort);
            try
            {
             
                MailAddress from = new MailAddress(mailFrom);
                MailAddress to = new MailAddress(mailTo);
                MailMessage m = new MailMessage(from, to);
                m.Subject = subject;
                m.Body = body;

                
                smtp.Credentials = new NetworkCredential(mailFrom, passWord);
                smtp.EnableSsl = false;
                smtp.Send(m);
                
            }
            catch (Exception ex)
            {
                Logger.ShowMessage("Ошибка подключения к SMTP " + ex.Message);
               
            }
            finally
            {
                smtp.Dispose();
            }


        }

        public static void SendEmail(string mailFrom, string passWord, List<string> mailTo, string subject, string body, List<string> attachment)
        {
            SmtpClient smtp = new SmtpClient(ProjectProperty.SmtpServer, ProjectProperty.SmtpPort);
            try
            {
                 MailMessage m = new MailMessage();


                foreach (var s in mailTo)
                {
                    m.To.Add(s);
                }

                m.From = new MailAddress(mailFrom);
                m.Subject = subject;
                m.Body = body;

                if (attachment.Count > 0)
                {
                    foreach (var s in attachment)
                    {
                        m.Attachments.Add(new Attachment(s));
                    }

                }
                
                smtp.Credentials = new NetworkCredential(mailFrom, passWord);
                smtp.EnableSsl = false;
                              
                if (m.To.Count > 0)
                {
                    smtp.Send(m);
                }
                else
                {
                    
                }
               

            }
            catch (Exception ex)
            {
                Logger.ShowMessage("Ошибка подключения к SMTP " + ex.Message);

            }
            finally
            {
                smtp.Dispose();
            }




        }
    }
}

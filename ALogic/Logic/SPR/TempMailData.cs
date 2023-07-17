using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ALogic.Logic.SPR
{
    public static class TempMailData
    {

        public static void GO()
        {
           
            var client = new ImapClient();

            
            client.Connect("mail.arkona36.ru", 143, MailKit.Security.SecureSocketOptions.None);

            client.Authenticate("arkona@arkona36.ru", "arkona))511");
            
            IMailFolder inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            

            for (int i = 0; i < inbox.Count; i++)
            {
                var nmess = inbox.GetMessage(i);

                if (nmess.Subject == "Undelivered Mail Returned to Sender")
                {
                    if (nmess.TextBody != null)
                    {
                        String BODY = nmess.TextBody.Split('<').Last().Split('>').First();

                        if (BODY.IndexOf("@") != -1 && BODY.Length < 100)
                        {
                            string str = "insert into TempEmailBad values(@email, @date, @body)";
                            DateTime date = nmess.Date.Date;
                            SqlParameter pEmail = new SqlParameter("email", BODY);
                            SqlParameter pDate = new SqlParameter("date", date);
                            SqlParameter pBody = new SqlParameter("body", nmess.TextBody);

                            DBConnector.DBExecutor.ExecuteQuery(str, pEmail, pDate, pBody);


                        }
                    }

                }
            }

          

        }
    }
}

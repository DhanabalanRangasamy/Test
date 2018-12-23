using System;
using Microsoft.SharePoint.Client;
using System.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
namespace Addcolumn
{
    class connectspo
    {
        static void Main(string[] args)
        {
            string userName = "rangasad@bms.com";
            Console.WriteLine("Enter your password.");
            SecureString password = getpassword();
            using (var ctx = new ClientContext("https://sites.bms.com/sites/TestFeb2018"))
            {
                ctx.Credentials = new SharePointOnlineCredentials(userName, password);
                Web subsite = ctx.Web;
                ctx.Load(subsite);
                ctx.ExecuteQuery();
                List userlist = subsite.Lists.GetByTitle("INC2113278");
                ctx.Load(userlist);
                ctx.ExecuteQuery();
                ContentType ct = userlist.ContentTypes.GetById("0x010800753FC8C377CE3C408FCBAFD41D9899DC0801009CC99615DF717545957DB37E4EE020F7");
                ctx.Load(ct);
                ctx.ExecuteQuery();
                //Field column = userlist.Fields.GetByInternalNameOrTitle("Title");
                //ctx.Load(column);
                //ctx.ExecuteQuery();
                Console.WriteLine("Title: " + subsite.Title + "; URL: " + subsite.Url );
                Console.ReadLine();
                FieldCollection addcolumntoct = ct.Fields;
                Field column = addcolumntoct.GetByInternalNameOrTitle("Title");
                column.DeleteObject();
                ct.Update(true);
                ctx.ExecuteQuery();
                //foreach (var item in addcolumntoct)
                //{
                //    if (item.Name == "Title")
                 //       return;
                //}
                //FieldLinkCreationInformation link = new FieldLinkCreationInformation();
                //link.Field = column;
                //addcolumntoct.Add(link);
                //ct.Update(false);
                //ctx.ExecuteQuery();
            }
        }

        private static SecureString getpassword()
        {
            ConsoleKeyInfo info;
            SecureString securepassword = new SecureString();
            do
            {
                info = Console.ReadKey(true);
                if (info.Key != ConsoleKey.Enter)
                {
                    securepassword.AppendChar(info.KeyChar);
                }

            }
            while (info.Key != ConsoleKey.Enter);
            return securepassword;
        }
    }
}

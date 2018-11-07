using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Properties;

namespace Capstone.Models
{
    public class SendEmail
    {
        public static void Send(string Subject, string To, string Body)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", "key - 2f384d9107f1b66c80e3184a0c9acbb5");
            RestRequest request = new RestRequest();
            request.Resource = "sandboxb77e1ebbedbd44f2a4b62242f31805c9.mailgun.org/messages";
            request.AddParameter("from", "Garrett Davis <garrett052093@gmail.com>");
            request.AddParameter("to", To);
            request.AddParameter("subject", Subject);
            request.AddParameter("text", Body);
            request.Method = Method.POST;
            client.Execute(request);
        }
    }
}
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Capstone
{
    public class EmailSend: IEmailSend
    {
        public readonly string domain;
        public readonly string defaultFrom;
        public readonly RestClient restClient;

        public string MailgunAPIUrl = "https://api.mailgun.net/v3";

        public EmailSend(string mailgunApiKey, string domain, string defaultFrom)
        {
            if (string.IsNullOrEmpty(mailgunApiKey)|| string.IsNullOrWhiteSpace(mailgunApiKey))
            {
                throw new ArgumentException($"{nameof(EmailSend)}::ctor: The passed{nameof(mailgunApiKey)} string is either null or empty!", mailgunApiKey);
            }
            if(string.IsNullOrEmpty(domain) || !domain.Contains("."))
            {
                throw new ArgumentException($"{nameof(EmailSend)}::ctor: The passed {nameof(domain)} string is either null, empty, or invalid!", domain);
            }
            if(string.IsNullOrEmpty(defaultFrom) || !defaultFrom.Contains("@"))
            {
                throw new ArgumentException($"{nameof(EmailSend)}::ctor: The passed {nameof(defaultFrom)} string is either null, empty or not a valid email address!", defaultFrom);
            }
            this.domain = domain;
            this.defaultFrom = defaultFrom;

            restClient = new RestClient
            {
                BaseUrl = new Uri(MailgunAPIUrl),
                Authenticator = new HttpBasicAuthenticator("api", mailgunApiKey)
            };
        }
        public async Task<IRestResponse> SendEmailAsync(string from, string to, string subject, string text, string html, string replyTo, string[] additionalRecipients = null, string[] cc = null, string[] bcc = null, IEnumerable<Attatchment> attachments = null)
        {
            var request = new RestRequest
            {
                Method = Method.POST,
                Resource = "{domain}/messages"
            };

            request.AddParameter("domain", domain, ParameterType.UrlSegment);
            request.AddParameter("subject", subject ?? string.Empty);
            request.AddParameter("text", text ?? string.Empty);
            request.AddParameter("from", from);
            request.AddParameter("to", to);

            if (!string.IsNullOrEmpty(html))
            {
                request.AddParameter("html", html);
            }

            if (!string.IsNullOrEmpty(replyTo))
            {
                request.AddParameter("h:Reply-To", replyTo);
            }

            if(additionalRecipients != null && additionalRecipients.Length > 0)
            {
                for (int i = additionalRecipients.Length - 1; i >= 0; i--)
                {
                    request.AddParameter("to", additionalRecipients[i]);
                }
            }

            if(cc != null && cc.Length > 0)
            {
                for (int i = cc.Length - 1; i >= 0; i--)
                {
                    request.AddParameter("cc", cc[i]);
                }
            }

            if(bcc != null && bcc.Length > 0)
            {
                for(int i = bcc.Length - 1; i >= 0; i--)
                {
                    request.AddParameter("bcc", bcc[i]);
                }
            }

            if(attachments != null)
            {
                foreach(var attachment in attachments)
                {
                    request.AddFile(attachment.name, attachment.file, attachment.fileName, attachment.contentType);
                }
            }

            return await restClient.ExecuteTaskAsync(request);
        }

        public Task<IRestResponse> SendEmailAsync(string subject, string text, string to)
        {
            throw new NotImplementedException();
        }

        Task<IRestResponse> IEmailSend.SendEmailAsync(string from, string to, string subject, string text, string html, string replyTo, string[] additionalRecipients, string[] cc, string[] bbc, IEnumerable<Attachment> attachments)
        {
            throw new NotImplementedException();
        }
    }
}
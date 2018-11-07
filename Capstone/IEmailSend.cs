using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Capstone
{
    interface IEmailSend
    {
        Task<IRestResponse> SendEmailAsync(string subject, string text, string to);
        Task<IRestResponse> SendEmailAsync(string from, string to, string subject, string text, string html, string replyTo = null, string[] additionalRecipients = null, string[] cc = null, string[] bbc = null, IEnumerable<Attatchment> attachments = null);
        Task<IRestResponse> SendEmailAsync(string from, string to, string subject, string text, string html, string replyTo, string[] additionalRecipients, string[] cc, string[] bbc, IEnumerable<Attachment> attachments);
    }
}

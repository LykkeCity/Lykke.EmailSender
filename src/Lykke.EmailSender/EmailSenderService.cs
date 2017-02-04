using System.Threading.Tasks;
using Common;

namespace Lykke.EmailSender
{

    public class EmailAttachment
    {
        public string FileName { get; set; }
        public string Mime { get; set; }
        public byte[] Data { get; set; }
    }

    public class EmailModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
        public EmailAttachment[] Attachments { get; set; }
    }

    public interface IEmailSender
    {
        Task SendEmailAsync(EmailModel model);
    }


    public class EmailSenderViaQueue : IEmailSender
    {
        private readonly IMessageProducer<EmailModel> _producer;

        public EmailSenderViaQueue(IMessageProducer<EmailModel> producer)
        {
            _producer = producer;
        }

        public async Task SendEmailAsync(EmailModel model)
        {
            await _producer.ProduceAsync(model);
        }
    }
}

using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;

namespace DesafioBackendPicPay.Platform.Infrastructure.EmailService
{
    public class EmailService
    {
        private static AmazonSimpleEmailServiceV2Client AmazonSesV2 { get; set; }

        public EmailService()
        {
            AmazonSesV2 = new AmazonSimpleEmailServiceV2Client(Amazon.RegionEndpoint.USEast1);

        }

        public async static Task<string> SendEmailAsync(string fromEmailAddress, List<string> toEmailAddresses, string? subject = null,
        string? htmlContent = null, string? textContent = null, string? templateName = null,
        string? templateData = null, string? contactListName = null, CancellationToken cancellationToken = default)
        {

            var request = new SendEmailRequest
            {
                FromEmailAddress = fromEmailAddress
            };

            if (toEmailAddresses.Count != 0)
            {
                request.Destination = new Destination { ToAddresses = toEmailAddresses };
            }

            if (!string.IsNullOrEmpty(templateName))
            {
                request.Content = new EmailContent()
                {
                    Template = new Template
                    {
                        TemplateName = templateName,
                        TemplateData = templateData
                    }
                };
            }
            else
            {
                request.Content = new EmailContent
                {
                    Simple = new Message
                    {
                        Subject = new Content { Data = subject },
                        Body = new Body
                        {
                            Html = new Content { Data = htmlContent },
                            Text = new Content { Data = textContent }
                        }
                    }
                };
            }

            if (!string.IsNullOrEmpty(contactListName))
            {
                request.ListManagementOptions = new ListManagementOptions
                {
                    ContactListName = contactListName
                };
            }

            try
            {
                var response = await AmazonSesV2.SendEmailAsync(request, cancellationToken);
                return response.MessageId;
            }
            catch (AccountSuspendedException ex)
            {
                Console.WriteLine("The account's ability to send email has been permanently restricted.");
                Console.WriteLine(ex.Message);
            }
            catch (MailFromDomainNotVerifiedException ex)
            {
                Console.WriteLine("The sending domain is not verified.");
                Console.WriteLine(ex.Message);
            }
            catch (MessageRejectedException ex)
            {
                Console.WriteLine("The message content is invalid.");
                Console.WriteLine(ex.Message);
            }
            catch (SendingPausedException ex)
            {
                Console.WriteLine("The account's ability to send email is currently paused.");
                Console.WriteLine(ex.Message);
            }
            catch (TooManyRequestsException ex)
            {
                Console.WriteLine("Too many requests were made. Please try again later.");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending the email: {ex.Message}");
            }

            return string.Empty;
        }
    }
}

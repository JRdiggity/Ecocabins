using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Ecocabins.Web.Pages
{
    public class ContactModel : PageModel
    {
        public void OnGet() { }
        [BindProperty]
        [Required]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        public string Address { get; set; }

        [BindProperty]
        [Required]
        public string City { get; set; }

        [BindProperty]
        [Required]
        public string State { get; set; }

        [BindProperty]
        [Required]
        public string Zip { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Phone { get; set; }

        [BindProperty]
        [Required]
        public string Way { get; set; }

        [BindProperty]
        [Required]
        public string Time { get; set; }

        [BindProperty]
        public string Plan { get; set; }

        [BindProperty]
        public string Land { get; set; }

        [BindProperty]
        public string Finance { get; set; }

        [BindProperty]
        public string Budget { get; set; }

        [BindProperty]
        public string Message { get; set; }

        [Required(AllowEmptyStrings = false)]
        [BindProperty(Name = "g-recaptcha-response")]
        public string GoogleReCaptchaResponse { get; set; }

        public async Task<JsonResult> OnPostContactFormSubmit()
        {
            try
            {
                // If recaptcha is not provided, or model state is invalid, throw exception (return false for JS submit)
                if (string.IsNullOrEmpty(GoogleReCaptchaResponse) || !ModelState.IsValid)
                    throw new Exception();

                // Validate recaptcha
                var recaptchaIsValid = await RecaptchaIsValid(GoogleReCaptchaResponse);

                if(!recaptchaIsValid)
                    throw new Exception();

                // All checks succeeded, send email
                // TODO: Needs to change to site's actual domain
                var mail = new MailMessage(
                    from: new MailAddress("sales@ecocabins.biz", "Website Submission"),
                    to: new MailAddress("sales@ecocabins.biz", "Ecocabins Sales")
                    )
                {
                    IsBodyHtml = true
                }; // Change to Sarah's dad

                SmtpClient smtpClient = new SmtpClient("outlook.office365.com")
                {
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("sales@ecocabins.biz", "TCLH1992ecocabins"),
                    Port = 587,
                    EnableSsl = true
                };

                mail.Subject = "Contact Form Submission";

                // Build body
                mail.Body = $@"<p>Contact form submission received on { DateTime.Now.ToString("MM/dd/yyyy") } @ { DateTime.Now.ToString("h:mm tt") }</p>
                                <table>
                                <tr>
                                    <td>
                                        <strong>Name:</strong>
                                    </td>
                                    <td>
                                        {Name}
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Address:</strong>
                                    </td>
                                    <td>
                                        {Address}
                                    </td>
                                </tr>                      
                                <tr>
                                    <td>
                                        <strong>City:</strong>
                                    </td>
                                    <td>
                                        {City}
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>State:</strong>
                                    </td>
                                    <td>
                                        {State}
                                    </td>
                                </tr>
                                    <tr>
                                    <td>
                                        <strong>Zip Code:</strong>
                                    </td>
                                    <td>
                                        {Zip}
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <strong>Email:</strong>
                                    </td>
                                    <td>
                                       <a href='mailto:{Email}'>{Email}</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Phone:</strong>
                                    </td>
                                    <td>
                                        {Phone}
                                    </td>
                                </tr> 
                                <tr>
                                    <td>
                                        <strong>Best way to contact:</strong>
                                    </td>
                                    <td>
                                        {Way}
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Best time to contact:</strong>
                                    </td>
                                    <td>
                                        {Time}
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Building timeframe:</strong>
                                    </td>
                                    <td>
                                        {Plan}
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Land owned:</strong>
                                    </td>
                                    <td>
                                        {Land}
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Financing:</strong>
                                    </td>
                                    <td>
                                        {Finance}
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Budget:</strong>
                                    </td>
                                    <td>
                                        {Budget}
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Message:</strong>
                                    </td>
                                    <td>
                                        {Message}
                                    </td>
                                </tr>            
                            </table>";

                smtpClient.Send(mail);
                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                return new JsonResult(false);
            }
        }

        private async Task<bool> RecaptchaIsValid(string googleReCaptchaResponse)
        {
            var reCaptchaSecret = "6Lcuh6cUAAAAANxh74oyTkGZgc5OWVy_emITkpGa";
            var httpResponse = await new HttpClient().GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={reCaptchaSecret}&response={googleReCaptchaResponse}");

            return httpResponse.StatusCode == HttpStatusCode.OK;
        }
    }
}

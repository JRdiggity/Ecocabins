using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


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


  

        public JsonResult OnPostContactFormSubmit()
        {
            try
            {
                var mail = new MailMessage(
                new MailAddress("josh.hill@outlook.es", "Joshua Hill"), // Needs to change to site's actual domain
                new MailAddress("josh.hill@outlook.es", "Josh Hill")); // Change to Sarah's dad
                mail.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com")
                {
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("josh.hill@outlook.es", "83x1LIXsIcH7"),
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
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace IdentityFrame.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public  Credential Credentials { get; set; }
        public void OnGet()
        {
            //this.Credentials = new Credential { UserName = "admin" };
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if(Credentials.Name == "admin" && Credentials.Password == "password")
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com")

                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                return RedirectToPage("/Index");
            
            }
            return Page();
        }
        public class Credential
        {
            [Required]
            public string Name { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

        }
    }
}

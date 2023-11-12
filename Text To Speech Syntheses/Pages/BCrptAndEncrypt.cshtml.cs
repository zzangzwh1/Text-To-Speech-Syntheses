using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BCrypt.Net;

namespace Text_To_Speech_Syntheses.Pages
{
    public class BCrptAndEncryptModel : PageModel
    {
        [BindProperty]
        public string InputText {get;set;}
        public string HashedText { get; set; }
        public string BackToText { get; set; }
        [BindProperty]
        public string Submit { get; set; } = string.Empty;
        public void OnGet()
        {
        }
        public void OnPost()
        {
            switch (Submit)
            {
                case "Bcrpyt":
                    string beforeBcryptText = InputText;
                    HashedText = BCrypt.Net.BCrypt.HashPassword(beforeBcryptText);
                    break;

                case "Encrypt":
                    
                    
                    break;
                default: break;



            }
            

        }
    }
}

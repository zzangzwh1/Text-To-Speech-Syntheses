using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Speech;
using System.Speech.Synthesis;
using Google.Cloud.Translation.V2;
using Newtonsoft.Json;
using Google.Apis.Translate.v2.Data;
using System.Net.Http;
using Text_To_Speech_Syntheses.Domain;

namespace Text_To_Speech_Syntheses.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string? Text { get; set; }
        [BindProperty]
        public string? languageOption { get; set; }
        public string baseUrl = @"http://api.mymemory.translated.net";
      
        public void OnGet()
        {
            string s = Text;

            Console.WriteLine(s);
        }
        public async Task OnPost()
        {
            string textToTranslate = Text;
            string targetText = languageOption;
      
            if (targetText != "en-US")
            {
                string translateText = await TranslateAsync(textToTranslate, targetText);
                ReadText(translateText, targetText);
            }
            else
            {
               
                ReadText(textToTranslate, targetText);
            }
           
        }
        public void ReadText(string text, string languageCode)
        {
            SpeechSynthesizer speech = new SpeechSynthesizer();

            // Set the speech synthesizer voice for the specified language
            foreach (InstalledVoice voice in speech.GetInstalledVoices())
            {
                if (voice.VoiceInfo.Culture.Name == languageCode)
                {
                    speech.SelectVoice(voice.VoiceInfo.Name);
                    break;
                }
            }

            speech.SpeakAsync(text);
        }
        public async Task<string> TranslateAsync(string text, string targetLang)
        {
            HttpClient httpClient = new HttpClient();
            string url = $"{baseUrl}/get?q={Uri.EscapeDataString(text)}&langpair=en-US|{targetLang}";
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseJson = await response.Content.ReadAsStringAsync();
                var translationResult = JsonConvert.DeserializeObject<TranslationResponse>(responseJson);

                if (translationResult.ResponseStatus == 200)
                {
                    return translationResult.ResponseData.TranslatedText;
                }
            }
            return string.Empty;
        }

    }

}


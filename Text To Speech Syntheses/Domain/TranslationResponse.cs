using Newtonsoft.Json;
namespace Text_To_Speech_Syntheses.Domain
{
    public class TranslationResponse
    {
        [JsonProperty("responseStatus")]
        public int ResponseStatus { get; set; }
        [JsonProperty("responseData")]
        public TranslationData ResponseData { get; set; }
        public string TranslatedText => ResponseData?.TranslatedText;
    }
 
    public class TranslationData
    {
        [JsonProperty("translatedText")]
        public string TranslatedText { get; set; }
    }

}

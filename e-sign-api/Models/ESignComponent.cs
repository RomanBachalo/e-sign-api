namespace e_sign_api.Models
{
    public class ESignComponent
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public ESignComponentStyle Style { get; set; }
    }
}

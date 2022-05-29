namespace e_sign_api.Models
{
    public class PostEnvelopeModel
    {
        public string TemplateId { get; set; }
        public string Name { get; set; }
        public string SignerEmail { get; set; }
        public string SignerName { get; set; }
    }
}

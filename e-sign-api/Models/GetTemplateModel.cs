namespace e_sign_api.Models
{
    public class GetTemplateModel
    {
        public string TemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ESignComponent[] Components { get; set; }
    }
}

namespace e_sign_api.Models
{
    public class PostTemplateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ESignComponent[] Components { get; set; }
    }
}

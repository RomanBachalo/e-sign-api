using DocuSign.eSign.Model;

namespace e_sign_api.Models
{
    public class Template
    {
        public string? TemplateId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? EmailSubject { get; set; }
        public Recipients? Recipients { get; set; }
        public List<Document>? Documents { get; set; }
        public string? Status { get; set; }
    }
}

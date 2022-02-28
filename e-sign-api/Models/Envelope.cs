using DocuSign.eSign.Model;

namespace e_sign_api.Models
{
    public class Envelope
    {
        public string? EnvelopeId { get; set; }
        public string? TemplateId { get; set; }
        public string? EmailSubject { get; set; }
        public string? Status { get; set; }
        public List<TemplateRole>? TemplateRoles { get; set; }
    }
}

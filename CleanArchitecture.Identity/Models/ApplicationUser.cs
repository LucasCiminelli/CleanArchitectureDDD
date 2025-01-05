
namespace CleanArchitecture.Identity.Models
{
    public class ApplicationUser : BaseDomainModel
    {
        public Guid IdentityId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Country { get; set; } = string.Empty;
    }
}

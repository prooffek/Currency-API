using System.ComponentModel.DataAnnotations;

namespace AveneoRerutacja.Models
{
    public class CreateAuthenticationKeyDto
    {
        [Required]
        public string KeyValue { get; set; }
    }
    
    public class UpdateAuthenticationKeyDto : CreateAuthenticationKeyDto
    { }
    
    public class AuthenticationKeyDto : CreateAuthenticationKeyDto
    {
        public int Id { get; set; }
    }
}
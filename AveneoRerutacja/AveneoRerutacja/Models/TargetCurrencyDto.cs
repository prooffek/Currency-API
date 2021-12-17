using System.ComponentModel.DataAnnotations;

namespace AveneoRerutacja.Models
{
    public class CreateTargetCurrencyDto
    {
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }
    }
    
    public class UpdateTargetCurrencyDto : CreateTargetCurrencyDto
    { }
    
    public class TargetCurrencyDto : CreateTargetCurrencyDto
    {
        [Required]
        public int Id { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace AveneoRerutacja.Models
{
    public class CreateSourceCurrencyDto
    {
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }
    }
    
    public class UpdateSourceCurrencyDto : CreateSourceCurrencyDto
    { }
    
    public class SourceCurrencyDto : CreateSourceCurrencyDto
    {
        [Required]
        public int Id { get; set; }
    }
}
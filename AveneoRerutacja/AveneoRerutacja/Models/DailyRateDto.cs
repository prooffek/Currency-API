using System.ComponentModel.DataAnnotations;

namespace AveneoRerutacja.Models
{
    public class CreateDailyRateDto
    {
        [Required]
        public DateClassDto Date { get; set; }
        
        [Required]
        public decimal Rate { get; set; }
        
        [Required]
        public SourceCurrencyDto SourceCurrency { get; set; }
        
        [Required] 
        public TargetCurrencyDto TargetCurrency { get; set; }
    }
    
    public class UpdateDailyRateDto : CreateDailyRateDto
    { }
    
    public class DailyRateDto : CreateDailyRateDto
    {
        [Required]
        public int Id { get; set; }
    }
}
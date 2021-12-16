using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using Newtonsoft.Json;

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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AveneoRerutacja.Domain;

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
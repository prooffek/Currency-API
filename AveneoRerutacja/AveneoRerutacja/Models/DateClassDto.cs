using System;
using System.ComponentModel.DataAnnotations;

namespace AveneoRerutacja.Models
{
    public class CreateDateClassDto
    {
        [Required]
        public DateTime Date { get; protected init; }
    }
    
    public class UpdateDateClassDto : CreateDateClassDto
    { }
    
    public class DateClassDto : CreateDateClassDto
    {
        [Required]
        public int Id { get; set; }
    }
}
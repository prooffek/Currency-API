using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AveneoRerutacja.Domain;

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
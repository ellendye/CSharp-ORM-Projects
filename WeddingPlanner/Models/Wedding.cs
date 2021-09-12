using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using WeddingPlanner.Class;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId {get;set;}
        [Required(ErrorMessage = "Wedder one is required")]
        [Display(Name = "Wedder One: ")]
        public string WedderOne {get;set;}
        [Required(ErrorMessage = "Wedder two is required")]
        [Display(Name = "Wedder Two: ")]
        public string WedderTwo {get;set;}
        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Date: ")]
        [FutureDate(ErrorMessage = "Date must be in the future")]
        public DateTime Date {get; set;}
        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Wedding Address: ")]
        public string Address {get;set;}
        public int UserId {get; set;}
        public User Creator {get; set;}
        public List<Guests> Attendees {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}
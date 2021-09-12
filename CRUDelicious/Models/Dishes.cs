using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
{
    public class Dishes
    {
        [Key]
        public int DishId {get;set;}
        [Display(Name ="Name of dish:")]
        [Required(ErrorMessage = "Dish Name is required")]
        public string Name {get;set;}
        [Display(Name ="Chef's name:")]
        [Required(ErrorMessage = "Chef's name is required")]
        public string Chef {get;set;}
        [Display(Name ="Tastiness:")]
        [Range(0,6, ErrorMessage ="Tastiness must be between 1 and 5")]
        [Required(ErrorMessage = "Tastiness is required")]
        public int Tastiness {get;set;}
        [Display(Name ="# of Calories:")]
        [Range(0, Int32.MaxValue, ErrorMessage ="Calorie count must be a positive number")]
        [Required(ErrorMessage = "Calorie Count is required")]
        public int Calories {get;set;}
        [Display(Name ="Description:")]
        [Required(ErrorMessage = "Description is required")]
        public string Description {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}
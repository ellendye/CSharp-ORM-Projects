using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace ProductCategory.Models
{
    public class Product
    {
        [Key]
        public int ProductID {get; set;}
        [Required]
        [Display(Name = "Product Name: ")]
        public string Name {get; set;}
        [Required]
        [Display(Name = "Product Description: ")]
        public string Description {get; set;}
        [Required]
        [Display(Name = "Price: ")]
        public int Price {get; set;}
        public List<Association> ProductCategories {get; set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}
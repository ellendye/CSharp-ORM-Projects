using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace ProductCategory.Models
{
    public class Category
    {
        [Key]
        public int CategoryID {get; set;}
        [Required]
        [Display(Name = "Category Name: ")]
        public string Name {get; set;}
        public List<Association> ProductsInCategory {get; set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProductCategory.Models
{
    public class Association
    {
        [Key]
        public int AssociationID {get; set;}
        public int CategoryID {get; set;}
        public Category Category {get; set;}
        public int ProductID {get; set;}
        public Product Product {get; set;}
    }
}
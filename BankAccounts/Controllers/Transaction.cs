using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BankAccounts.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID {get; set;}
        [Required]
        [Display(Name ="Withdraw/Deposit: ")]
        public int Amount {get; set;}
        [Required]
        public int UserID {get; set;}
        public User User {get; set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}
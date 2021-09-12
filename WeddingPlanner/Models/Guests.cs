using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using WeddingPlanner.Class;

namespace WeddingPlanner.Models
{
    public class Guests
    {
        [Key]
        public int GuestId {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
        public int WeddingId {get; set;}
        public Wedding Wedding {get; set;}
    }
}
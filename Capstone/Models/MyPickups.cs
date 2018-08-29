using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class MyPickups
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("DogId")]
        public Dog Dog { get; set; }
        public int DogId { get; set; }
        public DateTime DateTime { get; set; }
        public string Frequency { get; set; }
        
    }
}
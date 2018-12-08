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
        [ForeignKey("Dog")]
        public int DogId { get; set; }
        public Dog Dog { get; set; }
        public string DayOfTheWeek { get; set; }
        public DateTime Time { get; set; }
        public string Frequency { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
       
        

    }
}
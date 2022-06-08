using System;
using System.ComponentModel.DataAnnotations;

namespace FavouriteBoxers.Models
{
    public class Boxer
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string Full_Name { get; set; }

        public string Alias { get; set; }

        public string Nationality { get; set; }

        public string Stance { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "D.O.B")]
        public DateTime DOB { get; set; }

        public Boxer()
        {
        }
    }
}


using System;
namespace FavouriteBoxers.Models
{
    public class Boxer
    {
        public int Id { get; set; }
        public string Full_Name { get; set; }
        public string Alias { get; set; }
        public string Nationality { get; set; }
        public string Stance { get; set; }
        public DateTime DOB { get; set; }

        public Boxer()
        {
        }
    }
}


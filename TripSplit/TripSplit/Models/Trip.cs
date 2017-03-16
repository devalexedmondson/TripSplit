using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripSplit.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public bool IsPublic { get; set; }
        public string StartTrip { get; set; }
        public string EndTrip { get; set; }
        public string Waypoint { get; set; }
        public double Cost { get; set; }
        public string Theme { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
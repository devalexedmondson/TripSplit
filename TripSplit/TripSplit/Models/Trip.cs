using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TripSplit.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string IsPublic { get; set; }
        public string originInput { get; set; }
        public string destinationInput { get; set; }
        public string departureDate { get; set; }
        public string returnDate { get; set; }
        public double Cost { get; set; }
        public double tripDistance { get; set; }
        public double tripDuration { get; set; }
        public int totalUsersOnTrip { get; set; }

        public Theme Theme { get; set; }
        [ForeignKey("Theme")]
        public int ThemeId { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
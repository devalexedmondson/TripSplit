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

        [Display(Name = "Trip Name")]
        public string Name { get; set; }

        public string Type { get; set; }

        [Display(Name = "Trip Privacy")]
        public string IsPublic { get; set; }

        [Display(Name = "Trip Origin")]
        public string originInput { get; set; }

        [Display(Name = "Trip Destination")]
        public string destinationInput { get; set; }

        [Display(Name = "Departure Date")]
        public string departureDate { get; set; }

        [Display(Name = "Return Date")]
        public string returnDate { get; set; }

        public double Cost { get; set; }

        public double tripDistance { get; set; }

        public double tripDuration { get; set; }

        [Display(Name = "Total Trip Splitters")]
        public int totalUsersOnTrip { get; set; }

        public Theme Theme { get; set; }
        [ForeignKey("Theme")]
        public int ThemeId { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
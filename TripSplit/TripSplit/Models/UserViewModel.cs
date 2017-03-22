using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripSplit.Models
{
    public class UserIndexViewModel
    {

    }

    public class CreateDrivingTripViewModel
    {
        [Required]
        [Display(Name = "Trip Privacy")]
        public string IsPublic { get; set; }

        [Required]
        [Display(Name = "Trip Name")]
        public string Name { get; set; }
                
        [Display(Name = "Trip Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Trip Start")]
        public string originInput { get; set; }

        [Required]
        [Display(Name = "Trip End")]
        public string destinationInput { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Use numbers for cost please. Example: 150")]
        [Display(Name = "Cost of trip?")]
        public double Cost { get; set; }

        [Required]
        [Display(Name = "Theme")]
        public string Theme { get; set; }

        public double tripDistance { get; set; }
        public double tripDuration { get; set; }
        public int totalUsersOnTrip { get; set; }

        
    }

    public class CreateFlyingTripViewModel
    {

    }

    public class TripsViewModel
    {

    }

    public class UserTripAgreementViewModel
    {
    }

    public class DetailsViewModel
    {

    }
}
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
        public bool IsPublic { get; set; }
        
        [Display(Name = "Trip Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Trip Start")]
        public string originInput { get; set; }

        [Required]
        [Display(Name = "Trip End")]
        public string destinationInput { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Use numbers for zipcode please")]
        [Display(Name = "Cost of trip?")]
        public double Cost { get; set; }

        [Required]
        [Display(Name = "Theme")]
        public string Theme { get; set; }
    }

    public class CreateFlyingTripViewModel
    {

    }

    public class TripsViewModel
    {

    }

    public class UserTripAgreement
    {

    }
}
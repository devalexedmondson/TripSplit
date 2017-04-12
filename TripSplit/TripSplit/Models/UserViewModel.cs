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

        //Setting themes to list so they can be saved to db the correct way
        //public IEnumerable<Theme> Theme { get; set; }

        //[Required]
        //[Display(Name = "Theme")]
        //public string Theme { get; set; }

        public double tripDistance { get; set; }
        public double tripDuration { get; set; }
        public int totalUsersOnTrip { get; set; }
        public DateTime departureDate { get; set; }
        public DateTime returnDate { get; set; }

    }

    public class CreateFlyingTripViewModel
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

        //[Required]
        //[Display(Name = "Theme")]
        //public string Theme { get; set; }

        [Required]
        [Display(Name = "Flight Number")]
        public string flightNumber { get; set; }

        public double tripDistance { get; set; }
        public double tripDuration { get; set; }
        public int totalUsersOnTrip { get; set; }
        public DateTime departureDate { get; set; }
        public DateTime returnDate { get; set; }
    }

    public class ConfirmFlyingTripViewModel
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
        [Display(Name = "Flight Number")]
        public string flightNumber { get; set; }

        //[Required]
        //[Display(Name = "Theme")]
        //public string Theme { get; set; }

        public double tripDistance { get; set; }
        public double tripDuration { get; set; }
        public int totalUsersOnTrip { get; set; }
        public DateTime departureDate { get; set; }
        public DateTime returnDate { get; set; }
    }

    public class TripsViewModel
    {

    }

    public class TellAFriendViewModel
    {
        [Required]
        [Display(Name = "Friend Email")]
        public string friendEmail { get; set; }
    }

    public class UserTripAgreementViewModel
    {
    }

    public class DetailsViewModel
    {

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripSplit.Models
{
    public class Theme
    {
        [Key]
        public int Id { get; set; }
        public string destinationTheme { get; set; }

    }
}
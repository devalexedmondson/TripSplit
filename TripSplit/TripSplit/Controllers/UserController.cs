using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TripSplit.Models;

namespace TripSplit.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db;

        public UserController()
        {
            db = new ApplicationDbContext();
        }

        // GET: UserIndex
        public ActionResult UserIndex()
        {
            return View();
        }

        //GET: CreateDrivingTrip
        public ActionResult CreateDrivingTrip()
        {
            ViewBag.Name = new SelectList(db.Theme.ToList(), "Id", "destinationTheme");
            return View();
        }

        //
        //POST: CreateDrivingTrip
        [HttpPost]
        public ActionResult CreateDrivingTrip(CreateDrivingTripViewModel model)
        {
            var type = "Driving";
            var trip = new Trip
            {
                IsPublic = model.IsPublic,
                Type = model.Type,
                originInput = model.originInput,
                destinationInput = model.destinationInput,
                Cost = model.Cost,
                Theme = new Theme
                {
                    destinationTheme = model.Theme
                }
            };
            return RedirectToAction("UserIndex","User"); //send to verification page
        }

        //
        // GET: CreateFlyingTrip
        public ActionResult CreateFlyingTrip()
        {
            return View();
        }

        //GET: Trips
        public ActionResult Trips()
        {
            return View();
        }

        //GET: UserTripAgreement
        public ActionResult UserTripAgreement()
        {
            return View();
        }
        

    }
}
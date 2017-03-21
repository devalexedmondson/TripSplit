using System;
using System.Collections.Generic;
using System.IO;
using System.Data.Entity;

using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TripSplit.Models;
using Microsoft.AspNet.Identity;

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
        {//Trip.Users.Add(trip)

           
            var type = "Driving";
            var trip = new Trip
            {
                IsPublic = model.IsPublic,
                Name = model.Name,
                Type = type,
                originInput = model.originInput,
                destinationInput = model.destinationInput,
                Cost = model.Cost,
                tripDistance = model.tripDistance,
                tripDuration = model.tripDuration,
                ThemeId = int.Parse(model.Theme)
                
            };
            var user = db.Users.Find(User.Identity.GetUserId());
            trip.Users = new List<ApplicationUser>();
            trip.Users.Add(user);
            db.Trip.Add(trip);
            db.SaveChanges();
            return RedirectToAction("VerifyTrip","User"); //send to verification page
        }

        //
        //GET: VerifyTrip
        public ActionResult VerifyTrip()
        {
            return View();
        }

        //
        //GET: ViewMyTrips
        public ActionResult ViewMyTrips()
        {
            var myTrips = db.Trip.ToList();
            return View(myTrips);
        }
        
        //DETAILS   
        //GET: Details
        public ActionResult Detail(int id)
        {
            var trip = db.Trip.SingleOrDefault(t => t.Id == id);

            if(trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // EDIT TRIP
        //GET: EditTrip
        public ActionResult Edit(int id)
        {
            var trip = db.Trip.SingleOrDefault(c => c.Id == id);

            if(trip== null)
            {
                return HttpNotFound();
            }
            var viewModel = new CreateDrivingTripViewModel
            {
               
            };
            return View("CreateDrivingTrip");
        }

       
        //CREATEFLYINGTRIP
        // GET: CreateFlyingTrip
        public ActionResult CreateFlyingTrip()
        {
            return View();
        }

        //GET: Trips
        public ActionResult Trips()
        {
            var trips = db.Trip.ToList();
            return View(trips);
        }

        //GET: UserTripAgreement
        public ActionResult UserTripAgreement()
        {
            return View();
        }
        

    }
}
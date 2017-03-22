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
using System.Net.Mail;
using RestSharp;
using RestSharp.Authenticators;

namespace TripSplit.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db;

        public UserController()
        {
            db = new ApplicationDbContext();
        }

        //USER/USERINDEX
        // GET: UserIndex
        public ActionResult UserIndex()
        {
            return View();
        }

        //USER/CREATEDRIVINGTRIP
        //GET: CreateDrivingTrip
        public ActionResult CreateDrivingTrip()
        {
            ViewBag.Name = new SelectList(db.Theme.ToList(), "Id", "destinationTheme");
            return View();
        }

        //USER/CREATEDRIVINGTRIP
        //POST: CreateDrivingTrip
        [HttpPost]
        public ActionResult CreateDrivingTrip(CreateDrivingTripViewModel model)
        {
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
                ThemeId = int.Parse(model.Theme),
                totalUsersOnTrip = model.totalUsersOnTrip+1
                
            };
            var user = db.Users.Find(User.Identity.GetUserId());
            trip.Users = new List<ApplicationUser>();
            trip.Users.Add(user);
            db.Trip.Add(trip);
            db.SaveChanges();

            //MAIL TO CUSTOMER USING MAILTRAP.IO
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("3cad2a6d8a23a7", "150ffbb33ba612"),
                EnableSsl = true
            };
            client.Send("MasterSplitter@TripSplit.com", user.Email, "TripSplit", "Congrats on booking your trip! \nHere are your trip details. \nTrip Name: " + trip.Name + "\nTrip Type:" + trip.Type + "\nStart Location: " + trip.originInput + "\nEnd Location: " + trip.destinationInput + "\nTrip Cost: $" + trip.Cost + "\nTotal People on trip: " + trip.totalUsersOnTrip);

            return RedirectToAction("VerifyTrip", "User");

            //MAIL TO CUSTOMER USING MAILGUN
            //RestClient client = new RestClient();
            //client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            //client.Authenticator =
            //new HttpBasicAuthenticator("api",
            //                          "key-5100e29cf52d0d1095df81f780ffe08a");
            //RestRequest request = new RestRequest();
            //request.AddParameter("domain", "sandboxaad72b1f285c461db3c8bd22c0db248a.mailgun.org", ParameterType.UrlSegment);
            //request.Resource = "{domain}/messages";
            //request.AddParameter("from", "TripSplit <postmaster@sandboxaad72b1f285c461db3c8bd22c0db248a.mailgun.org>");
            //request.AddParameter("to", user.Email);
            //request.AddParameter("subject", "TripSplit");
            //request.AddParameter("text", "Congrats on booking your trip! \nHere are your trip details. \nTrip Name: " + trip.Name + "\nTrip Type:" + trip.Type + "\nStart Location: " + trip.originInput + "\nEnd Location: " + trip.destinationInput + "\nTrip Cost: $" + trip.Cost + "\nTotal People on trip: " + trip.totalUsersOnTrip);
            //request.Method = Method.POST;
            //client.Execute(request);
        }
        
        //User/CREATEFLYINGTRIP
        // GET: CreateFlyingTrip
        public ActionResult CreateFlyingTrip()
        {
            return View();
        }

        //User/Detail 
        //GET: Detail
        public ActionResult Detail(int id)
        {
            var trip = db.Trip.SingleOrDefault(t => t.Id == id);

            if(trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        //User/Edit
        //GET: Edit
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

        //User/Trips
        //GET: Trips
        public ActionResult Trips()
        {
            var trips = db.Trip.ToList();
            return View(trips);
        }

        //User/UserTripAgreement
        //GET: UserTripAgreement
        public ActionResult UserTripAgreement(int id)
        {
            var trip = db.Trip.SingleOrDefault(t => t.Id == id);

            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        //
        //POST: UserTripAgreement
        [HttpPost]
        public ActionResult UserTripAgreement(int id, UserTripAgreementViewModel model)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var trip = db.Trip.SingleOrDefault(t => t.Id == id);
            trip.Cost = trip.Cost / 2;
            trip.totalUsersOnTrip = trip.totalUsersOnTrip + 1;
            db.SaveChanges();
            
            //MAIL TO CUSTOMER USING MAILTRAP.IO
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("3cad2a6d8a23a7", "150ffbb33ba612"),
                EnableSsl = true
            };
            client.Send("MasterSplitter@TripSplit.com", user.Email, "You Split The Trip!", "Congrats on booking and splitting your trip! \nHere are your trip details. \nTrip Name: " + trip.Name + "\nTrip Type:" + trip.Type + "\nStart Location: " + trip.originInput + "\nEnd Location: " + trip.destinationInput + "\nTrip Cost: $" + trip.Cost + "\nTotal People on trip: " + trip.totalUsersOnTrip);

            return RedirectToAction("VerifyTrip", "User");

            //MAIL TO CUSTOMER USING MAILGUN
            //RestClient client = new RestClient();
            //client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            //client.Authenticator =
            //new HttpBasicAuthenticator("api",
            //                          "key-5100e29cf52d0d1095df81f780ffe08a");
            //RestRequest request = new RestRequest();
            //request.AddParameter("domain", "sandboxaad72b1f285c461db3c8bd22c0db248a.mailgun.org", ParameterType.UrlSegment);
            //request.Resource = "{domain}/messages";
            //request.AddParameter("from", "TripSplit <postmaster@sandboxaad72b1f285c461db3c8bd22c0db248a.mailgun.org>");
            //request.AddParameter("to", user.Email);
            //request.AddParameter("subject", "TripSplit");
            //request.AddParameter("text", "Congrats on booking your trip! \nHere are your trip details. \nTrip Name: " + trip.Name + "\nTrip Type:" + trip.Type + "\nStart Location: " + trip.originInput + "\nEnd Location: " + trip.destinationInput + "\nTrip Cost: $" + trip.Cost + "\nTotal People on trip: " + trip.totalUsersOnTrip);
            //request.Method = Method.POST;
            //client.Execute(request);
        }

        //USER/VERIFYTRIP
        //GET: VerifyTrip
        public ActionResult VerifyTrip()
        {
            return View();
        }

        //USER/VIEWMYTRIPS
        //GET: ViewMyTrips
        public ActionResult ViewMyTrips()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var myTrips = user.Trips.ToList();  
            return View(myTrips);
        }
    }
}
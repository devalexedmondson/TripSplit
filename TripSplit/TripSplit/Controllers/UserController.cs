﻿using System;
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
using System.Threading.Tasks;
using System.Net.Http;

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

            //Change "Id" to "destinationTheme" to capture actual theme displayed
            ViewBag.Name = new SelectList(db.Theme.ToList(), "destinationTheme", "destinationTheme");
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
                //ThemeId = int.Parse(model.Theme),
                totalUsersOnTrip = model.totalUsersOnTrip+1,
                departureDate = model.departureDate,
                returnDate = model.returnDate
                
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
            client.Send("MasterSplitter@TripSplit.com", user.Email, "TripSplit", "Congrats on booking your trip! \nHere are your trip details. \nTrip Name: " + trip.Name + "\nTrip Type:" + trip.Type + "\nStart Location: " + trip.originInput + "\nEnd Location: " + trip.destinationInput + "\nDeparture Date: " + trip.departureDate + "\nReturn Date: " + trip.returnDate + "\nTrip Cost: $" + trip.Cost + "\nTotal People on trip: " + trip.totalUsersOnTrip);

            return RedirectToAction("VerifyTrip", "User");

            #region MailGun
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
            #endregion
        }
        
      
       public async Task<ActionResult>CreateFlyingTrip()
        {
            //CAN ONLY REQUEST FLIGHTS THAT ARE 6 MONTHS OUT. EXAMPLE: AT THE TIME THAT THIS APPLICATION WAS MADE (3 / 22 / 17) CANNOT REQUEST ANY FLIGHTS THAT ARE SCHEDULED IN OCTOBER(10 / 01 / 2017) RIGHT NOW. 
            //CAN ONLY REQUEST TRIPS THAT ARE 16 DAYS LONG, ANY LONGER THAN THAT WILL THROW AN ERROR 

            if (Request.HttpMethod == "POST")
            {
                string formattedDeparture;
                string formattedReturn;
                string originInput = Request.Form["originInput"];
                string destinationInput = Request.Form["destinationInput"];
                DateTime departureDate = Convert.ToDateTime(Request.Form["departureDate"]);
                string departuredatestr = departureDate.Year.ToString() + "-" + departureDate.Month.ToString() + "-" + departureDate.Day.ToString();

                #region String Formatting for Departure and Return
                // FORMATTING THE DEPARTURE STRING SO IT CAN BE READ BY API

                //MONTH LESS THAN 2 AND DAY IS 2
                if(departureDate.Month.ToString().Length < 2 && (departureDate.Day.ToString().Length == 2))
                {
                      formattedDeparture = departuredatestr.Substring(0, departuredatestr.Length - 4) + 0 + departuredatestr.Substring(departuredatestr.Length - 4);

                        departuredatestr = formattedDeparture;
                }
                //MONTH AND DAY LESS THAN 2
                else if(departureDate.Month.ToString().Length < 2 && (departureDate.Day.ToString().Length < 2))
                {
                    formattedDeparture = departuredatestr.Substring(0, departuredatestr.Length - 4) + "-" + 0 + departuredatestr.Substring(departuredatestr.Length - 3);

                    departuredatestr = formattedDeparture;
                }
                //DAY LESS THAN 2 AND MONTH IS 2
                if (departureDate.Day.ToString().Length < 2 && (departureDate.Month.ToString().Length ==2))
                {
                     formattedDeparture = departuredatestr.Substring(0, departuredatestr.Length - 1) + 0 + departuredatestr.Substring(departuredatestr.Length - 1);

                    departuredatestr = formattedDeparture;
                }
                //DAY LESS THAN 2
                if (departureDate.Day.ToString().Length < 2)
                {
                    formattedDeparture = departuredatestr.Substring(0, departuredatestr.Length - 1) + 0 + departuredatestr.Substring(departuredatestr.Length - 1);

                    departuredatestr = formattedDeparture;
                }
                
                DateTime returnDate = Convert.ToDateTime(Request.Form["returnDate"]);
                string returndatestr = returnDate.Year.ToString() + "-" + returnDate.Month.ToString() + "-" + returnDate.Day.ToString();

                //FORMATTING  THE RETURN STRING SO IT CAN BE READ BY API

                //MONTH LESS THAN 2 AND DAY IS 2
                if (returnDate.Month.ToString().Length < 2 && (returnDate.Day.ToString().Length == 2))
                {
                    formattedReturn = returndatestr.Substring(0, returndatestr.Length - 4) + 0 + returndatestr.Substring(returndatestr.Length - 4);

                    returndatestr = formattedReturn;
                }
                //MONTH AND DAY LESS THAN 2
                else if (returnDate.Month.ToString().Length < 2 && (returnDate.Day.ToString().Length < 2))
                {
                    formattedReturn = returndatestr.Substring(0, returndatestr.Length - 4) + "-" + 0 + returndatestr.Substring(returndatestr.Length - 3);

                    returndatestr = formattedReturn;
                }
                //DAY LESS THAN 2
                if (returnDate.Day.ToString().Length < 2)
                {
                    formattedReturn = returndatestr.Substring(0, returndatestr.Length - 1) + 0 + returndatestr.Substring(returndatestr.Length - 1);

                    returndatestr = formattedReturn;
                }
                #endregion

                WebClient datawebclient = new WebClient();
                string url = "https://" + "api.test.sabre.com/v1/shop/flights?origin=" + originInput + "&destination=" + destinationInput + "&departuredate=" + departuredatestr + "&returndate=" + returndatestr + "&onlineitinerariesonly=N" + "&limit=10&offset=1&eticketsonly=N&sortby=totalfare&order=asc&sortby2=departuretime&order2=asc" + "&pointofsalecountry=US";

                #region API URLS
                //API URL that takes in user input USE THIS FOR CAPSTONE PRESENTATION
                //"https://" + "api.test.sabre.com/v1/shop/flights?origin=" + Origin + "&destination=" + Destination
                //+ "&departuredate=" + departuredatestr + "&returndate=" + returndatestr + "&onlineitinerariesonly=N"
                //+ "&limit=10&offset=1&eticketsonly=N&sortby=totalfare&order=asc&sortby2=departuretime&order2=asc"
                //+ "&pointofsalecountry=US";

                //API URL that works due to the correct date format
                //"https://api.test.sabre.com/v1/shop/flights?origin=JFK&destination=ORD&departuredate=2017-04-07&returndate=2017-04-10&onlineitinerariesonly=N&limit=10&offset=1&eticketsonly=N&sortby=totalfare&order=asc&sortby2=departuretime&order2=asc&pointofsalecountry=US";

                //API URL that is responded with my format
                //"https://api.test.sabre.com/v1/shop/flights?origin=JFK&destination=ORD&departuredate=2017-04-07&returndate=2017-04-10&onlineitinerariesonly=N&limit=10&offset=1&eticketsonly=N&sortby=totalfare&order=asc&sortby2=departuretime&order2=asc&pointofsalecountry=US";
                #endregion

                string data = string.Empty;
                string AccessToken = "T1RLAQIgOwqxiTtuAKtomp97LTdmlgMpxBBOcnoPfKwxt2fO5lhGSaSXAADAueBFRnkvhXajTlKQg7pw/cBWJ261FQgddKLBUpoOP1WnEVOpUvKm4x5bfHJJAl53hqH3a71JgdurkOabKBoXJmGkBUCKPfE01s6l5JtN0RXar1D0F24LkIH3/YlHLxavICfGlISe/gnAH0tPVPXg+SZlvL4ugZfFOOXbCL1VEEsbxfik7TPGhYLm7YaZ/4sxkTEoXVp4Hh0n2WmUMMmJTPxNonwNvttFnXv5fMUUs6TvGllcUAxthTVyVOdc5Jra";
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
                        data = await client.GetStringAsync(url);
                    }
                    catch(Exception )
                    {
                        return View("NoFlights");
                    }
                }

                ViewBag.url = url;
                ViewBag.data = data;
                
                //USED TO RETURN JSON SO THE PAGE DOES NOT RELOAD EVERY TIME YOU SELECT AN OPTION
                //return Json.Encode(data);
            }
            return View();
        }
        


        //USER/CONFIRMFLYINGTRIP
        //GET: ConfirmFlyingTrip
        public ActionResult ConfirmFlyingTrip()
        {
            ViewBag.Name = new SelectList(db.Theme.ToList(), "Id", "destinationTheme");
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmFlyingTrip(ConfirmFlyingTripViewModel model)
        {
            var type = "Flying";

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
                //ThemeId = int.Parse(model.Theme),
                totalUsersOnTrip = model.totalUsersOnTrip + 1,
                departureDate = model.departureDate,
                returnDate = model.returnDate,
                flightNumber = model.flightNumber
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
            client.Send("MasterSplitter@TripSplit.com", user.Email, "TripSplit", "Congrats on booking your trip! \nHere are your trip details. \nTrip Name: " + trip.Name + "\nTrip Type:" + trip.Type + "\nStart Location: " + trip.originInput + "\nEnd Location: " + trip.destinationInput + "\nDeparture Date: " + trip.departureDate + "\nReturn Date: " + trip.returnDate + "\nTrip Cost: $" + trip.Cost + "\nTotal People on trip: " + trip.totalUsersOnTrip);

            return RedirectToAction("VerifyTrip", "User");
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

        public ActionResult TellAFriend(int id)
        {
            var trip = db.Trip.SingleOrDefault(t => t.Id == id);

            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        [HttpPost]
        public ActionResult TellAFriend(int id, TellAFriendViewModel model)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var trip = db.Trip.SingleOrDefault(t => t.Id == id);

            var friendEmail = model.friendEmail;

            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("3cad2a6d8a23a7", "150ffbb33ba612"),
                EnableSsl = true
            };
            client.Send(user.Email , friendEmail, "Check Out the Trip I Just Booked", "I just booked a trip on TripSplit. Here are all of the details! \nHere are your trip details. \nTrip Name: " + trip.Name + "\nTrip Type:" + trip.Type + "\nStart Location: " + trip.originInput + "\nEnd Location: " + trip.destinationInput + "\nFlight Number: " + trip.flightNumber + "\nTrip Cost: $" + trip.Cost + "\nTotal People on trip: " + trip.totalUsersOnTrip);

            return RedirectToAction("UserIndex", "User");
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

        //USER/USERTRIPAGREEMENT
        //POST: UserTripAgreement
        [HttpPost]
        public ActionResult UserTripAgreement(int id, UserTripAgreementViewModel model)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var trip = db.Trip.SingleOrDefault(t => t.Id == id);
            trip.Cost = trip.Cost / 2;
            trip.totalUsersOnTrip = trip.totalUsersOnTrip + 1;
            trip.Users = new List<ApplicationUser>();
            trip.Users.Add(user);
            db.SaveChanges();

            //MAIL TO CUSTOMER USING MAILTRAP.IO
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("3cad2a6d8a23a7", "150ffbb33ba612"),
                EnableSsl = true
            };
            client.Send("MasterSplitter@TripSplit.com", user.Email, "You Split The Trip!", "Congrats on booking and splitting your trip! \nHere are your trip details. \nTrip Name: " + trip.Name + "\nTrip Type:" + trip.Type + "\nStart Location: " + trip.originInput + "\nEnd Location: " + trip.destinationInput + "\nFlight Number: " + trip.flightNumber + "\nTrip Cost: $" + trip.Cost + "\nTotal People on trip: " + trip.totalUsersOnTrip);

            return RedirectToAction("VerifyTrip", "User");

            #region MailGun
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
            #endregion
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
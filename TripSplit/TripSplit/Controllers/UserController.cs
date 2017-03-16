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
        // GET: UserIndex
        public ActionResult UserIndex()
        {
            return View();
        }

        //GET: CreateATrip
        public ActionResult CreateATrip()
        {
            return View();
        }

        //
        //POST: CreateATrip
        [HttpPost]
        public ActionResult CreateTrip()
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

        public string ThemeTravel()
        {
            //var userTeamSelection = (passed from create roster)
            WebRequest request = WebRequest.Create("https://api.test.sabre.com/v1/lists/supported/shop/themes");
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);
            reader.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
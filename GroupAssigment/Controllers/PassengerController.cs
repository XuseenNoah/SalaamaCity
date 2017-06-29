using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupAssigment.ViewModels;
using GroupAssigment.Models;
using GroupAssigment.Helpers;

namespace GroupAssigment.Controllers
{
    public class PassengerController : Controller
    {
        Passenger_repository _repository = new Passenger_repository();
        // GET: Passenger
        public ActionResult CreatePassenger()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePassenger(Paasengers pass)
        {
            if (ModelState.IsValid)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[15];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var gencode = new String(stringChars);

                _repository.CreatePassenger(pass,gencode);
                TempData["su"] = "Succesfully Saved";
                return RedirectToAction("PrintReceipt", new { Gencode = gencode });



            }
            return View();
        }
        public ActionResult PrintReceipt(string Gencode)
        {
            var getrec = _repository.GetPrint(Gencode);
            return View(getrec);
        }
        public ActionResult ListPassenger(string name,string from,DateTime?Datefrom=null,DateTime?Dateto=null)
        {
            var getlist = _repository.ListPassenger(name,from,Datefrom,Dateto);
            return View(getlist);
        }
        public ActionResult UpdatePassenger(string Id)
        {
            var getupdate = _repository.GetUpdatePasenger(Id);
            return View(getupdate);
        }
        [HttpPost]
        public ActionResult UpdatePassenger(Paasengers pass)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdatePassenger(pass);
                TempData["succ"] = "Succesfully Updated";
                return RedirectToAction("ListPassenger");
            }
            return View();
        }

        public ActionResult DeletePassenger(string Id)
        {
            _repository.DeletePassenger(Id);
            TempData["sud"] = "Succesfully Deleted";
            return RedirectToAction("ListPassenger");
        }
    }
}
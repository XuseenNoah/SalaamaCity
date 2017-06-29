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
    public class GoodsFromController : Controller
    {
        Goods_Repository _repository = new Goods_Repository();
        // GET: GoodsFrom
       public ActionResult CreateGoods()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGoods(GoodsFrom goods)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateGoods(goods);
                TempData["sucs"] = "Succesfully Saved";
                return RedirectToAction("ListGoods");
            }
            return View();
        }
        [PermisionRequired(LoginForm.Permissions.View)]
        public ActionResult ListGoods(string namesender,string phone,DateTime?Datefrom=null,DateTime?Dateto=null)
        {
            var getlist = _repository.LIstGoods(namesender,phone,Datefrom,Dateto);
            return View(getlist);
        }
        public ActionResult UpdateGoods(string Id)
        {
            var getupdate = _repository.GetUpdateGoods(Id);
            return View(getupdate);
        }
        [HttpPost]
        public ActionResult UpdateGoods(GoodsFrom goods)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateGoods(goods);
                TempData["SuccesUpdate"] = "Succesfully Updated";
                return RedirectToAction("ListGoods");
            }
            return View();
        }

        public ActionResult ReportGoods(DateTime?Datefrom=null,DateTime?Dateto=null)
        {
            var getrep = _repository.GetReportGoods(Datefrom,Dateto);
            return View(getrep);
        }


    }
}
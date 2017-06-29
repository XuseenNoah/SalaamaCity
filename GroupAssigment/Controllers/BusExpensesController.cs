using GroupAssigment.Models;
using GroupAssigment.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using GroupAssigment.Helpers;

namespace GroupAssigment.Controllers
{
    public class BusExpensesController : Controller
    {
        BusExpensesRepositroy _repository = new BusExpensesRepositroy();
        // GET: BusExpenses
        [PermisionRequired(LoginForm.Permissions.Add)]
        public ActionResult CreateBusExpenses()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateBusExpenses(BusExpenses busexp)

        {
            if (ModelState.IsValid)
            {
                _repository.Create(busexp);
                TempData["Succesfully"] = "Succesfully Saved";
                return RedirectToAction("ListBusExpenses");

            }
            return View();
        }
        [PermisionRequired(LoginForm.Permissions.View)]
        public ActionResult ListBusExpenses(string busnumber,string busdrivername,DateTime? Datefrom=null,DateTime? Dateto=null)
        {
            var getlist = _repository.List(busnumber,busdrivername,Datefrom,Dateto);
            Session["busexp"] = getlist;
            return View(getlist);
        }

        public ActionResult ExportEcelBusExp()
        {
            var result = (List<BusExpenses>)Session["busexp"];

            if (result.Count == 0)
            {
                TempData["SessionNull"] = "Nothing to export";
            }
            else
            {
                GridView gv = new GridView();
                gv.DataSource = result;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=excel.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                return RedirectToAction("ListBusExpenses");
            }
            return RedirectToAction("ListBusExpenses");
        }
        [PermisionRequired(LoginForm.Permissions.Update)]
        public ActionResult Update(string Id)
        {
            var getupdate = _repository.GetUpdate(Id);
            return View(getupdate);
        }
        [HttpPost]
        public ActionResult Update(BusExpenses bus)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(bus);
                TempData["suces"] = "Succesfully Updated";
                return RedirectToAction("ListBusExpenses");
            }
            return View();
        }
        [PermisionRequired(LoginForm.Permissions.Delete)]
        public ActionResult Delete(BusExpenses bus)
        {
            _repository.Delete(bus);
            TempData["Deletesuces"] = "Succesfully Deleted";
            return RedirectToAction("ListBusExpenses");
        }

        public ActionResult ReportGoodsFrom(DateTime?Datefrom=null,DateTime?Dateto=null)
        {
            var getrep = _repository.GetReport(Datefrom, Dateto);
            return View(getrep);
        }
    }
}
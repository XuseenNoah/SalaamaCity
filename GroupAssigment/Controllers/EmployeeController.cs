using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupAssigment.ViewModels;
using GroupAssigment.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace GroupAssigment.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeRepository _repository = new EmployeeRepository();
        // GET: Employee
        public  ActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateEmployee(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateEmployee(emp);
                TempData["Succes"] = "Succesfully Saved";
                return RedirectToAction("ListEmployee");
            }
            return View();
        }
        public ActionResult ListEmployee(string name,string phone,DateTime?Datefrom=null,DateTime?Dateto=null)
        {
            var getlsitemp = _repository.ListEmployee(name,phone,Datefrom,Dateto);
            Session["employee"] = getlsitemp;
            return View(getlsitemp);
        }

        public ActionResult ExportExcelEmployee()
        {
            var result = (List<Employee>)Session["employee"];

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
                return RedirectToAction("ListEmployee");
            }
            return RedirectToAction("ListEmployee");
        }
        public ActionResult UpdateEmployee(string Id)
        {
            var getupdate = _repository.GetUpdate(Id);
            return View(getupdate);
        }
        [HttpPost]
        public ActionResult UpdateEmployee(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(emp);
                TempData["SuccesUpdate"] = "Succesfully Updated";
                return RedirectToAction("ListEmployee");
            }
            return View();
        }

        public ActionResult Delete(Employee emp)
        {
            _repository.Delete(emp);
            TempData["SucesDeleted"] = "Succesfully Deleted";
            return RedirectToAction("ListEmployee");
        }


        public ActionResult CreateSallery()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateSallery(Sallery sell)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateSallery(sell);
                TempData["suc"] = "Succesfully Saved";

            }
            return View();
        }

        public ActionResult ListSallery(string firstname,string lastname,DateTime?Datefrom=null,DateTime?Dateto=null)
        {
            var getlist = _repository.ListSellary(firstname,lastname,Datefrom,Dateto);
            Session["sallery"] = getlist;
            return View(getlist);
        }
        public ActionResult ExportEcelSaalery()
        {
            var result = (List<Sallery>)Session["sallery"];

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
                return RedirectToAction("ListSallery");
            }
            return RedirectToAction("ListSallery");
        }
        public ActionResult UpdateSallery(string Id)
        {
            var getupdate = _repository.GetUpdateSallery(Id);
            return View(getupdate);
        }
        [HttpPost]
        public ActionResult UpdateSallery(Sallery sell)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateSallery(sell);
                TempData["sucp"] = "Succesfully Updated";
                return RedirectToAction("ListSallery");
            }
            return View();
        }

        public ActionResult DeleteSallery(string Id)
        {
            _repository.DeleteSallery(Id);
            TempData["sucd"] = "Succesfully Deleted";
            return RedirectToAction("ListSallery");
        }

        
    }
}
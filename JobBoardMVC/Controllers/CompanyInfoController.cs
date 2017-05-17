using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobBoardMVC.Models;
using Microsoft.AspNet.Identity;

namespace JobBoardMVC
{
    public class CompanyInfoController : Controller
    {
        private JobBoardMvcContext db = new JobBoardMvcContext();
        public ActionResult Details(string id)
        {
            var model = new CompanyViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.company = db.Companies.Find(id); 
            if (model.company == null)
            {
                return HttpNotFound();
            }
            // model.jobs 
            model.jobs = db.Jobs.Where(j => j.CompanyCompanyName == model.company.CompanyName).ToList();
            //<List>model.jobs = <List>db.Jobs.Where(j => j.CompanyCompanyName == model.company.CompanyName);
            
            model.jobCount = model.jobs.ToList().Count;

            // check if this company has already been saved by this user
            var userID = Guid.Parse(User.Identity.GetUserId());
            var saved = db.SavedCompanies.Where(s => s.CompanyCompanyName == model.company.CompanyName && s.UserID == userID).FirstOrDefault();
            model.companySaved = (saved == null ? false : true);

            return View(model);
        }
       protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

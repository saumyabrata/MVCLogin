using MVCLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLogin.Controllers
{
    public class dailynoteController : Controller
    {
        // GET: dailynote
        public ActionResult Index()
        {
            DNDataModel model = PopulateModel(null, DateTime.Now.Date);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string brcode,DateTime cmdate)
        {
            DNDataModel model = PopulateModel(brcode,cmdate);
            return View(model);
        }

        private static DNDataModel PopulateModel(string brcode,DateTime cmdate)
        {
            using (dndataEntities entities = new dndataEntities())
            {
                DNDataModel model = new DNDataModel()
                {
                    Branches = (from c in entities.tblDailyNotes
                                 where (c.brcode == brcode && c.cmdate == cmdate) orderby c.ID select c).ToList(),
                    GetBranches = (from c in entities.tblBranches
                                   select new SelectListItem { Text = c.branch_name, Value = c.brcode }).Distinct().ToList()
                    // || string.IsNullOrEmpty(brcode)
                };

                return model;
            }
        }
    }
}
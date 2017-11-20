using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PIDS_Final.Controllers
{
    public class WordFileController : Controller
    {
        // GET: WordFile
        public ActionResult DisplayWord()
        {
            if(Request["filename"] != null)
            {
                ViewBag.display = Request["filename"].ToString();
            }
            return View();
        }
    }
}
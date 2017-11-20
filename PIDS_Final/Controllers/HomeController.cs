using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace PIDS_Final.Controllers
{
    public class HomeController : Controller
    {
        public static Dictionary<string, Postings> t;
        public static Dictionary<string, Postings> t_stoplist;
        // GET: Home
        public ActionResult Index()
        {
            try
            {
                Postings p;
                if (Request["searchWord"] != null)
                {

                    if (String.IsNullOrEmpty(Request["searchWord"]))
                    {
                        return View();
                    }
                    else
                    {
                        string searchWord = Request["searchWord"].ToString()
                            .ToLower()
                            .Trim();
                        string filter = Request["filterStoplist"];
                        if (t == null || t_stoplist == null)
                        {
                            loadTokens();
                        }
                        string[] searchWords;
                        if (searchWord.Contains(" "))
                        {
                            searchWords = searchWord.Split(' ');
                        }

                        if (filter == "true")
                        {
                            if (t_stoplist.ContainsKey(searchWord))
                            {
                                p = t_stoplist[searchWord];
                                ViewBag.wordFound = true;
                                ViewBag.posting = p;
                            }
                            else
                            {
                                ViewBag.wordFound = false;
                            }
                        }
                        else
                        {
                            if (t.ContainsKey(searchWord))
                            {
                                p = t[searchWord];
                                ViewBag.wordFound = true;
                                ViewBag.posting = p;
                            }
                            else
                            {
                                ViewBag.wordFound = false;
                            }
                        }
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return View();
        }

        private void loadTokens()
        {
            string mapPathStoplist = "~/App_Data/tokens_stoplist.txt";
            string mapPathAll = "~/App_Data/tokens_all.txt";

            t = getTokens(mapPathAll);
            t_stoplist = getTokens(mapPathStoplist);
        }

        private Dictionary<string, Postings> getTokens(string mapPath)
        {
            Dictionary<string, Postings> tokens = new Dictionary<string, Postings>();
            var dataFile = Server.MapPath(mapPath);
            string userData = System.IO.File.ReadAllText(dataFile);
            userData = userData.Trim();

            string[] splitData = userData.Split('|');
            for (int i = 0; i < splitData.Count(); i++)
            {
                string[] sections = splitData[i].Split(new string[] { "__" }, StringSplitOptions.None);
                if (sections.Length == 3)
                {
                    sections[0] = sections[0].Replace("\r\n", string.Empty)
                    .Replace("\r", string.Empty)
                    .Replace("\n", string.Empty);

                    if (sections[0].Length > 1 && !tokens.ContainsKey(sections[0]))
                    {
                        Postings postings = new Postings(sections[0]);
                        postings.overall_repetitions = Convert.ToInt32(sections[1]);
                        postings.repeatsPerFile = new HashSet<string>(sections[2].Split(','));

                        tokens.Add(sections[0], postings);
                    }
                }
            }
            return tokens;
        }
    }
}
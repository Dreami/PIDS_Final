using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDS_Final.Controllers
{
    public class Postings
    {
        public string word { get; set; }
        public int overall_repetitions { get; set; }
        public HashSet<string> repeatsPerFile { get; set; }

        public Postings(string w)
        {
            repeatsPerFile = new HashSet<string>();
            word = w;
            overall_repetitions = 1;
        }

        public Postings(string w, string fileName)
        {
            repeatsPerFile = new HashSet<string>();
            word = w;
            overall_repetitions = 1;
            repeatsPerFile.Add(fileName);
        }

        public void addWord(string fileName)
        {
            overall_repetitions++;
            repeatsPerFile.Add(fileName);
            if (!repeatsPerFile.Contains(fileName))
            {
                repeatsPerFile.Add(fileName);
            }
        }
    }
}
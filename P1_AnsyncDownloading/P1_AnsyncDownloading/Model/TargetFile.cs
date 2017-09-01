using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P1_AnsyncDownloading.Model
{
    public class TargetFile
    {
        string url;
        bool validURL;
        string localDirectory;

        public string URL
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
                this.validURL = CheckValidURL(url);
            }
        }

        public bool ValidURL
        {
            get
            {
                return validURL;
            }
        }

        public TargetFile(String url,String localDirectory="")
        {
            this.url = url;
            this.validURL = CheckValidURL(url);
        }

        private static bool CheckValidURL(String url)
        {
            string pattern = @"^((http[s]?|ftp):\/)?\/?([^:\/\s]+)((\/\w+)*\/)([\w\-\.]+[^#?\s]+)(.*)?(#[\w\-]+)?$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(url);
            return match.Success;
        }
    }
}

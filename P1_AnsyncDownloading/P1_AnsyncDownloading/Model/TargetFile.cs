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
        }
    }
}

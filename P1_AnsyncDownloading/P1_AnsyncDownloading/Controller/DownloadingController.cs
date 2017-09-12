using P1_AnsyncDownloading.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P1_AnsyncDownloading.Controller
{
    class DownloadingController
    {
        private static DownloadingController instance;

        public static DownloadingController Instance
        {
            get {

                if (instance == null)
                {
                    instance = new DownloadingController();
                }
                return instance;
            }

            
        }

        public bool IsValidURL(string url)
        {
            string pattern = @"^((http[s]?|ftp):\/)?\/?([^:\/\s]+)((\/\w+)*\/)([\w\-\.]+[^#?\s]+)(.*)?(#[\w\-]+)?$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(url);
            return match.Success;
        }

        public async Task<int> DownloadAsync(string fileName)
        {
            TargetFile file = new TargetFile(fileName);
            return await DownloadAsync(file);
        }

        private async Task<int> DownloadAsync(TargetFile file)
        {
            HttpClient request = new HttpClient();

            //create new task, the task has not started yet
            Task<string> getStringTask = request.GetStringAsync("http://msdn.microsoft.com");

            //Do some independent job here            
            UpdateProgress();

            //use await to perform a task
            string urlContent = await getStringTask;
            Console.WriteLine("finish getStringTask");
            return urlContent.Length;
        }

        private void UpdateProgress()
        {
            Console.WriteLine("update the downloading progress...");
        }

    }
}

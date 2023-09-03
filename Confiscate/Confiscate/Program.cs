using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using Confiscate;

namespace Confiscate
{
    internal static class Program
    {
        public static Confiscate MainForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string response = "";
            Task.Run(async () =>
            {
                response = Convert.ToString(await Post.PostForAccessToken());
            }).GetAwaiter().GetResult();
            ResponseWithAccessToken responseWithToken = JsonConvert.DeserializeObject<ResponseWithAccessToken>(response);
            string AccessToken = responseWithToken.access_token;
            MainForm = new Confiscate(AccessToken);

            Application.Run(MainForm);
        }
    }
}


using System;
using System.Globalization;
using System.Net;
using System.Threading;

namespace ModMapConverter
{
    class InternetCheckApp
    {
        public static bool disableinternet { get; private set; } = false;
        public static bool CheckForInternetConnection(int timeoutMs = 10000)
        {
            string url = null; // removed from CheckForInternetConnection just in case
            try
            {
                string n = CultureInfo.InstalledUICulture.ToString();

                if (n.StartsWith("fa")) // Iran
                {
                    url = "http://www.aparat.com";
                }
                else if (n.StartsWith("zh")) // China
                {
                    url = "http://www.baidu.com";
                }
                else if (url == null)
                {
                    url = "http://www.gstatic.com/generate_204";
                }

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using (var response = (HttpWebResponse)request.GetResponse())
                    return !disableinternet;
            }
            catch
            {
                return false;
            }
        }

        public static void StartInternetCheck()
        {
            string ICAction = "IC";
            bool internetConnection = false;
            int i = 0;

            while (internetConnection != true)
            {
                i += 1;

                Write(ICAction, "Checking Internet Connection... " + i.ToString() + "/5", ConsoleColor.Blue, ConsoleColor.White, ConsoleColor.Yellow, "\r");
                internetConnection = CheckForInternetConnection(5000);

                if (internetConnection == true || i >= 5)
                {
                    break;
                }

                Thread.Sleep(5000);
            }

            if (i >= 5 && internetConnection != true)
            {
                Write(ICAction, "No Internet Connection...", ConsoleColor.Blue, ConsoleColor.White, ConsoleColor.Red);
                Thread.Sleep(5000);
                Environment.Exit(0);
            }
            else
            {
                Write(ICAction, "Internet Connection found!", ConsoleColor.Blue, ConsoleColor.White, ConsoleColor.Green);
                Thread.Sleep(1000);
            }

            void Write(string action, string text, ConsoleColor actColor1 = ConsoleColor.Blue, ConsoleColor actColor2 = ConsoleColor.Blue, ConsoleColor txtColor = ConsoleColor.White, string start = "\n")
            {
                Console.Write(start);
                Console.ForegroundColor = actColor2;
                Console.Write("[");
                Console.ForegroundColor = actColor1;
                Console.Write(action);
                Console.ForegroundColor = actColor2;
                Console.Write("] ");
                Console.ForegroundColor = txtColor;
                Console.Write(text);

                Console.ForegroundColor = ConsoleColor.White; // set back to white
            }
        }
    }
}


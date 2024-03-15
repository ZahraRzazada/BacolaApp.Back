using System;
using System.Net;

namespace Bacola.Service.Helpers
{
	public class Helper
	{
        public static void SendMessageToTelegram(string Message)
        {
            string urlString = $"https://api.telegram.org/bot7032866741:AAHvpkRuMHAifR6ao3R5kVAvHsR268ppIfk/sendMessage?chat_id=1247556075&text={Message}";
            WebClient webclient = new WebClient();
            string res = webclient.DownloadString(urlString);
            Console.WriteLine(res);
        }
    }
}


/* Abrarov 220
REST API 
Teacher D.K.Y */
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using Telegram.Bot;


namespace RestAPI.YESorNO
{
    class Program
    {
        static Root answer;
        static void Main(string[] args) 
        {
            TelegramBotClient bot = new TelegramBotClient("1850458854:AAGTxgQxSUlvJndSDbK7rh2OVvob5osovHI");
            //BOT NAME IN TG: @YesOrNo11222_bot !!!!!!!!!!!!!!!!!!!!!!!!!!

            bot.OnMessage += (s, arg) =>
            {
                GetAnswer();
           
                bot.SendTextMessageAsync(arg.Message.Chat.Id, answer.answer);
                bot.SendVideoAsync(arg.Message.Chat.Id, answer.image);

            };

            bot.StartReceiving();

            Console.ReadKey();
        }

        static void GetAnswer()
        {
            var url = $"https://yesno.wtf/api";

            var request = WebRequest.Create(url);

            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
                return;
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                answer = JsonConvert.DeserializeObject<Root>(result);
            }
        }
    }
}

/* Abrarov 220
REST API 
Teacher D.K.Y */
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;


namespace RestAPI.YESorNO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите вопрос, на который хотите получить ответ");
            Console.ReadLine();
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
                var answer = JsonConvert.DeserializeObject<Root>(result);
                Console.WriteLine(answer.answer);
                System.Diagnostics.Process.Start(answer.image);
            }
        }
    }
}

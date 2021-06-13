using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

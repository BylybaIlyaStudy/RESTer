using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Poster
{
    class Program
    {
        static void Main()
        {
            PostRequestAsync();
        }

        private static void PostRequestAsync()
        {
            string method = "";

            while (method != "e")
            {
                Console.Clear();
                Console.WriteLine("p - post");
                Console.WriteLine("u - put");
                Console.WriteLine("d - delete");
                Console.WriteLine("e - exit");

                method = Console.ReadLine().ToLower();

                WebRequest request = WebRequest.Create("https://localhost:5001/api/userstatistics");

                switch (method)
                {
                    case "p":
                        request.Method = "POST";
                        break;
                    case "u":
                        request.Method = "PUT";
                        break;
                    case "d":
                        request.Method = "DELETE";
                        break;
                    case "e":
                        Console.WriteLine("выход...");
                        break;
                    default:
                        Console.WriteLine("ошибка");
                        break;
                }

                if (method == "e") break;

                Console.Write("username: ");
                string username = Console.ReadLine();

                Console.Write("version: ");
                string version = Console.ReadLine();

                Console.Write("os: ");
                string os = Console.ReadLine();

                if (username == "") username = "default";
                if (version == "") version = "n/a";
                if (os == "") os = "n/a";

                Console.WriteLine($"user: {username}, {version}, {os}");

                UserStatistics user = new UserStatistics(username, DateTime.Now, version, os);

                string data = JsonSerializer.Serialize<UserStatistics>(user);

                Console.WriteLine(data);

                // преобразуем данные в массив байтов
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
                // устанавливаем тип содержимого - параметр ContentType
                request.ContentType = "application/json";
                // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
                request.ContentLength = byteArray.Length;

                //записываем данные в поток запроса
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using StreamReader reader = new StreamReader(stream);
                    Console.WriteLine(reader.ReadToEnd());
                }
                response.Close();
                Console.WriteLine("Запрос выполнен...");
                Thread.Sleep(500);
            }
        }
    }
}

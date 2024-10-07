using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;

namespace ApiProjectConsole
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== API Demo ===");

            // API 1: HTTP-запити
            Console.WriteLine("\nAPI 1: HTTP-запит");
            string apiResponse = await MakeApiRequest();
            Console.WriteLine("HTTP Response: " + apiResponse);

            // API 2: Серіалізація JSON
            Console.WriteLine("\nAPI 2: Серіалізація JSON");
            var sampleData = new { Name = "Yulia", Age = 19 };
            string jsonData = SerializeObject(sampleData);
            Console.WriteLine("Serialized JSON: " + jsonData);

            // API 3: Локальні файли
            Console.WriteLine("\nAPI 3: Робота з локальними файлами");
            WriteToFile("Hello from .NET!");
            string fileContent = ReadFromFile();
            Console.WriteLine("File content: " + fileContent);

            // API 4: Потоки даних
            Console.WriteLine("\nAPI 4: Робота з потоками даних");
            WriteToStream("Stream API example");
            string streamContent = ReadFromStream();
            Console.WriteLine("Stream content: " + streamContent);

            // API 5: Колекції
            Console.WriteLine("\nAPI 5: Використання колекцій");
            List<int> numbers = CreateList();
            AddToList(numbers, 6);
            Console.WriteLine("Updated list: " + string.Join(", ", numbers));
        }

        // API 1: HTTP-запити
        public static async Task<string> MakeApiRequest()
        {
            var response = await client.GetStringAsync("https://jsonplaceholder.typicode.com/posts/1");
            return response;
        }

        // API 2: Серіалізація JSON
        public static string SerializeObject<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        // API 3: Локальні файли
        public static void WriteToFile(string content)
        {
            File.WriteAllText("example.txt", content);
        }

        public static string ReadFromFile()
        {
            return File.ReadAllText("example.txt");
        }

        // API 4: Потоки даних
        public static void WriteToStream(string content)
        {
            using (var stream = new FileStream("example.txt", FileMode.OpenOrCreate))
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
                stream.Write(data, 0, data.Length);
            }
        }

        public static string ReadFromStream()
        {
            using (var stream = new FileStream("example.txt", FileMode.Open))
            {
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                return System.Text.Encoding.UTF8.GetString(data);
            }
        }

        // API 5: Колекції
        public static List<int> CreateList()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            return numbers;
        }

        public static void AddToList(List<int> numbers, int newNumber)
        {
            numbers.Add(newNumber);
        }
    }
}


using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Otus_hw_serializer
{
    class Program
    {
        static void Main(string[] args) 
        {
            Stopwatch stopWatch = new Stopwatch();

            var student = new Student().Get();

            stopWatch.Start();

            var customCsv = string.Empty;
            for (int i = 0; i < 10000; i++)
            {
                customCsv = CustomCsvSerializer.Serialize(student);
            }

            stopWatch.Stop();
            Console.WriteLine("Custom serializer:");
            Console.WriteLine($"{stopWatch.ElapsedMilliseconds} milliseconds");

            stopWatch.Reset();
            stopWatch.Start();

            for (int i = 0; i < 10000; i++)
            {
                var deserializedCustom = CustomCsvSerializer.Deserialize<Student>(customCsv);
            }

            stopWatch.Stop();
            Console.WriteLine("Custom seserializer");
            Console.WriteLine($"{stopWatch.ElapsedMilliseconds} milliseconds");


            stopWatch.Reset();
            stopWatch.Start();

            var json = string.Empty;
            for (int i = 0; i < 10000; i++)
            {
                json = JsonConvert.SerializeObject(student);
            }

            stopWatch.Stop();
            Console.WriteLine("Newtonsoft serializer:");
            Console.WriteLine($"{stopWatch.ElapsedMilliseconds} milliseconds");

            stopWatch.Reset();
            stopWatch.Start();

            for (int i = 0; i < 10000; i++)
            {
                var deserializedNewtonsoft = JsonConvert.DeserializeObject<Student>(json);
            }

            stopWatch.Stop();
            Console.WriteLine("Newtonsoft deseserializer");
            Console.WriteLine($"{stopWatch.ElapsedMilliseconds} milliseconds");
        }
    }
}

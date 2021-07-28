using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SistemLogArray
{
    class Program
    {
        public static string _path { get; set; }
        public static List<string> _log = new List<string>();
        static void Main(string[] args)
        {
            Program._path = Directory.GetCurrentDirectory();

            Parallel.Invoke(
                () => LogTaskOne(),
                () => LogTaskTwo(),
                () => LogTaskError()
                );

            WriteLog(_log);
            Console.WriteLine("Fim de processo!");
        }

        private static void LogTaskError()
        {
            for (int i = 0; i < 300; i++)
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Excessão erro! - {i}");
                _log.Add($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Excessão erro! - {i}");
            }
        }

        private static void LogTaskTwo()
        {
            for (int i = 0; i < 300; i++)
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Executando TaskTwo - {i}");
                _log.Add($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Executando TaskTwo - {i}");
            }
        }

        private static void LogTaskOne()
        {
            for (int i = 0; i < 300; i++)
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Executando TaskOne - {i}");
                _log.Add($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Executando TaskOne - {i}");
            }
        }

        public static void CreateDir(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                throw new IOException(e.Message, e.InnerException);
            }
        }

        private static async Task WriteLog(List<string> message)
        {
            string path = Path.Combine(Program._path, "log");
            string fileName = "log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            CreateDir(path);
            string fullpath = Path.Combine(path, fileName);

            if (!File.Exists(fullpath))
            {
                FileStream fileStream = File.Create(fullpath);
                fileStream.Dispose();
            }
            await File.WriteAllLinesAsync(fullpath, message);

        }

        public static async Task ExampleAsync()
        {
            string[] lines =
            {
                "First line", "Second line", "Third line"
            };

            await File.WriteAllLinesAsync("WriteLines.txt", lines);
        }

    }
}

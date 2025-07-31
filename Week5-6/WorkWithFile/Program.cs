using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WorkWithFile
{
    class Program
    {
        static async Task WriteToFile(string path, string content)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    await writer.WriteAsync(content);
                    Console.WriteLine($"Content written to {path} successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }

        static async Task ReadFromFile(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string content = await reader.ReadToEndAsync();
                    Console.WriteLine($"Content of {path}:\n{content}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from file: {ex.Message}");
            }
        }

        static async Task Main(string[] args)
        {
            string filePath = "data.txt";

            Console.WriteLine("Enter content write to file: ");
            string content = Console.ReadLine();
            await WriteToFile(filePath, content);

            Console.WriteLine("\nClick 'Enter' to start reading file: ");
            Console.ReadLine();

            Console.WriteLine("Reading file...");
            await ReadFromFile(filePath);
        }
    }
}

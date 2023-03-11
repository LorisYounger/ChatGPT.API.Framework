using ChatGPT.API.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPT.API.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ChatGPT API Test");
            Console.WriteLine("APIKey: ");
            ChatGPTClient cgc = new ChatGPTClient(Console.ReadLine());
            Console.WriteLine("System Message: ");
            cgc.CreateCompletions("def", Console.ReadLine());
            while (true)
            {
                Console.WriteLine("User Message: ");
                Console.WriteLine("System Message: \n" + cgc.Ask("def", Console.ReadLine()).GetMessageContent());
            }
        }
    }
}

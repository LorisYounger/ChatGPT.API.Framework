using ChatGPT.API.Framework;
using System;
using System.Collections.Generic;
using System.IO;
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
            ChatGPTClient cgc;
            Console.WriteLine("APIKey: ");
            if (File.Exists(Environment.CurrentDirectory + @"\.save.tmp"))
            {
                cgc = ChatGPTClient.Load(File.ReadAllText(Environment.CurrentDirectory + @"\.save.tmp"));
                Console.WriteLine(cgc.APIKey);
                Console.WriteLine("System Message: ");
                Console.WriteLine(cgc.Completions["def"].messages[0].content);
                for (int i = 1; i < cgc.Completions["def"].messages.Count; i++)
                {
                    Console.WriteLine(cgc.Completions["def"].messages[i].role.ToString() + " Message: ");
                    Console.WriteLine(cgc.Completions["def"].messages[i].content);
                }
            }
            else
            {
                cgc = new ChatGPTClient(Console.ReadLine());
                Console.WriteLine("System Message: ");
                cgc.CreateCompletions("def", Console.ReadLine());
            }
            while (true)
            {
                Console.WriteLine("User Message: ");
                var rl = Console.ReadLine();
                if (rl == "exit")
                {
                    File.WriteAllText(Environment.CurrentDirectory + @"\.save.tmp", cgc.Save());
                    return;
                }
                Console.WriteLine("System Message: \n" + cgc.Ask("def", rl).GetMessageContent());
            }
        }
    }
}

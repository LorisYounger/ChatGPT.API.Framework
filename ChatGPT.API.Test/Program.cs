using ChatGPT.API.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;

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
            if (cgc.Completions["def"].messages.Last().role == Message.RoleType.user)
            {
                Console.WriteLine("System Message: \n" + cgc.Completions["def"].GetResponse(cgc.APIUrl, cgc.APIKey).GetMessageContent());
            }
            Console.WriteLine("\nChoose Mode:");
            Console.WriteLine("1. GPT Ask");
            Console.WriteLine("2. GPT Stream Ask");
            Console.WriteLine("3. Moderations");

            switch (Console.ReadLine())
            {
                case "1":
                    AskLoop(cgc);
                    break;
                case "2":
                    StreamAskLoop(cgc);
                    break;
                case "3":
                    ModerationsLoop(cgc);
                    break;
            }
        }
        public static void ModerationsLoop(ChatGPTClient cgc)
        {
            while (true)
            {
                Console.WriteLine("User Message: ");
                var rl = Console.ReadLine();
                if (rl == "exit")
                {
                    File.WriteAllText(Environment.CurrentDirectory + @"\.save.tmp", cgc.Save());
                    return;
                }
                Console.WriteLine("System Message: \n" + string.Join(".", cgc.Moderation_async(rl).Result.FlaggedReason));
            }
        }

        public static void AskLoop(ChatGPTClient cgc)
        {
            while (true)
            {
                if (cgc.Completions["def"].messages.Last().role == Message.RoleType.user)
                {
                    Console.WriteLine("System Message: \n" + cgc.Completions["def"].GetResponse(cgc.APIUrl, cgc.APIKey).GetMessageContent());
                }
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

        public static void StreamAskLoop(ChatGPTClient cgc)
        {
            Console.WriteLine("User Message: ");
            var rl = Console.ReadLine();
            if (rl == "exit")
            {
                File.WriteAllText(Environment.CurrentDirectory + @"\.save.tmp", cgc.Save());
                return;
            }
            Console.WriteLine("System Message:"); //Type1: cgc.Ask("def", rl).GetMessageContent();
                                                  //Type2:
            bool cannotnext = true;//防止问问题的时候自动退出控制台
            cgc.Ask_stream("def", rl, (x) =>
            {
                if (!string.IsNullOrEmpty(x.GetDeltaContent()))
                {
                    Console.Write(x.GetDeltaContent());
                }
                else if (x.GetDelta()?.finish_reason != null)
                {
                    Console.WriteLine("\n---" + x.choices[0].delta.finish_reason + "---\n");
                    AskLoop(cgc);
                    cannotnext = false;
                }
            });
            while (cannotnext)
            {
                Thread.Sleep(1000);
            }
        }
    }
}

using ChatGPT.API.Framework;
using Newtonsoft.Json;
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
            if (File.Exists(Environment.CurrentDirectory + @"\ChatGPTClient.json"))
            {
                cgc = ChatGPTClient.Load(File.ReadAllText(Environment.CurrentDirectory + @"\ChatGPTClient.json"));
                Console.WriteLine("API URL: " + cgc.APIUrl);
                Console.WriteLine("Model: " + cgc.DefaultModel);
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
                cgc = new ChatGPTClient();

                Console.WriteLine("API URL:");
                cgc.APIUrl = Console.ReadLine();
                Console.WriteLine("Model:");
                cgc.DefaultModel = Console.ReadLine();
                Console.WriteLine("APIKey: ");
                cgc.APIKey = Console.ReadLine();
                Console.WriteLine("System Message: ");
                cgc.CreateCompletions("def", Console.ReadLine());
            }
            ConfigureThinking(cgc);
            if (cgc.Completions["def"].messages.Last().role == Message.RoleType.user)
            {
                var response = cgc.Completions["def"].GetResponse(cgc.APIUrl, cgc.APIKey);
                Console.WriteLine("System Message: \n" + response?.GetMessageContent());
                PrintReasoningContent(response?.GetMessage()?.reasoning_content);
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
                    File.WriteAllText(Environment.CurrentDirectory + @"\ChatGPTClient.json", cgc.Save());
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
                    var response = cgc.Completions["def"].GetResponse(cgc.APIUrl, cgc.APIKey);
                    Console.WriteLine("System Message: \n" + response?.GetMessageContent());
                    PrintReasoningContent(response?.GetMessage()?.reasoning_content);
                }
                Console.WriteLine("User Message: ");
                var rl = Console.ReadLine();
                if (rl == "exit")
                {
                    File.WriteAllText(Environment.CurrentDirectory + @"\ChatGPTClient.json", cgc.Save());
                    return;
                }
                var askResponse = cgc.Ask("def", rl);
                Console.WriteLine("System Message: \n" + askResponse?.GetMessageContent());
                PrintReasoningContent(askResponse?.GetMessage()?.reasoning_content);
            }
        }

        public static void StreamAskLoop(ChatGPTClient cgc)
        {
            Console.WriteLine("User Message: ");
            var rl = Console.ReadLine();
            if (rl == "exit")
            {
                File.WriteAllText(Environment.CurrentDirectory + @"\ChatGPTClient.json", cgc.Save());
                return;
            }
            Console.WriteLine("System Message:"); //Type1: cgc.Ask("def", rl).GetMessageContent();
                                                  //Type2:
            bool cannotnext = true;//防止问问题的时候自动退出控制台
            bool startcont = true;
            cgc.Ask_stream("def", rl, (x) =>
            {
                if (x == null)
                {
                    Console.WriteLine("\n------\n");
                    StreamAskLoop(cgc);
                    cannotnext = false;
                    return;
                }
                if (!string.IsNullOrEmpty(x.GetContent()))
                {
                    if (startcont)
                    {
                        Console.WriteLine("\n--- end thinking ---\n");
                        startcont = false;
                    }
                    Console.Write(x.GetDeltaContent());
                }
                if (!string.IsNullOrEmpty(x.GetReasoningContent()))
                {
                    Console.Write(x.GetReasoningContent());
                }
                else if (x.GetFinishReason() != null)
                {
                    Console.WriteLine("\n--- " + x.GetFinishReason() + " ---\n");
                    StreamAskLoop(cgc);
                    cannotnext = false;
                }
            });
            while (cannotnext)
            {
                Thread.Sleep(1000);
            }
        }

        public static void ConfigureThinking(ChatGPTClient cgc)
        {
            Console.WriteLine($"Thinking mode (enabled/disabled), now is {cgc.Completions["def"].thinking.type}:");
            var thinkingType = Console.ReadLine();
            if (thinkingType == "enabled" || thinkingType == "disabled")
            {
                cgc.Completions["def"].thinking = new Completions.Thinking { type = thinkingType };
            }
        }

        public static void PrintReasoningContent(string reasoningContent)
        {
            if (!string.IsNullOrEmpty(reasoningContent))
            {
                Console.WriteLine("Reasoning: \n" + reasoningContent);
            }
        }

    }
}

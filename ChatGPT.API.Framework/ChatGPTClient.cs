﻿using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static ChatGPT.API.Framework.Moderations;

namespace ChatGPT.API.Framework
{
#nullable enable
    /// <summary>
    /// ChatGPT Client
    /// </summary>
    public class ChatGPTClient
    {
        /// <summary>
        /// Json Serializer Settings
        /// </summary>
        public static JsonSerializerSettings JsonSerializerSettings { get; set; } = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        /// <summary>
        /// Create a new Client
        /// </summary>
        public ChatGPTClient(string apikey, string apiurl = TestedModel.OpenAI.APIUrl, HttpMessageHandler? proxy = null)
        {
            APIKey = apikey;
            APIUrl = apiurl;
            Proxy = proxy;
        }
        /// <summary>
        /// Create a new Client
        /// </summary>
        public ChatGPTClient(string apikey, string apiurl, string webproxy)
        {
            APIKey = apikey;
            APIUrl = apiurl;
            if (!string.IsNullOrWhiteSpace(webproxy))
            {
                WebProxy = webproxy;
                Proxy = new HttpClientHandler()
                {
                    Proxy = new WebProxy(webproxy),
                    UseProxy = true
                };
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatGPTClient"/> class.
        /// </summary>
        public ChatGPTClient()
        {
        }


        /// <summary>
        /// YOUR_API_KEY
        /// </summary>
        public string APIKey { get; set; } = "";
        /// <summary>
        /// ChatGPT API URL
        /// </summary>
        public string APIUrl { get; set; } = "";
        /// <summary>
        /// Total Token Usage
        /// </summary>
        public long TotalTokensUsage { get; set; }
        /// <summary>
        /// a completion for the chat message
        /// </summary>
        public ConcurrentDictionary<string, Completions> Completions { get; set; } = new ConcurrentDictionary<string, Completions>();



        /// <summary>
        /// Default Completion settings for the ChatGPT client.
        /// </summary>
        public Completions DefaultCompletion = new()
        {
            model = TestedModel.OpenAI.GPT_4o_mini
        };

        /// <summary>
        /// Default Model 
        /// </summary>
        public string? DefaultModel
        {
            get => DefaultCompletion.model;
            set => DefaultCompletion.model = value;
        }
        /// <summary>
        /// Save as Json
        /// </summary>
        public string Save() => JsonConvert.SerializeObject(this, JsonSerializerSettings);
        /// <summary>
        /// Load from Json
        /// </summary>
        public static ChatGPTClient? Load(string json)
        {
            var cgc = JsonConvert.DeserializeObject<ChatGPTClient>(json);
            if (!string.IsNullOrWhiteSpace(cgc?.WebProxy))
            {
                cgc!.Proxy = new HttpClientHandler()
                {
                    Proxy = new WebProxy(cgc.WebProxy),
                    UseProxy = true
                };
            }
            return cgc;
        }
        /// <summary>
        /// Create A new Chat Completions
        /// </summary>
        public Completions CreateCompletions(string id, string systemmessages)
        {
            var cp = new Completions()
            {
                model = DefaultCompletion.model,
                user = DefaultCompletion.user == "default" ? id : DefaultCompletion.user,
                temperature = DefaultCompletion.temperature,
                max_tokens = DefaultCompletion.max_tokens,
                presence_penalty = DefaultCompletion.presence_penalty,
                frequency_penalty = DefaultCompletion.frequency_penalty,
                n = DefaultCompletion.n
            };
            cp.messages.Add(new Message() { role = Message.RoleType.system, content = systemmessages });
            Completions.AddOrUpdate(id, cp, (key, existingValue) => cp);
            return cp;
        }
        /// <summary>
        /// Ask ChatGPT
        /// </summary>
        public Response? Ask(string id, string usermessage) => Ask_async(id, usermessage).Result;
        /// <summary>
        /// Proxy Handler
        /// </summary>
        [JsonIgnore] public HttpMessageHandler? Proxy { get; set; } = null;
        /// <summary>
        /// WebProxy url
        /// </summary>
        public string? WebProxy { get; set; } = null;
        /// <summary>
        /// Ask ChatGPT
        /// </summary>
        public async Task<Response?> Ask_async(string id, string usermessage)
        {
            if (!Completions.TryGetValue(id, out Completions? cp))
            {
                cp = new Completions()
                {
                    messages = DefaultCompletion.messages.ToList(),
                    model = DefaultCompletion.model,
                    user = DefaultCompletion.user == "default" ? id : DefaultCompletion.user,
                    temperature = DefaultCompletion.temperature,
                    max_tokens = DefaultCompletion.max_tokens,
                    presence_penalty = DefaultCompletion.presence_penalty,
                    frequency_penalty = DefaultCompletion.frequency_penalty,
                    n = DefaultCompletion.n
                };
                Completions.AddOrUpdate(id, cp, (key, existingValue) => cp);
            }
            var rs = await cp.Ask_async(usermessage, APIUrl, APIKey, Proxy);
            if (rs != null)
                TotalTokensUsage += rs.usage.total_tokens;
            return rs;
        }

        /// <summary>
        /// Sends a user message to ChatGPT and processes the response as a stream.
        /// </summary>
        /// <param name="id">The unique identifier for the conversation.</param>
        /// <param name="usermessage">The message from the user to send to ChatGPT.</param>
        /// <param name="Response">An action to handle the streamed response from ChatGPT.</param>
        public void Ask_stream(string id, string usermessage, Action<Response_Stream?> Response)
        {
            if (!Completions.TryGetValue(id, out Completions? cp))
            {
                cp = new Completions()
                {
                    messages = DefaultCompletion.messages.ToList(),
                    model = DefaultCompletion.model,
                    user = DefaultCompletion.user == "default" ? id : DefaultCompletion.user,
                    temperature = DefaultCompletion.temperature,
                    max_tokens = DefaultCompletion.max_tokens,
                    presence_penalty = DefaultCompletion.presence_penalty,
                    frequency_penalty = DefaultCompletion.frequency_penalty,
                    n = DefaultCompletion.n
                };
                Completions.AddOrUpdate(id, cp, (key, existingValue) => cp);
            }
            cp.Ask_stream(usermessage, APIUrl, APIKey, Response);
        }

        /// <summary>
        /// 审查文本
        /// </summary>
        /// <param name="texts">要审查的文本</param>
        /// <returns>审查结果</returns>
        public async Task<ModerationResponse> Moderation_async(params string[] texts)
        {
            return await ModerationAsync(texts, APIKey, APIUrl.Replace("chat/completions", "moderations"), Proxy);
        }
        /// <summary>
        /// 审查 Completions
        /// </summary>
        /// <param name="cp">Completions</param>
        /// <returns>审查结果</returns>
        public async Task<ModerationResponse> Moderation_async(Completions cp)
        {
            HashSet<string> texts = new HashSet<string>();
            foreach (var item in cp.messages)
            {
                texts.Add(item.content);
            }
            return await ModerationAsync(texts.ToArray(), APIKey, APIUrl.Replace("chat/completions", "moderations"), Proxy);
        }
    }
}

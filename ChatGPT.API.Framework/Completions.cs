﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPT.API.Framework
{
    /// <summary>
    /// a completion for the chat message
    /// </summary>
    public class Completions
    {
        public static class Model
        {
            public const string GPT_4 = "gpt-4";
            public const string GPT_40_0314 = "gpt-4-0314";
            public const string GPT_40_32k = "gpt-4-32k";
            public const string GPT_40_32k_0314 = "gpt-4-32k-0314";
            public const string GPT_35_turbo = "gpt-3.5-turbo";
            public const string GPT_35_turbo_0301 = "gpt-3.5-turbo-0301";
        }
        /// <summary>
        /// ID of the model to use.
        /// gpt-4, gpt-4-0314, gpt-4-32k, gpt-4-32k-0314, gpt-3.5-turbo, gpt-3.5-turbo-0301
        /// </summary>
        public string model { get; set; } = Model.GPT_35_turbo;
        /// <summary>
        /// What sampling temperature to use, between 0 and 2. 
        /// Higher values like 0.8 will make the output more random, 
        /// while lower values like 0.2 will make it more focused and deterministic.
        /// </summary>
        public double temperature { get; set; } = 1;
        /// <summary>
        /// The maximum number of tokens allowed for the generated answer. 
        /// By default, the number of tokens the model can return will be (4096 - prompt tokens).
        /// </summary>
        public int max_tokens { get; set; } = 2048;
        /// <summary>
        /// Number between -2.0 and 2.0. 
        /// Positive values penalize new tokens based on whether they appear in the text so far
        /// increasing the model's likelihood to talk about new topics.
        /// </summary>
        public double presence_penalty { get; set; } = 0;
        /// <summary>
        /// Number between -2.0 and 2.0. 
        /// Positive values penalize new tokens based on their existing frequency in the text so far
        /// decreasing the model's likelihood to repeat the same line verbatim
        /// </summary>
        public double frequency_penalty { get; set; } = 0;
        /// <summary>
        /// The messages to generate chat completions for
        /// </summary>
        public List<Message> messages { get; set; } = new List<Message>();
        /// <summary>
        /// How many chat completion choices to generate for each input message.
        /// </summary>
        public int n { get; set; } = 1;
        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse
        /// </summary>
        public string user { get; set; } = "default";
        /// <summary>
        /// Get Response
        /// </summary>
        public Response GetResponse(string APIUrl, string APIKey)
        {
            var request = (HttpWebRequest)WebRequest.Create(APIUrl);
            request.Method = "POST";
            request.ContentType = "application/json";//ContentType
            request.Headers.Add("Authorization", "Bearer " + APIKey);
            byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
            int length = byteData.Length;
            request.ContentLength = length;
            using (Stream writer = request.GetRequestStream())
            {
                writer.Write(byteData, 0, length);
                writer.Close();
            }
            string responseString;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                responseString = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            }
            var rs = JsonConvert.DeserializeObject<Response>(responseString);
            messages.Add(rs.GetMessage());
            return rs;
        }
        /// <summary>
        /// Ask and Get Response
        /// </summary>
        public Response Ask(string usermessage, string APIUrl, string APIKey)
        {
            messages.Add(new Message() { role = Message.RoleType.user, content = usermessage });
            return GetResponse(APIUrl, APIKey);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPT.API.Framework
{
    /// <summary>
    /// ChatGPT Client
    /// </summary>
    public class ChatGPTClient
    {
        /// <summary>
        /// Create a new Client
        /// </summary>
        public ChatGPTClient(string apikey, string apiurl = "https://api.openai.com/v1/chat/completions")
        {
            APIKey = apikey;
            APIUrl = apiurl;
        }

        public ChatGPTClient()
        {
        }


        /// <summary>
        /// YOUR_API_KEY
        /// </summary>
        public string APIKey { get; set; }
        /// <summary>
        /// ChatGPT API URL
        /// </summary>
        public string APIUrl { get; set; }
        /// <summary>
        /// Total Token Usage
        /// </summary>
        public long TotalTokensUsage { get; set; }
        /// <summary>
        /// a completion for the chat message
        /// </summary>
        public Dictionary<string, Completions> Completions { get; set; } = new Dictionary<string, Completions>();

        /// <summary>
        /// Save as Json
        /// </summary>
        public string Save() => JsonConvert.SerializeObject(this);
        /// <summary>
        /// Load from Json
        /// </summary>
        public static ChatGPTClient Load(string json) => JsonConvert.DeserializeObject<ChatGPTClient>(json);
        /// <summary>
        /// Create A new Chat Completions
        /// </summary>
        public Completions CreateCompletions(string id, string systemmessages)
        {
            var cp = new Completions();
            cp.messages.Add(new Message() { role = Message.RoleType.system, content = systemmessages });
            Completions.Add(id, cp);
            return cp;
        }
        /// <summary>
        /// Ask ChatGPT
        /// </summary>
        public Response Ask(string id, string usermessage)
        {
            if (!Completions.TryGetValue(id, out Completions cp))
            {
                cp = new Completions()
                {
                    user = id
                };
                Completions.Add(id, cp);
            }
            var rs = cp.Ask(usermessage, APIUrl, APIKey);
            TotalTokensUsage += rs.usage.total_tokens;
            return rs;
        }
    }
}

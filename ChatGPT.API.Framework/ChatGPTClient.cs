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
        public ChatGPTClient(string aPIKey)
        {
            APIKey = aPIKey;
        }

        public ChatGPTClient()
        {
        }


        /// <summary>
        /// YOUR_API_KEY
        /// </summary>
        public string APIKey { get; set; }
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
        /// <returns></returns>
        public Response Ask(string id, string usermessage)
        {
            if (!Completions.TryGetValue(id, out Completions cp))
            {
                cp = new Completions();
                Completions.Add(id, cp);
            }
            cp.messages.Add(new Message() { role = Message.RoleType.user, content = usermessage });

            var request = (HttpWebRequest)WebRequest.Create("https://api.openai.com/v1/chat/completions");
            request.Method = "POST";
            request.ContentType = "application/json";//ContentType
            request.Headers.Add("Authorization", "Bearer " + APIKey);
            var str = JsonConvert.SerializeObject(cp);
            byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cp));
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
            cp.messages.Add(rs.GetMessage());
            TotalTokensUsage += rs.usage.total_tokens;
            return rs;
        }
    }
}

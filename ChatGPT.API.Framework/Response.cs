using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPT.API.Framework
{
    public class Response
    {
        public string id { get; set; }
        [JsonProperty(PropertyName = "object")]
        public string _object { get; set; }
        public int created { get; set; }
        public Usage usage { get; set; }
        public Choices[] choices { get; set; }
        /// <summary>
        /// Usage
        /// </summary>
        public class Usage
        {
            public int prompt_tokens;
            public int completion_tokens;
            public int total_tokens;
        }

        public class Choices
        {
            public int index;
            public Message message;
            public string finish_reason;
        }
        /// <summary>
        /// Get Response Message
        /// </summary>
        public string GetMessageContent() => choices[0].message.content;
        /// <summary>
        /// Get Response Message
        /// </summary>
        public Message GetMessage() => choices[0].message;
    }
}

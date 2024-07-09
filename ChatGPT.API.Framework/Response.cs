using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChatGPT.API.Framework.Message;
using static ChatGPT.API.Framework.Response;
using static ChatGPT.API.Framework.Response_Stream.Choices;

namespace ChatGPT.API.Framework
{
    public class Response
    {
        public string id { get; set; }
        [JsonProperty(PropertyName = "object")]
        public string _object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
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
        /// Get Response Message String
        /// </summary>
        public string GetMessageContent() => choices.FirstOrDefault()?.message?.content;
        /// <summary>
        /// Get Response Message
        /// </summary>
        public Message GetMessage() => choices.FirstOrDefault()?.message;
    }
    public class Response_Stream
    {
        public string id { get; set; }
        [JsonProperty(PropertyName = "object")]
        public string _object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public Choices[] choices { get; set; }
        public class Choices
        {
            public int index;
            public Delta delta;
            public class Delta
            {
                /// <summary>
                /// Role
                /// </summary>
                [JsonConverter(typeof(StringEnumConverter))]
                public RoleType? role { get; set; } = null;
                public string content { get; set; } = null;
                public string finish_reason = null;
            }
        }
        /// <summary>
        /// Get Response Delta String
        /// </summary>
        public string GetDeltaContent() => choices.FirstOrDefault()?.delta?.content;
        /// <summary>
        /// Get Response Delta
        /// </summary>
        public Delta GetDelta() => choices.FirstOrDefault()?.delta;
    }
}

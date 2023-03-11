using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPT.API.Framework
{
    public class Message
    {
        /// <summary>
        /// Role
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public RoleType role { get; set; }
        public enum RoleType
        {
            system,
            user,
            assistant
        }
        /// <summary>
        /// Content
        /// </summary>
        public string content;
    }
}

using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPT.API.Framework
{
    /// <summary>
    /// Represents a message with a role and content.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the role of the message.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public RoleType role { get; set; }

        /// <summary>
        /// Defines the possible roles for a message.
        /// </summary>
        public enum RoleType
        {
            /// <summary>
            /// Represents a system role.
            /// </summary>
            system,

            /// <summary>
            /// Represents a user role.
            /// </summary>
            user,

            /// <summary>
            /// Represents an assistant role.
            /// </summary>
            assistant
        }

        /// <summary>
        /// The content of the message.
        /// </summary>
        public string content = string.Empty;
    }
}

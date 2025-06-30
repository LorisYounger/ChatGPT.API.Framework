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
    /// <summary>
    /// Represents a response from the ChatGPT API.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// The unique identifier of the response.
        /// </summary>
        public string id { get; set; } = string.Empty;

        /// <summary>
        /// The type of object returned (e.g., "response").
        /// </summary>
        [JsonProperty(PropertyName = "object")]
        public string _object { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp of when the response was created.
        /// </summary>
        public int created { get; set; }

        /// <summary>
        /// The model used to generate the response.
        /// </summary>
        public string model { get; set; } = string.Empty;

        /// <summary>
        /// Token usage details for the response.
        /// </summary>
        public Usage usage { get; set; } = new Usage();

        /// <summary>
        /// The choices returned in the response.
        /// </summary>
        public Choices[] choices { get; set; } = Array.Empty<Choices>();

        /// <summary>
        /// Represents token usage details.
        /// </summary>
        public class Usage
        {
            /// <summary>
            /// The number of tokens used in the prompt.
            /// </summary>
            public int prompt_tokens { get; set; }

            /// <summary>
            /// The number of tokens used in the completion.
            /// </summary>
            public int completion_tokens { get; set; }

            /// <summary>
            /// The total number of tokens used.
            /// </summary>
            public int total_tokens { get; set; }
        }

#nullable enable

        /// <summary>
        /// Represents a choice in the response.
        /// </summary>
        public class Choices
        {
            /// <summary>
            /// The index of the choice.
            /// </summary>
            public int index { get; set; }

            /// <summary>
            /// The message associated with the choice.
            /// </summary>
            public Message? message { get; set; }

            /// <summary>
            /// The reason why the choice was finished.
            /// </summary>
            public string finish_reason { get; set; } = string.Empty;
        }

        /// <summary>
        /// Gets the content of the first response message.
        /// </summary>
        /// <returns>The content of the first message, or null if none exists.</returns>
        public string? GetMessageContent() => choices.FirstOrDefault()?.message?.content;

        /// <summary>
        /// Gets the first response message.
        /// </summary>
        /// <returns>The first message, or null if none exists.</returns>
        public Message? GetMessage() => choices.FirstOrDefault()?.message;
    }
    /// <summary>
    /// Represents a response stream from the ChatGPT API.
    /// </summary>
    public class Response_Stream
    {
        /// <summary>
        /// The unique identifier of the response stream.
        /// </summary>
        public string id { get; set; } = string.Empty;

        /// <summary>
        /// The type of object returned (e.g., "response_stream").
        /// </summary>
        [JsonProperty(PropertyName = "object")]
        public string _object { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp of when the response stream was created.
        /// </summary>
        public int created { get; set; }

        /// <summary>
        /// The model used to generate the response stream.
        /// </summary>
        public string model { get; set; } = string.Empty;

        /// <summary>
        /// The choices returned in the response stream.
        /// </summary>
        public Choices[] choices { get; set; } = Array.Empty<Choices>();

        /// <summary>
        /// Represents a choice in the response stream.
        /// </summary>
        public class Choices
        {
            /// <summary>
            /// The index of the choice.
            /// </summary>
            public int index { get; set; }

            /// <summary>
            /// The delta information associated with the choice.
            /// </summary>
            public Delta delta { get; set; } = new Delta();

            /// <summary>
            /// Represents the delta information in a choice.
            /// </summary>
            public class Delta
            {
                /// <summary>
                /// The role associated with the delta.
                /// </summary>
                [JsonConverter(typeof(StringEnumConverter))]
                public RoleType? role { get; set; } = null;

                /// <summary>
                /// The content of the delta.
                /// </summary>
                public string? content { get; set; } = null;

                /// <summary>
                /// The reason why the delta was finished.
                /// </summary>
                public string? finish_reason = null;
            }
        }

        /// <summary>
        /// Gets the content of the first delta in the response stream.
        /// </summary>
        /// <returns>The content of the first delta, or null if none exists.</returns>
        public string? GetDeltaContent() => choices.FirstOrDefault()?.delta?.content;

        /// <summary>
        /// Gets the first delta in the response stream.
        /// </summary>
        /// <returns>The first delta, or null if none exists.</returns>
        public Delta? GetDelta() => choices.FirstOrDefault()?.delta;
    }
}

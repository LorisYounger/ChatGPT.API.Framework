using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using static ChatGPT.API.Framework.Moderations;
using static ChatGPT.API.Framework.Response_Stream.Choices;

namespace ChatGPT.API.Framework
{
#nullable enable
    /// <summary>
    /// a completion for the chat message
    /// </summary>
    public class Completions : ICloneable
    {
        /// <summary>
        /// Settings for the model's thinking behavior.
        /// </summary>
        public class Thinking
        {
            /// <summary>
            /// Thinking mode type, e.g. "enabled".
            /// </summary>
            public string? type { get; set; } = "enabled";
        }
        /// <summary>
        /// Response format settings for the model's output.
        /// </summary>
        public class ResponseFormat
        {
            /// <summary>
            /// Response format type, e.g. "text" or "json_object".
            /// </summary>
            public string? type { get; set; } = "text";
        }

        /// <summary>
        /// ID of the model to use.
        /// </summary>
        public string? model { get; set; } = TestedModel.OpenAI.GPT_5_6_Luna;
        /// <summary>
        /// What sampling temperature to use, between 0 and 2. 
        /// Higher values like 0.8 will make the output more random, 
        /// while lower values like 0.2 will make it more focused and deterministic.
        /// </summary>
        public double? temperature { get; set; } = 1;
        /// <summary>
        /// The maximum number of tokens allowed for the generated answer. 
        /// By default, the number of tokens the model can return will be (4096 - prompt tokens).
        /// </summary>
        public int? max_tokens { get; set; } = 2048;
        /// <summary>
        /// Enables or configures model thinking behavior. [enabled, disabled]
        /// </summary>
        public Thinking? thinking { get; set; } = new Thinking { type = "disabled" };
        /// <summary>
        /// Controls reasoning effort, e.g. "low", "medium", "high".
        /// </summary>
        public string? reasoning_effort { get; set; }
        /// <summary>
        /// Controls response output format.
        /// </summary>
        public ResponseFormat? response_format { get; set; }
        /// <summary>
        /// Number between -2.0 and 2.0. 
        /// Positive values penalize new tokens based on whether they appear in the text so far
        /// increasing the model's likelihood to talk about new topics.
        /// </summary>
        [Obsolete] public double? presence_penalty { get; set; } = 0;
        /// <summary>
        /// Number between -2.0 and 2.0. 
        /// Positive values penalize new tokens based on their existing frequency in the text so far
        /// decreasing the model's likelihood to repeat the same line verbatim
        /// </summary>
        [Obsolete] public double? frequency_penalty { get; set; } = 0;
        /// <summary>
        /// The messages to generate chat completions for
        /// </summary>
        public List<Message> messages { get; set; } = new List<Message>();
        /// <summary>
        /// How many chat completion choices to generate for each input message.
        /// </summary>
        public int? n { get; set; } = 1;
        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse
        /// </summary>
        public string? user { get; set; } = "default";
        /// <summary>
        /// If set, partial message deltas will be sent, like in ChatGPT. Tokens will be sent as data-only server-sent events as they become available
        /// </summary>
        [JsonProperty] private bool stream { get; set; } = false;

        /// <summary>
        /// Get Response
        /// </summary>
        public Response? GetResponse(string APIUrl, string APIKey, HttpMessageHandler? Proxy = null) => GetResponse_async(APIUrl, APIKey, Proxy).Result;
        /// <summary>
        /// Get Response
        /// </summary>
        public async Task<Response?> GetResponse_async(string APIUrl, string APIKey, HttpMessageHandler? Proxy = null)
        {
            this.stream = false;
            using (var httpClient = Proxy == null ? new HttpClient() : new HttpClient(Proxy))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + APIKey);
                var content = new StringContent(JsonConvert.SerializeObject(this, ChatGPTClient.JsonSerializerSettings), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(APIUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var rs = JsonConvert.DeserializeObject<Response>(responseString);
                var msg = rs?.GetMessage();
                if (msg != null)
                    messages.Add(msg);
                return rs;
            }
        }
#nullable enable
        /// <summary>
        /// Get Response by Stream
        /// </summary>
        public async void GetResponse_stream(string APIUrl, string APIKey, Action<Response_Stream?> Response, HttpMessageHandler? Proxy = null)
        {
            this.stream = true;
            Message FakeMessage = new Message() { role = Message.RoleType.assistant, content = "" };
            using (var httpClient = Proxy == null ? new HttpClient() : new HttpClient(Proxy))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + APIKey);
                var content = new StringContent(JsonConvert.SerializeObject(this, ChatGPTClient.JsonSerializerSettings), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(APIUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            while (!reader.EndOfStream)
                            {
                                string? responseString = await reader.ReadLineAsync();
                                if (responseString == null)
                                    continue;
                                if (responseString.StartsWith("data: "))
                                    responseString = responseString.Substring(6);
                                if (string.IsNullOrEmpty(responseString))
                                {
                                    continue;
                                }
                                if (responseString == "[DONE]")
                                    break;
                                var rs = JsonConvert.DeserializeObject<Response_Stream>(responseString);
                                Response(rs);
                                if (rs?.choices.FirstOrDefault()?.delta?.role != null)
                                {
#pragma warning disable CS8629,CS8602 // 可为 null 的值类型可为 null。
                                    FakeMessage.role = rs.choices.FirstOrDefault().delta.role.Value;
                                }
                                if (string.IsNullOrEmpty(rs?.choices.FirstOrDefault()?.delta?.content) == false)
                                {
                                    FakeMessage.content += rs.choices.FirstOrDefault().delta.content;
                                    FakeMessage.reasoning_content += rs.choices.FirstOrDefault().delta.reasoning_content ?? "";
#pragma warning restore CS8629,CS8602 // 可为 null 的值类型可为 null。
                                }
                            }
                            Response(null);
                        }
                    }
                }
                else
                {
                    throw new Exception("Error: " + response.ReasonPhrase);
                }
                messages.Add(FakeMessage);
            }
        }

        /// <summary>
        /// Ask and Get Response
        /// </summary>
        public Response? Ask(string usermessage, string APIUrl, string APIKey) => Ask_async(usermessage, APIUrl, APIKey).Result;

        /// <summary>
        /// Ask and Get Response
        /// </summary>
        public async Task<Response?> Ask_async(string usermessage, string APIUrl, string APIKey, HttpMessageHandler? Proxy = null)
        {
            messages.Add(new Message() { role = Message.RoleType.user, content = usermessage });
            return await GetResponse_async(APIUrl, APIKey, Proxy);
        }

        /// <summary>
        /// Sends a user message to the API and processes the response stream asynchronously.
        /// </summary>
        /// <param name="usermessage">The message from the user to send to the API.</param>
        /// <param name="APIUrl">The URL of the API endpoint.</param>
        /// <param name="APIKey">The API key for authentication.</param>
        /// <param name="Response">An action to handle the streamed response.</param>
        /// <param name="Proxy">Optional HTTP message handler for proxy configuration.</param>
        public void Ask_stream(string usermessage, string APIUrl, string APIKey, Action<Response_Stream?> Response, HttpMessageHandler? Proxy = null)
        {
            messages.Add(new Message() { role = Message.RoleType.user, content = usermessage });
            GetResponse_stream(APIUrl, APIKey, Response, Proxy);
        }
        /// <summary>
        /// Clones the current Completions instance, creating a new instance with the same property values.
        /// </summary>
        public Completions Clone()
        {
#pragma warning disable CS0612 // 类型或成员已过时
            return new Completions
            {
                model = model,
                temperature = temperature,
                max_tokens = max_tokens,
                thinking = thinking == null ? null : new Thinking { type = thinking.type },
                reasoning_effort = reasoning_effort,
                response_format = response_format == null ? null : new ResponseFormat { type = response_format.type },
                presence_penalty = presence_penalty,
                frequency_penalty = frequency_penalty,
                messages = messages.Select(x => new Message { role = x.role, content = x.content }).ToList(),
                n = n,
                user = user,
            };
#pragma warning restore CS0612 // 类型或成员已过时
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}

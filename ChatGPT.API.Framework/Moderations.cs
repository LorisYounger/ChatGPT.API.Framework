using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static ChatGPT.API.Framework.Moderations;

namespace ChatGPT.API.Framework
{
#nullable enable
    /// <summary>
    /// 审查类 (OpenAI Only)
    /// </summary>
    public class Moderations
    {

        /// <summary>
        /// 调用审查API的模型 (免费) (OpenAI Only)
        /// </summary>
        /// <param name="texts">文本</param>
        /// <param name="APIUrl">审查模型链接地址</param>
        /// <param name="APIKey">秘钥</param>
        /// <param name="Proxy">协议(可选)</param>
        /// <returns>审查结果</returns>
        public static async Task<ModerationResponse> ModerationAsync(string[] texts, string APIKey, string APIUrl = "https://api.openai.com/v1/moderations", HttpMessageHandler? Proxy = null)
        {
            using (var httpClient = Proxy == null ? new HttpClient() : new HttpClient(Proxy))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + APIKey);
                var content = new StringContent(JsonConvert.SerializeObject(new { input = texts }, ChatGPTClient.JsonSerializerSettings), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(APIUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ModerationResponse>(responseString) ?? throw new Exception("Empty Response");
            }
        }

        /// <summary>
        /// 表示针对内容的审查响应，包括审查请求的唯一标识符、使用的模型以及审查结果。
        /// </summary>
        public class ModerationResponse
        {
            /// <summary>
            /// 审查请求的唯一标识符。
            /// </summary>
            public string id { get; set; } = "";

            /// <summary>
            /// 用于生成审查结果的模型名称。
            /// </summary>
            public string model { get; set; } = "";

            /// <summary>
            /// 审查结果的列表，包含一个或多个结果对象。
            /// </summary>
            public List<Result> results { get; set; } = new List<Result>();
            /// <summary>
            /// 所有结果是否有内容被标记
            /// </summary>
            [JsonIgnore] public bool flagged => results.Any(x => x.flagged);
            /// <summary>
            /// 所有结果的标记理由
            /// </summary>
            [JsonIgnore] public List<string> FlaggedReason => results.SelectMany(x => x.FlaggedReason).ToList();
        }
        /// <summary>
        /// 表示单个审查结果，包括是否被标记、类别、类别分数和应用输入类型。
        /// </summary>
        public class Result
        {
            /// <summary>
            /// 指示是否有任何类别被标记。
            /// </summary>
            public bool flagged { get; set; } = false;

            /// <summary>
            /// 标记理由
            /// </summary>
            [JsonIgnore]
            public List<string> FlaggedReason => categories.Where(x => x.Value).Select(x => x.Key).ToList();

            /// <summary>
            /// 类别及其是否被标记的字典。
            /// </summary>
            public Dictionary<string, bool> categories { get; set; } = new Dictionary<string, bool>();

            /// <summary>
            /// 类别及其分数的字典，分数由模型预测。
            /// </summary>
            public Dictionary<string, double> category_scores { get; set; } = new Dictionary<string, double>();

            /// <summary>
            /// 类别及其适用输入类型的字典，指示每个类别适用于哪些输入类型。
            /// </summary>
            public Dictionary<string, List<string>> category_applied_input_types { get; set; } = new Dictionary<string, List<string>>();
        }
    }
}

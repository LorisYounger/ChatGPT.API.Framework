using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPT.API.Framework
{
    /// <summary>
    /// Tested Model Card
    /// </summary>
    public static class TestedModel
    {
        /// <summary>
        /// https://openai.com/
        /// </summary>
        public static class OpenAI
        {
            /// <summary>
            /// The API URL for OpenAI's chat completions endpoint.
            /// </summary>
            public const string APIUrl = "https://api.openai.com/v1/chat/completions";

            /// <summary>
            /// GPT-4 model identifier.
            /// </summary>
            public const string GPT_4 = "gpt-4";

            /// <summary>
            /// GPT-4 Turbo model identifier.
            /// </summary>
            public const string GPT_4_turbo = "gpt-4-turbo";

            /// <summary>
            /// GPT-3.5 Turbo model identifier.
            /// </summary>
            public const string GPT_35_turbo = "gpt-3.5-turbo";

            /// <summary>
            /// GPT-4o model identifier.
            /// </summary>
            public const string GPT_4o = "gpt-4o";

            /// <summary>
            /// GPT-4o Mini model identifier.
            /// </summary>
            public const string GPT_4o_mini = "gpt-4o-mini";

            /// <summary>
            /// O1 Preview model identifier.
            /// </summary>
            public const string o1_preview = "o1-preview";

            /// <summary>
            /// O1 Mini model identifier.
            /// </summary>
            public const string o1_mini = "o1-mini";
        }

        /// <summary>
        /// https://open.bigmodel.cn/
        /// </summary>
        public static class BigModel
        {
            /// <summary>
            /// The API URL for BigModel's chat completions endpoint.
            /// </summary>
            public const string APIUrl = "https://open.bigmodel.cn/api/paas/v4/chat/completions";

            /// <summary>
            /// GLM-4 Plus model identifier.
            /// </summary>
            public const string GLM4_Plus = "glm-4-plus";

            /// <summary>
            /// GLM-4 Air model identifier.
            /// </summary>
            public const string GLM4_Air = "glm-4-air";

            /// <summary>
            /// GLM-4 AirX model identifier.
            /// </summary>
            public const string GLM4_AirX = "glm-4-airx";

            /// <summary>
            /// GLM-4 Long model identifier.
            /// </summary>
            public const string GLM4_Long = "glm-4-long";

            /// <summary>
            /// GLM-4 Flash model identifier.
            /// </summary>
            public const string GLM4_Flash = "glm-4-flash";

            /// <summary>
            /// GLM-4 FlashX model identifier.
            /// </summary>
            public const string GLM4_FlashX = "glm-4-flashx";
        }

        /// <summary>
        /// https://deepseek.com/
        /// </summary>
        public static class DeepSeek
        {
            /// <summary>
            /// The API URL for DeepSeek's chat completions endpoint.
            /// </summary>
            public const string APIUrl = "https://api.deepseek.com/v1/chat/completions";

            /// <summary>
            /// DeepSeek Chat model identifier.
            /// </summary>
            public const string DeepSeek_V3 = "deepseek-chat";

            /// <summary>
            /// DeepSeek Reasoner model identifier.
            /// </summary>
            public const string DeepSeek_R1 = "deepseek-reasoner";
        }

        /// <summary>
        /// https://aistudio.google.com/
        /// </summary>
        public static class Gemini
        {
            /// <summary>
            /// The API URL for Gemini's generative language endpoint.
            /// </summary>
            public const string APIUrl = "https://generativelanguage.googleapis.com/v1beta/openai/";

            /// <summary>
            /// Gemini 2.0 Flash model identifier.
            /// </summary>
            public const string Gemini_20_Flash = "gemini-2.0-flash";

            /// <summary>
            /// Gemini 2.0 Flash Lite model identifier.
            /// </summary>
            public const string Gemini_20_Flash_Lite = "gemini-2.0-flash-lite";

            /// <summary>
            /// Gemini 1.5 Flash model identifier.
            /// </summary>
            public const string Gemini_15_Flash = "gemini-1.5-flash";

            /// <summary>
            /// Gemini 1.5 Flash 8B model identifier.
            /// </summary>
            public const string Gemini_15_Flash_8B = "gemini-1.5-flash-8b";

            /// <summary>
            /// Gemini 1.5 Pro model identifier.
            /// </summary>
            public const string Gemini_15_Pro = "gemini-1.5-pro";
        }
    }
}

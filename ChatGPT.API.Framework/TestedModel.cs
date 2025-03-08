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
            public const string APIUrl = "https://api.openai.com/v1/chat/completions";
            public const string GPT_4 = "gpt-4";
            public const string GPT_4_turbo = "gpt-4-turbo";
            public const string GPT_35_turbo = "gpt-3.5-turbo";
            public const string GPT_4o = "gpt-4o";
            public const string GPT_4o_mini = "gpt-4o-mini";
            public const string o1_preview = "o1-preview";
            public const string o1_mini = "o1-mini";
        }
        /// <summary>
        /// https://open.bigmodel.cn/
        /// </summary>
        public static class BigModel
        {
            public const string APIUrl = "https://open.bigmodel.cn/api/paas/v4/chat/completions";
            public const string GLM4_Plus = "glm-4-plus";
            public const string GLM4_Air = "glm-4-air";
            public const string GLM4_AirX = "glm-4-airx";
            public const string GLM4_Long = "glm-4-long";
            public const string GLM4_Flash = "glm-4-flash";
            public const string GLM4_FlashX = "glm-4-flashx";
        }
        /// <summary>
        /// https://deepseek.com/
        /// </summary>
        public static class DeepSeek
        {
            public const string APIUrl = "https://api.deepseek.com/v1/chat/completions";
            public const string DeepSeek_V3 = "deepseek-chat";
            public const string DeepSeek_R1 = "deepseek-reasoner";
        }
        /// <summary>
        /// https://aistudio.google.com/
        /// </summary>
        public static class Gemini
        {
            public const string APIUrl = "https://generativelanguage.googleapis.com/v1beta/openai/";
            public const string Gemini_20_Flash = "gemini-2.0-flash";
            public const string Gemini_20_Flash_Lite = "gemini-2.0-flash-lite";
            public const string Gemini_15_Flash = "gemini-1.5-flash";
            public const string Gemini_15_Flash_8B = "gemini-1.5-flash-8b";
            public const string Gemini_15_Pro = "gemini-1.5-pro";
        }
    }
}

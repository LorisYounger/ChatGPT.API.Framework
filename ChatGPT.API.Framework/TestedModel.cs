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
            /// GPT-5.6 Sol – Flagship for complex reasoning, programming, research. Higher cost, best quality.
            /// </summary>
            public const string GPT_5_6_Sol = "gpt-5.6-sol";

            /// <summary>
            /// GPT-5.6 Terra – Balanced performance/cost for general tasks and moderate complexity.
            /// </summary>
            public const string GPT_5_6_Terra = "gpt-5.6-terra";

            /// <summary>
            /// GPT-5.6 Luna – Lightweight, low-cost, high-throughput for cost‑sensitive scenarios (e.g., customer service).
            /// </summary>
            public const string GPT_5_6_Luna = "gpt-5.6-luna";
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
            /// GLM-5.2 – High-performance, 1M context, good for coding and long‑document tasks. Higher cost.
            /// </summary>
            public const string GLM5_2 = "glm-5.2";

            /// <summary>
            /// GLM-5.1 – High-performance for coding and long‑horizon agentic tasks. Higher cost.
            /// </summary>
            public const string GLM5_1 = "glm-5.1";

            /// <summary>
            /// GLM-5 – Base model, balanced for agentic planning and programming, moderate cost.
            /// </summary>
            public const string GLM5 = "glm-5";

            /// <summary>
            /// GLM-5-Turbo – Optimized for multi‑step agent tasks, good continuity, moderate cost.
            /// </summary>
            public const string GLM5_Turbo = "glm-5-turbo";

            /// <summary>
            /// GLM-4.7 – General‑purpose upgrade for dialogue, reasoning, agents. Stable, lower cost than 5‑series.
            /// </summary>
            public const string GLM4_7 = "glm-4.7";

            /// <summary>
            /// GLM-4.7-FlashX – Lightweight, fast, low‑cost for Chinese writing, translation, role‑play.
            /// </summary>
            public const string GLM4_7_FlashX = "glm-4.7-flashx";

            /// <summary>
            /// GLM-4.6 – 200K context, good for advanced coding, reasoning, and tool calling. Good value.
            /// </summary>
            public const string GLM4_6 = "glm-4.6";

            /// <summary>
            /// GLM-4.5-Air – Economical, stable for reasoning, coding, and agent tasks. Low cost.
            /// </summary>
            public const string GLM4_5_Air = "glm-4.5-air";

            /// <summary>
            /// GLM-4.5-AirX – Extremely fast, low‑cost version for low‑latency, high‑concurrency scenarios.
            /// </summary>
            public const string GLM4_5_AirX = "glm-4.5-airx";

            /// <summary>
            /// GLM-4-Long – 1M context, designed for long document analysis and memory‑intensive tasks.
            /// </summary>
            public const string GLM4_Long = "glm-4-long";

            /// <summary>
            /// GLM-4.7-Flash – Free tier, high‑speed, suitable for high‑concurrency testing or light use.
            /// </summary>
            public const string GLM4_7_Flash = "glm-4.7-flash";
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
            /// DeepSeek V4 Flash – Cost‑effective, 13B activated, 1M context. Fast, suitable for real‑time dialogue and function calling.
            /// </summary>
            public const string V4_Flash = "deepseek-v4-flash";

            /// <summary>
            /// DeepSeek V4 Pro – High‑performance, 49B activated, 1M context. Best for math, STEM, competitive programming. Higher cost.
            /// </summary>
            public const string V4_Pro = "deepseek-v4-pro";
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
            /// Gemini 3.6 Flash – Main workhorse for coding, knowledge, multimodal. $1.50/$7.50 per 1M tokens (in/out). Moderate cost.
            /// </summary>
            public const string Gemini_36_Flash = "gemini-3.6-flash";

            /// <summary>
            /// Gemini 3.5 Flash – Smarter Flash for agentic and coding tasks. Lower cost, good performance.
            /// </summary>
            public const string Gemini_35_Flash = "gemini-3.5-flash";

            /// <summary>
            /// Gemini 3.5 Flash-Lite – Fastest, cheapest in 3.5 series. 350 tokens/sec output. Very low cost.
            /// </summary>
            public const string Gemini_35_Flash_Lite = "gemini-3.5-flash-lite";

            /// <summary>
            /// Gemini 3.5 Flash Cyber – Specialized for cybersecurity (vulnerability detection/fix). Use with CodeMender.
            /// </summary>
            public const string Gemini_35_Flash_Cyber = "gemini-3.5-flash-cyber";

        }
    }
}
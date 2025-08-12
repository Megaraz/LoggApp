using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppLogic.Models.DTOs;
using AppLogic.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppLogic.Repositories
{
    /// <summary>
    /// Client for interacting with OpenAI's API to generate summaries based on prompts.
    /// </summary>
    public sealed class OpenAiResponseClient
    {
        private readonly JsonSerializerOptions _opts = new(JsonSerializerDefaults.Web);
        private static readonly HttpClient _http = new HttpClient();
        private static bool _configured;
        private static string? _apiKey;
        public OpenAiResponseClient()
        {
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            if (_apiKey is not null && !_configured)
            {
                _http.BaseAddress = new Uri("https://api.openai.com/v1/");
                var authSuccess = _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _apiKey);
                _configured = true;


            }
        }


        public async Task<string?> GenerateSummaryAsync(string prompt, string model = "gpt-3.5-turbo")
        {
            return await GenerateSummaryInternal(prompt.Trim(), model);
        }

        public async Task<string?> GenerateSummaryAsync(IPromptRenderable data, string model = "gpt-3.5-turbo")
        {
            return await GenerateSummaryInternal(data.ToPrompt().Trim(), model);
        }

        private async Task<string?> GenerateSummaryInternal(string prompt, string model)
        {
            if (!_configured)
            {
                return null;
            }

            var payload = new { model, input = prompt, temperature = 0.7 };

            try
            {
                using var req = new HttpRequestMessage(HttpMethod.Post, "responses")
                {
                    Content = new StringContent(JsonSerializer.Serialize(payload, _opts),
                                                Encoding.UTF8, "application/json")
                };

                req.Headers.Add("OpenAI-Beta", "responses=v1");

                var resp = await _http.SendAsync(req);
                if (!resp.IsSuccessStatusCode)
                {
                    // If unauthorized or any error, return null (no exception thrown)
                    return null;
                }

                var rawJson = await resp.Content.ReadAsStringAsync();
                var body = JsonSerializer.Deserialize<OpenAiResponse>(rawJson, _opts);
                if (body == null)
                {
                    return null;
                }

                while (body.Status == "in_progress")
                {
                    await Task.Delay(800);
                    resp = await _http.GetAsync($"responses/{body.Id}");
                    if (!resp.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    rawJson = await resp.Content.ReadAsStringAsync();
                    body = JsonSerializer.Deserialize<OpenAiResponse>(rawJson, _opts)!;
                }

                var text = body.Output?
                              .SelectMany(o => o.Content)
                              .FirstOrDefault(c => c.Type == "output_text")?
                              .Text;

                if (string.IsNullOrWhiteSpace(text))
                    return null;

                return text.ToString().Trim();
            }
            catch
            {
                // On any exception (e.g., network, deserialization), return null
                return null;
            }
        }


    }
}

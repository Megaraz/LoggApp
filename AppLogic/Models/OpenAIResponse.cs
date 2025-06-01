using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppLogic.Models
{

    /// <summary>
    /// Response from OpenAI API, specifically for the chat completion endpoint.
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="Text"></param>
    public record ContentPart(
        [property: JsonPropertyName("type")] string Type,
        [property: JsonPropertyName("text")] string? Text);

    public record OutputItem(
        [property: JsonPropertyName("role")] string Role,
        [property: JsonPropertyName("type")] string ItemType,
        [property: JsonPropertyName("content")] IReadOnlyList<ContentPart> Content);
    public record OpenAiResponse(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("status")] string Status,
        [property: JsonPropertyName("output_text")] string? OutputText,
        [property: JsonPropertyName("output")] IReadOnlyList<OutputItem>? Output,
        [property: JsonPropertyName("usage")] ResponseUsage Usage);

    public record ResponseUsage(
        [property: JsonPropertyName("input_tokens")] int InputTokens,
        [property: JsonPropertyName("output_tokens")] int OutputTokens,
        [property: JsonPropertyName("total_tokens")] int TotalTokens);

    public record ResponseMessage(
        [property: JsonPropertyName("role")] string Role,
        [property: JsonPropertyName("content")] string Content
    );

    public record ResponseChoice(
        [property: JsonPropertyName("index")] int Index,
        [property: JsonPropertyName("message")] ResponseMessage Message,
        [property: JsonPropertyName("finish_reason")] string FinishReason
    );


}

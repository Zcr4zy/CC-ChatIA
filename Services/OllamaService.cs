using System.Text.Json;
using CC_ChatIA.Interfaces;
using CC_ChatIA.Models;

namespace CC_ChatIA.Services
{
    public class OllamaService : IOllamaService
    {
        private readonly HttpClient _http;

        public OllamaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> GenerateTitleAsync(string text)
        {
            var request = new
            {
                model = "llama3.2:3b",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = @"Você é um assistente especializado em criar títulos concisos para conversas.
                                    Regras:
                                        - Máximo de 5 palavras
                                        - Seja direto e descritivo
                                        - Capture o tema principal
                                        - Use substantivos, evite verbos quando possível
                                        - NÃO use pontuação no final
                                        - NÃO use aspas
                                        - Responda APENAS com o título, nada mais"
                    },
                    new
                    {
                        role = "user",
                        content = $"Crie um título curto para esta mensagem: {text}"
                    }
                },
                stream = false,
                temperature = 0.3
            };

            var response = await _http.PostAsJsonAsync("http://localhost:11434/api/chat", request);
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            
            var title = json.GetProperty("message").GetProperty("content").GetString()?.Trim();
        
            title = title?.Trim('"', '.', '!', '?', ':');
            
            if (title?.Length > 50)
                title = title.Substring(0, 47) + "...";
            
            return title ?? "Nova conversa";
        }

        public async Task<string> SendMessageAsync(List<ChatMessage> messages)
        {
            var messagesWithSystemConfiguration = new List<ChatMessage>
            {
                new ChatMessage
                {
                    Role = "system",
                    Content = "Responda sempre em português do Brasil, de forma clara e objetiva."
                }
            };

            messagesWithSystemConfiguration.AddRange(messages);

            var request = new
            {
                model = "llama3.2:3b",
                messages = messagesWithSystemConfiguration.Select(m => new
                {
                    role = m.Role,
                    content = m.Content
                }),
                stream = false
            };

            var response = await _http.PostAsJsonAsync("http://localhost:11434/api/chat", request);
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            return json.GetProperty("message").GetProperty("content").GetString();
        }
    }
}
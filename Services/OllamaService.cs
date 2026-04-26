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

        public async Task<string> SendMessageAsync(List<ChatMessage> mensagens)
        {
            var mensagensComSistema = new List<ChatMessage>
            {
                new ChatMessage
                {
                    Role = "system",
                    Content = "Responda sempre em português do Brasil, de forma clara e objetiva."
                }
            };

            mensagensComSistema.AddRange(mensagens);

            var request = new
            {
                model = "llama3.2:3b",
                messages = mensagensComSistema.Select(m => new
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

        public async Task<string> GenerateTitleAsync(string texto)
        {
            var prompt = $"Resuma em até 5 palavras: {texto}";

            return await SendMessageAsync(new List<ChatMessage>
            {
                new() { Role = "user", Content = prompt }
            });
        }
    }
}
using CC_ChatIA.Models;

namespace CC_ChatIA.Interfaces
{
    public interface IOllamaService
    {
        Task<string> SendMessageAsync(List<ChatMessage> mensagens);
        Task<string> GenerateTitleAsync(string texto);
    }
}
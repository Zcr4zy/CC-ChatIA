using CC_ChatIA.Models;

namespace CC_ChatIA.Interfaces
{
    public interface IOllamaService
    {
        Task<string> GenerateTitleAsync(string text);
        Task<string> SendMessageAsync(List<ChatMessage> messages);
    }
}
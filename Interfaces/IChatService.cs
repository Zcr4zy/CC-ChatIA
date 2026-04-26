using CC_ChatIA.Models;

namespace CC_ChatIA.Interfaces
{
    public interface IChatService
    {
        Task<List<Chat>> ListChatsAsync();
        Task<List<ChatMessage>> GetMessagesAsync(Guid chatId);
        Task<Guid> CreateChatAsync(string firstMessage);
        Task<string> SendMessageAsync(Guid chatId, string message);
        Task DeleteChatAsync(Guid chatId);
    }
}
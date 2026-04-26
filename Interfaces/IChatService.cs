using CC_ChatIA.Models;

namespace CC_ChatIA.Interfaces
{
    public interface IChatService
    {
        Task<Guid> CreateChatAsync(string firstMessage);
        Task<List<Chat>> ListChatsAsync();
        Task<List<ChatMessage>> GetMessagesAsync(Guid chatId);
        Task<string> SendMessageAsync(Guid chatId, string mensagem);
        Task DeletarChatAsync(Guid chatId);
    }
}
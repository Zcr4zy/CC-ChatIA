using CC_ChatIA.Data;
using CC_ChatIA.Interfaces;
using CC_ChatIA.Models;
using Microsoft.EntityFrameworkCore;

namespace CC_ChatIA.Services
{
    public class ChatService : IChatService
    {
        private readonly IOllamaService _ollama;
        private readonly IDbContextFactory<AppDbContext> _factory;

        public ChatService(IOllamaService ollama, IDbContextFactory<AppDbContext> context)
        {
            _ollama = ollama;
            _factory = context;
        }

        public async Task<List<Chat>> ListChatsAsync()
        {
            using var context = _factory.CreateDbContext();

            return await context.Chats
                .OrderByDescending(c => c.UpdatedAt)
                .ToListAsync();
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(Guid chatId)
        {
            using var context = _factory.CreateDbContext();

            return await context.ChatMessages
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<Guid> CreateChatAsync(string firstMessage)
        {
            var chatId = Guid.NewGuid();
            var now = DateTime.UtcNow;

            var IAResponse = await _ollama.SendMessageAsync(new List<ChatMessage>
            {
                new() { Role = "user", Content = firstMessage }
            });

            var chatTitle = await _ollama.GenerateTitleAsync(firstMessage);

            var chat = new Chat
            {
                Id = chatId,
                Name = chatTitle,
                CreatedAt = now,
                UpdatedAt = now,
                ChatMessages = new List<ChatMessage>
                {
                    new ChatMessage
                    {
                        Id = Guid.NewGuid(),
                        ChatId = chatId,
                        Role = "user",
                        Content = firstMessage,
                        CreatedAt = now
                    },
                    new ChatMessage
                    {
                        Id = Guid.NewGuid(),
                        ChatId = chatId,
                        Role = "assistant",
                        Content = IAResponse,
                        CreatedAt = DateTime.UtcNow
                    }
                }
            };

            using var context = _factory.CreateDbContext();

            context.Chats.Add(chat);
            await context.SaveChangesAsync();

            return chatId;
        }

        public async Task<string> SendMessageAsync(Guid chatId, string message)
        {
            using var context = _factory.CreateDbContext();

            var chatExists = await context.Chats.AnyAsync(c => c.Id == chatId);

            if (!chatExists)
                throw new InvalidOperationException("Chat não encontrado!");
            
            var now = DateTime.UtcNow;

            var historic = await context.ChatMessages
                .AsNoTracking()
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.CreatedAt)
                .Select(m => new ChatMessage
                {
                    Role = m.Role,
                    Content = m.Content
                })
                .ToListAsync();

            historic.Add(new ChatMessage
            {
                Role = "user",
                Content = message
            });

            var response = await _ollama.SendMessageAsync(historic);

            context.ChatMessages.AddRange(
                new ChatMessage
                {
                    Id = Guid.NewGuid(),
                    ChatId = chatId,
                    Role = "user",
                    Content = message,
                    CreatedAt = now
                },
                new ChatMessage
                {
                    Id = Guid.NewGuid(),
                    ChatId = chatId,
                    Role = "assistant",
                    Content = response,
                    CreatedAt = DateTime.UtcNow
                });

            var affectedLines = await context.Chats
                .Where(c => c.Id == chatId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.UpdatedAt, DateTime.UtcNow));

            if (affectedLines == 0)
                throw new InvalidOperationException("Chat não encontrado para atualização.");
            
            await context.SaveChangesAsync();

            return response;
        }

        public async Task DeleteChatAsync(Guid chatId)
        {
            using var context = _factory.CreateDbContext();
            var chat = await context.Chats.FindAsync(chatId);

            if (chat == null)
                return;
        
            context.Chats.Remove(chat);
            await context.SaveChangesAsync();
        }
    }
}

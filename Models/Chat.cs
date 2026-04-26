namespace CC_ChatIA.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt {get; set; }
        public List<ChatMessage> ChatMessages {get; set;}
    }
}
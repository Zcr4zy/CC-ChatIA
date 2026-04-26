namespace CC_ChatIA.Models
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public Guid ChatId {get; set; }
        public Chat Chat {get; set; }
        public string Content {get; set; }        
        public string Role {get; set; }
        public DateTime CreatedAt {get; set; }
    }
}
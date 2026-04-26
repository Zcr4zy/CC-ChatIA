namespace CC_ChatIA.Models
{
    public class ChatStateService
    {
        public Guid IdSelectedChat { get; private set; }

        public event Action? OnChange;

        public void SelectChat(Guid id)
        {
            IdSelectedChat = id;
            OnChange?.Invoke();
        }
    }
}

namespace CC_ChatIA.Models
{
    public class ChatStateService
    {
        public Guid ChatSelecionadoId { get; private set; }

        public event Action? OnChange;

        public void SelecionarChat(Guid id)
        {
            ChatSelecionadoId = id;
            OnChange?.Invoke();
        }
    }
}

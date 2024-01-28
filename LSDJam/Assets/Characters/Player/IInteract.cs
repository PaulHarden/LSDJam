namespace Characters.Player
{
    public interface IInteract
    {
        float MaxRange { get; }
        void OnStartHover();
        void OnInteract();
        void OnEndHover();
    }
}

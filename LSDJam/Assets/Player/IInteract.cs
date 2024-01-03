namespace Player
{
    public interface IInteract
    {
        float MaxRange { get; }
        void OnStartHover();
        void OnInteract();
        void OnEndHover();
    }
}

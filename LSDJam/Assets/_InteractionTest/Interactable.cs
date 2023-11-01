public interface Interactable
{
    float MaxRange { get; }

    void OnStartHover();
    void OnInteract();
    void OnEndHover();
}

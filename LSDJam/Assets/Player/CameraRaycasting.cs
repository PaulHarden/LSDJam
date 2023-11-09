using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRaycasting : MonoBehaviour
{
    [SerializeField] private float range;
    private Interactable currentTarget;
    private Camera mainCamera;
    private StarterAssetsInputs _input;

    private void Awake()
    {
        mainCamera = Camera.main;
        _input = GetComponentInParent<StarterAssetsInputs>();
    }

    private void Update()
    {
        RaycastForInteractable();
        
        if (_input.interact)
            if (currentTarget != null)
                currentTarget.OnInteract();
    }

    private void RaycastForInteractable()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit, range))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                if (hit.distance <= interactable.MaxRange)
                {
                    if (interactable == currentTarget)
                        return;
                    if(currentTarget != null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = interactable;
                        currentTarget.OnStartHover();
                        return;
                    }
                    currentTarget = interactable;
                    currentTarget.OnStartHover();
                }
                else
                {
                    if (currentTarget != null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = null;
                    }
                }
            }
            else
            {
                if (currentTarget == null) 
                    return;
                currentTarget.OnEndHover();
                currentTarget = null;
            }
        }
        else
        {
            if (currentTarget == null) 
                return;
            currentTarget.OnEndHover();
            currentTarget = null;
        }
    }
}

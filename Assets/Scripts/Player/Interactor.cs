using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private AnimationController _animationController;

    private IInteractable _currentTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            _currentTarget = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && interactable == _currentTarget)
        {
            _currentTarget = null;
        }
    }

    public void Interact()
    {
        _currentTarget?.OnInteract();
        _animationController?.PlayInteraction();
    }
}

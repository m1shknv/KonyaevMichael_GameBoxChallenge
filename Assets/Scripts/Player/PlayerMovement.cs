using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private IPlayerInput _playerInput;
    private Mover _mover;
    private Climber _climber;
    private Interactor _interactor;

    private void Awake()
    {
        TryGetComponent(out _playerInput);
        if (_playerInput == null)
            Debug.LogError("IPlayerInput component not found!");

        TryGetComponent(out _mover);
        TryGetComponent(out _climber);
        TryGetComponent(out _interactor);
    }

    private void OnEnable()
    {
        if (_playerInput != null)
        {
            _playerInput.OnMoveInput += HandleMove;
            _playerInput.OnActionPressed += HandleAction;
            
        }
    }

    private void OnDisable()
    {
        if (_playerInput != null)
        {
            _playerInput.OnMoveInput -= HandleMove;
            _playerInput.OnActionPressed -= HandleAction;
        }
    }

    private void HandleMove(Vector2 move)
    {
        bool isMoving = Mathf.Abs(move.x) > 0.1f || (_climber.OnStairs && Mathf.Abs(move.y) > 0.1f);

        if (isMoving)
        {
            AudioManager.Instance?.PlayStepsMusic();
        }
        else
        {
            AudioManager.Instance?.StopStepsMusic();
        }

        _mover.Move(move.x);
        _climber.TryClimb(move.y);
    }

    private void HandleAction()
    {
        _interactor.Interact();
    }
}

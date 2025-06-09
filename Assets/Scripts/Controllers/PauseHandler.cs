using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PauseHandler : MonoBehaviour
{
    private PlayerInput _input;

    private void Awake()
    {
        TryGetComponent(out _input);
        if (_input)
        {
            _input.OnPausePressed += HandlePausePressed;
        }
    }

    private void OnDestroy()
    {
        if (_input)
        {
            _input.OnPausePressed -= HandlePausePressed;
        }
    }

    private void HandlePausePressed()
    {
        SceneController.Instance?.TogglePause();
    }
}

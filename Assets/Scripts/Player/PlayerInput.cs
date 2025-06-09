using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    public event Action<Vector2> OnMoveInput;
    public event Action OnActionPressed;
    public event Action OnPausePressed;
    public event Action OnInventoryTogglePressed;

    public bool IsInputEnabled { get; set; } = true;

    private void Update()
    {
        if (!IsInputEnabled && Time.timeScale != 0f)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            OnPausePressed?.Invoke();

        if (IsInputEnabled)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            OnMoveInput?.Invoke(moveInput);

            if (Input.GetKeyDown(KeyCode.E))
                OnActionPressed?.Invoke();

            if (Input.GetKeyDown(KeyCode.I))
                OnInventoryTogglePressed?.Invoke();
        }
    }
}

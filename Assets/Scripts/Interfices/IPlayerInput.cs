using System;
using UnityEngine;

public interface IPlayerInput
{
    event Action<Vector2> OnMoveInput;
    event Action OnActionPressed;
    event Action OnPausePressed;
    event Action OnInventoryTogglePressed;
}

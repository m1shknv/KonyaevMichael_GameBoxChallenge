using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
public class Climber : MonoBehaviour
{
    [SerializeField] private float _climbSpeed = 3f;

    private Rigidbody2D _rb;
    private AnimationController _animationController;
    private bool _onStairs;
    public bool OnStairs => _onStairs;

    private void Awake()
    {
        TryGetComponent(out _rb);
        TryGetComponent(out _animationController);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Stairs _))
        {
            _onStairs = true;
            if (_animationController != null)
                _animationController.OnStairs = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Stairs _))
        {
            _onStairs = false;
            _rb.gravityScale = 1f;

            if (_animationController != null)
            {
                _animationController.OnStairs = false;
                _animationController.VerticalInput = 0f; 
            }
        }
    }

    public void TryClimb(float yInput)
    {
        if (!_onStairs)
            return;

        _rb.gravityScale = 0f;
        _rb.velocity = new Vector2(_rb.velocity.x, yInput * _climbSpeed);

        if (_animationController != null)
            _animationController.VerticalInput = yInput;
    }
}

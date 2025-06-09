using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        TryGetComponent(out _rb);
    }

    public void Move(float xInput)
    {
        Vector2 velocity = _rb.velocity;
        velocity.x = xInput * _speed;
        _rb.velocity = velocity;
    }
}

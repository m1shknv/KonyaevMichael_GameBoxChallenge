using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Animator _animator;
    private Rigidbody2D _rb;

    public bool OnStairs { get; set; } = false;
    public float VerticalInput { get; set; } = 0f;

    private void Awake()
    {
        TryGetComponent(out _animator);
        TryGetComponent(out _rb);
    }

    private void FixedUpdate()
    {
        UpdateRunAnimation();
        UpdateClimbAnimation();
        FlipSprite();
    }

    private void UpdateRunAnimation()
    {
        float horizontalSpeed = Mathf.Abs(_rb.velocity.x);
        _animator.SetBool("isRun", horizontalSpeed > 0.1f && !OnStairs);
    }

    private void UpdateClimbAnimation()
    {
        _animator.SetBool("isClimb", OnStairs && Mathf.Abs(VerticalInput) > 0.1f);
    }

    private void FlipSprite()
    {
        if (_rb.velocity.x > 0.1f)
            _spriteRenderer.flipX = false;
        else if (_rb.velocity.x < -0.1f)
            _spriteRenderer.flipX = true;
    }

    public void PlayInteraction()
    {
        _animator.SetTrigger("doAction");
    }
}

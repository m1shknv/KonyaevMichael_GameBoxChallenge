using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Player>(out _))
            SceneController.Instance?.ExitHouse();
    }
}

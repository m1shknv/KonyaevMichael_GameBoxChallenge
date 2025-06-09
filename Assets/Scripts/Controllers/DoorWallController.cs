using UnityEngine;

public class DoorWallController : MonoBehaviour
{
    [SerializeField] private Collider2D wallCollider; 
    [SerializeField] private GameObject doorPlaceholder; 
    private bool doorOpened = false;

    public void OpenDoor()
    {
        if (doorOpened) return;

        if (wallCollider != null)
        {
            wallCollider.isTrigger = true; 
        }

        // Заменяем placeholder на дверь
        if (doorPlaceholder != null)
        {
            var replaceController = doorPlaceholder.GetComponent<ReplaceController>();
            if (replaceController != null)
            {
                replaceController.ReplacePrefab();
            }
        }

        doorOpened = true;
    }
}

using UnityEngine;

public class ReplaceController : MonoBehaviour
{
    [SerializeField] private GameObject replacedPrefab;

    public void ReplacePrefab()
    {
        {
            Instantiate(replacedPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}

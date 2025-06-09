using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AudioButton : MonoBehaviour
{
    private void Awake() =>
        GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.PlayButtonClick());
}
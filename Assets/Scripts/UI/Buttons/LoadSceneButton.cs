using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance?.PlayButtonClick();
            SceneController.Instance?.LoadSceneByName(_sceneName);
        });
    }
}

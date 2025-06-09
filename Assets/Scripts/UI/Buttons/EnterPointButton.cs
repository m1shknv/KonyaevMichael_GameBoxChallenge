using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EnterPointButton : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public UnityEvent<bool> OnInteractableChanged { get; } = new UnityEvent<bool>();

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.interactable = false;

        _button.onClick.AddListener(() =>
        {
            AudioManager.Instance?.PlayButtonClick();
            SceneController.Instance?.LoadSceneByName(_sceneName);
        });

        OnInteractableChanged.AddListener(SetInteractable);
    }

    public void SetInteractable(bool state)
    {
        _button.interactable = state;
    }
}
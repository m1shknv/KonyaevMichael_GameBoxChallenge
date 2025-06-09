using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class WaitButton : MonoBehaviour
{
    public static event Action OnWaitButtonClicked;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        if (AudioManager.Instance)
            AudioManager.Instance.PlayButtonClick();

        OnWaitButtonClicked?.Invoke();
    }
}
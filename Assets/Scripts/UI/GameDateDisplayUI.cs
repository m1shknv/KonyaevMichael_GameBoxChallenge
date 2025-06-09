using UnityEngine;
using UnityEngine.UI;

public class GameDateDisplayUI : MonoBehaviour
{
    [SerializeField] private Text dateText;

    private void Start()
    {
        if (GameDateSystem.Instance)
        {
            GameDateSystem.Instance.RegisterUIDisplay(dateText);
        }
    }
}

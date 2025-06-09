using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class OpenNotebookButton : MonoBehaviour
{
    [SerializeField] private string _notebookSceneName = "NotebookScene";

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnNotebookButtonClicked);
    }

    private void OnNotebookButtonClicked()
    {
        AudioManager.Instance?.PlayButtonClick();

        var currentEntries = NotebookDataStore.Instance.CollectedEntries;

        foreach (var entry in Inventory.Instance.Entries)
        {
            currentEntries.Add(entry);
        }

        NotebookDataStore.Instance.SetCollectedEntries(currentEntries);

        Inventory.Instance.ClearEntries();

        SceneManager.LoadScene(_notebookSceneName);
    }

}

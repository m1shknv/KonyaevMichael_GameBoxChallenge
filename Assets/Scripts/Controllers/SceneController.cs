using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    private IPopup pausePopup;
    private IPopup inventoryPopup;
    private IPopup exitPopup;
    private IPopup instructionPopup; 

    private IPlayerInput _playerInput;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        AudioManager.Instance?.PlayBackgroundMusic();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (_playerInput != null)
        {
            _playerInput.OnPausePressed -= TogglePause;
            _playerInput.OnInventoryTogglePressed -= HandleInventoryPressed;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPopupsInScene();
        FindPlayerInput();

        if (instructionPopup != null && !instructionPopup.IsVisible)
            instructionPopup.Show();
    }

    private void FindPopupsInScene()
    {
        var allPopups = FindObjectsOfType<Popup>();

        pausePopup = allPopups.FirstOrDefault(p => p.gameObject.name.Contains("PauseUI")) as IPopup;
        inventoryPopup = allPopups.FirstOrDefault(p => p.gameObject.name.Contains("InventoryUI")) as IPopup;
        exitPopup = allPopups.FirstOrDefault(p => p.gameObject.name.Contains("ExitUI")) as IPopup;
        instructionPopup = allPopups.FirstOrDefault(p => p.gameObject.name.Contains("InstructionUI")) as IPopup; 
    }

    private void FindPlayerInput()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
        if (_playerInput == null) return;

        _playerInput.OnPausePressed += TogglePause;
        _playerInput.OnInventoryTogglePressed += HandleInventoryPressed;
    }

    public void TogglePause()
    {
        if (pausePopup == null) return;

        if (inventoryPopup != null && inventoryPopup.IsVisible)
            return;

        if (pausePopup.IsVisible)
        {
            Time.timeScale = 1f;
            pausePopup.Hide();
        }
        else
        {
            Time.timeScale = 0f;
            pausePopup.Show();
        }
    }

    private void HandleInventoryPressed()
    {
        if (inventoryPopup == null) return;

        if (pausePopup != null && pausePopup.IsVisible)
            return;

        if (inventoryPopup.IsVisible)
            inventoryPopup.Hide();
        else
            inventoryPopup.Show();
    }

    public void LoadSceneByName(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("Имя сцены пустое.");
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ExitHouse()
    {
        GameDateSystem.Instance?.SkipDay();
        Time.timeScale = 0f;
        exitPopup?.Show();
    }
}
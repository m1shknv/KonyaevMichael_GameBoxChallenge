using UnityEngine;
using UnityEngine.UI;

public class NotebookEntryUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Text personText;
    [SerializeField] private Text placeText;
    [SerializeField] private Text eventText;
    [SerializeField] private Text dateText;
    [SerializeField] private Button mergeButton;

    private NotebookEntry _entry;
    private NotebookManager _manager;

    private Color _defaultTextColor;
    private Color _activeTextColor = Color.yellow;
    private Color _currentChainColor;
    private bool _isInChain = false;

    private void Awake()
    {
        _defaultTextColor = personText.color;
        mergeButton.onClick.AddListener(OnMergeClicked);
    }

    public void Setup(NotebookEntry entry, NotebookManager manager)
    {
        _entry = entry;
        _manager = manager;

        personText.text = string.IsNullOrEmpty(entry.person) ? "-" : entry.person;
        placeText.text = string.IsNullOrEmpty(entry.place) ? "-" : entry.place;
        eventText.text = string.IsNullOrEmpty(entry.eventDescription) ? "-" : entry.eventDescription;
        dateText.text = entry.date.ToString("dd.MM.yyyy");
    }

    public void SetActive(bool active)
    {
        if (!_isInChain)
        {
            SetTextColor(active ? _activeTextColor : _defaultTextColor);
        }
    }

    public void SetChainColor(Color chainColor)
    {
        _currentChainColor = chainColor;
        if (_isInChain)
        {
            SetTextColor(_currentChainColor);
        }
    }

    public void SetInChain(bool inChain)
    {
        _isInChain = inChain;
        SetTextColor(inChain ? _currentChainColor : _defaultTextColor);
    }

    private void SetTextColor(Color color)
    {
        personText.color = color;
        placeText.color = color;
        eventText.color = color;
        dateText.color = color;
    }

    public void OnMergeClicked()
    {
        _manager.HandleEntrySelection(this);
    }

    public NotebookEntry GetEntry() => _entry;

    public static Color GenerateColorFromName(string name)
    {
        if (string.IsNullOrEmpty(name)) return Color.cyan;

        int hash = name.GetHashCode();
        float r = Mathf.Abs((hash & 0xFF) / 255f);
        float g = Mathf.Abs(((hash >> 8) & 0xFF) / 255f);
        float b = Mathf.Abs(((hash >> 16) & 0xFF) / 255f);

        return new Color(r, g, b);
    }
}
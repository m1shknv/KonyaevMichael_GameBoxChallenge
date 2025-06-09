using UnityEngine;
using UnityEngine.UI;
using System;

public class GameDateSystem : MonoBehaviour
{
    [Header("Настройки даты")]
    [Tooltip("Формат: ДД.ММ.ГГГГ")]
    public string initialDate = "01.01.2023";

    private Text _dateDisplay;

    private DateTime _currentDate;

    public event Action<DateTime> OnDateChanged;

    private static GameDateSystem _instance;

    public static GameDateSystem Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ParseInitialDate();
        UpdateDateDisplay();
    }

    private void OnEnable()
    {
        WaitButton.OnWaitButtonClicked += SkipDay;
    }

    private void OnDisable()
    {
        WaitButton.OnWaitButtonClicked -= SkipDay;
    }

    private void ParseInitialDate()
    {
        try
        {
            string[] parts = initialDate.Split('.');
            _currentDate = new DateTime(
                int.Parse(parts[2]), // Год
                int.Parse(parts[1]), // Месяц
                int.Parse(parts[0]) // День
            );
        }
        catch
        {
            Debug.LogError("Ошибка формата даты! Установлена текущая дата.");
            _currentDate = DateTime.Today;
        }
    }

    public void SkipDay()
    {
        _currentDate = _currentDate.AddDays(1);
        UpdateDateDisplay();
        OnDateChanged?.Invoke(_currentDate);
    }

    public void RegisterUIDisplay(Text uiText)
    {
        _dateDisplay = uiText;
        UpdateDateDisplay();
    }

    private void UpdateDateDisplay()
    {
        if (_dateDisplay != null)
        {
            _dateDisplay.text = _currentDate.ToString("dd.MM.yyyy");
        }
    }

    public DateTime GetCurrentDate() => _currentDate;
}
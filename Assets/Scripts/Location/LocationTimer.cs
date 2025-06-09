using UnityEngine;

public class LocationTimer : MonoBehaviour
{
    [SerializeField] private float durationInSeconds = 60f;
    private float timeRemaining;
    private bool isRunning;

    public bool IsRunning => isRunning;

    private void Awake()
    {
        timeRemaining = durationInSeconds;
        isRunning = false;
    }

    private void Update()
    {
        if (!isRunning || timeRemaining <= 0f) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isRunning = false;
            OnTimerEnd();
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    private void OnTimerEnd()
    {
        Debug.Log("Время вышло.");
        AudioManager.Instance?.PlayRing();
        SceneController.Instance.ExitHouse();
    }

    public float GetRemainingTime() => timeRemaining;
}

using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLeft = 60f;
    public Text timerText;
    private bool isTimerRunning = false;

    void Start() {
        StartTimer(timeLeft);
    }

    void Update()
    {
        if (isTimerRunning && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                TimeUp();
                isTimerRunning = false;
            }
            UpdateTimerText();
        }
    }

    public void StartTimer(float startTime)
    {
        timeLeft = startTime;
        isTimerRunning = true;
    }

    public void AddTime(float extraTime)
    {
        timeLeft += extraTime;
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);
    }

    private void TimeUp()
    {
        Debug.Log("Time is up!");
        timerText.text = "Time is up!";
    }
}
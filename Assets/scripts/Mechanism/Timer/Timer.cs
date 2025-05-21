using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float elapsedTime = 0f;
    public bool isRunning = false;
    public TextMeshProUGUI timerText;
    [SerializeField] string timerLevel;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay(elapsedTime);
            TimerSave();
        }
    }

    void TimerSave()
    {
        if (timerLevel == "Level1")
        {
            SaveManager.instance.timerString1 = timerText.text;
            SaveManager.instance.Save();
        }
        else if (timerLevel == "Level2")
        {
            SaveManager.instance.timerString2 = timerText.text;
            SaveManager.instance.Save();
        }
        else if (timerLevel == "Level3")
        {
            SaveManager.instance.timerString3 = timerText.text;
            SaveManager.instance.Save();
        }
        else if (timerLevel == "Level4")
        {
            SaveManager.instance.timerString4 = timerText.text;
            SaveManager.instance.Save();
        }
        else if (timerLevel == "Level5")
        {
            SaveManager.instance.timerString5 = timerText.text;
            SaveManager.instance.Save();
        }
    }

        void UpdateTimerDisplay(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

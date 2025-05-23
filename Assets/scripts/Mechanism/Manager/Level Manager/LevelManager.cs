using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Score System Settings")]
    public TextMeshProUGUI scoreText;
    private int score;

    [Header("Paused Settings")]
    [SerializeField] GameObject pausedPanel;
    [SerializeField] Button pauseButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button quitButton;

    [Header("Script Reference")]
    [SerializeField] PlayerStatus playerStatus;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText() // Buat menampilkan Score ke UI
    {
        score = playerStatus.totalQuiz; // Mengambil value dari totalQuiz yang berada di PlayerStatus
        if (scoreText != null)
        {
            //scoreText.text = "Total Quiz terjawab : " + score.ToString() + "/5";
            scoreText.text = score.ToString() + "/5";
        }
        else
        {
            Debug.LogError("ScoreText belum diatur di Inspector!");
        }
    }

    public void Paused()
    {
        pausedPanel.SetActive(true);
        pauseButton.interactable = false;
        LeanTween.scale(pausedPanel, new Vector3(1f, 1f, 1f), 1f).setEase(LeanTweenType.easeOutSine).setOnComplete(() =>
        {
            
        });
        resumeButton.interactable = true;
        quitButton.interactable = true;
    }

    public void Resume()
    {
        quitButton.interactable = false;
        LeanTween.scale(pausedPanel, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutSine).setOnComplete(() =>
        {
            pausedPanel.SetActive(false);
            pauseButton.interactable = true;
            
        });
    }
}

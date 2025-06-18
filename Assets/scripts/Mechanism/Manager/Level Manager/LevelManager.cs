using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [Header("Level Settings")]
    [SerializeField] string nextLevelName;
    [SerializeField] GameObject finishCanvas;

    [Header("Script Reference")]
    [SerializeField] PlayerStatus playerStatus;

    [Header("Audio Reference")]
    [SerializeField] AudioClip clickSFX;

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

        AudioManager.instance.PlaySound(clickSFX);
    }

    public void Resume()
    {
        quitButton.interactable = false;
        LeanTween.scale(pausedPanel, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutSine).setOnComplete(() =>
        {
            pausedPanel.SetActive(false);
            pauseButton.interactable = true;            
        });
        AudioManager.instance.PlaySound(clickSFX);
    }

    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu Level"); // Pindah ke Home
        AudioManager.instance.PlaySound(clickSFX);
    }

    public void NextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName); // Pindah ke level berikutnya
        AudioManager.instance.PlaySound(clickSFX);
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("home");
    }
}

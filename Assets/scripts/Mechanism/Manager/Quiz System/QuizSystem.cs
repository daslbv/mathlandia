using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizSystem : MonoBehaviour
{
    [Header("Quiz System")]
    public GameObject quizPanel;
    public GameObject quizTrigger;
    public GameObject rightAnswerImage;
    public GameObject wrongAnswerImage;
    public float textDuration = 1;
    public LeanTweenType easingType;

    [Header("Script Reference")]
    public PlayerController playerController;

    [Header("Audio Clip")]
    [SerializeField] AudioClip rightAnswerClip;
    [SerializeField] AudioClip wrongAnswerClip;

    private bool isButtonPressed = false; // Penambahan variabel untuk mengecek apakah tombol sudah ditekan

    void Start()
    {
        // Initialization code
    }

    public void TrueAnswer()
    {
        if (isButtonPressed) return; // Cek apakah tombol sudah ditekan sebelumnya
        isButtonPressed = true; // Set tombol menjadi sudah ditekan

        StartCoroutine(DeactivateQuizPanelWithDelay());
        Time.timeScale = 1;
        GameObject.Destroy(quizTrigger);
        LeanTween.scale(rightAnswerImage, new Vector3(0.69942f, 0.69942f, 0.69942f), 1.3f).setEase(easingType);

        playerController.enabled = true;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerStatus playerStatus = player.GetComponent<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.totalQuiz += 1;
                AudioManager.instance.PlaySound(rightAnswerClip);
            }
        }
    }

    public void WrongAnswer()
    {
        if (isButtonPressed) return; // Cek apakah tombol sudah ditekan sebelumnya
        isButtonPressed = true; // Set tombol menjadi sudah ditekan

        StartCoroutine(DeactivateQuizPanelWithDelay());
        Time.timeScale = 1;
        LeanTween.scale(wrongAnswerImage, new Vector3(0.69942f, 0.69942f, 0.69942f), 1.3f).setEase(easingType);

        playerController.enabled = true;

        GameObject.Destroy(quizTrigger);
        AudioManager.instance.PlaySound(wrongAnswerClip);
    }

    private IEnumerator DeactivateQuizPanelWithDelay()
    {
        yield return new WaitForSeconds(1.4f);
        quizPanel.SetActive(false);
    }
}
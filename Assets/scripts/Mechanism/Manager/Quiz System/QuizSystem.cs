using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class QuizData
{
    public string question;
    public string answer;
}

public class QuizSystem : MonoBehaviour
{
    [Header("Quiz System")]
    public GameObject quizPanel;
    public GameObject quizTrigger;
    public GameObject rightAnswerImage;
    public GameObject wrongAnswerImage;
    public float textDuration = 1;
    public LeanTweenType easingType;

    [Header("Inputfield Settings")]
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TextMeshProUGUI answerText;

    [Header("Script Reference")]
    public PlayerController playerController;
    public Timer timer;

    [Header("Audio Clip")]
    [SerializeField] AudioClip rightAnswerClip;
    [SerializeField] AudioClip wrongAnswerClip;

    [Header("Quiz List")]
    public List<QuizData> quizList = new List<QuizData>();

    private QuizData currentQuiz;
    private bool isButtonPressed = false;

    void Start()
    {
        SetRandomQuiz();
        TimerSet();
    }

    void TimerSet()
    {
        timer.isRunning = true;
        Time.timeScale = 1;
    }

    void SetRandomQuiz()
    {
        if (quizList.Count == 0)
        {
            Debug.LogWarning("Quiz list kosong!");
            return;
        }

        int index = Random.Range(0, quizList.Count);
        currentQuiz = quizList[index];

        answerText.text = currentQuiz.question;
        inputField.text = "";
        inputField.ActivateInputField();
    }

    public void CheckAnswer()
    {
        string userInput = inputField.text.Trim();

        if (userInput == currentQuiz.answer)
        {
            TrueAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    void TrueAnswer()
    {
        rightAnswerImage.SetActive(true);
        StartCoroutine(DeactivateQuizPanelWithDelay());
        
        Destroy(quizTrigger);
        LeanTween.scale(rightAnswerImage, new Vector3(0.69942f, 0.69942f, 0.69942f), 1.3f).setEase(easingType);

        playerController.enabled = true;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerStatus playerStatus = player.GetComponent<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.totalQuiz += 1;
                timer.isRunning = false;
                //AudioManager.instance.PlaySound(rightAnswerClip);
            }
        }
    }

    void WrongAnswer()
    {
        Debug.Log("WrongAnswer called");
        wrongAnswerImage.SetActive(true);
        
        LeanTween.scale(wrongAnswerImage, new Vector3(0.69942f, 0.69942f, 0.69942f), 1.3f).setEase(easingType).setOnComplete(() =>
        {
            LeanTween.scale(wrongAnswerImage, new Vector3(0, 0, 0), 1.3f).setEase(easingType).setOnComplete(() =>
            {
                
            });
        });
    }

    private IEnumerator DeactivateQuizPanelWithDelay()
    {
        yield return new WaitForSeconds(1.4f);
        quizPanel.SetActive(false);
    }
}

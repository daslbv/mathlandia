using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("Score System Settings")]
    public TextMeshProUGUI scoreText;
    private int score;
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
            scoreText.text = "Total Quiz terjawab : " + score.ToString() + "/5";
        }
        else
        {
            Debug.LogError("ScoreText belum diatur di Inspector!");
        }
    }
}

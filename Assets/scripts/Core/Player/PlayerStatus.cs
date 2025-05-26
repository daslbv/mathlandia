using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerStatus : MonoBehaviour
{
    [Header("Player Status")]
    public int currHealth;
    private int maxHealth = 1;
    public int totalQuiz;

    [Header("Player Component")]
    private Animator playerAnim;
    public Checkpoint checkpoint;

    [Header("Gameobject Reference")]
    [SerializeField] GameObject finishCanvas;
    [SerializeField] string finishLevel;

    //[Header("Script Reference")]

    // Start is called before the first frame update
    void Awake()
    {
        currHealth = maxHealth;
        playerAnim = GetComponent<Animator>();
    }

    public void Respawn()
    {
        if (currHealth == 0)
        {
            checkpoint.RespawnCheck();
            currHealth = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            if (totalQuiz == 5 && finishLevel == "Level1")
            {
                SaveManager.instance.isLevel1Complete = true; // Set boolean isLevelDone menjadi true
                SaveManager.instance.Save(); // Simpan data ke file
                finishCanvas.SetActive(true); // Tampilkan canvas finish          
            }

            if (totalQuiz == 5 && finishLevel == "Level2")
            {
                SaveManager.instance.isLevel2Complete = true; // Set boolean isLevelDone menjadi true
                SaveManager.instance.Save(); // Simpan data ke file
                finishCanvas.SetActive(true); // Tampilkan canvas finish       
            }

            if (totalQuiz == 5 && finishLevel == "Level3")
            {
                SaveManager.instance.isLevel3Complete = true; // Set boolean isLevelDone menjadi true
                SaveManager.instance.Save(); // Simpan data ke file
                finishCanvas.SetActive(true); // Tampilkan canvas finish     
            }

            if (totalQuiz == 5 && finishLevel == "Level4")
            {
                SaveManager.instance.isLevel4Complete = true; // Set boolean isLevelDone menjadi true
                SaveManager.instance.Save(); // Simpan data ke file
                finishCanvas.SetActive(true); // Tampilkan canvas finish     
            }

            if (totalQuiz == 5 && finishLevel == "Level5")
            {
                SaveManager.instance.isLevel5Complete = true; // Set boolean isLevelDone menjadi true
                SaveManager.instance.isLevelDone = true; // Set boolean isLevelDone menjadi true
                SaveManager.instance.Save(); // Simpan data ke file
                finishCanvas.SetActive(true); // Tampilkan canvas finish     
            }
        }
    }
}

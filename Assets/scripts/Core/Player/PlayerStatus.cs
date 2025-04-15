using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Player Status")]
    public int currHealth;
    private int maxHealth = 1;
    public int totalQuiz;

    [Header("Player Component")]
    private Animator playerAnim;
    public Checkpoint checkpoint;

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
}

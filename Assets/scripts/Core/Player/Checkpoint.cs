using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private int checkpointCounter = 0; // Jumlah checkpoint yang disentuh

    [Header("Player Component")]
    public Animator playerAnim;
    public Transform playerPos; // Transform pemain
    private Vector2 initialPos = new Vector2(-5.3f, -0.9650002f); // Posisi awal game

    [Header("Script Reference")]
    public PlayerController playerController;

    private void Awake()
    {
        checkpointCounter = 0;

        // Jika playerPos belum di-assign di Inspector, cari otomatis
        if (playerPos == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerPos = player.transform;
                playerAnim = player.GetComponent<Animator>();
                playerController = player.GetComponent<PlayerController>();
            }
        }
    }

    public void RespawnCheck()
    {
        if (playerPos == null)
        {
            Debug.LogWarning("Player tidak ditemukan!");
            return;
        }

        if (currentCheckpoint == null) // Jika belum menyentuh checkpoint
        {
            Respawn(); // Respawn ke posisi awal
        }
        else
        {
            playerPos.position = currentCheckpoint.position; // Respawn ke checkpoint terakhir
        }
        RespawnPlayer();
    }

    void Respawn()
    {
        if (playerPos != null)
        {
            // Konversi initialPos (Vector2) ke Vector3
            playerPos.position = new Vector3(initialPos.x, initialPos.y, playerPos.position.z);
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        if (playerAnim != null)
        {
            playerAnim.SetBool("die", false);
            playerAnim.SetBool("isGrounded", true);
        }

        if (playerController != null)
        {
            playerController.enabled = true; // Aktifkan kembali kontrol pemain
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Jika pemain menyentuh checkpoint
        {
            currentCheckpoint = transform; // Simpan posisi checkpoint
            checkpointCounter++; // Tambah jumlah checkpoint yang disentuh

            // Jika ada sound manager, mainkan suara checkpoint
            if (checkpointSound != null)
            {
                // SoundManager.instance.PlaySound(checkpointSound);
            }

            // Nonaktifkan collider checkpoint agar tidak bisa disentuh lagi
            //GetComponent<Collider2D>().enabled = false;
        }
    }
}

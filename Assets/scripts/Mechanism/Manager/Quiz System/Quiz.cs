using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{    
    [Header("Script Reference")]
    public PlayerController playerController;

    [Header("Quiz Component")]
    public GameObject quizPanel;
    [SerializeField] private GameObject buttonInteract;
    [SerializeField] GameObject joystick;
    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (buttonInteract != null)
            {
                buttonInteract.SetActive(true);
                LeanTween.scale(buttonInteract, new Vector3(1, 1, 1), 1f).setEase(LeanTweenType.easeOutBack);
            }
            playerInRange = true;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && buttonInteract != null && buttonInteract.activeSelf)
        {
            OpenQuizPanel();
        }

        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (playerInRange && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Camera cam = Camera.main;
                if (cam != null)
                {
                    Vector2 touchPosition = cam.ScreenToWorldPoint(touch.position);
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                    if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                    {
                        OpenQuizPanel();
                    }
                }
                else
                {
                    Debug.LogError("Main Camera not found! Please ensure there is a camera tagged as 'MainCamera' in the scene.");
                }
            }
        }
    }

    private void OpenQuizPanel()
    {
        if (quizPanel != null)
        {
            quizPanel.SetActive(true);
            Debug.Log("Quiz panel opened.");
        }
        else
        {
            Debug.LogError("quizPanel is not assigned in the inspector.");
        }

      

        if (joystick != null)
        {
            joystick.SetActive(false);
            Debug.Log("Joystick hidden.");
        }

        if (playerController != null)
        {
            playerController.enabled = false;
            Debug.Log("playerController disabled.");
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (buttonInteract != null)
        {
            buttonInteract.SetActive(false);
            Debug.Log("buttonInteract hidden.");
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (buttonInteract != null)
            {
                buttonInteract.SetActive(false);
                LeanTween.scale(buttonInteract, new Vector3(0, 0, 0), 1f).setEase(LeanTweenType.easeOutBack);
            }
            playerInRange = false;
        }
    }

    private void OnDestroy()
    {
        if (joystick != null)
        {
            joystick.SetActive(true);
        }
    }
}
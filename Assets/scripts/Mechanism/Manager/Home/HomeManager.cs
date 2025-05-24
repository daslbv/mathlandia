using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] GameObject soundPanel;
    [SerializeField] GameObject soundPanelBox;
    [SerializeField] Button soundButton;
    [SerializeField] Button closeSoundButton;

    private void Awake()
    {
        InitialChecking();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitialChecking()
    {
        soundPanel.SetActive(false);
        soundPanelBox.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void PlayGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("menulevel");
    }

    public void SoundPanel()
    {
        soundPanel.SetActive(true);
        soundButton.interactable = false;
        closeSoundButton.interactable = false;
        LeanTween.scale(soundPanelBox, new Vector3(0.6082393f, 0.6082393f, 0.6082393f), 1f).setEase(LeanTweenType.easeOutSine).setOnComplete(() =>
        {
            closeSoundButton.interactable = true;
        });
    }

    public void CloseSoundPanel()
    {
        closeSoundButton.interactable = false;
        LeanTween.scale(soundPanelBox, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutSine).setOnComplete(() =>
        {
            soundPanel.SetActive(false);
            soundButton.interactable = true;
        });
    }

    public void Exit()
    {
        // Exit the application
        Application.Quit();
        // If running in the editor, stop playing the scene
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

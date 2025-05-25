using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Login Settings")]
    [SerializeField] TMP_InputField namaInput;
    [SerializeField] TMP_InputField absenInput;

    [Header("Gameobject Canvas")]
    [SerializeField] GameObject levelCanvas;
    [SerializeField] GameObject loginCanvas;
    [SerializeField] GameObject kodeCanvas;
    [SerializeField] GameObject tinjauanCanvas;

    [Header("display tinjauan")]
    [SerializeField] TextMeshProUGUI playerNamaText;
    [SerializeField] TextMeshProUGUI playerAbsenText;
    [SerializeField] TMP_InputField kodeInputField;
    private void Start()
    {
        SaveManager.instance.Load();
    }

    private void Update()
    {
        ShowNameAbsen(); 
        OpenLevelCanvas();
        CheckPasswordAndOpenPanel();
    }

    public void LoginCheck()
    {
        SaveManager.instance.playerName = namaInput.text;
        SaveManager.instance.playerAbsen = absenInput.text;

        SaveManager.instance.isLogin = true;

        SaveManager.instance.Save();
    }

    public void OpenLevelCanvas()
    {
        if (SaveManager.instance.isLogin == true)
        {
            levelCanvas.SetActive(true);
            loginCanvas.SetActive(false);
        }
        else
        {
            loginCanvas.SetActive(true);
        }
    }
    void ShowNameAbsen()
    {
        playerNamaText.text = SaveManager.instance.playerName;
        playerAbsenText.text = SaveManager.instance.playerAbsen;
    }
    public void CheckPasswordAndOpenPanel()
    {
        if (kodeInputField == null) return;

        if (kodeInputField.text == "nada123")
        {
            kodeCanvas.SetActive(false);
            tinjauanCanvas.SetActive(true);
        }
        else
        {
            kodeInputField.text = "kode salah";
        }
    }
}

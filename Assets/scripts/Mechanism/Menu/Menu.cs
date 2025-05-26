using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Level Setting")]
    [SerializeField] Button level1Button;
    [SerializeField] Button level2Button;
    [SerializeField] Button level3Button;
    [SerializeField] Button level4Button;
    [SerializeField] Button level5Button;

    [Header("Login Settings")]
    [SerializeField] TMP_InputField namaInput;
    [SerializeField] TMP_InputField absenInput;

    [Header("Gameobject Canvas")]
    [SerializeField] GameObject levelCanvas;
    [SerializeField] GameObject loginCanvas;
    [SerializeField] GameObject kodeCanvas;
    [SerializeField] GameObject tinjauanCanvas;

    [Header("Display Tinjauan")]
    [SerializeField] TextMeshProUGUI playerNamaText;
    [SerializeField] TMP_InputField kodeInputField;

    // Tambahan flag untuk menghindari duplikasi aksi
    private bool sudahTampilkanKodeCanvas = false;

    private void Start()
    {
        SaveManager.instance.Load();

        // Disable level 2-5 by default
        level2Button.interactable = false;
        level3Button.interactable = false;
        level4Button.interactable = false;
        level5Button.interactable = false;

        // Cek login dan level selesai
        OpenLevelCanvas();
        CekLevelUnlock();
        CekTampilkanKodeCanvas();
    }

    private void Update()
    {
        ShowNameAbsen();
        CekLevelUnlock();
    }

    void ShowNameAbsen()
    {
        playerNamaText.text = SaveManager.instance.playerName + " - " + SaveManager.instance.playerAbsen;
    }

    public void LoginCheck()
    {
        SaveManager.instance.playerName = namaInput.text;
        SaveManager.instance.playerAbsen = absenInput.text;
        SaveManager.instance.isLogin = true;
        SaveManager.instance.Save();

        OpenLevelCanvas();
    }

    public void OpenLevelCanvas()
    {
        if (SaveManager.instance.isLogin)
        {
            levelCanvas.SetActive(true);
            loginCanvas.SetActive(false);
        }
        else
        {
            loginCanvas.SetActive(true);
        }
    }

    public void CekLevelUnlock()
    {
        level2Button.interactable = SaveManager.instance.isLevel1Complete;
        level3Button.interactable = SaveManager.instance.isLevel2Complete;
        level4Button.interactable = SaveManager.instance.isLevel3Complete;
        level5Button.interactable = SaveManager.instance.isLevel4Complete;
    }

    public void CekTampilkanKodeCanvas()
    {
        if (SaveManager.instance.isLevelDone && !sudahTampilkanKodeCanvas)
        {
            kodeCanvas.SetActive(true);
            loginCanvas.SetActive(false);
            levelCanvas.SetActive(false);
            tinjauanCanvas.SetActive(false);
        }
    }

    public void CheckPasswordAndOpenPanel()
    {
        Debug.Log("Method terpanggil");

        if (kodeInputField == null)
        {
            Debug.LogWarning("Input field belum di-assign!");
            return;
        }

        string inputUser = kodeInputField.text.Trim().ToLower();
        Debug.Log("Input: " + inputUser);

        if (inputUser == "nada123")
        {
            Debug.Log("Password benar, buka panel.");
            kodeCanvas.SetActive(false);
            tinjauanCanvas.SetActive(true);
            sudahTampilkanKodeCanvas = true; // Supaya tidak ditimpa lagi
        }
        else
        {
            Debug.Log("Password salah");
            kodeInputField.text = "kode salah";
        }
    }

    // Level Loading Methods
    public void Level1() => SceneManager.LoadScene("Level1");
    public void Level2() => SceneManager.LoadScene("Level2");
    public void Level3() => SceneManager.LoadScene("Level3");
    public void Level4() => SceneManager.LoadScene("Level4");
    public void Level5() => SceneManager.LoadScene("Level5");
}

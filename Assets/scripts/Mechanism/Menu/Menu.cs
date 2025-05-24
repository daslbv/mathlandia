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

    private void Start()
    {
        SaveManager.instance.Load();
    }

    private void Update()
    {
        OpenLevelCanvas();
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
}

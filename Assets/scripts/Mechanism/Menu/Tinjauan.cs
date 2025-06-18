using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Tinjauan : MonoBehaviour
{
    [Header("Review 1-5")]
    [SerializeField] TextMeshProUGUI review1Text;
    [SerializeField] TextMeshProUGUI review2Text;
    [SerializeField] TextMeshProUGUI review3Text;
    [SerializeField] TextMeshProUGUI review4Text;
    [SerializeField] TextMeshProUGUI review5Text;

    [Header("Timer Accumulate")]
    [SerializeField] float timerAccumulate;
    [SerializeField] TextMeshProUGUI timerAccumulateText;

    [Header("Canvas Reference")]
    [SerializeField] GameObject canvasMenu;
    [SerializeField] GameObject canvasTinjauan;
    [SerializeField] GameObject canvasLogin;

    private void Start()
    {
        // Jumlahkan semua waktu dari level 1-5
        timerAccumulate =
            SaveManager.instance.timerString1 +
            SaveManager.instance.timerString2 +
            SaveManager.instance.timerString3 +
            SaveManager.instance.timerString4 +
            SaveManager.instance.timerString5;

        // Panggil review sekali saja saat start (lebih efisien dari Update)
        ShowReview();
        TimerAccumulate();
    }

    void ShowReview()
    {
        review1Text.text = FormatReview(SaveManager.instance.totalWrong1, SaveManager.instance.timerString1);
        review2Text.text = FormatReview(SaveManager.instance.totalWrong2, SaveManager.instance.timerString2);
        review3Text.text = FormatReview(SaveManager.instance.totalWrong3, SaveManager.instance.timerString3);
        review4Text.text = FormatReview(SaveManager.instance.totalWrong4, SaveManager.instance.timerString4);
        review5Text.text = FormatReview(SaveManager.instance.totalWrong5, SaveManager.instance.timerString5);
    }

    void TimerAccumulate()
    {
        // Format waktu total
        int menit = Mathf.FloorToInt(timerAccumulate / 60f);
        int detik = Mathf.FloorToInt(timerAccumulate % 60f);
        string waktuFormatted = string.Format("{0:00}:{1:00}", menit, detik);

        // Tampilkan di UI
        timerAccumulateText.text = $"Total Waktu: {waktuFormatted}";
    }

    public void ResetSave()
    {
        SaveManager.instance.timerString1 = 0f;
        SaveManager.instance.timerString2 = 0f;
        SaveManager.instance.timerString3 = 0f;
        SaveManager.instance.timerString4 = 0f;
        SaveManager.instance.timerString5 = 0f;
        SaveManager.instance.totalWrong1 = 0;
        SaveManager.instance.totalWrong2 = 0;
        SaveManager.instance.totalWrong3 = 0;
        SaveManager.instance.totalWrong4 = 0;
        SaveManager.instance.totalWrong5 = 0;
        SaveManager.instance.isLevel1Complete = false;
        SaveManager.instance.isLevel2Complete = false;
        SaveManager.instance.isLevel3Complete = false;
        SaveManager.instance.isLevel4Complete = false;
        SaveManager.instance.isLevel5Complete = false;
        SaveManager.instance.isLevelDone = false;

        SaveManager.instance.Save();

        // Kembali ke menu utama setelah reset
        canvasLogin.SetActive(false);
        canvasTinjauan.SetActive(false);
        canvasMenu.SetActive(true);
     }

    string FormatReview(int totalWrong, float waktu)
    {
        int menit = Mathf.FloorToInt(waktu / 60f);
        int detik = Mathf.FloorToInt(waktu % 60f);
        string waktuFormatted = string.Format("{0:00}:{1:00}", menit, detik);
        return $"{totalWrong} salah, {waktuFormatted} waktu";
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("home");
    }
}

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    //Timer Save
    [Header("Timer")]
    public float timerString1;
    public float timerString2;
    public float timerString3;
    public float timerString4;
    public float timerString5;
    public float timerAccumulate;

    //Wrong Answer Quiz Save
    [Header("Wrong Answer Quiz")]
    public int totalWrong1;
    public int totalWrong2;
    public int totalWrong3;
    public int totalWrong4;
    public int totalWrong5;
    public int wrongAccumulate;

    //Is player already finishing the game?
    [Header("Player Check")]
    public bool isLevelDone;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);

            // Timer Save
            timerString1 = data.timerString1;
            timerString2 = data.timerString2;
            timerString3 = data.timerString3;
            timerString4 = data.timerString4;
            timerString5 = data.timerString5;

            // Wrong Answer Quiz Save
            totalWrong1 = data.totalWrong1;
            totalWrong2 = data.totalWrong2;
            totalWrong3 = data.totalWrong3;
            totalWrong4 = data.totalWrong4;
            totalWrong5 = data.totalWrong5;

            // Boolean Save
            isLevelDone = data.isLevelDone;

            file.Close();
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        // Timer Save
        data.timerString1 = timerString1;
        data.timerString2 = timerString2;
        data.timerString3 = timerString3;
        data.timerString4 = timerString4;
        data.timerString5 = timerString5;

        // Wrong Answer Quiz Save
        data.totalWrong1 = totalWrong1;
        data.totalWrong2 = totalWrong2;
        data.totalWrong3 = totalWrong3;
        data.totalWrong4 = totalWrong4;
        data.totalWrong5 = totalWrong5;

        // Boolean Save
        data.isLevelDone = isLevelDone;

        bf.Serialize(file, data);
        file.Close();
    }
}

[Serializable]
class PlayerData_Storage
{
    // Timer Save
    public float timerString1;
    public float timerString2;
    public float timerString3;
    public float timerString4;
    public float timerString5;

    // Wrong Answer Quiz Save
    public int totalWrong1;
    public int totalWrong2;
    public int totalWrong3;
    public int totalWrong4;
    public int totalWrong5;

    // Boolean Save
    public bool isLevelDone;
}
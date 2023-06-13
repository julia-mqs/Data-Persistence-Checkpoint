using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManagerII : MonoBehaviour
{
    public static MainManagerII Instance;
    public string PlayerName = string.Empty;
    public string PlayerScore = string.Empty;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadInfo();
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public string playerScore;
    }

    public void SaveName(string leName)
    {
        PlayerName = leName;
    }

    public void SaveScore(string leScore)
    {
        if (int.Parse(leScore) > int.Parse(PlayerScore))
            PlayerScore = leScore;
    }

    public void SaveInfo()
    {
        SaveData data = new SaveData();
        data.playerName = PlayerName;
        data.playerScore = PlayerScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerName = data.playerName;
            PlayerScore = data.playerScore;
        }
    }
}
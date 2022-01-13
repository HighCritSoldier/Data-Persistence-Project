using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;
    public TMP_InputField NameField;
    public string UserName;
    public string HSname;
    public int HSnum;
    public int m_Score;

    [Serializable]
    public class PlayerData
    {
        public string HSName;
        public int HSNum = 0;
    }
    PlayerData myData = new PlayerData();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    public void Start()
    {
        LoadData();
    }

    // Update is called once per frame

    public void SaveData()
    {
        myData.HSName = HSname;
        myData.HSNum = HSnum;
        string json = JsonUtility.ToJson(myData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData myData = JsonUtility.FromJson<PlayerData>(json);

            HSname = myData.HSName;
            HSnum = myData.HSNum;
        }
    }

    public void OnStartClick()
    {
        HighScoreManager.Instance.UserName = NameField.text;
        SceneManager.LoadScene(1);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }

    public void HSRecord()
    {
        GameObject MM = GameObject.Find("MainManager");
        MainManager script = MM.GetComponent<MainManager>();
        HSname = UserName;
        HSnum = script.m_Points;
    }
}
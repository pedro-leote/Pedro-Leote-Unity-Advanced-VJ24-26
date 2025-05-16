using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class SaveManager : MonoBehaviour
{
    private SaveData _saveDataToReceive;
    
    private static SaveManager _instance;
    public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveManager();
            }
            return _instance;
        }
    }

    public UnityEvent<SaveData> OnSaveRequestedEvent = new UnityEvent<SaveData>();
    public UnityEvent<SaveData> OnLoadRequestedEvent = new UnityEvent<SaveData>();

    public void SaveGameState()
    {
        OnSaveRequestedEvent?.Invoke(_saveDataToReceive);
        string json = JsonUtility.ToJson(_saveDataToReceive);
        
        File.WriteAllText(Application.persistentDataPath + "/umgolfsave.json", json);
    }

    public void LoadGameState()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/umgolfsave.json");
        _saveDataToReceive = JsonUtility.FromJson<SaveData>(json);
        OnLoadRequestedEvent?.Invoke(_saveDataToReceive);
    }

}


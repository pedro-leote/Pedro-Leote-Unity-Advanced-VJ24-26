using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class SaveManager : MonoSingleton<SaveManager>
{
    [SerializeField] private SaveData _saveDataToReceive;
    
    public UnityEvent<SaveData> OnSaveRequestedEvent = new UnityEvent<SaveData>();
    public UnityEvent<SaveData> OnLoadRequestedEvent = new UnityEvent<SaveData>();


    public IEnumerator CreateGameState()
    {
        _saveDataToReceive = new SaveData
        {
            _coins = 0,
            _maxBalls = 5,
            _recordLevel = 0,
            _musicPercentage = 100,
            _sfxPercentage = 100
        };
        
        string json = JsonUtility.ToJson(_saveDataToReceive);
        
        File.WriteAllText($"{Application.persistentDataPath}/umgolfsave.json", json);
        yield return null;
    }
    public IEnumerator SaveGameState()
    {
        OnSaveRequestedEvent?.Invoke(_saveDataToReceive);
        string json = JsonUtility.ToJson(_saveDataToReceive);
        
        File.WriteAllText($"{Application.persistentDataPath}/umgolfsave.json", json);
        
        yield return null;
    }

    public IEnumerator LoadGameState()
    {
        string json = File.ReadAllText($"{Application.persistentDataPath}/umgolfsave.json");
        _saveDataToReceive = JsonUtility.FromJson<SaveData>(json);
        OnLoadRequestedEvent?.Invoke(_saveDataToReceive);
        yield return null;
    }

}


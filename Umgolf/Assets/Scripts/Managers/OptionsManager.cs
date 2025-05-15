using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    private OptionsManager _instance;
    public OptionsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new OptionsManager();
            }
            return _instance;
        }
    }
    
    //Progression Variables
    [SerializeField] private int Coins { get; set; }
    [SerializeField] private int MaxBalls { get; set; }
    [SerializeField] private int RecordLevel { get; set; }
    [SerializeField] private float MusicPercentage { get; set; }
    [SerializeField] private float SFXPercentage { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(_instance);
    }

    public void SaveRequested(SaveData saveData)
    {
        saveData._maxBalls = MaxBalls;
        saveData._recordLevel = RecordLevel;
        saveData._musicPercentage = MusicPercentage;
        saveData._sfxPercentage = SFXPercentage;
    }
    
    public void LoadRequested(SaveData saveData)
    {
        MaxBalls = saveData._maxBalls;
        RecordLevel = saveData._recordLevel;
        MusicPercentage = saveData._musicPercentage;
        SFXPercentage = saveData._sfxPercentage;
    }
    
}

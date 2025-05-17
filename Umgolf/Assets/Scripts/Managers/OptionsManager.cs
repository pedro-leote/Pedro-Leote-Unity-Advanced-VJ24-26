using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class OptionsManager : MonoSingleton<OptionsManager>
{
    
    //Progression Variables
    [SerializeField] private int _coins;
    [SerializeField] private int _maxBalls;
    [SerializeField] private int _recordLevel;
    [SerializeField] private float _musicPercentage;
    [SerializeField] private float _sfxPercentage;
    
    public void SaveRequested(SaveData saveData)
    {
        saveData._maxBalls = _maxBalls;
        saveData._recordLevel = _recordLevel;
        saveData._musicPercentage = _musicPercentage;
        saveData._sfxPercentage = _sfxPercentage;
    }
    
    public void LoadRequested(SaveData saveData)
    {
        _maxBalls = saveData._maxBalls;
        _recordLevel = saveData._recordLevel;
        _musicPercentage = saveData._musicPercentage;
        _sfxPercentage = saveData._sfxPercentage;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int _availableBalls;
    private int _activeLevelIndex;
    private int _nextLevelIndex;
    
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        _activeLevelIndex = 0;
        _nextLevelIndex = 1;
    }
    public void SetUpForNextLevel()
    {
        
    }
}

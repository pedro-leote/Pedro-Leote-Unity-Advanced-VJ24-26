using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    //Slight bug incoveniencing me so this is a quick fix so I can ship this off
    private bool _isWorking;
    
    [SerializeField] private int _availableBalls;
    [SerializeField] private int _activeLevelIndex;
    
    private GameObject _currentLevelObject;
    private GameObject _nextLevelObject;
    
    
    public UnityEvent OnGameStartingEvent = new UnityEvent();
    public UnityEvent OnGameReadyEvent = new UnityEvent();
    public UnityEvent OnLevelChangeEvent = new UnityEvent();
    public UnityEvent OnLevelReadyEvent = new UnityEvent();
    public UnityEvent OnGameWonEvent = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        OnGameStartingEvent?.Invoke();
        //All the Startup logic.
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        _activeLevelIndex = 0;
        StartCoroutine(SetUpFirstLevel());
    }

    public IEnumerator SetUpFirstLevel()
    {
        yield return new WaitForEndOfFrame();
        _currentLevelObject = LevelManager.Instance.GrabLevelData(0);
        _currentLevelObject.SetActive(true);
        _currentLevelObject.transform.position = Vector3.zero;
        
        OnGameReadyEvent?.Invoke();
    }

    public void SetupLevelWrapper()
    {
        if (_isWorking)
        {
            return;
        }

        StartCoroutine(SetUpForNextLevel());
    }
    public IEnumerator SetUpForNextLevel()
    {
        _isWorking = true;
        
        OnLevelChangeEvent?.Invoke();
        //Wait for the animation to hide screen
        yield return new WaitForSeconds(0.8f);
        
        int nextLevelIndex = _activeLevelIndex + 1;
        _nextLevelObject = LevelManager.Instance.GrabLevelData(nextLevelIndex);
        if (_nextLevelObject == null)
        {
            EndGame();
            yield break;       
        }
        

        _nextLevelObject.transform.position = new Vector3(6, 0, 0);
        
        yield return new WaitForSeconds(2f);
        _activeLevelIndex++;
        _nextLevelObject.SetActive(true);
        _nextLevelObject.transform.position = Vector3.zero;
        
        OnLevelReadyEvent?.Invoke();
        _isWorking = false;
    }

    public void IncrementCoinValue()
    {
        OptionsManager.Instance.ChangeCoins(1);
    }
    
    public void EndGame()
    {
        OnGameWonEvent?.Invoke();
    }
}

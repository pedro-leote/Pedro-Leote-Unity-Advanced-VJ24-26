using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int _availableBalls;
    private int _activeLevelIndex;
    private int _nextLevelIndex;
    
    private GameObject _currentLevelObject;
    private GameObject _nextLevelObject;

    public UnityEvent<int> OnLevelChangeEvent = new UnityEvent<int>();
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

    public IEnumerator SetUpFirstLevel()
    {
        _currentLevelObject = LevelManager.Instance.GrabLevelData(0);
        yield return new WaitForEndOfFrame();
        _currentLevelObject.SetActive(true);
        _currentLevelObject.transform.position = Vector3.zero;
    }

    public void SetupLevelWrapper()
    {
        StartCoroutine(SetUpForNextLevel());
    }
    public IEnumerator SetUpForNextLevel()
    {   
        _nextLevelObject = LevelManager.Instance.GrabLevelData(_nextLevelIndex);
        if (_nextLevelObject == null)
        {
            EndGame();
            yield break;       
        }
        
        OnLevelChangeEvent?.Invoke(_nextLevelIndex);
        _nextLevelObject.transform.position = new Vector3(6, 0, 0);
        
        
        yield return new WaitForSeconds(2f);
        _nextLevelIndex++;
        _activeLevelIndex++;
        _nextLevelObject.SetActive(true);
        _nextLevelObject.transform.position = Vector3.zero;
        //Call LevelManager and receive Object
    }

    public void EndGame()
    {
        
    }
}

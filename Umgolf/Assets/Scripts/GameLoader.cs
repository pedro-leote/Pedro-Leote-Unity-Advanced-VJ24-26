using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


//Used in the Loading screen to initialize everything into who needs it: levels, save file, next 2 scenes.
public class GameLoader : MonoBehaviour
{
    //Bools de verificação se todos os processos funcionaram.
    [SerializeField] private bool _hasFinishedSaveLoad = false;
    [SerializeField] private bool _hasFinishedLevels = false;
    [SerializeField] private bool _hasFinishedScenes = false;
    
    //Eventos para lançar informação obtida a quem precisar (ex.: LevelManager)
    public UnityEvent<LevelLayout[]> OnLevelsLoadedEvent = new UnityEvent<LevelLayout[]>();
    public UnityEvent OnScenesLoadedEvent = new UnityEvent();
    
    void Start()
    { 
        StartCoroutine(WaitUntilStartingLoading());
    }

    
    void Update()
    {
        if (_hasFinishedLevels && _hasFinishedScenes && _hasFinishedSaveLoad)
        {
            StopAllCoroutines();
            SceneLoadManager.Instance.SwitchToScene("TitleScreen");
        }
        
    }


    private IEnumerator WaitUntilStartingLoading()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoadSave());
        StartCoroutine(LoadLevels());
        StartCoroutine(LoadScenes());
        
        StopCoroutine(WaitUntilStartingLoading());
    }
    
    //No LoadLevels obtenho os SO's presentes no Resources folder, dou parse para um array dando-lhes load, e envio para o Dictionary do LevelManager
    private IEnumerator LoadLevels()
    {
        LevelLayout[] foundLayouts = Resources.FindObjectsOfTypeAll<LevelLayout>(); //Obtemos os SOs dentro do Resources folder, sem subfolders.
        if (foundLayouts == null || foundLayouts.Length == 0)
        {
            yield break; // Se não houver nada, não posso fazer nada lol
        }
        
        //Damos init a um array com o tamanho de todos os níveis encontrados.
        LevelLayout[] layoutArray = new LevelLayout[foundLayouts.Length];
        for (int i = 0; i < foundLayouts.Length; ++i)
        {
            //Fazemos request de load a cada um destes SO's para não esperar later no jogo.
            ResourceRequest request = Resources.LoadAsync<LevelLayout>(foundLayouts[i].name);
            yield return request; //Esperamos até o load terminar.
            layoutArray[i] = request.asset as LevelLayout; //E metemos como LevelLayout.
        }
        //Anteriormente este código dava IndexOutOfRangeException, mas agora parece funcionar bem.
        
        OnLevelsLoadedEvent?.Invoke(layoutArray); // O problema situa-se neste evento, pois o listener é o LevelManager que depois 
                                                  // transforma o array obtido em entries do seu SerializableDictionary. Mas não tem funcionado.
        yield return true;
        _hasFinishedLevels = true;
    }
    
    //No LoadSave, procuro o .json e chamo o LoadGameState do SaveManager, obtendo a info necessária deste documento.
    private IEnumerator LoadSave()
    {

        if (File.Exists(Application.persistentDataPath + "/umgolfsave.json"))
        {
            yield return SaveManager.Instance.LoadGameState();
        }

        _hasFinishedSaveLoad = true;
    }
    
    //No LoadScenes chamo o SceneLoadManager para dar async load das únicas 2 scenes que preciso.
    private IEnumerator LoadScenes()
    {
        yield return SceneLoadManager.Instance.LoadScenes(); //Aqui o loading async das scenes, pelo menos no Editor, nunca parece acabar. Estranho
        OnScenesLoadedEvent?.Invoke();
        _hasFinishedScenes = true;
    }
}

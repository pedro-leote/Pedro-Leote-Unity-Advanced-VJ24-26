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
    
    //Eventos para lançar informação obtida a quem precisar (ex.: LevelManager)
    public UnityEvent<LevelLayout[]> OnLevelsLoadedEvent = new UnityEvent<LevelLayout[]>();
    public UnityEvent OnScenesLoadedEvent = new UnityEvent();
    
    void Start()
    { 
        StartCoroutine(InitLoading());
    }
    

    private IEnumerator InitLoading()
    {
        //Initialize all the coroutines needed and wait for their completion.
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(LoadLevels());
        yield return StartCoroutine(LoadSave());
        yield return StartCoroutine(LoadScenes());

        //Catch-all case
        StopAllCoroutines();
        //Instruct scene change.
        SceneLoadManager.Instance.SwitchToScene("TitleScreen");
    }
    
    //No LoadLevels obtenho os SO's presentes no Resources folder, dou parse para um array dando-lhes load, e envio para o Dictionary do LevelManager
    private IEnumerator LoadLevels()
    {
        LevelLayout[] foundLayouts = Resources.LoadAll<LevelLayout>(""); //Obtemos os SOs dentro do Resources folder, sem subfolders.
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
        
        OnLevelsLoadedEvent?.Invoke(layoutArray);
        yield return true;
        Debug.Log($"Finished Level Load operation with {layoutArray.Length} levels.");
    }
    
    //No LoadSave, procuro o .json e chamo o LoadGameState do SaveManager, obtendo a info necessária deste documento.
    private IEnumerator LoadSave()
    {
        if (File.Exists($"{Application.persistentDataPath}/umgolfsave.json"))
        {
            yield return StartCoroutine(SaveManager.Instance.LoadGameState());
            Debug.Log("Found save. Loaded.");
            yield break;
        }

        Debug.Log("No file found.");
        yield return StartCoroutine(SaveManager.Instance.CreateGameState());
    }
    
    //No LoadScenes chamo o SceneLoadManager para dar async load das únicas 2 scenes que preciso.
    private IEnumerator LoadScenes()
    {
        yield return StartCoroutine(SceneLoadManager.Instance.LoadScene("TitleScreen"));
        OnScenesLoadedEvent?.Invoke();
        Debug.Log("Finished LoadScene operation.");
    }
}

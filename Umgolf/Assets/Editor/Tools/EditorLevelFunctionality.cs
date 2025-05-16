using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class EditorLevelFunctionality : MonoBehaviour
{
    private static SerializedDictionary<int, LevelLayout> _levelLayoutDictionary = new SerializedDictionary<int, LevelLayout>();
    
    #if UNITY_EDITOR
    [MenuItem("Tools/Populate Level Dictionary")]
    public static void PopulateLevelDictionary()
    {
        //Clear previous iteration
        _levelLayoutDictionary.Clear();
        //Grab all SO's from Resources/Levels
        LevelLayout[] levelLayoutCollection = Resources.LoadAll<LevelLayout>("Levels");
        Debug.Log($"Found {levelLayoutCollection.Length} levels in Resources/Levels.");
        //Pass them to the dictionary
        ReceiveDictionary(levelLayoutCollection);
    }

    [MenuItem("Tools/Test Level Dictionary")]
    public static void TestLevelDictionary()
    {
        Debug.Log($"Currently populated: {_levelLayoutDictionary.Count}\nIn depth: {_levelLayoutDictionary.ToString()}");
    }
    #endif
    public static void ReceiveDictionary(LevelLayout[] levelLayoutCollection)
    {
        for (int i = 0; i < levelLayoutCollection.Length; ++i)
        {
            _levelLayoutDictionary.Add(levelLayoutCollection[i]._levelIndex, levelLayoutCollection[i]);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameObjectToSO : EditorWindow
{
    private static GameObject _gameObjectToConvert;
    private static List<GameObject> _childrenList = new List<GameObject>();
    
    [MenuItem("Tools/Convert GameObject into Level SO")]
    public static void ConvertGameObjectIntoSO()
    {
        Debug.Log("!Begun Conversion!");
        //Grab references of a selected object, find out if children exist
        _gameObjectToConvert = Selection.activeGameObject;
        if (_gameObjectToConvert?.transform.childCount > 0)
        {
            for (int i = 0; i < _gameObjectToConvert.transform.childCount; ++i)
            {
                _childrenList.Add(_gameObjectToConvert.transform.GetChild(i).gameObject);
            }
            Debug.Log($"Found components in parent: {_childrenList.Count}...");
        }
        //Grab transforms and insert into List of objects.
        LevelLayout layoutToLoad = ScriptableObject.CreateInstance<LevelLayout>();
        layoutToLoad._levelParentObject = _gameObjectToConvert;
        if (_childrenList != null)
        {
            layoutToLoad._levelObjects = _childrenList;
        }
        
        //Open path to insert new ScriptableObject
        string path = EditorUtility.SaveFolderPanel("Select folder to place Scriptable Objects", "", "");
        
        Debug.Log($"Got path to {layoutToLoad} at: {path}. Initiating creation...");
        
        if (!string.IsNullOrEmpty(path))
        {
            //Put the path into Assets
            string relativePath = "Assets" + path.Substring(Application.dataPath.Length);
            
            //We check if there are any existing Level Layouts and init a int to store max index.
            string[] existingLayouts = AssetDatabase.FindAssets("t:LevelLayout", new[] { relativePath });
            int maxExistingIndex = -1; //Este S0 pode ser o primeiro, thus the 0.
            Debug.Log($"Found {existingLayouts.Length} existing layouts in {path}. Grabbing index...");
            
            if (existingLayouts.Length > 0)
            {
                for (int i = 0; i < existingLayouts.Length; ++i)
                {
                    //This autocompleted and I have no idea what it means. EDIT: Já percebi
                   LevelLayout foundLayout = AssetDatabase.LoadAssetAtPath<LevelLayout>(AssetDatabase.GUIDToAssetPath(existingLayouts[i]));
                   
                   //Compare if the grabbed index is greater than the current max index found
                   maxExistingIndex = Mathf.Max(maxExistingIndex, foundLayout._levelIndex);
                }
                
            }
            //and thus that should inform our index.
            layoutToLoad._levelIndex = ++maxExistingIndex;
            
            //Create the finished asset.
            string layoutName = $"Layout_{layoutToLoad._levelIndex}.asset";
            AssetDatabase.CreateAsset(layoutToLoad, $"{relativePath}/{layoutName}");
            AssetDatabase.SaveAssets();
        }
        
        
        AssetDatabase.Refresh();
    }
    
    [MenuItem("Tools/Convert GameObject into SO", true)]
    public static bool IsGameObjectSelected()
    {
        return Selection.activeGameObject != null;
    }
    

}

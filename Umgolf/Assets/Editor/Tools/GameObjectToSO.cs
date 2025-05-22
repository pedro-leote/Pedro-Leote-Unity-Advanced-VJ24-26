using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameObjectToSO : EditorWindow
{
    private static LevelObjectData _parentObject;
    private static List<LevelObjectData> _childrenList = new List<LevelObjectData>();
    private static List<LevelPrefabData> _prefabList = new List<LevelPrefabData>();
    
    [MenuItem("Tools/Convert GameObject into Level SO")]
    public static void ConvertGameObjectIntoSO()
    {
        _parentObject = null;
        _childrenList.Clear();
        _prefabList.Clear();
        
        GameObject selectedGameObject = Selection.activeGameObject;
        Debug.Log("!Begun Conversion!");
        //Grab references of a selected object, find out if children exist
        _parentObject = new LevelObjectData
        {
            _name = selectedGameObject.name,
            _position = selectedGameObject.transform.position,
            _rotation = selectedGameObject.transform.rotation,
            _scale = selectedGameObject.transform.localScale,
            //Estas linhas demonstram o uso de conditional check ao atribuir uma variável, que dá replace a um if(Try...) -> _boxCollider = collider, else() -> null; Assim fica numa sentence.
            
            _collider2DState = selectedGameObject.TryGetComponent<Collider2D>(out Collider2D collider) && collider.enabled,
            _collider2DSize = selectedGameObject.TryGetComponent<Collider2D>(out Collider2D collider2) ? collider2.bounds.size : Vector2.zero,
            _spriteRendererColor = selectedGameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer) ? spriteRenderer.color : Color.black,
            _spriteRendererLayer = selectedGameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer2) ? spriteRenderer2.sortingOrder : 0,
        };

        if (selectedGameObject.transform.childCount > 0)
        {
            for (int i = 0; i < selectedGameObject.transform.childCount; ++i)
            {
                if (PrefabUtility.IsPartOfAnyPrefab(selectedGameObject.transform.GetChild(i).gameObject))
                {
                    _prefabList.Add(new LevelPrefabData
                    {
                        _name = selectedGameObject.transform.GetChild(i).gameObject.name,
                        _position = selectedGameObject.transform.GetChild(i).localPosition,
                        _rotation = selectedGameObject.transform.GetChild(i).localRotation,
                        _scale = selectedGameObject.transform.GetChild(i).localScale,
                        _prefab = PrefabUtility.GetCorrespondingObjectFromSource(selectedGameObject.transform.GetChild(i).gameObject),
                        _prefabName = PrefabUtility.GetCorrespondingObjectFromSource(selectedGameObject.transform.GetChild(i).gameObject).name,
                    });
                    continue;
                }
                
                _childrenList.Add(new LevelObjectData
                {
                    _name = selectedGameObject.transform.GetChild(i).gameObject.name,
                    _position = selectedGameObject.transform.GetChild(i).localPosition,
                    _rotation = selectedGameObject.transform.GetChild(i).localRotation,
                    _scale = selectedGameObject.transform.GetChild(i).localScale,
                    _collider2DState = selectedGameObject.transform.GetChild(i).TryGetComponent<Collider2D>(out Collider2D childCollider) && childCollider.enabled,
                    _collider2DSize = selectedGameObject.transform.GetChild(i).TryGetComponent<Collider2D>(out Collider2D childCollider2) ? childCollider2.bounds.size : Vector2.zero,
                    _spriteRendererColor = selectedGameObject.transform.GetChild(i).TryGetComponent<SpriteRenderer>(out SpriteRenderer childSpriteRenderer) ? childSpriteRenderer.color : Color.black,
                    _spriteRendererLayer = selectedGameObject.transform.GetChild(i).TryGetComponent<SpriteRenderer>(out SpriteRenderer childSpriteRenderer2) ? childSpriteRenderer2.sortingOrder : 0
                });
            }
            Debug.Log($"Found generic children in parent: {_childrenList.Count}...");
        }
        //Compile LevelObjectData into LevelLayout.
        LevelLayout layoutToLoad = ScriptableObject.CreateInstance<LevelLayout>();
        layoutToLoad._levelParentObject = _parentObject;
        if (_childrenList != null)
        {
            layoutToLoad._levelObjects = _childrenList;
        }

        if (_prefabList != null)
        {
            layoutToLoad._levelPrefabs = _prefabList;
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
            
            Debug.Log($"Finished Build {layoutName} at {relativePath}.");
        }
        
        
        AssetDatabase.Refresh();
    }
    
    [MenuItem("Tools/Convert GameObject into SO", true)]
    public static bool IsGameObjectSelected()
    {
        return Selection.activeGameObject != null;
    }
    

}

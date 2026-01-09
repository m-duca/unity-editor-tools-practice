using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace EditorToolsPractice
{
    /// <summary>
    /// Ferramenta capaz de setar refs de GameObjects encontradas em campos serializados, com base em seus nomes na Hierarquia
    /// </summary>
    public class AutoLinkerTool : Editor
    {
        private static Dictionary<string, GameObject> _gameObjectsDict ;

        [InitializeOnLoadMethod]
        private static void Setup()
        {
            SetupDictionary();
            LinkProperties();
        }

        private static void SetupDictionary()
        {
            _gameObjectsDict = new();
            GameObject[] gameObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

            foreach(GameObject go in gameObjects)
            {
                string goName = go.name.ToLower().Replace(" ", "");

                if (!_gameObjectsDict.ContainsKey(goName))
                    _gameObjectsDict.Add(goName, go);                
            }
        }

        private static void LinkProperties()
        {
            foreach(GameObject go in _gameObjectsDict.Values)
            {
                Component[] components = go.GetComponents<Component>();

                foreach (Component comp in components)
                {
                    SerializedObject serializedObject = new SerializedObject(comp);
                    SerializedProperty serializedProperty = serializedObject.GetIterator();
                    
                    while(serializedProperty.NextVisible(true))
                    {
                        if (serializedProperty.propertyType != SerializedPropertyType.ObjectReference)
                            continue;

                        string key = serializedProperty.displayName.ToLower().Replace(" ", "");
                        
                        if (_gameObjectsDict.ContainsKey(key))
                        {
                            GameObject fieldGo = _gameObjectsDict[key];
                            serializedProperty.objectReferenceValue = fieldGo;
                            serializedObject.ApplyModifiedProperties();
                        }
                    }
                }
            }
        }
    }
}

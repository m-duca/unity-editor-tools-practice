using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace EditorToolsPractice
{
    public class GameObjectDebuggerWindow : EditorWindow
    {
        private GameObject _selectedGameObject;
        private List<GameObject> _filteredGameObjects = new();

        private string _searchQuery = String.Empty;
        private string _tagFilter = String.Empty;
        private string _componentFilter = String.Empty;


        [MenuItem("CustomTools/GameObject Debugger")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(GameObjectDebuggerWindow));
            GUIContent guiContent = new GUIContent();
            guiContent.text = "GameObject Debugger";

            window.titleContent = guiContent;
            window.Show();
        }

        private void OnEnable() => _filteredGameObjects.AddRange(GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None));

        private void OnDisable() => _filteredGameObjects.Clear();

        private void OnGUI()
        {
            GUILayout.Label("Custom GameObject Debugger", EditorStyles.boldLabel);
            GUILayout.Label("Search & Filter", EditorStyles.boldLabel);

            _searchQuery = EditorGUILayout.TextField("Search by Name", _searchQuery);
            _tagFilter = EditorGUILayout.TagField("Filter by Tag", _tagFilter);
            _componentFilter = EditorGUILayout.TextField("Filter by Component", _componentFilter);

            ApplyFilters();
            ShowFilteredGameObjects();
        }

        private void ApplyFilters()
        {
            _filteredGameObjects.Clear();
            List<GameObject> allGameObjects = new List<GameObject>();
            allGameObjects.AddRange(GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None));

            foreach(GameObject gameObject in allGameObjects)
            {
                bool matchesSearch = string.IsNullOrEmpty(_searchQuery) || gameObject.name.ToLower().Contains(_searchQuery.ToLower());
                bool matchesTag = string.IsNullOrEmpty(_tagFilter) || gameObject.CompareTag(_tagFilter);
                bool matchesComponent = string.IsNullOrEmpty(_componentFilter) || gameObject.GetComponent(_componentFilter) != null;
            
                if (matchesSearch && matchesTag && matchesComponent)
                    _filteredGameObjects.Add(gameObject);
            }
        }

        private void ShowFilteredGameObjects()
        {
            if (_filteredGameObjects.Count > 0)
            {
                foreach(GameObject gameObject in _filteredGameObjects)
                {
                    if (GUILayout.Button(gameObject.name))
                        _selectedGameObject = gameObject;
                }
            }
            else
            {
                GUILayout.Label("No gameObjects matched the filters criteria.", EditorStyles.boldLabel);
            }
        }
    }
}

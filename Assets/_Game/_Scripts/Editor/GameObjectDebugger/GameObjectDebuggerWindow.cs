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

        private bool _showTransform = true;
        private bool _showRenderer = true;
        private bool _showRigidbody = true;

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
            GUILayout.Label("🔎Search & Filter", EditorStyles.boldLabel);

            _searchQuery = EditorGUILayout.TextField("Search by Name", _searchQuery);
            _tagFilter = EditorGUILayout.TagField("Filter by Tag", _tagFilter);
            _componentFilter = EditorGUILayout.TextField("Filter by Component", _componentFilter);

            ApplyFilters();
            ShowFilteredGameObjects();
            
            if(_selectedGameObject != null)
                DrawSelectedGameObject();
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

        private void DrawSelectedGameObject()
        {
            GUILayout.Space(10);
            GUILayout.Label($"Current GameObject: {_selectedGameObject.name}", EditorStyles.boldLabel);

            _showTransform = EditorGUILayout.Foldout(_showTransform, "🔄Transform");
            if (_showTransform)
            {
                Transform transform = _selectedGameObject.transform;
                transform.position = EditorGUILayout.Vector3Field("Position", transform.position);
                transform.rotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Rotation", transform.rotation.eulerAngles));
                transform.localScale = EditorGUILayout.Vector3Field("Scale", transform.localScale);
            }

            _showRenderer = EditorGUILayout.Foldout(_showRenderer, "🖼️Renderer");
            if (_showRenderer)
            {
                Renderer renderer = _selectedGameObject.GetComponent<Renderer>();
                if (renderer != null)
                    renderer.enabled = EditorGUILayout.Toggle("Renderer Enabled", renderer.enabled);
                else
                    GUILayout.Label("No Renderer founded.", EditorStyles.boldLabel);
            }

            _showRigidbody = EditorGUILayout.Foldout(_showRigidbody, "⚛️Rigidbody");
            if (_showRigidbody)
            {
                Rigidbody rb = _selectedGameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.mass = EditorGUILayout.FloatField("Mass", rb.mass);
                    rb.linearDamping = EditorGUILayout.FloatField("Linear Damping", rb.linearDamping);
                    rb.angularDamping = EditorGUILayout.FloatField("Angular Damping", rb.angularDamping);
                    rb.useGravity = EditorGUILayout.Toggle("Gravity Enabled", rb.useGravity);
                    rb.isKinematic = EditorGUILayout.Toggle("Is Kinematic", rb.isKinematic);
                }
                else
                {
                    GUILayout.Label("No Rigidbody founed.", EditorStyles.boldLabel);
                }
            }
        }
    }
}

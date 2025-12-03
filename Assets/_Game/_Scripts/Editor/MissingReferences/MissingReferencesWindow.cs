using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    /// <summary>
    /// Busca fields que estão com referência nulas no inspector
    /// </summary>
    public class MissingReferencesWindow : EditorWindow
    {
        [MenuItem("Window/Find Missing References")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(MissingReferencesWindow));
            window.maxSize = new Vector2(380, 100);
            window.minSize = window.maxSize;

            GUIContent guiContent = new GUIContent();
            guiContent.text = "Missing References Detector";

            window.titleContent = guiContent;
            window.Show();
        }

        private void OnGUI()
        {
            DrawButton();
            Repaint();            
        }
        
        private void DrawButton()
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();

            if (GUILayout.Button("Find"))
                FindMissingReferences();

            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        private void FindMissingReferences()
        {
            GameObject[] gameObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
                
                foreach(GameObject go in gameObjects)
                {
                    Component[] components = go.GetComponents<Component>();
                    foreach(Component comp in components)
                    {
                        SerializedObject serializedObject = new SerializedObject(comp);
                        SerializedProperty serializedProperty = serializedObject.GetIterator();

                        while(serializedProperty.NextVisible(true))
                        {
                            if (serializedProperty.propertyType == SerializedPropertyType.ObjectReference
                                && serializedProperty.objectReferenceValue == null)
                                Debug.LogError($"<color=red><b>Missing Reference:</b></color> Campo: {serializedProperty.name} / Componente: {comp.name} / GameObject: {go.name}", go);
                        }
                    }
                }
        }
    }
}

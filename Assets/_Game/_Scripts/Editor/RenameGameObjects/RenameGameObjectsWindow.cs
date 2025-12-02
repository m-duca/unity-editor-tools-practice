using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    /// <summary>
    /// Janela para renomear um grupo específico de GameObjects na hierarquia
    /// </summary>
    public class RenameGameObjectsWindow : EditorWindow
    {
        // Valores dos text fields
        private string _newName;
        private string _initialIndex;

        [MenuItem("Window/Rename GameObjects")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(RenameGameObjectsWindow));
            window.maxSize = new Vector2(500, 150);
            window.minSize = window.maxSize;

            GUIContent guiContent = new GUIContent();
            guiContent.text = "Rename GameObjects";
            window.titleContent = guiContent;

            window.Show();
        }

        private void OnGUI()
        {
            DrawStep1();
            DrawStep2();
            DrawStep3();

            Repaint();
        }

        private void DrawStep1()
        {
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Passo 1: Selecione os GameObjects que irão ser renomeados", EditorStyles.boldLabel);

            EditorGUILayout.Space();
        }

        private void DrawStep2()
        {
            EditorGUILayout.LabelField("Passo 2: Informe os novos valores", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField("\tInsira o novo nome");
            _newName = EditorGUILayout.TextField(_newName);
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField("\tInsira o índice inicial");
            _initialIndex = EditorGUILayout.TextField(_initialIndex);
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
        }

        private void DrawStep3()
        {
            EditorGUILayout.LabelField("Passo 3: Clique no botão abaixo", EditorStyles.boldLabel);
        }
    }
}

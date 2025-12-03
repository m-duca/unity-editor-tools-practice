using System;
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

        // Foldout Passo 2
        private bool _showFields;

        [MenuItem("Window/Rename/GameObjects")]
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

            DrawRenameButton();

            Repaint();
        }

        private void DrawStep1()
        {
            EditorGUILayout.Space();

            GUIStyle guiStyle = new GUIStyle(EditorStyles.foldout);
            guiStyle.fontStyle = FontStyle.Bold;

            _showFields = EditorGUILayout.Foldout(_showFields, "Passo 1: Informe os novos valores", guiStyle);

            if (!_showFields)
            {
                EditorGUILayout.Space();
                return;
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("\tInsira o novo nome");
            _newName = EditorGUILayout.TextField(_newName);

            EditorGUILayout.Space();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("\tInsira o índice inicial");
            _initialIndex = EditorGUILayout.TextField(_initialIndex);

            EditorGUILayout.Space();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
        }

        private void DrawStep2()
        {
            EditorGUILayout.LabelField("Passo 2: Selecione os GameObjects na Hierarquia", EditorStyles.boldLabel);

            EditorGUILayout.Space();
        }

        private void DrawStep3()
        {
            EditorGUILayout.LabelField("Passo 3: Clique no botão abaixo", EditorStyles.boldLabel);
            EditorGUILayout.Space();
        }

        private void DrawRenameButton()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();

            if (GUILayout.Button("Renomear"))
            {
                if (String.IsNullOrEmpty(_newName) || String.IsNullOrEmpty(_initialIndex))
                {
                    if (String.IsNullOrEmpty(_newName))
                        Debug.LogError("Novo nome não informado!");

                    if (String.IsNullOrEmpty(_initialIndex))
                        Debug.LogError("Índice inicial não informado!");
                }
                else
                {
                    int curIndex = int.Parse(_initialIndex);

                    Undo.RegisterCompleteObjectUndo(Selection.objects, "Rename GameObjects");

                    foreach (GameObject gameObject in Selection.objects)
                    {
                        gameObject.name = $"{_newName}_{curIndex}";
                        curIndex++;
                    }
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
        }
    }
}

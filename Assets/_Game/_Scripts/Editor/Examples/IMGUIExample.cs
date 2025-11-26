using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    /// <summary>
    /// Exemplo simples de criação de uma janela no editor
    /// </summary>
    public class IMGUIExample : EditorWindow
    {
        [MenuItem("Window/Examples/IMGUI")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(IMGUIExample));
            window.Show();            
        }

        // Preenchendo o conteúdo na Janela
        private void OnGUI()
        {
            GUILayout.Label("Exemplo de texto....");
            EditorGUILayout.Space();
            GUILayout.Button("Clique em mim!");
        }
    }
}

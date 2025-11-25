using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    public class IMGUIExample : EditorWindow
    {
        [MenuItem("Window/Example")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(IMGUIExample));
            window.Show();            
        }

        private void OnGUI()
        {
            GUILayout.Label("Exemplo de texto....");
            EditorGUILayout.Space();
            GUILayout.Button("Clique em mim!");
        }
    }
}

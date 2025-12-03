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
            window.maxSize = new Vector2(380, 120);
            window.minSize = window.maxSize;

            GUIContent guiContent = new GUIContent();
            guiContent.text = "Missing References Detector";

            window.titleContent = guiContent;
            window.Show();
        }

        private void OnGUI()
        {
            
        }
    }
}

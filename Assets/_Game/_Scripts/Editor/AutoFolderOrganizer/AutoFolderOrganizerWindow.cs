using UnityEngine;
using UnityEditor;

namespace EditorToolsPractice
{
    public class AutoFolderOrganizerWindow : EditorWindow
    {
        // Não serializadas
        private int _curTabIndex = 0;
        private string[] _tabs = { "Organize", "Extensions"};

        [MenuItem("Window/Auto Organize Folders")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(AutoFolderOrganizerWindow));
            GUIContent guiContent = new GUIContent();
            guiContent.text = "Auto Folder Organizer";
            window.titleContent = guiContent;
            window.Show();
        }

        private void OnGUI()
        {
            DrawTabToolbar();
        }

        private void DrawTabToolbar()
        {
            GUILayout.BeginHorizontal();
            _curTabIndex = GUILayout.Toolbar(_curTabIndex, _tabs);
            GUILayout.EndHorizontal();
        }
    }
}

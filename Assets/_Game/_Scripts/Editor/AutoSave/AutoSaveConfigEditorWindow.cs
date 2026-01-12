using UnityEngine;
using UnityEditor;

namespace EditorToolsPractice
{
    public class AutoSaveConfigEditorWindow : EditorWindow
    {
        private enum AutoSaveChoiceType
        {
            OneSecond = 0,
            ThirtySeconds = 1,
            OneMinute = 2,
            FiveMinutes = 3
        }

        private static EditorWindow _window;

        private int _choiceIndex;

        private const string ONE_SECOND = "1 second";
        private const string THIRTY_SECONDS = "30 seconds";
        private const string ONE_MINUTE = "1 minute";
        private const string FIVE_MINUTES = "5 minutes";

        private string[] _choices = { ONE_SECOND, THIRTY_SECONDS, ONE_MINUTE, FIVE_MINUTES };

        public static void ShowWindow()
        {
            _window = GetWindow(typeof(AutoSaveConfigEditorWindow));
            GUIContent guiContent = new GUIContent();
            guiContent.text = "AutoSave Settings";
            _window.titleContent = guiContent;
            _window.Show();
        }

        public static void CloseWindow()
        {
            if (_window == null)
                return;

            _window.Close();
        }

        private void OnGUI() 
        {
            EditorGUILayout.LabelField("Interval: ");
            EditorGUILayout.Space();
            _choiceIndex = EditorGUILayout.Popup("", _choiceIndex, _choices);
            Repaint();    
        }
    }
}

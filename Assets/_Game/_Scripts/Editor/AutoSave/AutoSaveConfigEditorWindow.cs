using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace EditorToolsPractice
{
    [ExecuteAlways]
    public class AutoSaveConfigEditorWindow : EditorWindow
    {
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

            DrawSaveTimeSelection();

            Repaint();
        }

        private void DrawSaveTimeSelection()
        {
            EditorGUI.BeginChangeCheck();

            _choiceIndex = EditorGUILayout.Popup("", _choiceIndex, _choices);

            if (EditorGUI.EndChangeCheck())
            {
                switch(_choices[_choiceIndex])
                {
                    case ONE_SECOND:
                        AutoSaveExecutor.SetInterval(1f);
                        break;
                    
                    case THIRTY_SECONDS:
                        AutoSaveExecutor.SetInterval(30f);
                        break;

                    case ONE_MINUTE:
                        AutoSaveExecutor.SetInterval(60f);
                        break;

                    case FIVE_MINUTES:
                        AutoSaveExecutor.SetInterval(300f);
                        break;
                }
            }
        }
    }
}

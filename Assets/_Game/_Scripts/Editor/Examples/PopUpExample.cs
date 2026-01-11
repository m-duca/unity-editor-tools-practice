using UnityEngine;
using UnityEditor;

namespace EditorToolsPractice
{
    /// <summary>
    /// Exemplo de como utilizar dropdowns / popups em uma EditorWindow 
    /// </summary>
    public class PopUpExample : EditorWindow
    {
        private int _curOptionIndex;
        private string[] _options = { "Option1", "Option2", "Option3" };

        [MenuItem("Window/Examples/PopUp")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(PopUpExample));
            GUIContent guiContent = new GUIContent();
            guiContent.text = "PopUpExample";
            window.titleContent = guiContent;
            window.Show();
        }

        private void OnGUI() => ShowOptionsPopUp();

        private void ShowOptionsPopUp()
        {
            _curOptionIndex = EditorGUILayout.Popup(_curOptionIndex, _options);   
        }
    }
}

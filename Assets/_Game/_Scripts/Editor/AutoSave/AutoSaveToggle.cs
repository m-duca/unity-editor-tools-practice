using UnityEngine;
using UnityEditor;

namespace EditorToolsPractice
{
    public class AutoSaveToggle : EditorWindow
    {
        private const string MENU_PATH = "File/AutoSave";

        public static bool IsEnabled
        {
            get { return EditorPrefs.GetBool(MENU_PATH, false); }
            set { EditorPrefs.SetBool(MENU_PATH, value); }
        }

        [MenuItem(MENU_PATH, false, 175)]
        public static void ToggleAutoSave()
        {
            IsEnabled = !IsEnabled;
        }

        [MenuItem(MENU_PATH, true)]
        private static bool ToggleActionValidate()
        {
            Menu.SetChecked(MENU_PATH, IsEnabled);
            return true;            
        }
    }
}

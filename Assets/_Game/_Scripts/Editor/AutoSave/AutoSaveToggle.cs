using UnityEditor;

namespace EditorToolsPractice
{
    /// <summary>
    /// Toggle para ativar / desativar o auto save
    /// </summary>
    public class AutoSaveToggle : EditorWindow
    {
        private const string MENU_PATH = "File/AutoSave";

        // Propriedades
        public static bool IsEnabled
        {
            get { return EditorPrefs.GetBool(MENU_PATH, false); }
            set { EditorPrefs.SetBool(MENU_PATH, value); }
        }

        [MenuItem(MENU_PATH, false, 175)]
        public static void ToggleAutoSave()
        {
            IsEnabled = !IsEnabled;

            if (IsEnabled) AutoSaveConfigEditorWindow.ShowWindow();
            else AutoSaveConfigEditorWindow.CloseWindow();
        }

        [MenuItem(MENU_PATH, true)]
        private static bool ToggleActionValidate()
        {
            Menu.SetChecked(MENU_PATH, IsEnabled);
            return true;            
        }
    }
}

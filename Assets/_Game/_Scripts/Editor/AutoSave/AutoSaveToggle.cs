using UnityEditor;

namespace EditorToolsPractice
{
    /// <summary>
    /// Toggle para ativar / desativar o auto save
    /// </summary>
    [InitializeOnLoad]
    public class AutoSaveToggle : EditorWindow
    {
        private const string MENU_PATH = "File/AutoSave";

        [MenuItem(MENU_PATH, false, 175)]
        public static void ToggleAutoSave()
        {
            AutoSaveSettings.IsEnabled = !AutoSaveSettings.IsEnabled;

            if (AutoSaveSettings.IsEnabled) AutoSaveConfigEditorWindow.ShowWindow();
            else AutoSaveConfigEditorWindow.CloseWindow();
        }

        [MenuItem(MENU_PATH, true)]
        private static bool ToggleActionValidate()
        {
            Menu.SetChecked(MENU_PATH, AutoSaveSettings.IsEnabled);
            return true;            
        }
    }
}

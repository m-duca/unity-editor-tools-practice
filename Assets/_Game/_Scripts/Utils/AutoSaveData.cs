using UnityEditor;

namespace EditorToolsPractice
{
    public static class AutoSaveSettings
    {
        // Propriedades
        public static bool IsEnabled
        {
            get { return EditorPrefs.GetBool("AutoSave", false); }
            set { EditorPrefs.SetBool("AutoSave", value); }
        }
    }
}

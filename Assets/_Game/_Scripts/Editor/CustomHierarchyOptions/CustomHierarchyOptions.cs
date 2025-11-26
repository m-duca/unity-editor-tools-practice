using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    [InitializeOnLoad]
    public class CustomHierarchyOptions : Editor
    {
        static CustomHierarchyOptions()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;            
        }

        private static void HierarchyWindowItemOnGUI(int idValue, Rect rectValue)
        {
            Debug.Log("Script foi chamado!");            
        }
    }
}

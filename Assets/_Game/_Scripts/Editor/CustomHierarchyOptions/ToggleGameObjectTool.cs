using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    /// <summary>
    /// Fornece um atalho na GUI da Hierarquia para agilizar a desativação / ativação de um GameObject
    /// </summary>
    [InitializeOnLoad]
    public class ToggleGameObjectTool : Editor
    {
        static ToggleGameObjectTool()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;            
        }

        private static void HierarchyWindowItemOnGUI(int id, Rect rect)
        {
            RenderButtonWithToggle(id, rect);
        }

        private static Rect DrawRect(float x, float y, float width, float height)
        {
            return new Rect(x, y, width, height);
        }

        private static void DrawButtonWithToggle(int id, float x, float y, float width, float height)
        {
            GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;

            if (gameObject == null) return;

            Rect toggleRect = DrawRect(x, y, width, height);
            gameObject.SetActive(GUI.Toggle(toggleRect, gameObject.activeSelf, string.Empty));
        }

        private static void RenderButtonWithToggle(int id, Rect rect)
        {
            float offsetX = 20;
            float offsetY = -3;
            float width = 10;
            float height = 10;

            DrawButtonWithToggle(id, rect.x - offsetX, rect.y - offsetY, width, height);
        }
    }
}

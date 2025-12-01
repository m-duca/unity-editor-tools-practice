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
        static ToggleGameObjectTool() => EditorApplication.hierarchyWindowItemOnGUI += AttachToggleItemOnGUI;

        private static void AttachToggleItemOnGUI(int id, Rect rect) => RenderButtonWithToggle(id, rect);

        private static void RenderButtonWithToggle(int id, Rect rect)
        {
            float offsetX = 20;
            float offsetY = -3;
            float width = 10;
            float height = 10;

            HierarchyGUIUtils.DrawButtonWithToggle(id, rect.x - offsetX, rect.y - offsetY, width, height);
        }
    }
}

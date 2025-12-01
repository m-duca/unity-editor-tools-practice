using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    /// <summary>
    /// Exibe um ícone na GUI da hierarquia para mostrar a descrição de um gameobject
    /// </summary>
    [InitializeOnLoad]
    public class GameObjectInfoTool : Editor
    {
        static GameObjectInfoTool() => EditorApplication.hierarchyWindowItemOnGUI += AttachInfoButton;

        private static void AttachInfoButton(int id, Rect rect) => DrawInfoButton(id, rect);

        private static void DrawInfoButton(int id, Rect rect)
        {
            float x = 150, y = 2;
            float width = 14, height = 14;
            string infoIconPath = "Icons/Spr_Icon_Info";
            string infoFallbackText = "No description";

            GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;

            if (gameObject == null) return;

            if (gameObject.GetComponent<GameObjectInfo>() == null)
                gameObject.AddComponent<GameObjectInfo>();

            string info = gameObject.GetComponent<GameObjectInfo>().InfoText;
            if (info == string.Empty)
                info = infoFallbackText;

            HierarchyGUIUtils.DrawButtonWithTexture(rect.x + x, rect.y + y, width, height, infoIconPath, () => { }, gameObject, info);
        }
    }
}

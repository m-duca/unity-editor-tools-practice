using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    /// <summary>
    /// Atalho para destruir um gameObject na hierarquia
    /// </summary>
    [InitializeOnLoad]
    public class DeleteGameObjectTool : Editor
    {
        static DeleteGameObjectTool() => EditorApplication.hierarchyWindowItemOnGUI += AttachDeleteButton;
        
        private static void AttachDeleteButton(int id, Rect rect)
        {
            GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;

            if (gameObject == null) return;

            float x = 225, y = 2;
            float width = 14, height = 14;
            string deleteIconPath = "Icons/Spr_Icon_DeleteGameObject";
            string tooltip = $"Delete {gameObject.name}";

            HierarchyGUIUtils.DrawButtonWithTexture(rect.x + x, rect.y + y, width, height, deleteIconPath, () => { DeleteGameObject(gameObject); }, 
                                                        gameObject, tooltip);
        }

        private static void DeleteGameObject(GameObject gameObject)
        {
            UnityEditor.Undo.DestroyObjectImmediate(gameObject);
        }
    }
}

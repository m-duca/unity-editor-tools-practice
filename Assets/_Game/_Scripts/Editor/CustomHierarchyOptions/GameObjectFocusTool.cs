using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    [InitializeOnLoad]
    public class GameObjectFocusTool : MonoBehaviour
    {
        static GameObjectFocusTool() => EditorApplication.hierarchyWindowItemOnGUI += AttachFocusButton;

        private static void AttachFocusButton(int id, Rect rect)
        {
            GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;

            if (gameObject == null) return;

            float x = 175, y = 2;
            float width = 14, height = 14;
            string focusIconPath = "Icons/Spr_Icon_Focus";
            string tooltip = $"Focus view on {gameObject.name}";

            HierarchyGUIUtils.DrawButtonWithTexture(rect.x + x, rect.y + y, width, height, focusIconPath, () => { FocusGameObject(gameObject); } , 
                                                        gameObject, tooltip);
        }

        private static void FocusGameObject(GameObject gameObject)
        {
            Selection.activeGameObject = gameObject;
            SceneView.FrameLastActiveSceneView();
            EditorGUIUtility.PingObject(gameObject);
        }
    }
}

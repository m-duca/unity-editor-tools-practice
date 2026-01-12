using UnityEngine;
using UnityEditor;
using System;

namespace EditorToolsPractice
{
    /// <summary>
    /// Atalho presente na Hierarquia para favoritar gameObjects
    /// </summary> <summary>
    [InitializeOnLoad]
    public class FavoriteGameObjectTool : MonoBehaviour
    {
        static FavoriteGameObjectTool() => EditorApplication.hierarchyWindowItemOnGUI += AttachFavoriteButton;

        private static void AttachFavoriteButton(int id, Rect rect) => DrawFavoriteButton(id, rect);

        private static void DrawFavoriteButton(int id, Rect rect)
        {
            GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;

            if (gameObject == null) return;

            float x = 125, y = 3;
            float width = 10, height = 10;
            string focusIconPath = "Icons/Spr_Icon_Favorite_Unfilled";
            string tooltip = "Add to Favorites";

            HierarchyGUIDrawer.DrawButtonWithTexture(rect.x + x, rect.y + y, width, height, focusIconPath, () => { } , 
                                                        gameObject, tooltip);
        }
    }
}

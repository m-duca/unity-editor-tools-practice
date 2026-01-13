using UnityEngine;
using UnityEditor;

namespace EditorToolsPractice
{
    /// <summary>
    /// Atalho presente na Hierarquia para favoritar gameObjects
    /// </summary> <summary>
    [InitializeOnLoad]
    public class FavoriteGameObjectTool : MonoBehaviour
    {
        private static string _gameObjectName;
        public static bool IsFavorited
        {
            get { return EditorPrefs.GetBool($"favorite_{_gameObjectName}", false); }
            set { EditorPrefs.SetBool($"favorite_{_gameObjectName}", value); }
        }

        static FavoriteGameObjectTool() => EditorApplication.hierarchyWindowItemOnGUI += AttachFavoriteButton;

        private static void AttachFavoriteButton(int id, Rect rect) => DrawFavoriteButton(id, rect);

        private static void DrawFavoriteButton(int id, Rect rect)
        {
            GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;

            if (gameObject == null) return;

            _gameObjectName = gameObject.name;

            float x = 125, y = 3;
            float width = 10, height = 10;
            string focusIconPath = "Icons/Spr_Icon_Favorite_Unfilled";

            if (IsFavorited)
                focusIconPath = "Icons/Spr_Icon_Favorite_Filled";

            string tooltip = "Add to Favorites";

            HierarchyGUIDrawer.DrawButtonWithTexture(rect.x + x, rect.y + y, width, height, focusIconPath, () => { ToggleIsFavoritedState(gameObject); } , 
                                                        gameObject, tooltip);
        }

        private static void ToggleIsFavoritedState(GameObject gameObject)
        {
            IsFavorited = !IsFavorited;

            if (IsFavorited)
                FavoriteGameObjetsExecutioner.AddToFavorites(gameObject);
            else
                FavoriteGameObjetsExecutioner.RemoveFromFavorites(gameObject);
        }
    }
}

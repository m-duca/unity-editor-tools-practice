using UnityEngine;
using UnityEditor;

namespace EditorToolsPractice
{
    /// <summary>
    /// Opções para criar instâncias dos prefabs favoritados 
    /// </summary>
    public class FavoriteGameObjectsWindow : EditorWindow
    {
        public static int MaxSize = 3;

        [MenuItem("CustomTools/Favorites/Favorite GameObject1", false, 50)]
        public static void HandleFavoriteItem1Clicked()
        {
            HandleFavoriteItemClicked(0);
        }

        [MenuItem("CustomTools/Favorites/Favorite GameObject1", true)]
        public static bool ValidateFavoriteItem1()
        {
            return FavoriteGameObjectsExecutioner.FavoritedObjects.Count >= 1;           
        }

        [MenuItem("CustomTools/Favorites/Favorite GameObject2", false, 50)]
        public static void HandleFavoriteItem2Clicked()
        {
            HandleFavoriteItemClicked(1);
        }

        [MenuItem("CustomTools/Favorites/Favorite GameObject2", true)]
        public static bool ValidateFavoriteItem2()
        {
            return FavoriteGameObjectsExecutioner.FavoritedObjects.Count >= 2;           
        }

        [MenuItem("CustomTools/Favorites/Favorite GameObject3", false, 50)]
        public static void HandleFavoriteItem3Clicked()
        {
            HandleFavoriteItemClicked(2);
        }

        [MenuItem("CustomTools/Favorites/Favorite GameObject3", true)]
        public static bool ValidateFavoriteItem3()
        {
            return FavoriteGameObjectsExecutioner.FavoritedObjects.Count >= 3;           
        }

        [MenuItem("CustomTools/Favorites/Clear Favorites", false, 100)]
        private static void ClearFavorites()
        {
            FavoriteGameObjectsExecutioner.Setup();
        }

        private static void HandleFavoriteItemClicked(int index)
        {
            GameObject prefab = FavoriteGameObjectsExecutioner.FavoritedObjects[index];
            GameObject newInstance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            SceneView currentSceneView = UnityEditor.SceneView.lastActiveSceneView; 
            Camera currentCamera = currentSceneView.camera;

            Vector3 spawnPosition = currentCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            newInstance.transform.position = spawnPosition;
            Selection.activeObject = newInstance;

            currentSceneView.AlignViewToObject(newInstance.transform);
            EditorGUIUtility.PingObject(newInstance);
        }
    }
}

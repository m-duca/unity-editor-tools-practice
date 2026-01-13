using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace EditorToolsPractice
{
    /// <summary>
    /// Executa toda a lógica de adicionar / remover gameObjects favoritados
    /// </summary>
    [InitializeOnLoad]
    public class FavoriteGameObjectsExecutioner
    {
        private const string PATH_FOLDER_FAVORITES = "Assets/_Game/Prefabs/Favorites";
        public static List<GameObject> FavoritedObjects = new();

        [InitializeOnLoadMethod]
        public static void Setup()
        {
            bool folderExists = AssetDatabase.IsValidFolder(PATH_FOLDER_FAVORITES);

            if (folderExists)
                AssetDatabase.DeleteAsset(PATH_FOLDER_FAVORITES);

            AssetDatabase.CreateFolder("Assets/_Game/Prefabs", "Favorites");
            FavoritedObjects.Clear();
            EditorPrefs.DeleteAll();
        }

        public static void AddToFavorites(GameObject gameObject)
        {
            if (FavoritedObjects.Count >= FavoriteGameObjectsWindow.MaxSize)
            {
                FavoriteGameObjectTool.IsFavorited = false;
                return;
            } 

            string prefabName = $"Prefab_{gameObject.name.Replace(" ", "")}.prefab";
            string prefabPath = $"{PATH_FOLDER_FAVORITES}/{prefabName}";
            AssetDatabase.DeleteAsset(prefabPath);
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(gameObject, prefabPath);
            FavoritedObjects.Add(prefab);
        }

        public static void RemoveFromFavorites(GameObject gameObject)
        {
            string prefabName = $"Prefab_{gameObject.name.Replace(" ", "")}.prefab";
            string prefabPath = $"{PATH_FOLDER_FAVORITES}/{prefabName}";

            AssetDatabase.DeleteAsset(prefabPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            FavoritedObjects.RemoveAll(obj => obj != null && obj.name == prefabName);

            Debug.Log($"<color=cyan><b>{prefabName} foi removido!</b></color>");
        }
    }
}

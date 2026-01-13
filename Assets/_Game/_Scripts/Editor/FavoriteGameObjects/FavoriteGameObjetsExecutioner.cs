using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace EditorToolsPractice
{
    public class FavoriteGameObjetsExecutioner : EditorWindow
    {
        private const string PATH_FOLDER_FAVORITES = "Assets/_Game/Prefabs/Favorites";
        private static List<GameObject> _favoritedObjects = new();

        public static void AddToFavorites(GameObject gameObject)
        {
            bool folderExists = AssetDatabase.IsValidFolder(PATH_FOLDER_FAVORITES);

            if (!folderExists)
            {
                Debug.LogError($"Pasta de favoritos não encontrada no caminho:\n{PATH_FOLDER_FAVORITES}");
                Debug.Log("Criando nova pasta...");

                AssetDatabase.CreateFolder("Assets/_Game/Prefabs", "Favorites");
            }

            string prefabName = $"Prefab_{gameObject.name.Replace(" ", "")}.prefab";
            string prefabPath = $"{PATH_FOLDER_FAVORITES}/{prefabName}";
            AssetDatabase.DeleteAsset(prefabPath);
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(gameObject, prefabPath);
            _favoritedObjects.Add(prefab);
        }

        public static void RemoveFromFavorites(GameObject gameObject)
        {
            string prefabName = $"Prefab_{gameObject.name.Replace(" ", "")}.prefab";
            string prefabPath = $"{PATH_FOLDER_FAVORITES}/{prefabName}";

            AssetDatabase.DeleteAsset(prefabPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            _favoritedObjects.RemoveAll(obj => obj != null && obj.name == prefabName);

            Debug.Log($"<color=cyan><b>{prefabName} foi removido!</b></color>");
        }
    }
}

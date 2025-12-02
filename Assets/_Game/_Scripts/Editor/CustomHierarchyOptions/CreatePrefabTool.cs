using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    /// <summary>
    /// Atalho para criaçãoo de um prefab a partir de um gameObject da hierarquia
    /// </summary>
    [InitializeOnLoad]
    public class CreatePrefabTool : Editor
    {
        static CreatePrefabTool() => EditorApplication.hierarchyWindowItemOnGUI += AttachCreatePrefabButton;

        private static void AttachCreatePrefabButton(int id, Rect rect)
        {
            GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;

            if (gameObject == null) return;

            float x = 198, y = 0;
            float width = 18, height = 18;
            string createPrefabIconPath = "Icons/Spr_Icon_CreatePrefab";
            string tooltip = $"Create prefab of {gameObject.name}";

            HierarchyGUIUtils.DrawButtonWithTexture(rect.x + x, rect.y + y, width, height, createPrefabIconPath, () => { CreatePrefab(gameObject); },
                                                        gameObject, tooltip);
        }

        private static void CreatePrefab(GameObject gameObject)
        {
            string prefabsFolderPath = "Assets/_Game/Prefabs";
            string prefabName = $"Prefab_{gameObject.name}.prefab";

            if (!AssetDatabase.IsValidFolder(prefabsFolderPath))
                AssetDatabase.CreateFolder("Assets/_Game", "Prefabs");

            // Deletando cópia antiga, caso exista
            AssetDatabase.DeleteAsset(prefabName);
            
            GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(
                gameObject,
                prefabsFolderPath + "/" + prefabName,
                InteractionMode.AutomatedAction
            );

            EditorGUIUtility.PingObject(prefab);
        }
    }
}

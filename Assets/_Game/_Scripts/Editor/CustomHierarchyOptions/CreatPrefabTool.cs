using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    [InitializeOnLoad]
    public class CreatePrefabTool : Editor
    {
        static CreatePrefabTool() => EditorApplication.hierarchyWindowItemOnGUI += AttachCreatePrefabButton;

        private static void AttachCreatePrefabButton(int id, Rect rect)
        {
            GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;

            if (gameObject == null) return;

            float x = 198, y = 2;
            float width = 14, height = 14;
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

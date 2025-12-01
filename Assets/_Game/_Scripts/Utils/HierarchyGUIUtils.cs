using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    /// <summary>
    /// Coleção de métodos de utilidade para criação de elementos na GUI da hierarquia
    /// </summary>
    public class HierarchyGUIUtils
    {
        public static Rect DrawRect(float x, float y, float width, float height)
        {
            return new Rect(x, y, width, height);
        }

        public static void DrawButtonWithToggle(int id, float x, float y, float width, float height)
        {
            GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;

            if (gameObject == null) return;

            Rect toggleRect = DrawRect(x, y, width, height);
            gameObject.SetActive(GUI.Toggle(toggleRect, gameObject.activeSelf, string.Empty));
        }
    }
}

using System;
using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    public class InfoGameObjectTool : Editor
    {
        [Header("Description")]
        [SerializeField] private string _infoText;

        static InfoGameObjectTool()
        {
            EditorApplication.hierarchyWindowItemOnGUI += AttachInfoItemOnGUI;
        }

        private static void AttachInfoItemOnGUI(int id, Rect rect)
        {

        }

        private static void DrawButtonWithTexture(float x, float y, float width, float height, string name,
        Action action, GameObject gameObject, string tooltip)
        {
            if (gameObject == null) return;

            GUIStyle guiStyle = new GUIStyle();
            guiStyle.fixedHeight = 0;
            guiStyle.fixedWidth = 0;
            guiStyle.stretchWidth = true;
            guiStyle.stretchHeight = true;
        }
    }
}

using System;
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

            Rect btnRect = DrawRect(x, y, width, height);
            gameObject.SetActive(GUI.Toggle(btnRect, gameObject.activeSelf, string.Empty));
        }

        public static void DrawButtonWithTexture(float x, float y, float width, float height, string texturePath,
        Action action, GameObject gameObject, string tooltipText)
        {
            if (gameObject == null) return;

            GUIStyle guiStyle = new GUIStyle();
            guiStyle.fixedHeight = 0;
            guiStyle.fixedWidth = 0;
            guiStyle.stretchWidth = true;
            guiStyle.stretchHeight = true;

            Rect btnRect = DrawRect(x, y, width, height);

            Texture btnIcon = Resources.Load(texturePath) as Texture;
            
            GUIContent guiContent = new GUIContent();
            guiContent.image = btnIcon;
            guiContent.text = string.Empty;
            guiContent.tooltip = tooltipText;
            
            bool isClicked = GUI.Button(btnRect, guiContent, guiStyle);

            if (isClicked) 
                action?.Invoke();
        }
    }
}

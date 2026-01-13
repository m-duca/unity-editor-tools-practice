using UnityEngine;
using UnityEditor;
using System;

namespace EditorToolsPractice
{
    /// <summary>
    /// Custom Editor que modifica o inspector do TextureImporter, adicionando um novo campo para otimizar o maxTextureSize da Sprite
    /// </summary>
    [CustomEditor(typeof(TextureImporter), true)]
    public class SpriteOptimizer : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if(GUILayout.Button("Optimize"))
            {
                string path = AssetDatabase.GetAssetPath(Selection.activeObject);
                TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(path);

                int width, height;

                importer.GetSourceTextureWidthAndHeight(out width, out height);

                int maxDimensionSize = Mathf.Max(width, height);

                TextureImporterSettings textureImporterSettings = new TextureImporterSettings();

                importer.ReadTextureSettings(textureImporterSettings);

                textureImporterSettings.maxTextureSize = (int)Mathf.Pow(2, (int)Math.Log(maxDimensionSize - 1, 2) + 1);
                importer.SetTextureSettings(textureImporterSettings);

                EditorUtility.SetDirty(importer);
                importer.SaveAndReimport();
            }
        }
    }
}

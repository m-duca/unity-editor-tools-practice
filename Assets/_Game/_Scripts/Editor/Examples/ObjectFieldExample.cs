using UnityEngine;
using UnityEditor;

namespace EditorToolsPractice
{
    /// <summary>
    /// Exemplo de como utilizar o ObjectField em EditorWindows
    /// </summary>
    public class ObjectFieldExample : EditorWindow
    {
        private Object _obj;

        [MenuItem("Window/Examples/Object Field")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(ObjectFieldExample));
            GUIContent guiContent = new GUIContent();
            guiContent.text = "Object Field Example";
            window.titleContent = guiContent;
            window.Show();
        }

        private void OnGUI() => DrawExample();
            
        private void DrawExample()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Select a GameObject");
            SetupObjectField();
            EditorGUILayout.EndHorizontal();
        }

        private void SetupObjectField()
        {
            _obj = EditorGUILayout.ObjectField(_obj, typeof(GameObject), true);   
        }
    }
}

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace EditorToolsPractice
{
    /// <summary>
    /// Executa o auto save conforme as configurações atuais da Tool
    /// </summary>
    [InitializeOnLoad]
    public static class AutoSaveExecutor
    {
        private static float _saveInterval = 1f;
        private static float _nextSave = 0f;

        static AutoSaveExecutor()
        {
            EditorApplication.update += Execute;
        }

        private static void Execute()
        {
            if (!AutoSaveSettings.IsEnabled)
                return;

            if ((float)EditorApplication.timeSinceStartup > _nextSave)
            {
                var scene = EditorSceneManager.GetActiveScene();
                string[] path = scene.path.Split(char.Parse("/"));

                if (!scene.isDirty)
                    return;

                bool saveHasSucceed = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), string.Join("/", path));;

                if (saveHasSucceed)
                    Debug.Log("<color=green><b>AutoSave feito com sucesso!</b></color>");
                else
                    Debug.LogError("<b>Falha no AutoSave!</b>");

                _nextSave = (float)EditorApplication.timeSinceStartup + _saveInterval;
            }
        }

        public static void SetInterval(float intervalValue)
        {
            _saveInterval = intervalValue;
        }
    }
}

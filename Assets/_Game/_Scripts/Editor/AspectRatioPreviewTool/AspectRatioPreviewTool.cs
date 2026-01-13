using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace EditorToolsPractice
{
    /// <summary>
    /// Atalho para visualizar o conteúdo da gameview em diferentes aspects
    /// Gerando imagens de exemplo na pasta de Assets
    /// </summary>
    [InitializeOnLoad]
    public class AspectRatioPreviewTool : Editor
    {
        private static string _applicationDataPath = Application.dataPath + "/";
        private static int _screenshotsCount = 0;
        private static bool _menuOptionWasClicked = false;
        private static bool _startedScreenshot = false;
        private static List<string> _aspectRatios = new List<string>()
        {
            _applicationDataPath + "Free Aspect.png",
            _applicationDataPath + "Aspect 16x9.png",
            _applicationDataPath + "Aspect 16x10.png",
        };

        static AspectRatioPreviewTool()
        {
            EditorApplication.update += Execute;
        }

        [MenuItem("CustomTools/Preview Aspect Ratios")]
        private static void PreviewClicked()
        {
            _menuOptionWasClicked = true;
            _startedScreenshot = false;
            _screenshotsCount = 0;
        }

        private static void Execute()
        {
            if (_menuOptionWasClicked && !_startedScreenshot)
            {
                _startedScreenshot = true;
                SaveScreenshotAtAspectRatio(_screenshotsCount, _aspectRatios[_screenshotsCount]);
            }

            if (_screenshotsCount < _aspectRatios.Count && System.IO.File.Exists(_aspectRatios[_screenshotsCount]))
            {
                _screenshotsCount++;
                _startedScreenshot = false;
                AssetDatabase.Refresh();
            }

            if (_screenshotsCount == _aspectRatios.Count)
                _menuOptionWasClicked = false;
        }

        private static void SetSize(int index)
        {
            Type gameViewWindowType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
            EditorWindow gameViewWindow = EditorWindow.GetWindow(gameViewWindowType);
            var sizeSelectionCallBack = gameViewWindowType.GetMethod("SizeSelectionCallback", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            sizeSelectionCallBack.Invoke(gameViewWindow, new object[] {index, null});
        }

        private static void TakeScreenshot(string fileName)
        {
            ScreenCapture.CaptureScreenshot(fileName);
        }

        private static void SaveScreenshotAtAspectRatio(int index, string fileName)
        {
            SetSize(index);
            TakeScreenshot(fileName);
        }
    }
}

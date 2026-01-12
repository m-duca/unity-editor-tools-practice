using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace EditorToolsPractice
{
    public class AutoFolderOrganizerWindow : EditorWindow
    {
        // Não serializadas
        private int _curTabIndex = 0;
        private string[] _tabs = { "Organize", "Extensions"};
        
        private int _rowsCount;
        private List<AssetTypeRow> _rows; 
        
        private int _extensionsCount;
        
        private bool _isDirty = false;
        
        private string[] _assetTypeNames;
        private Dictionary<string, List<string>> _assetTypes = new Dictionary<string, List<string>>
        {
            { "Prefabs", new List<string>(){".prefab"} },
            { "Animations", new List<string>(){".anim"} },
            { "Sprites", new List<string>(){".png, .jpeg"} }
        };

        private enum AutoFolderTabType
        {
            Organizer = 0,
            Extensions = 1
        }

        [MenuItem("Window/Auto Organize Folders")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(AutoFolderOrganizerWindow));
            GUIContent guiContent = new GUIContent();
            guiContent.text = "Auto Folder Organizer";
            window.titleContent = guiContent;
            window.Show();
        }

        private void Awake()
        {
            InitializeFields();
        }

        private void InitializeFields()
        {
            foreach(string key in _assetTypes.Keys)
                _extensionsCount += _assetTypes[key].Count;                

            _rowsCount = _extensionsCount;
            _rows = new List<AssetTypeRow>();

            _assetTypeNames = new string[_extensionsCount];
            _assetTypes.Keys.CopyTo(_assetTypeNames, 0);

            for (int i = 0; i < _extensionsCount; i++)
            {
                string name = _assetTypeNames[i];

                if (name != null)
                {
                    int extensionsForNameCount = _assetTypes[name].Count;

                    for (int j = 0; j  < extensionsForNameCount; j++)
                        _rows.Add(new AssetTypeRow(_assetTypeNames[i], _assetTypes[_assetTypeNames[i]][j]));
                }
            }
        }

        private void OnGUI()
        {
            DrawTabToolbar();
            GUILayout.Space(20);

            if (_curTabIndex == (int)AutoFolderTabType.Organizer)
                ShowOrganizerGUI();
            else
                ShowExtensionsGUI();
        }

        private void DrawTabToolbar()
        {
            GUILayout.BeginHorizontal();
            _curTabIndex = GUILayout.Toolbar(_curTabIndex, _tabs);
            GUILayout.EndHorizontal();
        }

        private void ShowOrganizerGUI()
        {
            if (!_isDirty) 
                return;

            _isDirty = false;
            UpdateAssetTypes(_assetTypeNames.Length);
        }

        private void UpdateAssetTypes(int curIndex)
        {
            _assetTypes.Add(_rows[curIndex].Name, new List<string>() { });
            _assetTypes[_rows[curIndex].Name].Add(_rows[curIndex].FileExtension);

            _extensionsCount = 0;

            foreach(string key in _assetTypes.Keys)
                _extensionsCount += _assetTypes[key].Count;
        }

        private void ShowExtensionsGUI()
        {
            for (int i = 0; i < _rowsCount; i++)
                DrawRow(i);
        }

        private void DrawRow(int curIndex)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("Name");
            EditorGUI.BeginChangeCheck();

            if (_rows != null)
                _rows[curIndex].Name = EditorGUILayout.TextField(_rows[curIndex].Name);

            // Caso a Linha for atualizada
            if (EditorGUI.EndChangeCheck())
                _isDirty = true;

            GUILayout.EndVertical();
            EditorGUILayout.Space();

            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("File Extension");
            EditorGUI.BeginChangeCheck();

            if (_rows != null)
                _rows[curIndex].FileExtension = EditorGUILayout.TextField(_rows[curIndex].FileExtension);
            
            if (EditorGUI.EndChangeCheck() && _assetTypes.ContainsKey(_rows[curIndex].Name))
                _isDirty = true;
            
            GUILayout.EndVertical();
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }
    }
}

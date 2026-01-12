using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace EditorToolsPractice
{
    /// <summary>
    /// Janela que organiza de maneira automática os arquivos nas suas devidas pastas, seguindo extensão / caminho da pasta 
    /// </summary> <summary>
    public class AutoFolderOrganizerWindow : EditorWindow
    {
        // Não serializadas
        private enum AutoFolderTabType
        {
            Organizer = 0,
            Extensions = 1
        }

        private int _curTabIndex = 0;
        private string[] _tabs = { "Organize", "Extensions" };

        private int _assetTypeRowsCount;
        private List<AssetTypeRow> _assetTypeRows;

        private int _extensionsCount;

        private string[] _assetTypeNames;
        private Dictionary<string, List<string>> _assetTypes = new Dictionary<string, List<string>>
        {
            { "Prefabs", new List<string>(){".prefab"} },
            { "Animations", new List<string>(){".anim"} },
            { "Sprites", new List<string>(){".png", ".jpeg"} }
        };

        private int _organizersRowsCount;
        private List<OrganizerRow> _organizerRows;
        private const string DEFAULT_PATH = "Assets/_Game/";

        private bool _isDirty = false;

        [MenuItem("Window/Auto Organize Folders")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(AutoFolderOrganizerWindow));
            GUIContent guiContent = new GUIContent();
            guiContent.text = "Auto Folder Organizer";
            window.titleContent = guiContent;
            window.Show();
        }

        private void Awake() => InitializeFields();

        private void InitializeFields()
        {
            foreach (string key in _assetTypes.Keys)
                _extensionsCount += _assetTypes[key].Count;

            _assetTypeRowsCount = _extensionsCount;
            _assetTypeRows = new List<AssetTypeRow>();

            _assetTypeNames = new string[_extensionsCount];
            _assetTypes.Keys.CopyTo(_assetTypeNames, 0);

            for (int i = 0; i < _extensionsCount; i++)
            {
                string name = _assetTypeNames[i];

                if (name != null)
                {
                    int extensionsForNameCount = _assetTypes[name].Count;

                    for (int j = 0; j < extensionsForNameCount; j++)
                        _assetTypeRows.Add(new AssetTypeRow(_assetTypeNames[i], _assetTypes[_assetTypeNames[i]][j]));
                }
            }

            _organizersRowsCount = _assetTypes.Keys.Count;
            _organizerRows = new List<OrganizerRow>();

            for (int i = 0; i < _organizersRowsCount; i++)
                _organizerRows.Add(new OrganizerRow(i, DEFAULT_PATH + _assetTypeNames[i]));
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

        private void DrawAddAndRemoveControls()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(position.width - 80);    

            DrawAddControl();
            DrawRemoveControl();

            GUILayout.EndHorizontal();
        }

        private void DrawAddControl()
        {
            GUIContent guiContentAdd = new GUIContent();
            guiContentAdd.text = "+";
            if (GUILayout.Button(guiContentAdd, GUILayout.ExpandWidth(false)))
            {
                if (_curTabIndex == (int)AutoFolderTabType.Organizer)
                {
                    _organizersRowsCount++;
                    _organizerRows.Add(new OrganizerRow(0, ""));
                }
                else
                {
                    _assetTypeRowsCount++;
                    _assetTypeRows.Add(new AssetTypeRow("", ""));
                }
            }
        }

        private void DrawRemoveControl()
        {
            GUIContent guiContentRemove = new GUIContent();
            guiContentRemove.text = "—";
            if (GUILayout.Button(guiContentRemove, GUILayout.ExpandWidth(false)))
            {
                if (_curTabIndex == (int)AutoFolderTabType.Organizer)
                {
                    _organizersRowsCount--;
                    _organizerRows.RemoveAt(_organizerRows.Count - 1);
                }
                else
                {
                    _assetTypeRowsCount--;
                    _assetTypeRows.RemoveAt(_assetTypeRows.Count - 1);
                }
            }
        }

        private void DrawOrganizerControl()
        {
            if (GUILayout.Button("Organize"))
                OrganizeFilesIntoFolders();
        }

        private void OrganizeFilesIntoFolders()
        {
            Dictionary<string, string> extensionsToFoldersMap = new ();

            foreach(string name in _assetTypes.Keys)
            {
                for (int i = 0; i < _assetTypes[name].Count; i++)
                {
                    string folderPath = DEFAULT_PATH + name + "/";
                    extensionsToFoldersMap.Add(_assetTypes[name][i], folderPath);
                }
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(DEFAULT_PATH);
            foreach(string extension in extensionsToFoldersMap.Keys)
            {
                string query = $"*{extension}";
                FileInfo[] files = directoryInfo.GetFiles(query);
                foreach(FileInfo file in files)
                {
                    string filePath = extensionsToFoldersMap[extension] + file.Name;
                    AssetDatabase.MoveAsset(DEFAULT_PATH + file.Name, filePath);
                }
            }
        }

        #region Organizer

        private void ShowOrganizerGUI()
        {
            if (_isDirty)
            {
                _isDirty = false;
                UpdateAssetTypes(_assetTypeNames.Length);
            }

            for (int i = 0; i < _organizersRowsCount; i++)
                DrawOrganizerRow(i);

            DrawAddAndRemoveControls();

            GUILayout.Space(40);
            DrawOrganizerControl();
        }

        private void DrawOrganizerRow(int curIndex)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("Asset Type");
            EditorGUI.BeginChangeCheck();
            _organizerRows[curIndex].SelectionIndex = EditorGUILayout.Popup(
                "",
                _organizerRows[curIndex].SelectionIndex,
                _assetTypeNames
            );

            if (EditorGUI.EndChangeCheck())
                _organizerRows[curIndex].FolderPath = DEFAULT_PATH + _assetTypeNames[_organizerRows[curIndex].SelectionIndex];

            GUILayout.EndVertical();
            EditorGUILayout.Space();

            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("Path to Folder");
            _organizerRows[curIndex].FolderPath = EditorGUILayout.TextField(_organizerRows[curIndex].FolderPath);
            GUILayout.EndVertical();
            EditorGUILayout.Space();

            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("Select Folder");
            EditorGUI.BeginChangeCheck();
            _organizerRows[curIndex].Obj = EditorGUILayout.ObjectField(_organizerRows[curIndex].Obj, typeof(UnityEditor.DefaultAsset), true);

            if (EditorGUI.EndChangeCheck())
                _organizerRows[curIndex].FolderPath = DEFAULT_PATH + _organizerRows[curIndex].Obj.name;
            GUILayout.EndVertical();
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }

        #endregion

        #region Extensions

        private void UpdateAssetTypes(int curIndex)
        {
            _assetTypes.Add(_assetTypeRows[curIndex].Name, new List<string>() { });
            _assetTypes[_assetTypeRows[curIndex].Name].Add(_assetTypeRows[curIndex].FileExtension);

            _extensionsCount = 0;
            foreach (string key in _assetTypes.Keys)
                _extensionsCount += _assetTypes[key].Count;

            _assetTypeNames = new string[_extensionsCount - 1];
            _assetTypes.Keys.CopyTo(_assetTypeNames, 0);
        }

        private void ShowExtensionsGUI()
        {
            for (int i = 0; i < _assetTypeRowsCount; i++)
                DrawAssetTypeRow(i);
        
            DrawAddAndRemoveControls();
        }

        private void DrawAssetTypeRow(int curIndex)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("Name");
            EditorGUI.BeginChangeCheck();

            if (_assetTypeRows != null)
                _assetTypeRows[curIndex].Name = EditorGUILayout.TextField(_assetTypeRows[curIndex].Name);

            // Caso a Linha for atualizada
            if (EditorGUI.EndChangeCheck())
                _isDirty = true;

            GUILayout.EndVertical();
            EditorGUILayout.Space();

            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("File Extension");
            EditorGUI.BeginChangeCheck();

            if (_assetTypeRows != null)
                _assetTypeRows[curIndex].FileExtension = EditorGUILayout.TextField(_assetTypeRows[curIndex].FileExtension);

            if (EditorGUI.EndChangeCheck() && _assetTypes.ContainsKey(_assetTypeRows[curIndex].Name))
                _isDirty = true;

            GUILayout.EndVertical();
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }

        #endregion
    }
}

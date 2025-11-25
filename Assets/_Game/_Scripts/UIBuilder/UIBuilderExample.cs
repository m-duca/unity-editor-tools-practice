using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBuilderExample : EditorWindow
{
    [SerializeField] private VisualTreeAsset _visualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/UIBuilderExample")]
    public static void ShowExample()
    {
        UIBuilderExample wnd = GetWindow<UIBuilderExample>();
        wnd.titleContent = new GUIContent("UIBuilderExample");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        VisualElement labelFromUXML = _visualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }
}

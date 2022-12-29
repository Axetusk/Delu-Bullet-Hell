using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class LevelEditor : EditorWindow
{
    [MenuItem("Window/UI Toolkit/LevelEditor")]
    public static void ShowLevelEditor()
    {
        LevelEditor wnd = GetWindow<LevelEditor>();
        wnd.titleContent = new GUIContent("LevelEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Delu Bullet Hell/Editor/LevelEditor.uss");

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Delu Bullet Hell/Editor/LevelEditor.uxml");
        VisualElement treeElement = visualTree.Instantiate();
        treeElement.styleSheets.Add(styleSheet);
        root.Add(treeElement);
    }
}
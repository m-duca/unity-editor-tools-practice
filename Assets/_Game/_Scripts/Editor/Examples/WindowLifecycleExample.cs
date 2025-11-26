using UnityEditor;
using UnityEngine;

namespace EditorToolsPractice
{
    /// <summary>
    /// Várias chamadas exemplificando o ciclo de vida um script de editor
    /// </summary>
    public class WindowLifecycleExample : EditorWindow
    {
        [MenuItem("Window/Examples/LifecycleExample")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(WindowLifecycleExample));
            window.Show();            
        }

        // Chamado após a janela ser aberta
        public void Awake() => Debug.Log("Awake() foi chamado!");

        // Chamado após o root visual element estiver pronto para ser preenchido
        public void CreateGUI() => Debug.Log("CreateGUI() foi chamado!");

        // Chamado após a janela ser adicionada ao content view
        public void OnBecameVisible() => Debug.Log("OnBecameVisibile() foi chamado!");

        // Chamado após a janela ser focada através do teclado
        public void OnFocus() => Debug.Log("OnFocus() foi chamado!");

        // Utilizado para implementar a interface da janela que criamos, sendo chamado repetidamente para renderizar o conteúdo
        public void OnGUI() 
        {
            GUILayout.Label("<====== Cheque o Console");
            Debug.Log("OnGUI() foi chamado!");
        }

        // Chamado após um / vários gameobject(s) mudam na hierarquia
        public void OnHierarchyChange() => Debug.Log("OnHierarchyChange() foi chamado!");

        // Chamado 10 vezes por frame
        public void OnInspectorUpdate() => Debug.Log("OnInspectorUpdate() foi chamado!");

        // Chamado quando há uma mudança no estado atual do Projeto
        public void OnProjectChange() => Debug.Log("OnProjectChange() foi chamado!");

        // Chamado após a seleção mudar
        public void OnSelectionChange() => Debug.Log("OnSelectionChange() foi chamado!");

        // Similar ao Update do MonoBehaviour, é chamado por frame em todas as janelas visíveis
        public void Update() => Debug.Log("Update() foi chamado!");

        // Chamado após a perca de foco pelo teclado
        public void OnLostFocus() => Debug.Log("OnLostFocus() foi chamado!");

        // Chamado após a remoção da janela de um content view
        public void OnBecameInvisible() => Debug.Log("OnBecameInvisible() foi chamado!");

        // Chamado após a janela ser fechada
        public void OnDestroy() => Debug.Log("OnDestroy() foi chamado!");
    }
}

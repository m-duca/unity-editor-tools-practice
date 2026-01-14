using UnityEngine;
using UnityEditor;

namespace EditorToolsPractice
{
    /// <summary>
    /// Exemplos utilizando as tags de inicialização para classes e métodos
    /// </summary>
    [InitializeOnLoad]
    public class InitializeOnLoadExample : MonoBehaviour
    {
        static InitializeOnLoadExample()
        {
            Debug.Log("Test 1");
        }

        [InitializeOnLoadMethod]
        private static void ShowInitializeDebug()
        {
            Debug.Log("Test 2");
        }
    }
}

using UnityEngine;

namespace EditorToolsPractice
{
    /// <summary>
    /// Exemplo prático da ferramenta de AutoLink de refs serializadas
    /// </summary> <summary>
    public class AutoLinkingExample : MonoBehaviour
    {
        [Header("Referências")]
        [SerializeField] private GameObject Linking1;
        [SerializeField] private GameObject Linking2;
        [SerializeField] private GameObject Linking3;
    
        private void OnValidate()
        {
            CheckIsNull(Linking1);
            CheckIsNull(Linking2);
            CheckIsNull(Linking3);
        }

        private void CheckIsNull(GameObject target)
        {
            if (target != null)
                Debug.Log($"<color=green><b>{target.name} atribuído!</b></color>");
            else
                Debug.LogError($"{target.name} não atribuído!");
        }
    }
}

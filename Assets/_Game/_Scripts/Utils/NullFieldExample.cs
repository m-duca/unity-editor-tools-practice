using UnityEngine;

namespace EditorToolsPractice
{
    /// <summary>
    /// Exemplo da ferramenta de Missing References
    /// </summary>
    public class NullFieldExample : MonoBehaviour
    {
        [Header("<color=yellow>Não preencher!</color>")]
        [SerializeField] private GameObject _field;
    }
}

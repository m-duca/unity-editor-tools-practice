using UnityEngine;

namespace EditorToolsPractice
{
    /// <summary>
    /// Armazena o texto da descrição do GameObject
    /// </summary>
    public class GameObjectInfo : MonoBehaviour
    {
        [Header("Descrição do GameObject")]
        [SerializeField, TextArea(2, 3)] private string _infoText;

        // Propriedade
        public string InfoText { get { return _infoText; } }
    }
}

using UnityEngine;

namespace EditorToolsPractice
{
    /// <summary>
    /// Utilizado para informar o conteúdo das linhas de organização da Auto Folder Organizer Window
    /// </summary>
    public class OrganizerRow : MonoBehaviour
    {
        public int SelectionIndex;
        public string FolderPath;
        public Object Obj;

        public OrganizerRow(int selectionIndex, string folderPath)
        {
            this.SelectionIndex = selectionIndex;
            this.FolderPath = folderPath;
        }
    }
}

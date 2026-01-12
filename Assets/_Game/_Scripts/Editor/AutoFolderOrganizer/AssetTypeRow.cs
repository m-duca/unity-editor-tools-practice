namespace EditorToolsPractice
{
    /// <summary>
    /// Utilizado para filtrar novos tipos de assets / suas extensões na Auto Folder Organizer Window
    /// </summary>
    public class AssetTypeRow
    {
        public string Name;
        public string FileExtension;

        public AssetTypeRow(string name, string fileExtension)
        {
            this.Name = name;
            this.FileExtension = fileExtension;
        }
    }
}

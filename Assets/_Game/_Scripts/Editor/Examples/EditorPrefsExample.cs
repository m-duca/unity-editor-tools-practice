using UnityEngine;
using UnityEditor;

namespace EditorToolsPractice
{
    /// <summary>
    /// Exemplifica como salvar / manipular dados relacionados as nossas ferramentas de Editor
    /// </summary>
    public class EditorPrefsExample : MonoBehaviour
    {
        [InitializeOnLoadMethod]
        private static void SaveData()
        {
            string boolKeyName = "boolExample";
            string intKeyName = "intExample";
            string floatKeyName = "floatExample";
            string stringKeyName = "stringExample";

            EditorPrefs.SetBool(boolKeyName, true);
            EditorPrefs.SetInt(intKeyName, 2);
            EditorPrefs.SetFloat(floatKeyName, 3.0f);
            EditorPrefs.SetString(stringKeyName, "Saving Data...");

            Debug.Log($"{boolKeyName} possui o valor: {EditorPrefs.GetBool(boolKeyName)}");
            Debug.Log($"{intKeyName} possui o valor: {EditorPrefs.GetInt(intKeyName)}");
            Debug.Log($"{floatKeyName} possui o valor: {EditorPrefs.GetFloat(floatKeyName)}");

            if (EditorPrefs.HasKey(stringKeyName))
            {
                EditorPrefs.DeleteKey(stringKeyName);
                Debug.Log($"<color=yellow>{stringKeyName} foi removida com sucesso!</color>");
            }
        }
    }
}

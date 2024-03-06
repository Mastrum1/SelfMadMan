#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(MiniGameSelector))]
public class DictionaryDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MiniGameSelector script = (MiniGameSelector)target;

        // Iterate through the dictionary and display key-value pairs in the Inspector
        foreach (var pair in script.MinigameLists)
        {
            EditorGUILayout.LabelField(pair.Key, string.Join(", ", pair.Value.ToArray()));
        }
    }
}
#endif
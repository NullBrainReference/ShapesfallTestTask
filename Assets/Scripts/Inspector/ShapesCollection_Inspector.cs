using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShapesCollection))]
public class ShapesCollection_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ShapesCollection collection = (ShapesCollection)target;

        if (GUILayout.Button("Refresh"))
        {
            collection.Refresh();
            EditorUtility.SetDirty(collection);
        }
    }
}

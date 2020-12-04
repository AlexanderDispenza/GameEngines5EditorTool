using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectTagChanger : EditorWindow
{
    public string objectTag = "";
    public string tagSearch = "";

    [MenuItem("DevTools/Object Tag Changer")]
    private static void OnCreate()
    {
        // setting Window pos
        EditorWindow window = GetWindow<ObjectTagChanger>();
        window.position = new Rect(Screen.width, Screen.height * 1.5f, 500, 350);
        window.Show();
    }

    public void OnGUI()
    {
        tagSearch = EditorGUI.TagField(new Rect(0, 0, position.width / 1.5f, 20), "Select All tags with: ", tagSearch);

        if (GUI.Button(new Rect(0, 30, 170, 17), "Find Object(s)"))
        {
            Debug.Log("Searching for: " + tagSearch);

            // find gameobjects with the searched tags and select them. 
            GameObject[] gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            List<GameObject> taggedObjects = new List<GameObject>();

            foreach (GameObject go in gameObjects)
            {
                if (go.tag == tagSearch)
                {
                    taggedObjects.Add(go);
                    Selection.objects = taggedObjects.ToArray();
                }
            }
        }

        objectTag = EditorGUI.TagField(new Rect(0, 50, position.width / 1.5f, 20), "New Tag: ", objectTag);

        //Check if theres a object highlighted in the Hierachy 
        if (Selection.activeObject)
        {
            if (GUI.Button(new Rect(0, 90, 170, 17), "Change Object tag"))
            {
                foreach (GameObject gameObject in Selection.gameObjects)
                {
                    // change selected gameobject tag to new desired tag.
                    gameObject.tag = objectTag;
                }
            }
        }
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }

}

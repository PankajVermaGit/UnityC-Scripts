using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;
using System.Reflection;

[ExecuteInEditMode]
public class AutoUpdate : MonoBehaviour {

    public int currentSceneIndex = 0;
    //public GameObject g;

    public void Awake()
    {
        currentSceneIndex = EditorSceneManager.GetActiveScene().buildIndex;
        EditorSceneManager.sceneOpened += SceneOpenedCallback;
    }

    void CopyComponent(Component original, GameObject destination)
    {
        UnityEditorInternal.ComponentUtility.CopyComponent(original);
        if (destination.GetComponent(original.GetType()) == null)
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(destination);
        else
        UnityEditorInternal.ComponentUtility.PasteComponentValues(original);
    }

    // Use this for initialization
    void Start () {

        // g = GameObject.Find("GameObject");

        //Component[] comp = GameObject.Find("Directional Light").GetComponents<Component>(); 

        //foreach(Component c in comp)
        //{
        //    if(c.GetType()==typeof(Transform))
        //    {
        //        continue;
        //    }

        //    CopyComponent(c, g); 
        //}

        TaskInScene();

        for (int i=0;i<SceneManager.sceneCountInBuildSettings;i++)
        {
            if (EditorSceneManager.GetActiveScene().buildIndex==i)
                continue;

            print(i);

            EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(i),OpenSceneMode.Additive);

        }

        //EditorSceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
        //DestroyImmediate(GetComponent<AutoUpdate>());
        EditorSceneManager.SaveOpenScenes();

    }

    void SceneOpenedCallback(Scene scene, OpenSceneMode mode)
    {
        if (currentSceneIndex == scene.buildIndex)
        {
            print("Found Current Scene");
            return;
        }
        EditorSceneManager.SetActiveScene(scene);
        TaskInScene();
        EditorSceneManager.CloseScene(scene,true);
        
    }


    void TaskInScene()
    {
        GameObject g = new GameObject();
        g.name = "Hello";
    }



}

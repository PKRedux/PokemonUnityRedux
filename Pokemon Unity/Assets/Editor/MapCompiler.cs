using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
 
public class MapCompiler : EditorWindow
{ 
    //private static int totalSprites = 2; // change depending on the total sprites in this batch!
    [MenuItem("Pokémon Unity/Compile Selected Map")]
    static void CompileMenu()
    {
        Compile(Selection.activeGameObject);
    }
    public static void Compile(GameObject compileObj)
    {
        Debug.Log("Compiling map...");
        Mesh map = new Mesh();
        if(!compileObj.transform.GetComponent<MeshFilter>())
        {
            compileObj.AddComponent<MeshFilter>();
        }
        if(!compileObj.transform.GetComponent<MeshCollider>())
        {
            compileObj.AddComponent<MeshCollider>();
        }
        compileObj.transform.GetComponent<MeshFilter>().mesh = null;
        MeshFilter[] meshFilters = compileObj.transform.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length) {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            i++;
        }
		map.CombineMeshes(combine);
        compileObj.transform.GetComponent<MeshFilter>().sharedMesh = map;
		compileObj.transform.GetComponent<MeshCollider>().sharedMesh = compileObj.transform.GetComponent<MeshFilter>().sharedMesh;
        compileObj.SetActive(true);
    }
    // Validate the menu item defined by the function above.
    // The menu item will be disabled if this function returns false.
    [MenuItem("Pokémon Unity/Compile Selected Map", true)]
    static bool ValidateCompile()
    {
        // Return false if no gameobject is selected.
        return Selection.activeGameObject != null;
    }
}
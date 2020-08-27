/// Source: Pokémon Unity Redux
/// Purpose: Global scene variables for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace PokemonUnity.Frontend.Global {
public class GlobalScript : MonoBehaviour
{
    public static GlobalScript global;
    public SceneActivity sceneActivity;
    void OnDestroy()
    {
        //SceneManager.sceneLoaded -= CheckLevelLoaded;
    }
    void Awake()
    {
        //SceneManager.sceneLoaded += CheckLevelLoaded;
        if (global == null)
        {
            global = this;
            Object.DontDestroyOnLoad(this.gameObject);
            Display.updateResolution();
            sceneActivity = gameObject.AddComponent<SceneActivity>();
        }
        else if (global != this)
        {
            Destroy(gameObject);
        }
    }
}
}

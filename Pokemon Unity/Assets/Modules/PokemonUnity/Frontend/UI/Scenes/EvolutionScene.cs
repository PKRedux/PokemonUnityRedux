/// Source: Pokémon Unity Redux
/// Purpose: Evolution UI (temporary) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio

using System.Collections;
using PokemonUnity.Backend.Serializables;
using UnityEngine;
namespace PokemonUnity.Frontend.UI.Scenes {
public class EvolutionScene : BaseScene
{
    public IEnumerator control(Pokemon pokemon, string how)
    {
        //TODO
        yield return StartCoroutine(SceneScript.main.Dialog.DrawText("This menu has not yet been implemented."));   
    }
}
}

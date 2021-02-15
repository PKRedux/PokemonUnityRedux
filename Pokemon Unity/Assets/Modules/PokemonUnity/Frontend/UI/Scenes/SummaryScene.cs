/// Source: Pokémon Unity Redux
/// Purpose: Summary UI (temporary) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio

using System;
using System.Collections;
using PokemonUnity.Backend.Serializables;
using UnityEngine;
using UnityEngine.UI;
namespace PokemonUnity.Frontend.UI.Scenes {
public class SummaryScene : BaseScene
{
    public IEnumerator control(Pokemon[] pokemonList, int currentPosition = 0, bool learning = false,
        string newMoveString = null)
    {
        //TODO
        yield return StartCoroutine(SceneScript.main.Dialog.DrawText("This menu has not yet been implemented."));
    }
}
}

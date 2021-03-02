/// Source: Pokémon Unity Redux
/// Purpose: Bag UI (temporary) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PokemonUnity.Frontend.UI;
namespace PokemonUnity.Frontend.UI.Scenes {
public class BagScene : BaseScene
{
    public string chosenItem = "";
    public IEnumerator control(bool partyAccessible = true, bool getItem = false)
    {
        //TODO
        yield return StartCoroutine(SceneScript.main.Dialog.DrawText("This menu has not yet been implemented."));
    }
}
}

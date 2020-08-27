/// Source: Pokémon Unity Redux
/// Purpose: Interaction object for redirecting input to another target for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using System.Collections;
namespace PokemonUnity.Frontend.Interacts {
public class InteractRedirect : MonoBehaviour
{
    public GameObject target;

    private IEnumerator interact()
    {
        target.SendMessage("interact", SendMessageOptions.DontRequireReceiver);
        yield return null;
    }

    private IEnumerator bump()
    {
        target.SendMessage("bump", SendMessageOptions.DontRequireReceiver);
        yield return null;
    }
}
}

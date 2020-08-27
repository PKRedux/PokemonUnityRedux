/// Source: Pokémon Unity Redux
/// Purpose: C variable related classes
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using System.Collections;
namespace PokemonUnity.Backend.Serializables {
[System.Serializable]
public class CVariable
{
    public string name;
    public float value;

    public CVariable(string name, float value)
    {
        this.name = name;
        this.value = value;
    }
}
}

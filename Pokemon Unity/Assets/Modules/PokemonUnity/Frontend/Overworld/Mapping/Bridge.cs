/// Source: Pokémon Unity Redux
/// Purpose: Bridges intended to be used in map creation for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using System.Collections;
namespace PokemonUnity.Frontend.Overworld.Mapping {
public class Bridge : MonoBehaviour
{
    //Bridges are a class used to classify a mesh as a Bridge.

    //This is used in collision detection to differentiate between terrain and overlaying structures.

    public MapSettings.Environment bridgeEnvironment = MapSettings.Environment.IndoorB;
}
}

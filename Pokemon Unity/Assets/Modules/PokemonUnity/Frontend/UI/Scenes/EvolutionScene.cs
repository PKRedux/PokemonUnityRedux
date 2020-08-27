/// Source: Pokémon Unity Redux
/// Purpose: Evolution UI (temporary) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using UnityEngine.UI;
using PokemonUnity.Backend.Serializables;
using PokemonUnity.Frontend.UI;
namespace PokemonUnity.Frontend.UI.Scenes {
public class EvolutionScene : MonoBehaviour
{
    private DialogBox dialog;

    private int pokemonFrame = 0;
    private int evolutionFrame = 0;
    private Sprite[] pokemonSpriteAnimation;
    private Sprite[] evolutionSpriteAnimation;
    private Image pokemonSprite;
    private Image evolutionSprite;

    private Image topBorder;
    private Image bottomBorder;
    private Image glow;

    private Pokemon selectedPokemon;
    private string evolutionMethod;
    private int evolutionID;

    public AudioClip
        evolutionBGM,
        evolvingClip,
        evolvedClip,
        forgetMoveClip;

    private bool stopAnimations = false;


    private bool evolving = true;
    private bool evolved = false;

    private Coroutine c_animateEvolution;
    private Coroutine c_pokemonGlow;
    private Coroutine c_glowGrow;
    private Coroutine c_pokemonPulsate;
    private Coroutine c_glowPulsate;
}
}

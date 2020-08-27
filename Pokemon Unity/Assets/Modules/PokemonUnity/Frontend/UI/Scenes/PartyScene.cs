/// Source: Pokémon Unity Redux
/// Purpose: Party UI (temporary) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PokemonUnity.Frontend.UI;
using PokemonUnity.Backend.Serializables;
namespace PokemonUnity.Frontend.UI.Scenes {
public class PartyScene : MonoBehaviour
{
    private DialogBox Dialog;

    private Image cancel;

    public Sprite selectBallOpen;
    public Sprite selectBallClosed;

    public Sprite panelRound;
    public Sprite panelRect;
    public Sprite panelRoundFaint;
    public Sprite panelRectFaint;
    public Sprite panelRoundSwap;
    public Sprite panelRectSwap;
    public Sprite panelRoundSel;
    public Sprite panelRectSel;
    public Sprite panelRoundFaintSel;
    public Sprite panelRectFaintSel;
    public Sprite panelRoundSwapSel;
    public Sprite panelRectSwapSel;

    public Sprite cancelTex;
    public Sprite cancelHighlightTex;

    private Image[] slot = new Image[6];

    private Image[] selectBall = new Image[6];
    private Image[] icon = new Image[6];
    private Text[] pokemonName = new Text[6];
    private Text[] pokemonNameShadow = new Text[6];
    private Text[] gender = new Text[6];
    private Text[] genderShadow = new Text[6];
    private Image[] HPBarBack = new Image[6];
    private Image[] HPBar = new Image[6];
    private Image[] lv = new Image[6];
    private Text[] level = new Text[6];
    private Text[] levelShadow = new Text[6];
    private Text[] currentHP = new Text[6];
    private Text[] currentHPShadow = new Text[6];
    private Text[] slash = new Text[6];
    private Text[] slashShadow = new Text[6];
    private Text[] maxHp = new Text[6];
    private Text[] maxHPShadow = new Text[6];
    private Image[] status = new Image[6];
    private Image[] item = new Image[6];

    private AudioSource PartyAudio;
    public AudioClip selectClip;

    private int currentPosition;
    private int swapPosition = -1;
    private bool running = false;
    private bool switching = false;
}
}

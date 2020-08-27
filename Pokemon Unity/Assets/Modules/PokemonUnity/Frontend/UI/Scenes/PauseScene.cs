/// Source: Pokémon Unity Redux
/// Purpose: Pause UI (temporary) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PokemonUnity.Frontend.UI;
namespace PokemonUnity.Frontend.UI.Scenes {
public class PauseScene : MonoBehaviour
{
    private RectTransform pauseTop;
    private RectTransform pauseBottom;

    private Text selectedText;
    private Text selectedTextShadow;

    public Image iconPokedexTex;
    public Image iconPartyTex;
    public Image iconBagTex;
    public Image iconTrainerTex;
    public Image iconSaveTex;
    public Image iconSettingsTex;

    private Image iconPokedex;
    private Image iconParty;
    private Image iconBag;
    private Image iconTrainer;
    private Image iconSave;
    private Image iconSettings;

    private RectTransform saveDataDisplay;
    private Text mapName;
    private Text mapNameShadow;
    private Text dataText;
    private Text dataTextShadow;

    private DialogBox Dialog;

    private AudioSource PauseAudio;
    public AudioClip selectClip;
    public AudioClip openClip;

    private int selectedIcon;
    private Image targetIcon;

    private bool running;
}
}

/// Source: Pokémon Unity Redux
/// Purpose: Trainer UI (temporary) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PokemonUnity.Backend.Serializables;
namespace PokemonUnity.Frontend.UI {

public class TrainerScene : MonoBehaviour
{
    private DialogBox Dialog;

    private RectTransform cancel;

    private Transform screens;

    private RectTransform card;

    private RectTransform IDnoBox;
    private Text IDnoText;
    private Text IDnoTextShadow;
    private Text IDnoData;
    private Text IDnoDataShadow;
    private RectTransform nameBox;
    private Text nameText;
    private Text nameTextShadow;
    private Text nameData;
    private Text nameDataShadow;
    private RectTransform picture;
    private RectTransform moneyBox;
    private Text moneyText;
    private Text moneyTextShadow;
    private Text moneyData;
    private Text moneyDataShadow;
    private RectTransform pokedexBox;
    private Text pokedexText;
    private Text pokedexTextShadow;
    private Text pokedexData;
    private Text pokedexDataShadow;
    private RectTransform scoreBox;
    private Text scoreText;
    private Text scoreTextShadow;
    private Text scoreData;
    private Text scoreDataShadow;
    private RectTransform timeBox;
    private Text timeText;
    private Text timeTextShadow;
    private Text timeHour;
    private Text timeHourShadow;
    private Text timeColon;
    private Text timeColonShadow;
    private Text timeMinute;
    private Text timeMinuteShadow;
    private RectTransform adventureBox;
    private Text adventureText;
    private Text adventureTextShadow;
    private Text adventureData;
    private Text adventureDataShadow;

    private RectTransform badgeBox;

    private RectTransform badgeBoxLid;
    private RectTransform GLPictureBox;
    private RectTransform GLPicture;
    private RectTransform GLNameBox;
    private Text GLNameData;
    private Text GLNameDataShadow;
    private RectTransform GLTypeBox;
    private RectTransform GLType;
    private RectTransform GLBeatenBox;
    private Text GLBeatenText;
    private Text GLBeatenTextShadow;
    private Text GLBeatenData;
    private Text GLBeatenDataShadow;
    private RectTransform[] badges = new RectTransform[12];
    private RectTransform badgeSel;

    private RectTransform background;

    private AudioSource TrainerAudio;
    public AudioClip selectClip;

    public RectTransform cancelTex;
    public RectTransform cancelHighlightTex;

    private bool running;
    private int currentScreen;
    private bool interactingScreen;
    private int currentBadge;
    private bool cancelSelected;
}
}

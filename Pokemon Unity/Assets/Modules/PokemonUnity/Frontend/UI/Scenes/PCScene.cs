/// Source: Pokémon Unity Redux
/// Purpose: PC UI (temporary) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using PokemonUnity.Backend.Serializables;
using PokemonUnity.Frontend.UI;
using UnityEngine.UI;
namespace PokemonUnity.Frontend.UI.Scenes {
public class PCScene : MonoBehaviour
{
    public enum CursorMode
    {
        Standard,
        HotMove,
        WithdrawDeposit
    }

    public CursorMode cursorMode = CursorMode.Standard;

    public Sprite boxEditIcon;

    private int currentBoxID;
    private int nextBoxID;
    private int previousBoxID;

    private int selectedBoxID;
    private int selectedIndex;

    private DialogBox Dialog;

    private GameObject dialogBox;
    private GameObject choiceBox;

    private Text dialogText;
    private Text dialogTextShadow;
    private Text choiceText;
    private Text choiceTextShadow;

    private Pokemon selectedPokemon;

    private Transform selectedInfo;

    private Text selectedName;
    private Text selectedNameShadow;
    private Text selectedGender;
    private Text selectedGenderShadow;
    private int frame = 0;
    private Sprite[] selectedSpriteAnimation;
    private Sprite selectedSprite;
    private Sprite selectedType1;
    private Sprite selectedType2;
    private Text selectedLevel;
    private Text selectedLevelShadow;
    private Text selectedAbility;
    private Text selectedAbilityShadow;
    private Text selectedItem;
    private Text selectedItemShadow;
    private Sprite selectedStatus;

    private Sprite cursor;
    private Sprite grabbedPokemon;
    private Sprite grabbedPokemonItem;

    private Sprite[] partyIcons = new Sprite[6];
    private Sprite[] partyItems = new Sprite[6];

    private GameObject currentBox;
    private GameObject nextBox;
    private GameObject previousBox;

    private Sprite currentBoxSprite;
    private Sprite nextBoxSprite;
    private Sprite previousBoxSprite;
    private Text currentBoxHeader;
    private Text nextBoxHeader;
    private Text previousBoxHeader;
    private Text currentBoxHeaderShadow;
    private Text nextBoxHeaderShadow;
    private Text previousBoxHeaderShadow;

    private Transform currentBoxIcons;
    private Sprite[] currentBoxIconsArray = new Sprite[30];
    private Sprite[] currentBoxItemsArray = new Sprite[30];
    private Transform nextBoxIcons;
    private Sprite[] nextBoxIconsArray = new Sprite[30];
    private Sprite[] nextBoxItemsArray = new Sprite[30];
    private Transform previousBoxIcons;
    private Sprite[] previousBoxIconsArray = new Sprite[30];
    private Sprite[] previousBoxItemsArray = new Sprite[30];

    private AudioSource PCaudio;

    public AudioClip offClip;
    public AudioClip openClip;
    public AudioClip selectClip;
    public AudioClip pickUpClip;
    public AudioClip putDownClip;

    private bool running = false;
    private bool carrying = false;
    private bool switching = false;

    //private SceneTransition sceneTransition;

    private float moveSpeed = 0.16f;
}
}

/// Source: Pokémon Unity Redux
/// Purpose: Bag UI (temporary) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using UnityEngine.UI;
using PokemonUnity.Frontend.UI;
namespace PokemonUnity.Frontend.UI.Scenes {
public class BagScene : MonoBehaviour
{
    private DialogBox Dialog;

    public string chosenItem;

    private Transform party;

    private RectTransform[] partySlot = new RectTransform[6];
    private RectTransform[] partyIcon = new RectTransform[6];
    private Text[] partyName = new Text[6];
    private Text[] partyNameShadow = new Text[6];
    private Text[] partyGender = new Text[6];
    private Text[] partyGenderShadow = new Text[6];
    private GameObject[] partyStandardDisplay = new GameObject[6];
    private RectTransform[] partyHPBarBack = new RectTransform[6];
    private RectTransform[] partyHPBar = new RectTransform[6];
    private RectTransform[] partyLv = new RectTransform[6];
    private Text[] partyLevel = new Text[6];
    private Text[] partyLevelShadow = new Text[6];
    private Text[] partyTextDisplay = new Text[6];
    private Text[] partyTextDisplayShadow = new Text[6];
    private RectTransform[] partyStatus = new RectTransform[6];
    private RectTransform[] partyItem = new RectTransform[6];

    private RectTransform scrollBar;

    private Transform itemList;

    private RectTransform[] itemSlot = new RectTransform[8];
    private Text[] itemName = new Text[8];
    private Text[] itemNameShadow = new Text[8];
    private RectTransform[] itemIcon = new RectTransform[8];
    private Text[] itemX = new Text[8];
    private Text[] itemXShadow = new Text[8];
    private Text[] itemQuantity = new Text[8];
    private Text[] itemQuantityShadow = new Text[8];

    private Text itemDescription;
    private Text itemDescriptionShadow;

    private GameObject[] screens = new GameObject[6];
    private GameObject[] shopScreens = new GameObject[6];

    private GameObject numbersBox;
    private RectTransform numbersBoxBorder;
    private Text numbersBoxText;
    private Text numbersBoxTextShadow;
    private Text numbersBoxSelector;
    private Text numbersBoxSelectorShadow;

    private Text shopName;
    private Text shopNameShadow;

    private GameObject moneyBox;
    private RectTransform moneyBoxBorder;
    private Text moneyValueText;
    private Text moneyValueTextShadow;

    private GameObject dataBox;
    private RectTransform dataBoxBorder;
    private Text dataText;
    private Text dataTextShadow;
    private Text dataValueText;
    private Text dataValueTextShadow;

    private RectTransform tmType;
    private RectTransform tmCategory;
    private Text tmPower;
    private Text tmPowerShadow;
    private Text tmAccuracy;
    private Text tmAccuracyShadow;
    private Text tmDescription;
    private Text tmDescriptionShadow;

    public Texture itemListTex;
    public Texture itemListHighlightTex;
    public Texture itemListPlaceTex;
    public Texture itemListSelectedTex;
    public Texture itemListPlaceSelectedTex;

    public Texture partySlotTex;
    public Texture partySlotSelectedTex;

    public int currentScreen = 1;

    public int[] currentPosition = new int[]
    {
        -1, 1, 1, 1, 1, 1
    };

    public int[] currentTopPosition = new int[]
    {
        -1, 0, 0, 0, 0, 0
    };

    private int visableSlots = 6;

    public int unselectedItemIconX = 138;
    public int unselectedItemNameX = 159;

    public bool switching = false;
    public int selected = -1;

    private int selectedPosition = 0;
    private int selectedTopPosition = 0;

    public bool inParty = false;
    public int partyPosition = 0;
    public int partyLength;

    private string[] currentItemList;

    private bool shopMode;
    private string[] shopItemList;
    private int shopSelectedQuantity;

    private AudioSource BagAudio;

    public AudioClip selectClip;
    public AudioClip healClip;
    public AudioClip tmBootupClip;
    public AudioClip forgetMoveClip;
    public AudioClip saleClip;
}
}

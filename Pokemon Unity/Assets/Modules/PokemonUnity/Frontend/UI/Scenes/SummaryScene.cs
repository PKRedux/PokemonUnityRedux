/// Source: Pokémon Unity Redux
/// Purpose: Summary UI (temporary) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using UnityEngine.UI;
namespace PokemonUnity.Frontend.UI {
public class SummaryScene : MonoBehaviour
{
    public string replacedMove;

    private Image
        selectedCaughtBall,
        selectedSprite,
        selectedStatus,
        selectedShiny;

    private Text
        selectedName,
        selectedNameShadow,
        selectedGender,
        selectedGenderShadow,
        selectedLevel,
        selectedLevelShadow,
        selectedHeldItem,
        selectedHeldItemShadow;

    private int frame = 0;
    private Sprite[] selectedSpriteAnimation;

    private GameObject[] pages = new GameObject[8];

    private Text
        dexNo,
        dexNoShadow,
        species,
        speciesShadow,
        OT,
        OTShadow,
        IDNo,
        IDNoShadow,
        expPoints,
        expPointsShadow,
        toNextLevel,
        toNextLevelShadow;

    private Image
        type1,
        type2,
        expBar;

    private Text
        nature,
        natureShadow,
        metDate,
        metDateShadow,
        metMap,
        metMapShadow,
        metLevel,
        metLevelShadow,
        characteristic,
        characteristicShadow;

    private Image HPBar;

    private Text
        HPText,
        HPTextShadow,
        HP,
        HPShadow,
        StatsTextShadow,
        Stats,
        StatsShadow,
        abilityName,
        abilityNameShadow,
        abilityDescription,
        abilityDescriptionShadow;

    private RectTransform moves;

    private Image
        moveSelector,
        selectedMove,
        selectedCategory,
        move1Type,
        move2Type,
        move3Type,
        move4Type;

    private Text
        move1Name,
        move1NameShadow,
        move1PPText,
        move1PPTextShadow,
        move1PP,
        move1PPShadow,
        move2Name,
        move2NameShadow,
        move2PPText,
        move2PPTextShadow,
        move2PP,
        move2PPShadow,
        move3Name,
        move3NameShadow,
        move3PPText,
        move3PPTextShadow,
        move3PP,
        move3PPShadow,
        move4Name,
        move4NameShadow,
        move4PPText,
        move4PPTextShadow,
        move4PP,
        move4PPShadow,
        selectedPower,
        selectedPowerShadow,
        selectedAccuracy,
        selectedAccuracyShadow,
        selectedDescription,
        selectedDescriptionShadow;

    private GameObject learnScreen;

    private RectTransform newMove;

    private Image
        moveNewType;

    private Text
        moveNewName,
        moveNewNameShadow,
        moveNewPPText,
        moveNewPPTextShadow,
        moveNewPP,
        moveNewPPShadow;

    private GameObject forget;

    //ribbons not yet implemented.

    public AudioClip selectClip;
    public AudioClip scrollClip;
    public AudioClip returnClip;
}
}

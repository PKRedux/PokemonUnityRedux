//Original Scripts by IIColour (IIColour_Spectrum)

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Modules.PokemonUnity.Backend;
using Modules.PokemonUnity.Backend.Battle;
using Modules.PokemonUnity.Frontend.UI;
using PokemonUnity.Backend.Databases;
using PokemonUnity.Backend.Datatypes;
using PokemonUnity.Backend.Serializables;
using PokemonUnity.Frontend.Global;
using PokemonUnity.Frontend.Overworld;
using PokemonUnity.Frontend.UI;
using PokemonUnity.Frontend.UI.Scenes;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleScene : BaseScene
{
    public int victor = -1; //0 = player, 1 = opponent, 2 = tie
    private bool trainerBattle;

    private DialogBox Dialog;

    private AudioSource BattleAudio;
    public AudioClip defaultTrainerBGM;
    public int defaultTrainerBGMLoopStart = 577000;

    public AudioClip defaultTrainerVictoryBGM;
    public int defaultTrainerVictoryBGMLoopStart = 79000;

    public AudioClip defaultWildBGM;
    public int defaultWildBGMLoopStart = 578748;

    public AudioClip defaultWildVictoryBGM;
    public int defaultWildVictoryBGMLoopStart = 65000;

    private bool runState = true;
    public AudioClip
        scrollClip,
        selectClip,
        runClip,
        statUpClip,
        statDownClip,
        healFieldClip,
        fillExpClip,
        expFullClip,
        pokeballOpenClip,
        pokeballBounceClip,
        faintClip,
        hitClip,
        hitSuperClip,
        hitPoorClip;

    public Sprite
        partySpaceTex,
        partyBallTex,
        partyStatusTex,
        partyFaintTex,
        buttonFightTex,
        buttonFightSelTex,
        buttonBagTex,
        buttonBagSelTex,
        buttonRunTex,
        buttonRunSelTex,
        buttonPokeTex,
        buttonPokeSelTex,
        buttonMoveBackgroundTex,
        buttonMoveBackgroundSelTex,
        buttonMegaTex,
        buttonMegaActiveTex,
        buttonMegaActiveSelTex,
        buttonReturnTex,
        buttonReturnSelTex,
        buttonBlueTex,
        buttonBlueSelTex,
        buttonBackBagTex,
        buttonBackBagSelTex,
        buttonBagItemCategoryTex,
        buttonBagItemCategorySelTex,
        buttonBagItemListTex,
        buttonBagItemListSelTex,
        buttonBackPokeTex,
        buttonBackPokeSelTex,
        buttonPokemonTex,
        buttonPokemonRoundTex,
        buttonPokemonFntTex,
        buttonPokemonRoundFntTex,
        buttonPokemonSelTex,
        buttonPokemonRoundSelTex,
        buttonPokemonFntSelTex,
        buttonPokemonRoundFntSelTex;

    public Texture
        overlayHealTex,
        overlayStatUpTex,
        overlayStatDownTex;


    //TASK BUTTONS
    private Image
        buttonFight,
        buttonBag,
        buttonRun,
        buttonPoke;

    private Image[]
        buttonMoveCover = new Image[4],
        buttonMove = new Image[4];

    private Image
        buttonMegaEvolution,
        buttonMoveReturn;

    private Image
        buttonBackBag,
        buttonBackPoke;

    private Image buttonItemLastUsed;

    private Image[]
        buttonItemCategory = new Image[4],
        buttonItemList = new Image[8];

    private Image[] buttonPokemonSlot = new Image[6];

    private Image
        buttonSwitch,
        buttonCheck;

    private Text
        buttonCheckText,
        buttonCheckTextShadow;


    //MOVE BUTTON DETAILS
    private Image[] buttonMoveType = new Image[4];

    private Text[]
        buttonMoveName = new Text[4],
        buttonMoveNameShadow = new Text[4],
        buttonMovePP = new Text[4],
        buttonMovePPShadow = new Text[4];

    private GameObject bagObject;
    //ITEM LIST DETAILS
    private GameObject itemList;

    private Text
        itemListCategoryText,
        itemListCategoryTextShadow,
        itemListPageNumber,
        itemListPageNumberShadow;

    private GameObject
        itemListArrowPrev,
        itemListArrowNext;

    private GameObject[] itemListButton = new GameObject[8];
    private Image[] itemListIcon = new Image[8];

    private Text[]
        itemListName = new Text[8],
        itemListNameShadow = new Text[8],
        itemListQuantity = new Text[8],
        itemListQuantityShadow = new Text[8];

    private Text
        itemListDescription,
        itemListDescriptionShadow;

    //POKEMON LIST DETAILS
    private GameObject pokemonPartyObject;
    private Image[] pokemonSlotIcon = new Image[6];

    private Text[]
        pokemonSlotName = new Text[6],
        pokemonSlotNameShadow = new Text[6],
        pokemonSlotGender = new Text[6],
        pokemonSlotGenderShadow = new Text[6],
        pokemonSlotLevel = new Text[6],
        pokemonSlotLevelShadow = new Text[6],
        pokemonSlotCurrentHP = new Text[6],
        pokemonSlotCurrentHPShadow = new Text[6],
        pokemonSlotMaxHP = new Text[6],
        pokemonSlotMaxHPShadow = new Text[6];

    private Image[]
        pokemonSlotHPBar = new Image[6],
        pokemonSlotStatus = new Image[6],
        pokemonSlotItem = new Image[6];

    private Sprite[][] pokemonIconAnim = {
        new Sprite[2],
        new Sprite[2],
        new Sprite[2],
        new Sprite[2],
        new Sprite[2],
        new Sprite[2]
    };

    //POKE SELECTED DETAILS
    private GameObject pokemonSelectedPokemon;

    private Image
        pokemonSelectedIcon,
        pokemonSelectedStatus,
        pokemonSelectedType1,
        pokemonSelectedType2;

    private Text
        pokemonSelectedName,
        pokemonSelectedNameShadow,
        pokemonSelectedGender,
        pokemonSelectedGenderShadow,
        pokemonSelectedLevel,
        pokemonSelectedLevelShadow;

    private GameObject pokeObject;
    //POKE SUMMARY DETAILS
    private GameObject pokemonSummary;

    private Text
        pokemonSummaryHP,
        pokemonSummaryHPShadow,
        pokemonSummaryStatsTextShadow,
        pokemonSummaryStats,
        pokemonSummaryStatsShadow,
        pokemonSummaryNextLevelEXP,
        pokemonSummaryNextLevelEXPShadow,
        pokemonSummaryItemName,
        pokemonSummaryItemNameShadow,
        pokemonSummaryAbilityName,
        pokemonSummaryAbilityNameShadow,
        pokemonSummaryAbilityDescription,
        pokemonSummaryAbilityDescriptionShadow;

    private Image
        pokemonSummaryHPBar,
        pokemonSummaryEXPBar,
        pokemonSummaryItemIcon;

    //POKE MOVES DETAILS
    private GameObject pokemonMoves;

    private Text[]
        pokemonMovesName = new Text[4],
        pokemonMovesNameShadow = new Text[4],
        pokemonMovesPPText = new Text[4],
        pokemonMovesPPTextShadow = new Text[4],
        pokemonMovesPP = new Text[4],
        pokemonMovesPPShadow = new Text[4];

    private Image[] pokemonMovesType = new Image[4];

    private Text
        pokemonMovesSelectedPower,
        pokemonMovesSelectedPowerShadow,
        pokemonMovesSelectedAccuracy,
        pokemonMovesSelectedAccuracyShadow,
        pokemonMovesSelectedDescription,
        pokemonMovesSelectedDescriptionShadow;

    private Image
        pokemonMovesSelectedCategory,
        pokemonMovesSelector,
        pokemonMovesSelectedMove;


    //PARTY DISPLAYS
    private Image
        playerPartyBar,
        opponentPartyBar;

    private Image[]
        playerPartyBarSpace = new Image[6],
        opponentPartyBarSpace = new Image[6];

    //POKEMON STAT DISPLAYS
    private Text
        pokemon0CurrentHP,
        pokemon0CurrentHPShadow,
        pokemon0MaxHP,
        pokemon0MaxHPShadow;

    private Image pokemon0ExpBar;

    private Image[]
        pokemonStatsDisplay = new Image[6],
        statsHPBar = new Image[6],
        statsStatus = new Image[6];

    private Text[]
        statsName = new Text[6],
        statsNameShadow = new Text[6],
        statsGender = new Text[6],
        statsGenderShadow = new Text[6],
        statsLevel = new Text[6],
        statsLevelShadow = new Text[6];

    //BACKGROUNDS
    private Image
        playerBase,
        opponentBase,
        background;

    //DEBUG
    public bool canMegaEvolve = false;

    private Sprite[] playerTrainer1Animation;
    private Image playerTrainerSprite1;
    private Sprite[] trainer1Animation;
    private Image trainerSprite1;

    private Sprite[] player1Animation;
    private Image player1;
    private RawImage player1Overlay;
    private Sprite[] opponent1Animation;
    private Image opponent1;
    private RawImage opponent1Overlay;

    private Coroutine animatePlayer1;
    private Coroutine animateOpponent1;

    private Coroutine animatingPartyIcons;


    //POSITIONS
    private int
        currentTask = 0,
        // 0 = task choice, 1 = move choice, 2 = bag choice, 3 = pokemon choice
        //								4 = item list,			  5 = summary, 6 = moves
        taskPosition = 1,
        // 0/3 = bag, 1 = fight, 2/5 = pokemon, 4 = run
        movePosition = 1,
        // 0 = Mega Evolution, 1/2/4/5 = move, 3 = back
        bagCategoryPosition = 0,
        // 0 = HPPP, 1 = Pokeballs, 2 = Status, 3 = Battle, 4 = Back
        pokePartyPosition = 0,
        // 0-5 = pokemon, 6 = Back
        itemListPagePosition = 0,
        //which item list page is currently open (displays +1 of variable)
        itemListPageCount = 0;

    private string[] itemListString;


    private int pokemonPerSide = 1;

    //pokemon
    private Pokemon[] pokemon = new Pokemon[6];
    private PokemonBattleData[] battleData = new PokemonBattleData[]
    {
        new PokemonBattleData(), new PokemonBattleData(), new PokemonBattleData(),
        new PokemonBattleData(), new PokemonBattleData(), new PokemonBattleData()
    };
    
    private ActiveBattleData[] activeBattleData = new ActiveBattleData[2];
    private bool running;
//check if any opponents are left
    private bool allOpponentsDefeated = true;
    private int playerFleeAttempts;


    //Field effects
    private enum WeatherEffect
    {
        NONE,
        RAIN,
        SUN,
        SAND,
        HAIL,
        HEAVYRAIN,
        HEAVYSUN,
        STRONGWINDS
    }

    private enum TerrainEffect
    {
        NONE,
        ELECTRIC,
        GRASSY,
        MISTY
    }

    private WeatherEffect weather = WeatherEffect.NONE;
    private int weatherTurns = 0;
    private TerrainEffect terrain = TerrainEffect.NONE;
    private int terrainTurns = 0;
    private int gravityTurns = 0;

    //Turn Feedback Data
    private bool[] pokemonHasMoved = new bool[6];
    private string[] previousMove = new string[6];

    void Awake()
    {
        Dialog = SceneScript.main.Dialog;

        BattleAudio = transform.GetComponent<AudioSource>();

        playerBase = transform.Find("player0").GetComponent<Image>();
        opponentBase = transform.Find("opponent0").GetComponent<Image>();
        background = transform.Find("Background").GetComponent<Image>();

        trainerSprite1 = opponentBase.transform.Find("Trainer").GetComponent<Image>();
        playerTrainerSprite1 = playerBase.transform.Find("Trainer").GetComponent<Image>();

        player1 = playerBase.transform.Find("Pokemon").Find("Mask").Find("Sprite").GetComponent<Image>();
        opponent1 =
            opponentBase.transform.Find("Pokemon").Find("Mask").Find("Sprite").GetComponent<Image>();
        player1Overlay = player1.transform.Find("Overlay").GetComponent<RawImage>();
        opponent1Overlay = opponent1.transform.Find("Overlay").GetComponent<RawImage>();

        Transform playerPartyBarTrn = transform.Find("playerParty");
        Transform opponentPartyBarTrn = transform.Find("opponentParty");
        playerPartyBar = playerPartyBarTrn.Find("bar").GetComponent<Image>();
        opponentPartyBar = opponentPartyBarTrn.Find("bar").GetComponent<Image>();
        for (int i = 0; i < 6; i++)
        {
            playerPartyBarSpace[i] = playerPartyBarTrn.Find("space" + i).GetComponent<Image>();
        }
        for (int i = 0; i < 6; i++)
        {
            opponentPartyBarSpace[i] = opponentPartyBarTrn.Find("space" + i).GetComponent<Image>();
        }

        pokemonStatsDisplay[0] = transform.Find("playerStats0").GetComponent<Image>();
        statsNameShadow[0] = pokemonStatsDisplay[0].transform.Find("Name").GetComponent<Text>();
        statsName[0] = statsNameShadow[0].transform.Find("Text").GetComponent<Text>();
        statsGenderShadow[0] = pokemonStatsDisplay[0].transform.Find("Gender").GetComponent<Text>();
        statsGender[0] = statsGenderShadow[0].transform.Find("Text").GetComponent<Text>();
        statsLevelShadow[0] = pokemonStatsDisplay[0].transform.Find("Level").GetComponent<Text>();
        statsLevel[0] = statsLevelShadow[0].transform.Find("Text").GetComponent<Text>();
        statsHPBar[0] = pokemonStatsDisplay[0].transform.Find("HPBar").GetComponent<Image>();
        statsStatus[0] = pokemonStatsDisplay[0].transform.Find("Status").GetComponent<Image>();
        pokemon0CurrentHPShadow = pokemonStatsDisplay[0].transform.Find("CurrentHP").GetComponent<Text>();
        pokemon0CurrentHP = pokemon0CurrentHPShadow.transform.Find("Text").GetComponent<Text>();
        pokemon0MaxHPShadow = pokemonStatsDisplay[0].transform.Find("MaxHP").GetComponent<Text>();
        pokemon0MaxHP = pokemon0MaxHPShadow.transform.Find("Text").GetComponent<Text>();
        pokemon0ExpBar = pokemonStatsDisplay[0].transform.Find("ExpBar").GetComponent<Image>();

        pokemonStatsDisplay[3] = transform.Find("opponentStats0").GetComponent<Image>();
        statsNameShadow[3] = pokemonStatsDisplay[3].transform.Find("Name").GetComponent<Text>();
        statsName[3] = statsNameShadow[3].transform.Find("Text").GetComponent<Text>();
        statsGenderShadow[3] = pokemonStatsDisplay[3].transform.Find("Gender").GetComponent<Text>();
        statsGender[3] = statsGenderShadow[3].transform.Find("Text").GetComponent<Text>();
        statsLevelShadow[3] = pokemonStatsDisplay[3].transform.Find("Level").GetComponent<Text>();
        statsLevel[3] = statsLevelShadow[3].transform.Find("Text").GetComponent<Text>();
        statsHPBar[3] = pokemonStatsDisplay[3].transform.Find("HPBar").GetComponent<Image>();
        statsStatus[3] = pokemonStatsDisplay[3].transform.Find("Status").GetComponent<Image>();

        Transform optionBox = transform.Find("OptionBox");
        buttonFight = optionBox.Find("ButtonFight").GetComponent<Image>();
        buttonBag = optionBox.Find("ButtonBag").GetComponent<Image>();
        buttonPoke = optionBox.Find("ButtonPoke").GetComponent<Image>();
        buttonRun = optionBox.Find("ButtonRun").GetComponent<Image>();

        for (int i = 0; i < 4; i++)
        {
            buttonMove[i] = optionBox.Find("Move" + (i + 1)).GetComponent<Image>();
            buttonMoveType[i] = buttonMove[i].transform.Find("Type").GetComponent<Image>();
            buttonMoveNameShadow[i] = buttonMove[i].transform.Find("Name").GetComponent<Text>();
            buttonMoveName[i] = buttonMoveNameShadow[i].transform.Find("Text").GetComponent<Text>();
            buttonMovePPShadow[i] = buttonMove[i].transform.Find("PP").GetComponent<Text>();
            buttonMovePP[i] = buttonMovePPShadow[i].transform.Find("Text").GetComponent<Text>();
            buttonMoveCover[i] = buttonMove[i].transform.Find("Cover").GetComponent<Image>();
        }
        buttonMoveReturn = optionBox.Find("MoveReturn").GetComponent<Image>();
        buttonMegaEvolution = optionBox.Find("MoveMegaEvolution").GetComponent<Image>();

        bagObject = optionBox.Find("Bag").gameObject;
        pokeObject = optionBox.Find("Poke").gameObject;
        pokemonPartyObject = optionBox.Find("Party").gameObject;

        buttonBackBag = bagObject.transform.Find("ButtonBack").GetComponent<Image>();
        buttonBackPoke = pokeObject.transform.Find("ButtonBack").GetComponent<Image>();

        buttonItemCategory[0] = bagObject.transform.Find("ButtonHPPPRestore").GetComponent<Image>();
        buttonItemCategory[1] = bagObject.transform.Find("ButtonPokeBalls").GetComponent<Image>();
        buttonItemCategory[2] = bagObject.transform.Find("ButtonStatusHealers").GetComponent<Image>();
        buttonItemCategory[3] = bagObject.transform.Find("ButtonBattleItems").GetComponent<Image>();
        buttonItemLastUsed = bagObject.transform.Find("ButtonItemUsedLast").GetComponent<Image>();

        itemList = bagObject.transform.Find("Items").gameObject;
        for (int i = 0; i < 8; i++)
        {
            buttonItemList[i] = itemList.transform.Find("Item" + i).GetComponent<Image>();
            itemListIcon[i] = buttonItemList[i].transform.Find("Icon").GetComponent<Image>();
            itemListNameShadow[i] = buttonItemList[i].transform.Find("Item").GetComponent<Text>();
            itemListName[i] = itemListNameShadow[i].transform.Find("Text").GetComponent<Text>();
            itemListQuantityShadow[i] = buttonItemList[i].transform.Find("Quantity").GetComponent<Text>();
            itemListQuantity[i] = itemListQuantityShadow[i].transform.Find("Text").GetComponent<Text>();
        }

        for (int i = 0; i < 6; i++)
        {
            buttonPokemonSlot[i] = pokemonPartyObject.transform.Find("Slot" + i).GetComponent<Image>();
            pokemonSlotIcon[i] = buttonPokemonSlot[i].transform.Find("Icon").GetComponent<Image>();
            pokemonSlotNameShadow[i] = buttonPokemonSlot[i].transform.Find("Name").GetComponent<Text>();
            pokemonSlotName[i] = pokemonSlotNameShadow[i].transform.Find("Text").GetComponent<Text>();
            pokemonSlotGenderShadow[i] = buttonPokemonSlot[i].transform.Find("Gender").GetComponent<Text>();
            pokemonSlotGender[i] = pokemonSlotGenderShadow[i].transform.Find("Text").GetComponent<Text>();
            pokemonSlotLevelShadow[i] = buttonPokemonSlot[i].transform.Find("Level").GetComponent<Text>();
            pokemonSlotLevel[i] = pokemonSlotLevelShadow[i].transform.Find("Text").GetComponent<Text>();
            pokemonSlotCurrentHPShadow[i] = buttonPokemonSlot[i].transform.Find("CurrentHP").GetComponent<Text>();
            pokemonSlotCurrentHP[i] = pokemonSlotCurrentHPShadow[i].transform.Find("Text").GetComponent<Text>();
            pokemonSlotMaxHPShadow[i] = buttonPokemonSlot[i].transform.Find("MaxHP").GetComponent<Text>();
            pokemonSlotMaxHP[i] = pokemonSlotMaxHPShadow[i].transform.Find("Text").GetComponent<Text>();
            pokemonSlotHPBar[i] = buttonPokemonSlot[i].transform.Find("HPBar").GetComponent<Image>();
            pokemonSlotStatus[i] = buttonPokemonSlot[i].transform.Find("Status").GetComponent<Image>();
            pokemonSlotItem[i] = buttonPokemonSlot[i].transform.Find("Item").GetComponent<Image>();
        }

        buttonSwitch = pokeObject.transform.Find("ButtonSwitch").GetComponent<Image>();
        buttonCheck = pokeObject.transform.Find("ButtonCheck").GetComponent<Image>();
        buttonCheckTextShadow = buttonCheck.transform.Find("Text").GetComponent<Text>();
        buttonCheckText = buttonCheckTextShadow.transform.Find("Text").GetComponent<Text>();

        //ITEM LIST DETAILS
        itemListCategoryTextShadow = itemList.transform.Find("Category").GetComponent<Text>();
        itemListCategoryText = itemListCategoryTextShadow.transform.Find("Text").GetComponent<Text>();
        itemListPageNumberShadow = itemList.transform.Find("Page").GetComponent<Text>();
        itemListPageNumber = itemListPageNumberShadow.transform.Find("Text").GetComponent<Text>();
        itemListArrowPrev = itemList.transform.Find("PageArrowPrev").gameObject;
        itemListArrowNext = itemList.transform.Find("PageArrowNext").gameObject;
        itemListDescriptionShadow = itemList.transform.Find("ItemDescription").GetComponent<Text>();
        itemListDescription = itemListDescriptionShadow.transform.Find("Text").GetComponent<Text>();

        //POKE SELECTED DETAILS
        pokemonSelectedPokemon = pokeObject.transform.Find("SelectedPokemon").gameObject;
        pokemonSelectedIcon = pokemonSelectedPokemon.transform.Find("Icon").GetComponent<Image>();
        pokemonSelectedNameShadow = pokemonSelectedPokemon.transform.Find("Name").GetComponent<Text>();
        pokemonSelectedName = pokemonSelectedNameShadow.transform.Find("Text").GetComponent<Text>();
        pokemonSelectedGenderShadow = pokemonSelectedPokemon.transform.Find("Gender").GetComponent<Text>();
        pokemonSelectedGender = pokemonSelectedGenderShadow.transform.Find("Text").GetComponent<Text>();
        pokemonSelectedLevelShadow = pokemonSelectedPokemon.transform.Find("Level").GetComponent<Text>();
        pokemonSelectedLevel = pokemonSelectedLevelShadow.transform.Find("Text").GetComponent<Text>();
        pokemonSelectedStatus = pokemonSelectedPokemon.transform.Find("Status").GetComponent<Image>();
        pokemonSelectedType1 = pokemonSelectedPokemon.transform.Find("Type1").GetComponent<Image>();
        pokemonSelectedType2 = pokemonSelectedPokemon.transform.Find("Type2").GetComponent<Image>();

        //POKE SUMMARY DETAILS
        pokemonSummary = pokeObject.transform.Find("Summary").gameObject;
        pokemonSummaryHPShadow = pokemonSummary.transform.Find("HP").GetComponent<Text>();
        pokemonSummaryHP = pokemonSummaryHPShadow.transform.Find("Text").GetComponent<Text>();
        pokemonSummaryHPBar = pokemonSummary.transform.Find("HPBar").GetComponent<Image>();
        pokemonSummaryStatsTextShadow = pokemonSummary.transform.Find("StatsText").GetComponent<Text>();
        pokemonSummaryStatsShadow = pokemonSummary.transform.Find("Stats").GetComponent<Text>();
        pokemonSummaryStats = pokemonSummaryStatsShadow.transform.Find("Text").GetComponent<Text>();
        pokemonSummaryNextLevelEXPShadow = pokemonSummary.transform.Find("ToNextLevel").GetComponent<Text>();
        pokemonSummaryNextLevelEXP = pokemonSummaryNextLevelEXPShadow.transform.Find("Text").GetComponent<Text>();
        pokemonSummaryEXPBar = pokemonSummary.transform.Find("ExpBar").GetComponent<Image>();
        pokemonSummaryItemIcon = pokemonSummary.transform.Find("ItemIcon").GetComponent<Image>();
        pokemonSummaryItemNameShadow = pokemonSummary.transform.Find("Item").GetComponent<Text>();
        pokemonSummaryItemName = pokemonSummaryItemNameShadow.transform.Find("Text").GetComponent<Text>();
        pokemonSummaryAbilityNameShadow = pokemonSummary.transform.Find("Ability").GetComponent<Text>();
        pokemonSummaryAbilityName = pokemonSummaryAbilityNameShadow.transform.Find("Text").GetComponent<Text>();
        pokemonSummaryAbilityDescriptionShadow =
            pokemonSummary.transform.Find("AbilityDescription").GetComponent<Text>();
        pokemonSummaryAbilityDescription =
            pokemonSummaryAbilityDescriptionShadow.transform.Find("Text").GetComponent<Text>();

        //POKE MOVES DETAILS
        pokemonMoves = pokeObject.transform.Find("Moves").gameObject;
        for (int i = 0; i < 4; i++)
        {
            pokemonMovesNameShadow[i] = pokemonMoves.transform.Find("Move" + (i + 1)).GetComponent<Text>();
            pokemonMovesName[i] = pokemonMovesNameShadow[i].transform.Find("Text").GetComponent<Text>();
            pokemonMovesType[i] = pokemonMoves.transform.Find("Move" + (i + 1) + "Type").GetComponent<Image>();
            pokemonMovesPPShadow[i] = pokemonMoves.transform.Find("Move" + (i + 1) + "PP").GetComponent<Text>();
            pokemonMovesPP[i] = pokemonMovesPPShadow[i].transform.Find("Text").GetComponent<Text>();
            pokemonMovesPPTextShadow[i] =
                pokemonMoves.transform.Find("Move" + (i + 1) + "PPText").GetComponent<Text>();
            pokemonMovesPPText[i] = pokemonMovesPPTextShadow[i].transform.Find("Text").GetComponent<Text>();
        }

        pokemonMovesSelectedCategory = pokemonMoves.transform.Find("SelectedCategory").GetComponent<Image>();
        pokemonMovesSelectedPowerShadow = pokemonMoves.transform.Find("SelectedPower").GetComponent<Text>();
        pokemonMovesSelectedPower = pokemonMovesSelectedPowerShadow.transform.Find("Text").GetComponent<Text>();
        pokemonMovesSelectedAccuracyShadow = pokemonMoves.transform.Find("SelectedAccuracy").GetComponent<Text>();
        pokemonMovesSelectedAccuracy =
            pokemonMovesSelectedAccuracyShadow.transform.Find("Text").GetComponent<Text>();
        pokemonMovesSelectedDescriptionShadow =
            pokemonMoves.transform.Find("SelectedDescription").GetComponent<Text>();
        pokemonMovesSelectedDescription =
            pokemonMovesSelectedDescriptionShadow.transform.Find("Text").GetComponent<Text>();
        pokemonMovesSelector = pokemonMoves.transform.Find("MoveSelector").GetComponent<Image>();
        pokemonMovesSelectedMove = pokemonMoves.transform.Find("SelectedMove").GetComponent<Image>();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator SlidePokemonStats(int position, bool retract)
    {
        float distanceX = pokemonStatsDisplay[position].rectTransform.sizeDelta.x;
        float startX = (retract) ? 171f - (distanceX / 2) : 171f + (distanceX / 2);
        //flip values if opponent stats
        if (position > 2)
        {
            startX = -startX;
            distanceX = -distanceX;
        }
        //flip movement direction if retracting
        if (retract)
        {
            distanceX = -distanceX;
        }

        pokemonStatsDisplay[position].gameObject.SetActive(true);

        float speed = 0.3f;
        float increment = 0f;
        while (increment < 1)
        {
            increment += (1 / speed) * Time.deltaTime;
            if (increment > 1)
            {
                increment = 1f;
            }

            pokemonStatsDisplay[position].rectTransform.localPosition = new Vector3(startX - (distanceX * increment),
                pokemonStatsDisplay[position].rectTransform.localPosition.y, 0);

            yield return null;
        }
    }

    private void HidePartyBar(bool isOpponent)
    {
        Image bar = (isOpponent) ? opponentPartyBar : playerPartyBar;
        Image[] space = (isOpponent) ? opponentPartyBarSpace : playerPartyBarSpace;

        var color = bar.color;
        color = new Color(color.r, color.g, color.b, 0);
        bar.color = color;
        var rectTransform = bar.rectTransform;
        rectTransform.sizeDelta = new Vector2(0, rectTransform.sizeDelta.y);
        for (int i = 0; i < 6; i++)
        {
            space[i].color = new Color(space[i].color.r, space[i].color.g, space[i].color.b, 0);
            space[i].rectTransform.localPosition = new Vector3(-96 + (16 * i) + 128,
                space[i].rectTransform.localPosition.y);
        }
    }

    /// <summary> Displays the party length bar. </summary>
    /// <param name="isOpponent">Does this bar belong to the opponent?</param>
    /// <param name="party">The party, used to determine length and if fainted.</param>
    private IEnumerator DisplayPartyBar(bool isOpponent, Pokemon[] party)
    {
        Image bar = (isOpponent) ? opponentPartyBar : playerPartyBar;
        Image[] space = (isOpponent) ? opponentPartyBarSpace : playerPartyBarSpace;

        HidePartyBar(isOpponent); //this line reset the position to hidden, but also sets alpha to 0
        //set alpha to 1
        var color = bar.color;
        color = new Color(color.r, color.g, color.b, 1);
        bar.color = color;
        for (int i = 0; i < 6; i++)
        {
            space[i].color = new Color(space[i].color.r, space[i].color.g, space[i].color.b, 1);
        }

        StartCoroutine(BattleAnimator.StretchBar(bar, 128, 320));
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 6; i++)
        {
            //Set space sprite
            space[i].sprite = partySpaceTex;
            if (party.Length > i)
            {
                if (party[i] != null)
                {
                    if (party[i].getStatus() == Pokemon.Status.FAINTED)
                    {
                        space[i].sprite = partyFaintTex;
                    }
                    else if (party[i].getStatus() == Pokemon.Status.NONE)
                    {
                        space[i].sprite = partyBallTex;
                    }
                    else
                    {
                        space[i].sprite = partyStatusTex;
                    }
                }
            }
            //slide down the line
            StartCoroutine(BattleAnimator.SlidePartyBarBall(space[i], -96 + (16 * i) + 128, -99 + (16 * i), 0.35f));
            yield return new WaitForSeconds(0.05f);
        }
        //Wait for last space to stop moving
        yield return new WaitForSeconds(0.3f);
        //Slide all spaces back a tiny bit
        for (int i = 0; i < 6; i++)
        {
            StartCoroutine(BattleAnimator.SlidePartyBarBall(space[i], -99 + (16 * i), -96 + (16 * i), 0.1f));
        }
        //Wait for last space to stop moving
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator DismissPartyBar(bool isOpponent)
    {
        Image bar = (isOpponent) ? opponentPartyBar : playerPartyBar;
        Image[] space = (isOpponent) ? opponentPartyBarSpace : playerPartyBarSpace;

        //Slide all out and fade
        Image[] images = new Image[space.Length + 1];
        images[0] = bar;
        for (int i = 0; i < space.Length; i++)
        {
            images[i + 1] = space[i];
        }
        StartCoroutine(BattleAnimator.FadeImages(images, 0.6f));

        StartCoroutine(BattleAnimator.StretchBar(bar, 192, 128));
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 6; i++)
        {
            //slide down the line
            StartCoroutine(BattleAnimator.SlidePartyBarBall(space[i], -96 + (16 * i), -96 + (16 * i) - 192, 0.5f));
            yield return new WaitForSeconds(0.05f + (0.02f * i));
        }
        //Wait for last space to stop moving
        yield return new WaitForSeconds(0.45f);
        HidePartyBar(isOpponent);
    }

    private IEnumerator AnimatePartyIcons()
    {
        while (true)
        {
            for (int i = 0; i < 6; i++)
            {
                pokemonSlotIcon[i].sprite = pokemonIconAnim[i][0];
            }
            yield return new WaitForSeconds(0.15f);
            for (int i = 0; i < 6; i++)
            {
                pokemonSlotIcon[i].sprite = (i == pokePartyPosition) ? pokemonIconAnim[i][1] : pokemonIconAnim[i][0];
            }
            yield return new WaitForSeconds(0.15f);

            for (int i = 0; i < 6; i++)
            {
                pokemonSlotIcon[i].sprite = (i == pokePartyPosition) ? pokemonIconAnim[i][0] : pokemonIconAnim[i][1];
            }
            yield return new WaitForSeconds(0.15f);
            for (int i = 0; i < 6; i++)
            {
                pokemonSlotIcon[i].sprite = pokemonIconAnim[i][1];
            }
            yield return new WaitForSeconds(0.15f);
        }
    }

    //////////////////////////////////


    //////////////////////////////////
    /// GUI Display Updaters
    // 
    /// updates the displayed task.
    /// 0 = task choice, 1 = move choice, 2 = bag choice, 3 = pokemon choice, 4 = item list, 5 = summary, 6 = moves
    private void UpdateCurrentTask(int newState)
    {
        switch (currentTask)
        {
            case 0:
                buttonBag.gameObject.SetActive(false);
                buttonFight.gameObject.SetActive(false);
                buttonPoke.gameObject.SetActive(false);
                buttonRun.gameObject.SetActive(false);
                break;
            case 1:
                buttonMove[0].gameObject.SetActive(false);
                buttonMove[1].gameObject.SetActive(false);
                buttonMove[2].gameObject.SetActive(false);
                buttonMove[3].gameObject.SetActive(false);
                buttonMegaEvolution.gameObject.SetActive(false);
                buttonMoveReturn.gameObject.SetActive(false);
                break;
            case 2:
                //Bag
                bagObject.SetActive(false);
                buttonItemCategory[0].gameObject.SetActive(false);
                buttonItemCategory[1].gameObject.SetActive(false);
                buttonItemCategory[2].gameObject.SetActive(false);
                buttonItemCategory[3].gameObject.SetActive(false);
                buttonItemLastUsed.gameObject.SetActive(false);
                break;
            case 3:
                //Poke
                pokeObject.SetActive(false);
                pokemonPartyObject.SetActive(false);
                StopCoroutine(animatingPartyIcons);
                break;
            case 4:
                //ItemList
                bagObject.SetActive(false);
                itemList.SetActive(false);
                UpdateItemListDisplay();
                break;
            case 5:
                //Summary
                pokeObject.SetActive(false);
                buttonSwitch.gameObject.SetActive(false);
                buttonCheck.gameObject.SetActive(false);
                pokemonSelectedPokemon.SetActive(false);
                pokemonSummary.SetActive(false);
                break;
            case 6:
                //Moves
                pokeObject.SetActive(false);
                buttonSwitch.gameObject.SetActive(false);
                buttonCheck.gameObject.SetActive(false);
                pokemonSelectedPokemon.SetActive(false);
                pokemonMoves.SetActive(false);
                break;
        }

        currentTask = newState;

        if (currentTask == 0)
        {
            buttonBag.gameObject.SetActive(true);
            buttonFight.gameObject.SetActive(true);
            buttonPoke.gameObject.SetActive(true);
            buttonRun.gameObject.SetActive(true);
        }
        else if (currentTask == 1)
        {
            buttonMove[0].gameObject.SetActive(true);
            buttonMove[1].gameObject.SetActive(true);
            buttonMove[2].gameObject.SetActive(true);
            buttonMove[3].gameObject.SetActive(true);
            buttonMegaEvolution.gameObject.SetActive(true);
            buttonMoveReturn.gameObject.SetActive(true);
        }
        else if (currentTask == 2)
        {
            //Bag
            bagObject.SetActive(true);
            buttonItemCategory[0].gameObject.SetActive(true);
            buttonItemCategory[1].gameObject.SetActive(true);
            buttonItemCategory[2].gameObject.SetActive(true);
            buttonItemCategory[3].gameObject.SetActive(true);
            buttonItemLastUsed.gameObject.SetActive(true);
            UpdateSelectedBagCategory(bagCategoryPosition);
        }
        else if (currentTask == 3)
        {
            //Poke
            pokeObject.SetActive(true);
            pokemonPartyObject.SetActive(true);
            UpdatePokemonSlotsDisplay();
            UpdateSelectedPokemonSlot(pokePartyPosition);
            animatingPartyIcons = StartCoroutine(AnimatePartyIcons());
        }
        else if (currentTask == 4)
        {
            //ItemList
            bagObject.SetActive(true);
            itemList.SetActive(true);
            if (bagCategoryPosition == 0)
            {
                itemListString = SaveData.currentSave.Bag.getBattleTypeArray(ItemData.BattleType.HPPPRESTORE);
                itemListCategoryText.text = "HP/PP Restore";
            }
            else if (bagCategoryPosition == 1)
            {
                itemListString = SaveData.currentSave.Bag.getBattleTypeArray(ItemData.BattleType.POKEBALLS);
                itemListCategoryText.text = "Poké Balls";
            }
            else if (bagCategoryPosition == 2)
            {
                itemListString = SaveData.currentSave.Bag.getBattleTypeArray(ItemData.BattleType.STATUSHEALER);
                itemListCategoryText.text = "Status Healers";
            }
            else if (bagCategoryPosition == 3)
            {
                itemListString = SaveData.currentSave.Bag.getBattleTypeArray(ItemData.BattleType.BATTLEITEMS);
                itemListCategoryText.text = "Battle Items";
            }
            itemListCategoryTextShadow.text = itemListCategoryText.text;
            itemListPagePosition = 0;

            itemListPageCount = Mathf.CeilToInt((float) itemListString.Length / 8f);
            UpdateItemListDisplay();
        }
        else if (currentTask == 5)
        {
            //Summary
            pokeObject.SetActive(true);
            buttonSwitch.gameObject.SetActive(true);
            buttonCheck.gameObject.SetActive(true);
            buttonCheckText.text = "Check Moves";
            buttonCheckTextShadow.text = buttonCheckText.text;
            pokemonSelectedPokemon.SetActive(true);
            pokemonSummary.SetActive(true);
            UpdatePokemonSummaryDisplay(SaveData.currentSave.PC.boxes[0][pokePartyPosition]);
        }
        else if (currentTask == 6)
        {
            //Moves
            pokeObject.SetActive(true);
            buttonSwitch.gameObject.SetActive(true);
            buttonCheck.gameObject.SetActive(true);
            buttonCheckText.text = "Check Summary";
            buttonCheckTextShadow.text = buttonCheckText.text;
            pokemonSelectedPokemon.SetActive(true);
            pokemonMoves.SetActive(true);
            UpdateMovesPosition(5);
        }
    }

    private void UpdatePokemonStatsDisplay(int position)
    {
        if (pokemon[position] != null)
        {
            statsName[position].text = pokemon[position].getName();
            statsNameShadow[position].text = statsName[position].text;
            if (pokemon[position].getGender() == Pokemon.Gender.FEMALE)
            {
                statsGender[position].text = "♀";
                statsGender[position].color = new Color(1, 0.2f, 0.2f, 1);
            }
            else if (pokemon[position].getGender() == Pokemon.Gender.MALE)
            {
                statsGender[position].text = "♂";
                statsGender[position].color = new Color(0.2f, 0.4f, 1, 1);
            }
            else
            {
                statsGender[position].text = null;
            }
            statsGenderShadow[position].text = statsGender[position].text;
            statsLevel[position].text = "" + pokemon[position].getLevel();
            statsLevelShadow[position].text = statsLevel[position].text;
            statsHPBar[position].rectTransform.sizeDelta =
                new Vector2(Mathf.CeilToInt(pokemon[position].getPercentHP() * 48f), 4f);

            BattleCalculation.SetHpBarColor(statsHPBar[position], 48f);


            if (pokemon[position].getStatus() != Pokemon.Status.NONE)
            {
                statsStatus[position].sprite =
                    Resources.Load<Sprite>("PCSprites/status" + pokemon[position].getStatus().ToString());
            }
            else
            {
                statsStatus[position].sprite = Resources.Load<Sprite>("null");
            }
            if (position == 0 && pokemonPerSide == 1)
            {
                pokemon0CurrentHP.text = "" + pokemon[0].getCurrentHP();
                pokemon0CurrentHPShadow.text = pokemon0CurrentHP.text;
                pokemon0MaxHP.text = "" + pokemon[0].getHP();
                pokemon0MaxHPShadow.text = pokemon0MaxHP.text;
                float expCurrentLevel =
                    PokemonDatabase.getLevelExp(PokemonDatabase.getPokemon(pokemon[0].getID()).getLevelingRate(),
                        pokemon[0].getLevel());
                float expNextlevel =
                    PokemonDatabase.getLevelExp(PokemonDatabase.getPokemon(pokemon[0].getID()).getLevelingRate(),
                        pokemon[0].getLevel() + 1);
                float expAlong = pokemon[0].getExp() - expCurrentLevel;
                float expDistance = expAlong / (expNextlevel - expCurrentLevel);
                pokemon0ExpBar.rectTransform.sizeDelta = new Vector2(Mathf.Floor(expDistance * 80f), 2f);
            }
        }
        else
        {
        }
    }

    private void UpdateMovesetDisplay(string[] moveset, int[] PP, int[] maxPP)
    {
        for (int i = 0; i < 4; i++)
        {
            if (moveset[i] != null)
            {
                PokemonData.Type type = MoveDatabase.getMove(moveset[i]).getType();
                
                buttonMoveType[i].sprite = Resources.Load<Sprite>("PCSprites/type" + type);
                var typeColors = new Dictionary<PokemonData.Type, Color>()
                {
                    {PokemonData.Type.BUG, new Color(0.47f, 0.57f, 0.06f, 1)},
                    {PokemonData.Type.DARK, new Color(0.32f, 0.28f, 0.24f, 1)},
                    {PokemonData.Type.DRAGON, new Color(0.32f, 0.25f, 1f, 1)},
                    {PokemonData.Type.ELECTRIC, new Color(0.64f, 0.52f, 0.04f, 1)},
                    {PokemonData.Type.FAIRY, new Color(0.7f, 0.33f, 0.6f, 1)},
                    {PokemonData.Type.FIGHTING, new Color(0.75f, 0.19f, 0.15f, 1)},
                    {PokemonData.Type.FIRE, new Color(0.94f, 0.5f, 0.19f, 1)},
                    {PokemonData.Type.FLYING, new Color(0.5f, 0.43f, 0.72f, 1)},
                    {PokemonData.Type.GHOST, new Color(0.4f, 0.32f, 0.55f, 1)},
                    {PokemonData.Type.GRASS, new Color(0.34f, 0.5f, 0.25f, 1)},
                    {PokemonData.Type.GROUND, new Color(0.53f, 0.4f, 0.19f, 1)},
                    {PokemonData.Type.ICE, new Color(0.4f, 0.6f, 0.6f, 1)},
                    {PokemonData.Type.NORMAL, new Color(0.5f, 0.5f, 0.35f, 1)},
                    {PokemonData.Type.POISON, new Color(0.63f, 0.25f, 0.63f, 1)},
                    {PokemonData.Type.PSYCHIC, new Color(0.75f, 0.25f, 0.4f, 1)},
                    {PokemonData.Type.ROCK, new Color(0.48f, 0.35f, 0.14f, 1)},
                    {PokemonData.Type.STEEL, new Color(0.6f, 0.6f, 0.67f, 1)},
                    {PokemonData.Type.WATER, new Color(0.25f, 0.42f, 0.75f, 1)}
                };
                buttonMoveCover[i].color = typeColors[type];

                buttonMoveName[i].text = moveset[i];
                buttonMoveNameShadow[i].text = buttonMoveName[i].text;
                buttonMovePP[i].text = PP[i] + "/" + maxPP[i];
                buttonMovePPShadow[i].text = buttonMovePP[i].text;
            }
            else
            {
                buttonMoveCover[i].color = new Color(0.5f, 0.5f, 0.5f, 1);
                buttonMoveType[i].sprite = Resources.Load<Sprite>("null");
                buttonMoveName[i].text = "";
                buttonMoveNameShadow[i].text = buttonMoveName[i].text;
                buttonMovePP[i].text = "";
                buttonMovePPShadow[i].text = buttonMovePP[i].text;
            }
        }
    }

    /// Updates the Item List to show the correct 8 items from the page
    private void UpdateItemListDisplay()
    {
        if (itemListPageCount < 1)
        {
            itemListPageCount = 1;
        }
        itemListPageNumber.text = (itemListPagePosition + 1) + "/" + itemListPageCount;
        itemListPageNumberShadow.text = itemListPageNumber.text;

        itemListArrowPrev.SetActive(itemListPagePosition > 0);
        itemListArrowNext.SetActive(itemListPagePosition + 1 < itemListPageCount);

        string[] itemListPageString = new string[8];
        for (int i = 0; i < 8; i++)
        {
            if (i + (itemListPagePosition * 8) < itemListString.Length)
            {
                itemListPageString[i] = itemListString[i + (itemListPagePosition * 8)];
            }
        }

        for (int i = 0; i < 8; i++)
        {
            if (itemListPageString[i] != null)
            {
                buttonItemList[i].gameObject.SetActive(true);
                itemListIcon[i].sprite = Resources.Load<Sprite>("Items/" + itemListPageString[i]);
                itemListName[i].text = itemListPageString[i];
                itemListNameShadow[i].text = itemListName[i].text;
                itemListQuantity[i].text = "" + SaveData.currentSave.Bag.getQuantity(itemListPageString[i]);
                itemListQuantityShadow[i].text = itemListQuantity[i].text;
            }
            else
            {
                buttonItemList[i].gameObject.SetActive(false);
            }
        }
    }

    /// updates the pokemon slots to show the correct pokemon in the player's party
    private void UpdatePokemonSlotsDisplay()
    {
        for (int i = 0; i < 6; i++)
        {
            Pokemon selectedPokemon = SaveData.currentSave.PC.boxes[0][i];
            if (selectedPokemon == null)
            {
                buttonPokemonSlot[i].gameObject.SetActive(false);
            }
            else
            {
                buttonPokemonSlot[i].gameObject.SetActive(true);
                if (i == 0)
                {
                    if (i == pokePartyPosition)
                    {
                        buttonPokemonSlot[i].sprite = selectedPokemon.getStatus() != Pokemon.Status.FAINTED ? 
                            buttonPokemonRoundSelTex : buttonPokemonRoundFntSelTex;
                    }
                    else
                    {
                        buttonPokemonSlot[i].sprite = selectedPokemon.getStatus() != Pokemon.Status.FAINTED ?
                            buttonPokemonRoundTex : buttonPokemonRoundFntTex;
                    }
                }
                else
                {
                    if (i == pokePartyPosition)
                    {
                        buttonPokemonSlot[i].sprite = selectedPokemon.getStatus() != Pokemon.Status.FAINTED ?
                            buttonPokemonSelTex : buttonPokemonFntSelTex;
                    }
                    else
                    {
                        buttonPokemonSlot[i].sprite = selectedPokemon.getStatus() != Pokemon.Status.FAINTED ?
                            buttonPokemonTex : buttonPokemonFntTex;
                    }
                }
                pokemonIconAnim[i] = selectedPokemon.GetIcons_();
                pokemonSlotIcon[i].sprite = pokemonIconAnim[i][0];
                pokemonSlotName[i].text = selectedPokemon.getName();
                pokemonSlotNameShadow[i].text = pokemonSlotName[i].text;
                if (selectedPokemon.getGender() == Pokemon.Gender.FEMALE)
                {
                    pokemonSlotGender[i].text = "♀";
                    pokemonSlotGender[i].color = new Color(1, 0.2f, 0.2f, 1);
                }
                else if (selectedPokemon.getGender() == Pokemon.Gender.MALE)
                {
                    pokemonSlotGender[i].text = "♂";
                    pokemonSlotGender[i].color = new Color(0.2f, 0.4f, 1, 1);
                }
                else
                {
                    pokemonSlotGender[i].text = null;
                }
                pokemonSlotGenderShadow[i].text = pokemonSlotGender[i].text;
                pokemonSlotHPBar[i].rectTransform.sizeDelta =
                    new Vector2(
                        Mathf.FloorToInt(48f *
                                         ((float) selectedPokemon.getCurrentHP() / (float) selectedPokemon.getHP())), 4);

                BattleCalculation.SetHpBarColor(pokemonSlotHPBar[i], 48f);

                pokemonSlotLevel[i].text = "" + selectedPokemon.getLevel();
                pokemonSlotLevelShadow[i].text = pokemonSlotLevel[i].text;
                pokemonSlotCurrentHP[i].text = "" + selectedPokemon.getCurrentHP();
                pokemonSlotCurrentHPShadow[i].text = pokemonSlotCurrentHP[i].text;
                pokemonSlotMaxHP[i].text = "" + selectedPokemon.getHP();
                pokemonSlotMaxHPShadow[i].text = pokemonSlotMaxHP[i].text;
                pokemonSlotStatus[i].sprite = selectedPokemon.getStatus() != Pokemon.Status.NONE ?
                    Resources.Load<Sprite>("PCSprites/status" + selectedPokemon.getStatus()) 
                    : Resources.Load<Sprite>("null");
                pokemonSlotItem[i].enabled = !string.IsNullOrEmpty(selectedPokemon.getHeldItem());
            }
        }
    }

    private void UpdatePokemonSummaryDisplay(Pokemon selectedPokemon)
    {
        pokemonSelectedIcon.sprite = selectedPokemon.GetIcons_()[0];
        pokemonSelectedName.text = selectedPokemon.getName();
        pokemonSelectedNameShadow.text = pokemonSelectedName.text;
        if (selectedPokemon.getGender() == Pokemon.Gender.FEMALE)
        {
            pokemonSelectedGender.text = "♀";
            pokemonSelectedGender.color = new Color(1, 0.2f, 0.2f, 1);
        }
        else if (selectedPokemon.getGender() == Pokemon.Gender.MALE)
        {
            pokemonSelectedGender.text = "♂";
            pokemonSelectedGender.color = new Color(0.2f, 0.4f, 1, 1);
        }
        else
        {
            pokemonSelectedGender.text = null;
        }
        pokemonSelectedGenderShadow.text = pokemonSelectedGender.text;
        pokemonSelectedLevel.text = "" + selectedPokemon.getLevel();
        pokemonSelectedLevelShadow.text = pokemonSelectedLevel.text;
        pokemonSelectedStatus.sprite = selectedPokemon.getStatus() != Pokemon.Status.NONE ? 
            Resources.Load<Sprite>("PCSprites/status" + selectedPokemon.getStatus()) 
            : Resources.Load<Sprite>("null");
        pokemonSelectedType1.sprite = Resources.Load<Sprite>("null");
        pokemonSelectedType2.sprite = Resources.Load<Sprite>("null");
        PokemonData.Type type1 = PokemonDatabase.getPokemon(selectedPokemon.getID()).getType1();
        PokemonData.Type type2 = PokemonDatabase.getPokemon(selectedPokemon.getID()).getType2();
        if (type1 != PokemonData.Type.NONE)
        {
            pokemonSelectedType1.sprite = Resources.Load<Sprite>("PCSprites/type" + type1);
        }
        if (type2 != PokemonData.Type.NONE)
        {
            pokemonSelectedType2.sprite = Resources.Load<Sprite>("PCSprites/type" + type2);
        }

        //Summary
        float expCurrentLevel =
            PokemonDatabase.getLevelExp(PokemonDatabase.getPokemon(selectedPokemon.getID()).getLevelingRate(),
                selectedPokemon.getLevel());
        float expNextlevel =
            PokemonDatabase.getLevelExp(PokemonDatabase.getPokemon(selectedPokemon.getID()).getLevelingRate(),
                selectedPokemon.getLevel() + 1);
        float expAlong = selectedPokemon.getExp() - expCurrentLevel;
        float expDistance = expAlong / (expNextlevel - expCurrentLevel);
        pokemonSummaryNextLevelEXP.text = "" + (expNextlevel - selectedPokemon.getExp());
        pokemonSummaryNextLevelEXPShadow.text = pokemonSummaryNextLevelEXP.text;
        pokemonSummaryEXPBar.rectTransform.sizeDelta = new Vector2(Mathf.Floor(expDistance * 64), 3f);
        pokemonSummaryItemIcon.sprite = Resources.Load<Sprite>("null");
        pokemonSummaryItemName.text = "No held item.";
        if (!string.IsNullOrEmpty(selectedPokemon.getHeldItem()))
        {
            pokemonSummaryItemIcon.sprite = Resources.Load<Sprite>("Items/" + selectedPokemon.getHeldItem());
            pokemonSummaryItemName.text = selectedPokemon.getHeldItem();
        }

        pokemonSummaryItemNameShadow.text = pokemonSummaryItemName.text;
        //Stats
        float currentHp = selectedPokemon.getCurrentHP();
        float maxHp = selectedPokemon.getHP();
        pokemonSummaryHP.text = currentHp + "/" + maxHp;
        pokemonSummaryHPShadow.text = pokemonSummaryHP.text;
        pokemonSummaryHPBar.rectTransform.sizeDelta = new Vector2(Mathf.Floor((1 - (maxHp - currentHp) / maxHp) * 48f),
            4f);

        BattleCalculation.SetHpBarColor(pokemonSummaryHPBar, 48f);

        float[] natureMod = {
            NatureDatabase.getNature(selectedPokemon.getNature()).getATK(),
            NatureDatabase.getNature(selectedPokemon.getNature()).getDEF(),
            NatureDatabase.getNature(selectedPokemon.getNature()).getSPA(),
            NatureDatabase.getNature(selectedPokemon.getNature()).getSPD(),
            NatureDatabase.getNature(selectedPokemon.getNature()).getSPE()
        };
        pokemonSummaryStats.text =
            selectedPokemon.getATK() + "\n" +
            selectedPokemon.getDEF() + "\n" +
            selectedPokemon.getSPA() + "\n" +
            selectedPokemon.getSPD() + "\n" +
            selectedPokemon.getSPE();
        pokemonSummaryStatsShadow.text = pokemonSummaryStats.text;

        string[] statsLines = {"Attack", "Defence", "Sp. Atk", "Sp. Def", "Speed"};
        pokemonSummaryStatsTextShadow.text = "";
        for (int i = 0; i < 5; i++)
        {
            if (natureMod[i] > 1)
            {
                pokemonSummaryStatsTextShadow.text += "<color=#A01010FF>" + statsLines[i] + "</color>\n";
            }
            else if (natureMod[i] < 1)
            {
                pokemonSummaryStatsTextShadow.text += "<color=#0030A2FF>" + statsLines[i] + "</color>\n";
            }
            else
            {
                pokemonSummaryStatsTextShadow.text += statsLines[i] + "\n";
            }
        }

        pokemonSummaryAbilityName.text =
            PokemonDatabase.getPokemon(selectedPokemon.getID()).getAbility(selectedPokemon.getAbility());
        pokemonSummaryAbilityNameShadow.text = pokemonSummaryAbilityName.text;
        //abilities not yet implemented
        pokemonSummaryAbilityDescription.text = "";
        pokemonSummaryAbilityDescriptionShadow.text = pokemonSummaryAbilityDescription.text;

        //Moves
        string[] moveset = selectedPokemon.getMoveset();
        int[] maxPP = selectedPokemon.getMaxPP();
        int[] PP = selectedPokemon.getPP();
        for (int i = 0; i < 4; i++)
        {
            if (moveset[i] != null)
            {
                pokemonMovesName[i].text = moveset[i];
                pokemonMovesNameShadow[i].text = pokemonMovesName[i].text;
                pokemonMovesType[i].sprite =
                    Resources.Load<Sprite>("PCSprites/type" + MoveDatabase.getMove(moveset[i]).getType().ToString());
                pokemonMovesPPText[i].text = "PP";
                pokemonMovesPPTextShadow[i].text = pokemonMovesPPText[i].text;
                pokemonMovesPP[i].text = PP[i] + "/" + maxPP[i];
                pokemonMovesPPShadow[i].text = pokemonMovesPP[i].text;
            }
            else
            {
                pokemonMovesName[i].text = null;
                pokemonMovesNameShadow[i].text = pokemonMovesName[i].text;
                pokemonMovesType[i].sprite = Resources.Load<Sprite>("null");
                pokemonMovesPPText[i].text = null;
                pokemonMovesPPTextShadow[i].text = pokemonMovesPPText[i].text;
                pokemonMovesPP[i].text = null;
                pokemonMovesPPShadow[i].text = pokemonMovesPP[i].text;
            }
        }
    }

    private void UpdateSelectedTask(int newPosition)
    {
        taskPosition = newPosition;

        buttonBag.sprite = (taskPosition == 0 || taskPosition == 3) ? buttonBagSelTex : buttonBagTex;
        buttonFight.sprite = (taskPosition == 1) ? buttonFightSelTex : buttonFightTex;
        buttonPoke.sprite = (taskPosition == 2 || taskPosition == 5) ? buttonPokeSelTex : buttonPokeTex;
        buttonRun.sprite = (taskPosition == 4) ? buttonRunSelTex : buttonRunTex;
    }

    private void UpdateSelectedMove(int newPosition)
    {
        movePosition = newPosition;

        if (movePosition == 0)
        {
            buttonMegaEvolution.sprite = buttonMegaActiveSelTex;
        }
        else
        {
            buttonMegaEvolution.sprite = (canMegaEvolve) ? buttonMegaActiveTex : buttonMegaTex;
        }

        buttonMove[0].sprite = (movePosition == 1) ? buttonMoveBackgroundSelTex : buttonMoveBackgroundTex;
        buttonMove[1].sprite = (movePosition == 2) ? buttonMoveBackgroundSelTex : buttonMoveBackgroundTex;
        buttonMoveReturn.sprite = (movePosition == 3) ? buttonReturnSelTex : buttonReturnTex;
        buttonMove[2].sprite = (movePosition == 4) ? buttonMoveBackgroundSelTex : buttonMoveBackgroundTex;
        buttonMove[3].sprite = (movePosition == 5) ? buttonMoveBackgroundSelTex : buttonMoveBackgroundTex;
    }

    private void UpdateSelectedBagCategory(int newPosition)
    {
        bagCategoryPosition = newPosition;

        for (int i = 0; i < 6; i++)
        {
            if (i != bagCategoryPosition)
            {
                //deselect
                if (i == 4)
                {
                    buttonBackBag.sprite = buttonBackBagTex;
                }
                else if (i < 4)
                {
                    buttonItemCategory[i].sprite = buttonBagItemCategoryTex;
                }
                else
                {
                    buttonItemLastUsed.sprite = buttonBlueTex;
                }
            }
            else
            {
                //select
                if (i == 4)
                {
                    buttonBackBag.sprite = buttonBackBagSelTex;
                }
                else if (i < 4)
                {
                    buttonItemCategory[i].sprite = buttonBagItemCategorySelTex;
                }
                else
                {
                    buttonItemLastUsed.sprite = buttonBlueSelTex;
                }
            }
        }
    }

    private int UpdateSelectedItemListSlot(int currentPosition, int modifier)
    {
        int newPosition = currentPosition + modifier;

        //adjust for empty slots
        if (newPosition < 8)
        {
            if (!buttonItemList[newPosition].gameObject.activeSelf)
            {
                bool spaceFound = false;
                int checkPosition = currentPosition;
                //keep going back by modifier until avaiable position found
                while (!spaceFound)
                {
                    checkPosition += modifier;
                    if (checkPosition < 0)
                    {
                        if (buttonItemList[0].gameObject.activeSelf)
                        {
                            newPosition = 0; //move to first button
                        }
                        else
                        {
                            newPosition = 8; //move to back button
                        }
                        spaceFound = true;
                    }
                    else if (checkPosition > 7)
                    {
                        newPosition = 8; //set position to Back button
                        spaceFound = true;
                    }
                    else if (buttonItemList[checkPosition].gameObject.activeSelf)
                    {
                        newPosition = checkPosition; //adjust the position
                        spaceFound = true;
                    }
                    if (modifier == 0)
                    {
                        modifier = 1; //prevent infinite loops in case the modifier is set to never increment
                    }
                }
            }
        }
        else
        {
            newPosition = 8;
        }

        for (int i = 0; i < 8; i++)
        {
            if (i == newPosition)
            {
                buttonItemList[i].sprite = buttonBagItemListSelTex;
                buttonItemList[i].rectTransform.SetSiblingIndex(7);
            }
            else
            {
                buttonItemList[i].sprite = buttonBagItemListTex;
            }
        }
        buttonBackBag.sprite = newPosition == 8 ? buttonBackBagSelTex : buttonBackBagTex;

        itemListDescription.text = newPosition + (itemListPagePosition * 8) < itemListString.Length ?
            ItemDatabase.getItem(itemListString[newPosition + (itemListPagePosition * 8)]).getDescription() : "";
        itemListDescriptionShadow.text = itemListDescription.text;

        return newPosition;
    }
    private void UpdateSelectedPokemonSlot(int newPosition, bool backSelectable = true)
    {
        int maxPosition = 5;
        if (backSelectable)
        {
            maxPosition = 6;
        }
        if (newPosition < 6)
        {
            if (SaveData.currentSave.PC.boxes[0][newPosition] == null)
            {
                int checkPosition = pokePartyPosition;
                bool spaceFound = false;
                if (newPosition < pokePartyPosition)
                {
                    //keep going back 1 until avaiable position found
                    while (!spaceFound)
                    {
                        checkPosition -= 1;
                        if (checkPosition < 0)
                        {
                            newPosition = pokePartyPosition; //don't move the position
                            spaceFound = true;
                        }
                        else if (SaveData.currentSave.PC.boxes[0][checkPosition] != null)
                        {
                            newPosition = checkPosition; //adjust the position
                            spaceFound = true;
                        }
                    }
                }
                else
                {
                    //keep going forward 1
                    while (!spaceFound)
                    {
                        checkPosition += 1;
                        if (checkPosition > 5)
                        {
                            if (backSelectable)
                            {
                                newPosition = 6;
                            } //set position to Back button
                            else
                            {
                                newPosition = 5;
                                while (SaveData.currentSave.PC.boxes[0][newPosition] == null && newPosition > 0)
                                {
                                    newPosition -= 1;
                                }
                            }
                            spaceFound = true;
                        }
                        else if (SaveData.currentSave.PC.boxes[0][checkPosition] != null)
                        {
                            newPosition = checkPosition; //adjust the position
                            spaceFound = true;
                        }
                    }
                }
            }
        }
        else
        {
            newPosition = maxPosition;
            if (newPosition < 6)
            {
                if (SaveData.currentSave.PC.boxes[0][newPosition] == null)
                {
                    int checkPosition = pokePartyPosition;
                    bool spaceFound = false;
                    if (newPosition < pokePartyPosition)
                    {
                        //keep going back 1 until avaiable position found
                        while (!spaceFound)
                        {
                            checkPosition -= 1;
                            if (checkPosition < 0)
                            {
                                newPosition = pokePartyPosition; //don't move the position
                                spaceFound = true;
                            }
                            else if (SaveData.currentSave.PC.boxes[0][checkPosition] != null)
                            {
                                newPosition = checkPosition; //adjust the position
                                spaceFound = true;
                            }
                        }
                    }
                }
            }
        }

        pokePartyPosition = newPosition;

        for (int i = 0; i < 7; i++)
        {
            if (i != pokePartyPosition)
            {
                //unhighlight
                if (i == 0)
                {
                    if (SaveData.currentSave.PC.boxes[0][i] != null)
                    {
                        buttonPokemonSlot[i].sprite = (SaveData.currentSave.PC.boxes[0][i].getStatus() !=
                                                       Pokemon.Status.FAINTED)
                            ? buttonPokemonRoundTex
                            : buttonPokemonRoundFntTex;
                    }
                }
                else if (i < 6)
                {
                    if (SaveData.currentSave.PC.boxes[0][i] != null)
                    {
                        buttonPokemonSlot[i].sprite = (SaveData.currentSave.PC.boxes[0][i].getStatus() !=
                                                       Pokemon.Status.FAINTED)
                            ? buttonPokemonTex
                            : buttonPokemonFntTex;
                    }
                }
                else
                {
                    buttonBackPoke.sprite = buttonBackPokeTex;
                }
            }
            else
            {
                //highlight
                if (i == 0)
                {
                    if (SaveData.currentSave.PC.boxes[0][i] != null)
                    {
                        buttonPokemonSlot[i].sprite = (SaveData.currentSave.PC.boxes[0][i].getStatus() !=
                                                       Pokemon.Status.FAINTED)
                            ? buttonPokemonRoundSelTex
                            : buttonPokemonRoundFntSelTex;
                    }
                }
                else if (i < 6)
                {
                    if (SaveData.currentSave.PC.boxes[0][i] != null)
                    {
                        buttonPokemonSlot[i].sprite = (SaveData.currentSave.PC.boxes[0][i].getStatus() !=
                                                       Pokemon.Status.FAINTED)
                            ? buttonPokemonSelTex
                            : buttonPokemonFntSelTex;
                    }
                }
                else
                {
                    buttonBackPoke.sprite = buttonBackPokeSelTex;
                }
            }
        }
    }

    private int UpdateSummaryPosition(int newPosition)
    {
        buttonSwitch.sprite = (newPosition == 0) ? buttonBlueSelTex : buttonBlueTex;
        buttonCheck.sprite = (newPosition == 1) ? buttonBlueSelTex : buttonBlueTex;
        buttonBackPoke.sprite = (newPosition == 2) ? buttonBackPokeSelTex : buttonBackPokeTex;
        return newPosition;
    }

    private int UpdateMovesPosition(int newPosition)
    {
        Vector3[] positions = {
            new Vector3(-36, 51, 0), new Vector3(51, 51, 0),
            new Vector3(-36, 19, 0), new Vector3(51, 19, 0)
        };

        buttonSwitch.sprite = buttonBlueTex;
        buttonCheck.sprite = buttonBlueTex;
        buttonBackPoke.sprite = buttonBackPokeTex;

        if (newPosition < 4)
        {
            string[] moveset = SaveData.currentSave.PC.boxes[0][pokePartyPosition].getMoveset();
            if (string.IsNullOrEmpty(moveset[newPosition]))
            {
                pokemonMovesSelectedCategory.sprite = Resources.Load<Sprite>("null");
                pokemonMovesSelectedPower.text = null;
                pokemonMovesSelectedPowerShadow.text = pokemonMovesSelectedPower.text;
                pokemonMovesSelectedAccuracy.text = null;
                pokemonMovesSelectedAccuracyShadow.text = pokemonMovesSelectedAccuracy.text;
                pokemonMovesSelectedDescription.text = null;
                pokemonMovesSelectedDescriptionShadow.text = pokemonMovesSelectedDescription.text;
            }
            else
            {
                MoveData selectedMove = MoveDatabase.getMove(moveset[newPosition]);
                pokemonMovesSelectedCategory.sprite =
                    Resources.Load<Sprite>("PCSprites/category" + selectedMove.getCategory());
                pokemonMovesSelectedPower.text = "" + selectedMove.getPower();
                if (pokemonMovesSelectedPower.text == "0")
                {
                    pokemonMovesSelectedPower.text = "-";
                }
                pokemonMovesSelectedPowerShadow.text = pokemonMovesSelectedPower.text;
                pokemonMovesSelectedAccuracy.text = "" + Mathf.Round(selectedMove.getAccuracy() * 100f);
                if (pokemonMovesSelectedAccuracy.text == "0")
                {
                    pokemonMovesSelectedAccuracy.text = "-";
                }
                pokemonMovesSelectedAccuracyShadow.text = pokemonMovesSelectedAccuracy.text;
                pokemonMovesSelectedDescription.text = selectedMove.getDescription();
                pokemonMovesSelectedDescriptionShadow.text = pokemonMovesSelectedDescription.text;

                pokemonMovesSelectedMove.rectTransform.localPosition = positions[newPosition];
                pokemonMovesSelectedMove.enabled = true;
                if (pokemonMovesSelector.enabled)
                {
                    StartCoroutine(MoveMoveSelector(positions[newPosition]));
                }
                else
                {
                    pokemonMovesSelector.enabled = true;
                    pokemonMovesSelector.rectTransform.localPosition = positions[newPosition];
                }
            }
        }
        else
        {
            pokemonMovesSelectedCategory.sprite = Resources.Load<Sprite>("null");
            pokemonMovesSelectedPower.text = null;
            pokemonMovesSelectedPowerShadow.text = pokemonMovesSelectedPower.text;
            pokemonMovesSelectedAccuracy.text = null;
            pokemonMovesSelectedAccuracyShadow.text = pokemonMovesSelectedAccuracy.text;
            pokemonMovesSelectedDescription.text = null;
            pokemonMovesSelectedDescriptionShadow.text = pokemonMovesSelectedDescription.text;

            pokemonMovesSelectedMove.enabled = false;
            pokemonMovesSelector.enabled = false;
            if (newPosition == 4)
            {
                buttonSwitch.sprite = buttonBlueSelTex;
            }
            else if (newPosition == 5)
            {
                buttonCheck.sprite = buttonBlueSelTex;
            }
            else if (newPosition == 6)
            {
                buttonBackPoke.sprite = buttonBackPokeSelTex;
            }
        }
        return newPosition;
    }

    private IEnumerator MoveMoveSelector(Vector3 destinationPosition)
    {
        Vector3 startPosition = pokemonMovesSelector.rectTransform.localPosition;

        Vector3 distance = destinationPosition - startPosition;

        float increment = 0f;
        float speed = 0.2f;
        while (increment < 1)
        {
            increment += (1f / speed) * Time.deltaTime;
            if (increment > 1)
            {
                increment = 1;
            }
            pokemonMovesSelector.rectTransform.localPosition = startPosition + (distance * increment);
            yield return null;
        }
    }

    private IEnumerator AddExp(int position, int exp)
    {
        yield return
            StartCoroutine(DrawTextAndWait(pokemon[position].getName() + 
                                           " gained " + exp + " Exp. Points!", 1.8f, 1.8f));
        Dialog.UnDrawDialogBox();

        int expPool = exp;
        while (expPool > 0)
        {
            int expToNextLevel = pokemon[position].getExpNext() - pokemon[position].getExp();

            //if enough exp left to level up
            if (expPool >= expToNextLevel)
            {
                pokemon[position].addExp(expToNextLevel);
                expPool -= expToNextLevel;

                AudioSource fillSource = SfxHandler.Play(fillExpClip);
                yield return StartCoroutine(BattleAnimator.StretchBar(pokemon0ExpBar, 80f));
                SfxHandler.FadeSource(fillSource, 0.2f);
                SfxHandler.Play(expFullClip);
                yield return new WaitForSeconds(1f);

                BattleCalculation.UpdatePokemonStats(battleData[position].pokemonStats, pokemon[position]);
                UpdatePokemonStatsDisplay(position);

                BgmHandler.main.PlayMFX(Resources.Load<AudioClip>("Audio/mfx/GetAverage"));
                yield return
                    StartCoroutine(DrawTextAndWait(pokemon[position].getName() + " grew to Level " 
                            + pokemon[position].getLevel() + "!", 1.8f,1.8f));

                string newMove = pokemon[position].MoveLearnedAtLevel(pokemon[position].getLevel());
                if (!string.IsNullOrEmpty(newMove) && !pokemon[position].HasMove(newMove))
                {
                    yield return StartCoroutine(LearnMove(pokemon[position], newMove));
                }

                Dialog.UnDrawDialogBox();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                pokemon[position].addExp(expPool);
                expPool = 0;
                float levelStartExp =
                    PokemonDatabase.getLevelExp(
                        PokemonDatabase.getPokemon(pokemon[position].getID()).getLevelingRate(),
                        pokemon[position].getLevel());
                float currentExpMinusStart = pokemon[position].getExp() - levelStartExp;
                float nextLevelExpMinusStart = pokemon[position].getExpNext() - levelStartExp;

                AudioSource fillSource = SfxHandler.Play(fillExpClip);
                yield return
                    StartCoroutine(BattleAnimator.StretchBar(pokemon0ExpBar, 80f * (currentExpMinusStart / nextLevelExpMinusStart)));
                SfxHandler.FadeSource(fillSource, 0.2f);
                yield return new WaitForSeconds(1f);
            }


            yield return null;
        }
    }

    private IEnumerator LearnMove(Pokemon selectedPokemon, string move)
    {
        int chosenIndex = 1;
        if (chosenIndex == 1)
        {
            bool learning = true;
            while (learning)
            {
                //Moveset is full
                if (selectedPokemon.getMoveCount() == 4)
                {
                    yield return
                        StartCoroutine(
                            DrawTextAndWait(selectedPokemon.getName() + " wants to learn the \nmove " + move + "."));
                    yield return
                        StartCoroutine(
                            DrawTextAndWait("However, " + selectedPokemon.getName() + " already \nknows four moves."));
                    yield return
                        StartCoroutine(DrawTextAndWait("Should a move be deleted and \nreplaced with " + move + "?",
                            0.1f));

                    yield return StartCoroutine(Dialog.DrawChoiceBox());
                    chosenIndex = Dialog.chosenIndex;
                    Dialog.UndrawChoiceBox();
                    if (chosenIndex == 1)
                    {
                        yield return StartCoroutine(DrawTextAndWait("Which move should \nbe forgotten?"));

                        yield return StartCoroutine(ScreenFade.main.Fade(false, ScreenFade.defaultSpeed));

                        //Set SceneSummary to be active so that it appears
                        SceneScript.main.Summary.gameObject.SetActive(true);
                        StartCoroutine(SceneScript.main.Summary.control(new Pokemon[] { selectedPokemon }, learning: true, newMoveString:move));
                        //Start an empty loop that will only stop when SceneSummary is no longer active (is closed)
                        while (SceneScript.main.Summary.gameObject.activeSelf)
                        {
                            yield return null;
                        }

                        string replacedMove = SceneScript.main.Summary.replacedMove;
                        yield return StartCoroutine(ScreenFade.main.Fade(true, ScreenFade.defaultSpeed));

                        if (!string.IsNullOrEmpty(replacedMove))
                        {
                            Dialog.DrawDialogBox();
                            yield return StartCoroutine(Dialog.DrawTextSilent("1, "));
                            yield return new WaitForSeconds(0.4f);
                            yield return StartCoroutine(Dialog.DrawTextSilent("2, "));
                            yield return new WaitForSeconds(0.4f);
                            yield return StartCoroutine(Dialog.DrawTextSilent("and... "));
                            yield return new WaitForSeconds(0.4f);
                            yield return StartCoroutine(Dialog.DrawTextSilent("... "));
                            yield return new WaitForSeconds(0.4f);
                            yield return StartCoroutine(Dialog.DrawTextSilent("... "));
                            yield return new WaitForSeconds(0.4f);
                            SfxHandler.Play(pokeballBounceClip);
                            yield return StartCoroutine(Dialog.DrawTextSilent("Poof!"));
                            while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back"))
                            {
                                yield return null;
                            }

                            yield return
                                StartCoroutine(
                                    DrawTextAndWait(selectedPokemon.getName() + " forgot how to \nuse " + replacedMove +
                                                    "."));
                            yield return StartCoroutine(DrawTextAndWait("And..."));

                            Dialog.DrawDialogBox();
                            AudioClip mfx = Resources.Load<AudioClip>("Audio/mfx/GetAverage");
                            BgmHandler.main.PlayMFX(mfx);
                            StartCoroutine(Dialog.DrawTextSilent(selectedPokemon.getName() + " learned \n" + move + "!"));
                            yield return new WaitForSeconds(mfx.length);
                            while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back"))
                            {
                                yield return null;
                            }
                            Dialog.UnDrawDialogBox();
                            learning = false;
                        }
                        else
                        {
                            //give up?
                            chosenIndex = 0;
                        }
                    }
                    if (chosenIndex == 0)
                    {
                        //NOT ELSE because this may need to run after (chosenIndex == 1) runs
                        yield return
                            StartCoroutine(DrawTextAndWait("Give up on learning the move \n" + move + "?", 0.1f));

                        yield return StartCoroutine(Dialog.DrawChoiceBox());
                        chosenIndex = Dialog.chosenIndex;
                        Dialog.UndrawChoiceBox();
                        if (chosenIndex == 1)
                        {
                            learning = false;
                            chosenIndex = 0;
                        }
                    }
                }
                //Moveset is not full, can fit the new move easily
                else
                {
                    selectedPokemon.addMove(move);

                    Dialog.DrawDialogBox();
                    AudioClip mfx = Resources.Load<AudioClip>("Audio/mfx/GetAverage");
                    BgmHandler.main.PlayMFX(mfx);
                    StartCoroutine(Dialog.DrawTextSilent(selectedPokemon.getName() + " learned \n" + move + "!"));
                    yield return new WaitForSeconds(mfx.length);
                    while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back"))
                    {
                        yield return null;
                    }
                    Dialog.UnDrawDialogBox();
                    learning = false;
                }
            }
        }
        if (chosenIndex == 0)
        {
            //NOT ELSE because this may need to run after (chosenIndex == 1) runs
            //cancel learning loop
            yield return StartCoroutine(DrawTextAndWait(selectedPokemon.getName() + " did not learn \n" + move + "."));
        }
    }
    
    /// Apply the Move Effect to the target pokemon if possible
    private IEnumerator ApplyEffect(int attackerPosition, int targetPosition, MoveData.Effect effect, float parameter,
        bool animate = true)
    {
        var statIndices = new Dictionary<MoveData.Effect, int>
        {
            {MoveData.Effect.ATK, 0},
            {MoveData.Effect.ATKself, 0},
            {MoveData.Effect.DEF, 1},
            {MoveData.Effect.DEFself, 1},
            {MoveData.Effect.SPA, 2},
            {MoveData.Effect.SPAself, 2},
            {MoveData.Effect.SPD, 3},
            {MoveData.Effect.SPDself, 3},
            {MoveData.Effect.SPE, 4},
            {MoveData.Effect.SPEself, 4},
            {MoveData.Effect.ACC, 5},
            {MoveData.Effect.ACCself, 5},
            {MoveData.Effect.EVA, 6},
            {MoveData.Effect.EVAself, 6},
            
        };
        //most effects won't happen if a target has fainted.
        if (pokemon[targetPosition] != null)
        {
            if (pokemon[targetPosition].getStatus() != Pokemon.Status.FAINTED)
            {
                switch (effect)
                {
                    case MoveData.Effect.ATK:
                    case MoveData.Effect.DEF:
                    case MoveData.Effect.SPA:
                    case MoveData.Effect.SPD:
                    case MoveData.Effect.SPE:
                    case MoveData.Effect.ACC:
                    case MoveData.Effect.EVA:
                        yield return StartCoroutine(ModifyStat(targetPosition, 
                            statIndices[effect], parameter, animate));
                        break;
                    case MoveData.Effect.Burn:
                    {
                        yield return ApplyStatus(targetPosition, parameter, Pokemon.Status.BURNED, " was burned!");
                        break;
                    }
                    case MoveData.Effect.Freeze:
                    {
                        yield return ApplyStatus(targetPosition, parameter, Pokemon.Status.BURNED, " was frozen solid!");
                        break;
                    }
                    case MoveData.Effect.Paralyze:
                    {
                        yield return ApplyStatus(targetPosition, parameter, Pokemon.Status.PARALYZED, 
                            " was paralyzed! \\nIt may be unable to move!");
                        break;
                    }
                    case MoveData.Effect.Poison:
                    {
                        yield return ApplyStatus(targetPosition, parameter, Pokemon.Status.POISONED, " was poisoned!");
                        break;
                    }
                    case MoveData.Effect.Toxic:
                    {
                        yield return ApplyStatus(targetPosition, parameter, Pokemon.Status.POISONED,
                            " was badly posioned!");
                        break;
                    }
                    case MoveData.Effect.Sleep:
                    {
                        yield return ApplyStatus(targetPosition, parameter, Pokemon.Status.PARALYZED, " fell asleep!");
                        break;
                    }
                }
            }
        }

        switch (effect)
        {
            //effects that happen regardless of target fainting or not
            case MoveData.Effect.ATKself:
            case MoveData.Effect.DEFself:
            case MoveData.Effect.SPAself:
            case MoveData.Effect.SPDself:
            case MoveData.Effect.SPEself:
            case MoveData.Effect.ACCself:
            case MoveData.Effect.EVAself:
                yield return StartCoroutine(ModifyStat(attackerPosition, statIndices[effect],
                    parameter, animate));
                break;
        }
    }

    private IEnumerator ApplyStatus(int targetPosition, float parameter, Pokemon.Status status, string message)
    {
        if (Random.value <= parameter)
        {
            if (pokemon[targetPosition].setStatus(status))
            {
                yield return StartCoroutine(DrawTextAndWait(GeneratePreString(targetPosition) +
                                                            pokemon[targetPosition].getName() + message, 2.4f));
            }
        }
    }

    private IEnumerator ModifyStat(int targetPosition, int statIndex, float param, bool animate)
    {
        int parameter = Mathf.FloorToInt(param);

        string[] statName = {
            "Attack", "Defense", "Special Attack", "Special Defense", "Speed", "Accuracy", "Evasion"
        };

        bool canModify = true;
        if (battleData[targetPosition].pokemonStatsMod[statIndex] >= 6 && parameter > 0)
        {
            //can't go higher
            yield return StartCoroutine(DrawTextAndWait(GeneratePreString(targetPosition) 
                    + pokemon[targetPosition].getName() + "'s " + statName[statIndex] + " \\nwon't go any higher!", 
                2.4f));
            canModify = false;
        }
        else if (battleData[targetPosition].pokemonStatsMod[statIndex] <= -6 && parameter < 0)
        {
            //can't go lower
            yield return StartCoroutine(DrawTextAndWait(GeneratePreString(targetPosition) + 
                    pokemon[targetPosition].getName() + "'s " + statName[statIndex] + " won't go any lower!", 
                2.4f));
            canModify = false;
        }

        if (canModify)
        {
            battleData[targetPosition].pokemonStatsMod[statIndex] += parameter;
            if (battleData[targetPosition].pokemonStatsMod[statIndex] > 6)
            {
                battleData[targetPosition].pokemonStatsMod[statIndex] = 6;
            }
            else if (battleData[targetPosition].pokemonStatsMod[statIndex] < -6)
            {
                battleData[targetPosition].pokemonStatsMod[statIndex] = -6;
            }

            if (animate)
            {
                //multiple pokemon not yet implemented
                RawImage overlay = (targetPosition < 3) ? player1Overlay : opponent1Overlay;
                if (parameter > 0)
                {
                    SfxHandler.Play(statUpClip);
                    StartCoroutine(BattleAnimator.AnimateOverlayer(overlay, overlayStatUpTex, -1, 0, 1.2f, 0.3f));
                }
                else if (parameter < 0)
                {
                    SfxHandler.Play(statDownClip);
                    StartCoroutine(BattleAnimator.AnimateOverlayer(overlay, overlayStatDownTex, 1, 0, 1.2f, 0.3f));
                }

                yield return new WaitForSeconds(statUpClip.length + 0.2f);
            }
            
            var changeTexts = new Dictionary<int, string>
            {
                {1, " \\nrose!"},
                {-1, " \\nfell!"},
                {2, " \\nrose sharply!"},
                {-2, " \\nharshly fell!"},
                {3, " \\nrose drastically!"},
                {-3, " \\nseverely fell!"}
            };

            switch (parameter)
            {
                case 1:
                case -1:
                case 2:
                case -2:
                        yield return StartCoroutine(DrawTextAndWait(GeneratePreString(targetPosition) 
                            + pokemon[targetPosition].getName() + "'s " + statName[statIndex] + changeTexts[parameter], 
                            2.4f));
                    break;
                default:
                {
                    if (parameter >= 3)
                    {
                        yield return StartCoroutine(DrawTextAndWait(GeneratePreString(targetPosition) 
                            + pokemon[targetPosition].getName() + "'s " + statName[statIndex] + changeTexts[3], 
                            2.4f));
                    }
                    else if (parameter <= -3)
                    {
                        yield return StartCoroutine(DrawTextAndWait(GeneratePreString(targetPosition) 
                            + pokemon[targetPosition].getName() + "'s " + statName[statIndex] + changeTexts[-3], 
                            2.4f));
                    }
                    break;
                }
            }
        }
    }


    //////////////////////////////////

    //////////////////////////////////
    /// REPEATED SEQUENCES
    //
    ///This sequence heals a pokemon on the field. Do not use to heal a pokemon in the party.  
    private IEnumerator Heal(int index, float healAmount = -1, bool curingStatus = false)        
    {
        //If healing, and HP is already full    OR   if curing status, and status is already none or fainted (fainted pokemon can only be cured off-field).
        if ((!curingStatus && pokemon[index].getCurrentHP() == pokemon[index].getHP()) ||
            (curingStatus &&
             (pokemon[index].getStatus() == Pokemon.Status.NONE || pokemon[index].getStatus() == Pokemon.Status.FAINTED)))
        {
            //no effect
            yield return StartCoroutine(DrawTextAndWait("It had no effect...", 2.4f));
        }
        else
        {
            //SOUND
            SfxHandler.Play(healFieldClip);
            //Animate
            RawImage overlay = (index < 3) ? player1Overlay : opponent1Overlay;
            yield return new WaitForSeconds(1.2f);
            yield return StartCoroutine(BattleAnimator.AnimateOverlayer(overlay, overlayHealTex, -1, 0, 1.2f, 0.3f));
            if (curingStatus)
            {
                yield return CureStatus(index);
            }
            else
            {
                //if under 1.01f, heal based on percentage
                if (healAmount < 1.01f)
                {
                    healAmount = Mathf.CeilToInt(pokemon[index].getHP() * healAmount);
                }

                //Heal the pokemon and record how much HP was healed
                int healedHP = pokemon[index].getCurrentHP();
                pokemon[index].healHP(healAmount);
                healedHP = pokemon[index].getCurrentHP() - healedHP;

                if (index == 0)
                {
                    yield return StartCoroutine(BattleAnimator.StretchBar(statsHPBar[index], 
                        pokemon[index].getPercentHP() * 48f, 32, true,
                        pokemon0CurrentHP, pokemon0CurrentHPShadow, pokemon[index].getCurrentHP()));
                }
                else
                {
                    yield return StartCoroutine(BattleAnimator.StretchBar(statsHPBar[index], 
                            pokemon[index].getPercentHP() * 48f, 32, true));
                }
                yield return StartCoroutine(DrawTextAndWait(GeneratePreString(index) + 
                            pokemon[index].getName() + "'s HP was restored by " + healedHP + " point(s).", 2.4f));
            }
        }
    }

    private IEnumerator CureStatus(int index)
    {
        Pokemon.Status status = pokemon[index].getStatus();
        pokemon[index].healStatus();
        UpdatePokemonStatsDisplay(index);
        yield return new WaitForSeconds(0.3f);
        var cureMessages = new Dictionary<Pokemon.Status, string>
        {
            {Pokemon.Status.ASLEEP, " woke up!"},
            {Pokemon.Status.BURNED, "'s burn was healed!"},
            {Pokemon.Status.FROZEN, " thawed out!"},
            {Pokemon.Status.PARALYZED, " was cured of its paralysis!"},
            {Pokemon.Status.POISONED, " was cured of its poison!"}
            
        };
        switch (status)
        {
            case Pokemon.Status.ASLEEP:
            case Pokemon.Status.BURNED:
            case Pokemon.Status.FROZEN:
            case Pokemon.Status.PARALYZED:
            case Pokemon.Status.POISONED:
                yield return StartCoroutine(DrawTextAndWait(GeneratePreString(index) + 
                        pokemon[index].getName() + cureMessages, 2.4f));
                break;
        }
    }

    private IEnumerator DrawTextAndWait(string message, float time = 0, float lockedTime = 0, bool silent = true)
    {
        Dialog.DrawDialogBox();
        float startTime = Time.time;
        if (silent)
        {
            yield return StartCoroutine(Dialog.DrawTextSilent(message));
        }
        else
        {
            yield return StartCoroutine(Dialog.DrawText(message));
        }
        if (lockedTime > 0)
        {
            while (Time.time < startTime + lockedTime)
            {
                yield return null;
            }
        }
        if (time > 0)
        {
            while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back") && Time.time < startTime + time)
            {
                yield return null;
            }
        }
        else
        {
            while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back"))
            {
                yield return null;
            }
        }
    }

    private string GeneratePreString(int pokemonPosition)
    {
        string preString = "";
        if (pokemonPosition > 2)
        {
            preString = (trainerBattle) ? "The foe's " : "The wild ";
        }
        return preString;
    }


    private static float PlayCry(Pokemon pokemon)
    {
        SfxHandler.Play(pokemon.GetCry(), pokemon.GetCryPitch());
        return pokemon.GetCry().length / pokemon.GetCryPitch();
    }

    private IEnumerator PlayCryAndWait(Pokemon pokemon)
    {
        yield return new WaitForSeconds(PlayCry(pokemon));
    }


    /// Basic Wild Battle
    public IEnumerator Control(Pokemon wildPokemon)
    {
        yield return StartCoroutine(Control(false, new Trainer(new[] {wildPokemon}), false));
    }

    /// Basic Trainer Battle
    public IEnumerator Control(Trainer trainer)
    {
        yield return StartCoroutine(Control(true, trainer, false));
    }

    public IEnumerator Control(bool isTrainerBattle, Trainer trainer, bool healedOnDefeat)
    {
        //Used to compare after the battle to check for evolutions.
        int[] initialLevels = GetInitialLevels();

        trainerBattle = isTrainerBattle;
        Pokemon[] opponentParty = trainer.GetParty();
        string opponentName = trainer.GetName();
        
        GetBattleBackgrounds();
        SetTrainerSprites(trainer);
        victor = -1; //0 = player, 1 = opponent, 2 = tie

        //Reset position variables
        currentTask = 0;
        taskPosition = 1;
        movePosition = 1;
        bagCategoryPosition = 0;
        pokePartyPosition = 0;
        itemListPagePosition = 0;
        itemListPageCount = 0;

        UpdateCurrentTask(-1);
        UpdateSelectedTask(1);
        UpdateSelectedMove(1);
        InitializePokemon(opponentParty);
        ShowActivePokemon();
        running = true;

		Debug.Log(pokemon[0].getName()+": HP: "+pokemon[0].getHP()+"ATK: "+pokemon[0].getATK()+"DEF: "+pokemon[0].getDEF()+"SPA: "+pokemon[0].getSPA()+"SPD: "+pokemon[0].getSPD()+"SPE:"+pokemon[0].getSPE());
		Debug.Log(pokemon[3].getName()+": HP: "+pokemon[3].getHP()+"ATK: "+pokemon[3].getATK()+"DEF: "+pokemon[3].getDEF()+"SPA: "+pokemon[3].getSPA()+"SPD: "+pokemon[3].getSPD()+"SPE:"+pokemon[3].getSPE());


        if (trainerBattle)
        {
            yield return InitTrainerBattle(opponentParty, opponentName);
        }
        else
        {
            yield return InitWildBattle();
        }
        UpdateCurrentTask(0);

        playerFleeAttempts = 0;
        while (running)
        {
            ResetTurnTasks();
            //Reset Turn Feedback
            pokemonHasMoved = new bool[6];


            if (pokemon[0] != null)
            {
                UpdateMovesetDisplay(battleData[0].pokemonMoveset, pokemon[0].getPP(), pokemon[0].getMaxPP());
            }

            UpdatePokemonStatsDisplay(0);
            UpdatePokemonStatsDisplay(3);
            UpdateCurrentTask(0);

            //		Debug.Log(pokemon[0].getName()+": HP: "+pokemon[0].getHP()+"ATK: "+pokemon[0].getATK()+"DEF: "+pokemon[0].getDEF()+"SPA: "+pokemon[0].getSPA()+"SPD: "+pokemon[0].getSPD()+"SPE:"+pokemon[0].getSPE());
            //		Debug.Log(pokemon[3].getName()+": HP: "+pokemon[3].getHP()+"ATK: "+pokemon[3].getATK()+"DEF: "+pokemon[3].getDEF()+"SPA: "+pokemon[3].getSPA()+"SPD: "+pokemon[3].getSPD()+"SPE:"+pokemon[3].getSPE());

            runState = true;
            while (runState)
            {
                SetDebugOverlayTextures();
                if (Math.Abs(Input.GetAxisRaw("Horizontal")) > 0.001f || Math.Abs(Input.GetAxisRaw("Vertical")) > 0.001f)
                {
                    yield return NavigateMainOptions();
                }
                else if (Input.GetButtonDown("Select"))
                {
                    yield return ControlSelect();
                }
                yield return null;
            }
            AITurnSelection();
            yield return new WaitForSeconds(0.3f);
            yield return RunCommands(opponentName);
            
            if (running)
            {
                yield return AfterEffectsState();
            }
            if (running)
            {
                yield return ReplacementState(trainer, opponentParty, opponentName);
            }
        }


        //if defeated
        if (victor == 1)
        {
            //empty the paused clip, as the paused audio won't be resumed upon respawning
            BgmHandler.main.ResumeMain(1.4f, null, 0);
        }
        else
        {
            //if not defeated, the scene won't have faded out already
            StartCoroutine(ScreenFade.main.Fade(false, 1f));
            BgmHandler.main.ResumeMain(1.4f);
        }
        yield return new WaitForSeconds(1.4f);

        //check for evolutions to run ONLY if won, or healed on defeat
        if (victor == 0 || healedOnDefeat)
        {
            for (int i = 0; i < initialLevels.Length; i++)
            {
                if (SaveData.currentSave.PC.boxes[0][i] == null ||
                    SaveData.currentSave.PC.boxes[0][i].getLevel() == initialLevels[i] ||
                    !SaveData.currentSave.PC.boxes[0][i].canEvolve("Level")) continue;
                //if level is different to it was at the start of the battle
                //if can evolve
                BgmHandler.main.PlayOverlay(null, 0, 0);

                //Set SceneEvolution to be active so that it appears
                SceneScript.main.Evolution.gameObject.SetActive(true);
                StartCoroutine(SceneScript.main.Evolution.control(SaveData.currentSave.PC.boxes[0][i], "Level"));
                //Start an empty loop that will only stop when SceneEvolution is no longer active (is closed)
                while (SceneScript.main.Evolution.gameObject.activeSelf)
                {
                    yield return null;
                }
            }
        }
        //if defeated
        if (victor == 1)
        {
            if (!healedOnDefeat)
            {
                GlobalScript.global.sceneActivity.Respawn();
            }
        }

        GlobalScript.global.sceneActivity.resetFollower();
        this.gameObject.SetActive(false);
    }

    private IEnumerator ReplacementState(Trainer trainer, Pokemon[] opponentParty, string opponentName)
    {
        allOpponentsDefeated = true;
        for (int i = 0; i < opponentParty.Length; i++)
        {
            //check each opponent
            if (opponentParty[i].getStatus() != Pokemon.Status.FAINTED)
            {
                allOpponentsDefeated = false;
            }
        }
        //check if any player Pokemon are left
        bool allPlayersDefeated = true;
        for (int i = 0; i < 6; i++)
        {
            //check each player
            if (SaveData.currentSave.PC.boxes[0][i] != null)
            {
                if (SaveData.currentSave.PC.boxes[0][i].getStatus() != Pokemon.Status.FAINTED)
                {
                    allPlayersDefeated = false;
                }
            }
        }
        //if both sides have Pokemon left
        if (!allOpponentsDefeated && !allPlayersDefeated)
        {
            yield return ReplaceFaintedOpponentPokemon(opponentParty, opponentName);

            yield return ReplacePlayerFaintedPokemon();
        }
        yield return EndCheck(trainer, opponentName, allPlayersDefeated);
    }

    private IEnumerator EndCheck(Trainer trainer, string opponentName, bool allPlayersDefeated)
    {
        if (allPlayersDefeated)
        {
            victor = 1;
        }
        else if (allOpponentsDefeated)
        {
            victor = 0;
        }


        switch (victor)
        {
            case 0:
            {
                if (trainerBattle)
                {
                    if (trainer.victoryBGM == null)
                    {
                        BgmHandler.main.PlayOverlay(defaultTrainerVictoryBGM, defaultTrainerVictoryBGMLoopStart);
                    }
                    else
                    {
                        BgmHandler.main.PlayOverlay(trainer.victoryBGM, trainer.victorySamplesLoopStart);
                    }
                    yield return StartCoroutine(DrawTextAndWait(SaveData.currentSave.playerName + " defeated " + 
                                    opponentName + "!", 2.4f, 2.4f));
                    Dialog.UnDrawDialogBox();
                    yield return StartCoroutine(BattleAnimator.SlideTrainer(opponentBase, trainerSprite1, true, false));
                    foreach (var t in trainer.playerVictoryDialog)
                    {
                        yield return StartCoroutine(DrawTextAndWait(t));
                    }
                    Dialog.UnDrawDialogBox();
                    yield return StartCoroutine(DrawTextAndWait(SaveData.currentSave.playerName + " received $" +
                                            trainer.GetPrizeMoney() + " for winning!"));
                    SaveData.currentSave.playerMoney += trainer.GetPrizeMoney();
                }
                else
                {
                    if (trainer.victoryBGM == null)
                    {
                        BgmHandler.main.PlayOverlay(defaultWildVictoryBGM, defaultWildVictoryBGMLoopStart);
                    }
                    else
                    {
                        BgmHandler.main.PlayOverlay(trainer.victoryBGM, trainer.victorySamplesLoopStart);
                    }

                    //wild exp print out not yet implemented here
                }

                yield return new WaitForSeconds(0.2f);
                running = false;
                break;
            }
            case 1:
            {
                if (trainerBattle)
                {
                    yield return StartCoroutine(DrawTextAndWait(opponentName + " defeated " + 
                            SaveData.currentSave.playerName + "!", 2.4f, 2.4f));
                    Dialog.UnDrawDialogBox();
                    yield return StartCoroutine(BattleAnimator.SlideTrainer(opponentBase, trainerSprite1, true, false));
                    foreach (var t in trainer.playerLossDialog)
                    {
                        yield return StartCoroutine(DrawTextAndWait(t));
                    }

                    Dialog.UnDrawDialogBox();

                    StartCoroutine(ScreenFade.main.Fade(false, 1f));
                }
                else
                {
                    yield return StartCoroutine(DrawTextAndWait(SaveData.currentSave.playerName + 
                                                                " is out of usable Pokémon!", 2f));
                    yield return StartCoroutine(DrawTextAndWait(SaveData.currentSave.playerName + 
                                " dropped $200 in panic!", 2f));
                    yield return StartCoroutine(DrawTextAndWait("... ... ... ...", 2f));

                    yield return StartCoroutine(DrawTextAndWait(SaveData.currentSave.playerName + " blacked out!", 1.8f, 
                            1.8f));
                    Dialog.UnDrawDialogBox();
                    //overlayed dialog box not yet implemented
                    StartCoroutine(ScreenFade.main.Fade(false, 1f));
                }

                yield return new WaitForSeconds(0.2f);
                running = false;

                //fully heal players party so that they cant walk around with a defeated party
                for (int i = 0; i < 6; i++)
                {
                    if (SaveData.currentSave.PC.boxes[0][i] != null)
                    {
                        SaveData.currentSave.PC.boxes[0][i].healFull();
                    }
                }
                break;
            }
        }

        Dialog.UnDrawDialogBox();
        yield return new WaitForSeconds(0.4f);
    }

    private IEnumerator ReplacePlayerFaintedPokemon()
    {
//replace fainted Player Pokemon
        for (int i = 0; i < pokemonPerSide; i++)
        {
            //replace each player
            if (pokemon[i] != null) continue;
            Dialog.UnDrawDialogBox();

            UpdateCurrentTask(3);
            UpdateSelectedPokemonSlot(pokePartyPosition, false);
            yield return new WaitForSeconds(0.2f);
            while (currentTask == 3)
            {
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    if (pokePartyPosition < 5)
                    {
                        int positionBeforeModification = pokePartyPosition;
                        if (pokePartyPosition == 4)
                        {
                            UpdateSelectedPokemonSlot(pokePartyPosition + 1, false);
                        }
                        else
                        {
                            UpdateSelectedPokemonSlot(pokePartyPosition + 2, false);
                        }

                        if (positionBeforeModification != pokePartyPosition)
                        {
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                        }
                    }
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    if (pokePartyPosition < 6)
                    {
                        int positionBeforeModification = pokePartyPosition;
                        UpdateSelectedPokemonSlot(pokePartyPosition + 1, false);
                        if (positionBeforeModification != pokePartyPosition)
                        {
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                        }
                    }
                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    if (pokePartyPosition > 0)
                    {
                        int positionBeforeModification = pokePartyPosition;
                        UpdateSelectedPokemonSlot(pokePartyPosition - 1, false);
                        if (positionBeforeModification != pokePartyPosition)
                        {
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                        }
                    }
                }
                else if (Input.GetAxisRaw("Vertical") > 0)
                {
                    if (pokePartyPosition > 1)
                    {
                        int positionBeforeModification = pokePartyPosition;
                        if (pokePartyPosition == 6)
                        {
                            UpdateSelectedPokemonSlot(pokePartyPosition - 1, false);
                        }
                        else
                        {
                            UpdateSelectedPokemonSlot(pokePartyPosition - 2, false);
                        }

                        if (positionBeforeModification != pokePartyPosition)
                        {
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                        }
                    }
                }
                else if (Input.GetButtonDown("Select"))
                {
                    if (pokePartyPosition == 6)
                    {
                    } //debug
                    else if (SaveData.currentSave.PC.boxes[0][pokePartyPosition] != null)
                    {
                        UpdateCurrentTask(5);
                        SfxHandler.Play(selectClip);
                        int summaryPosition = UpdateSummaryPosition(0);
                        //0 = Switch, 1 = Moves, 2 = Back

                        yield return new WaitForSeconds(0.2f);
                        while (currentTask == 5)
                        {
                            if (Input.GetAxisRaw("Vertical") < 0)
                            {
                                if (pokePartyPosition < 5)
                                {
                                    int positionBeforeModification = pokePartyPosition;
                                    UpdateSelectedPokemonSlot(pokePartyPosition + 1, false);
                                    if (positionBeforeModification != pokePartyPosition)
                                    {
                                        SfxHandler.Play(scrollClip);
                                    }

                                    UpdatePokemonSummaryDisplay(
                                        SaveData.currentSave.PC.boxes[0][pokePartyPosition]);
                                    yield return new WaitForSeconds(0.2f);
                                }
                            }
                            else if (Input.GetAxisRaw("Horizontal") > 0)
                            {
                                if (summaryPosition < 2)
                                {
                                    summaryPosition = UpdateSummaryPosition(summaryPosition + 1);
                                    SfxHandler.Play(scrollClip);
                                    yield return new WaitForSeconds(0.2f);
                                }
                            }
                            else if (Input.GetAxisRaw("Horizontal") < 0)
                            {
                                if (summaryPosition > 0)
                                {
                                    summaryPosition = UpdateSummaryPosition(summaryPosition - 1);
                                    SfxHandler.Play(scrollClip);
                                    yield return new WaitForSeconds(0.2f);
                                }
                            }
                            else if (Input.GetAxisRaw("Vertical") > 0)
                            {
                                if (pokePartyPosition > 0)
                                {
                                    int positionBeforeModification = pokePartyPosition;
                                    UpdateSelectedPokemonSlot(pokePartyPosition - 1, false);
                                    if (positionBeforeModification != pokePartyPosition)
                                    {
                                        SfxHandler.Play(scrollClip);
                                    }

                                    UpdatePokemonSummaryDisplay(
                                        SaveData.currentSave.PC.boxes[0][pokePartyPosition]);
                                    yield return new WaitForSeconds(0.2f);
                                }
                            }
                            else if (Input.GetButtonDown("Select"))
                            {
                                if (summaryPosition == 0)
                                {
                                    // switch
                                    if (
                                        SaveData.currentSave.PC.boxes[0][pokePartyPosition].getStatus() !=
                                        Pokemon.Status.FAINTED)
                                    {
                                        //check that pokemon is not on the field
                                        bool notOnField = true;
                                        for (int i2 = 0; i2 < pokemonPerSide; i2++)
                                        {
                                            if (SaveData.currentSave.PC.boxes[0][pokePartyPosition] ==
                                                pokemon[i2])
                                            {
                                                notOnField = false;
                                                i2 = pokemonPerSide;
                                            }
                                        }

                                        if (notOnField)
                                        {
                                            BattleCalculation.SwitchPokemon(pokemon, i, battleData[i],
                                                SaveData.currentSave.PC.boxes[0][pokePartyPosition]);
                                            UpdateCurrentTask(-1);
                                            SfxHandler.Play(selectClip);

                                            yield return StartCoroutine(DrawTextAndWait("Go! "
                                                                                        + pokemon[i].getName() + "!", 1.5f,
                                                    1.5f))
                                                ;
                                            Dialog.UnDrawDialogBox();
                                            if (i == 0)
                                            {
                                                //DEBUG
                                                Debug.Log(pokemon[0].getLongID());
                                                StopCoroutine(animatePlayer1);
                                                animatePlayer1 =
                                                    StartCoroutine(BattleAnimator.AnimatePokemon(player1,
                                                        pokemon[0].GetBackAnim_()));
                                                yield return new WaitForSeconds(0.2f);
                                                UpdatePokemonStatsDisplay(i);
                                                yield return StartCoroutine(BattleAnimator.ReleasePokemon(player1));
                                                PlayCry(pokemon[0]);
                                                yield return new WaitForSeconds(0.3f);
                                                yield return StartCoroutine(SlidePokemonStats(0, false))
                                                    ;
                                            }
                                        }
                                        else
                                        {
                                            yield return StartCoroutine(DrawTextAndWait(
                                                SaveData.currentSave.PC.boxes[0][pokePartyPosition]
                                                    .getName() + " is already fighting!"));
                                            Dialog.UnDrawDialogBox();
                                        }
                                    }
                                    else
                                    {
                                        yield return StartCoroutine(DrawTextAndWait(
                                            SaveData.currentSave.PC.boxes[0][pokePartyPosition]
                                                .getName() + " is unable to fight!"));
                                        Dialog.UnDrawDialogBox();
                                    }
                                }
                                else if (summaryPosition == 1)
                                {
//check moves
                                    UpdateCurrentTask(6);
                                    SfxHandler.Play(selectClip);
                                    yield return new WaitForSeconds(0.2f);

                                    int movesPosition = 5;
                                    //0-3 = Moves, 4 = Switch, 5 = Summary, 6 = Back
                                    while (currentTask == 6)
                                    {
                                        if (Input.GetAxisRaw("Vertical") < 0)
                                        {
                                            if (movesPosition < 4)
                                            {
                                                if (movesPosition == 2)
                                                {
                                                    movesPosition =
                                                        UpdateMovesPosition(movesPosition + 3);
                                                }
                                                else
                                                {
                                                    movesPosition =
                                                        UpdateMovesPosition(movesPosition + 2);
                                                }

                                                SfxHandler.Play(scrollClip);
                                                yield return new WaitForSeconds(0.2f);
                                            }
                                        }
                                        else if (Input.GetAxisRaw("Horizontal") > 0)
                                        {
                                            if (movesPosition != 1 || movesPosition != 3 ||
                                                movesPosition != 6)
                                            {
                                                movesPosition = UpdateMovesPosition(movesPosition + 1);
                                                SfxHandler.Play(scrollClip);
                                                yield return new WaitForSeconds(0.2f);
                                            }
                                        }
                                        else if (Input.GetAxisRaw("Horizontal") < 0)
                                        {
                                            if (movesPosition == 1 || movesPosition == 3 ||
                                                movesPosition > 4)
                                            {
                                                movesPosition = UpdateMovesPosition(movesPosition - 1);
                                                SfxHandler.Play(scrollClip);
                                                yield return new WaitForSeconds(0.2f);
                                            }
                                        }
                                        else if (Input.GetAxisRaw("Vertical") > 0)
                                        {
                                            if (movesPosition > 1)
                                            {
                                                if (movesPosition > 3)
                                                {
                                                    movesPosition = UpdateMovesPosition(2);
                                                }
                                                else
                                                {
                                                    movesPosition =
                                                        UpdateMovesPosition(movesPosition - 2);
                                                }

                                                SfxHandler.Play(scrollClip);
                                                yield return new WaitForSeconds(0.2f);
                                            }
                                        }
                                        else if (Input.GetButtonDown("Select"))
                                        {
                                            if (movesPosition == 4)
                                            {
                                                // switch
                                                if (
                                                    SaveData.currentSave.PC.boxes[0][pokePartyPosition]
                                                        .getStatus() != Pokemon.Status.FAINTED)
                                                {
                                                    //check that pokemon is not on the field
                                                    bool notOnField = true;
                                                    for (int i2 = 0; i2 < pokemonPerSide; i2++)
                                                    {
                                                        if (
                                                            SaveData.currentSave.PC.boxes[0][
                                                                pokePartyPosition] == pokemon[i2])
                                                        {
                                                            notOnField = false;
                                                            i2 = pokemonPerSide;
                                                        }
                                                    }

                                                    if (notOnField)
                                                    {
                                                        BattleCalculation.SwitchPokemon(pokemon, i,
                                                            battleData[i], SaveData.currentSave.PC.boxes[0][
                                                                pokePartyPosition]);
                                                        UpdateCurrentTask(-1);
                                                        SfxHandler.Play(selectClip);

                                                        yield return
                                                            StartCoroutine(
                                                                DrawTextAndWait(
                                                                    SaveData.currentSave.playerName +
                                                                    " sent out " + pokemon[i].getName() +
                                                                    "!", 1.5f, 1.5f));
                                                        Dialog.UnDrawDialogBox();
                                                        if (i == 0)
                                                        {
                                                            //DEBUG
                                                            Debug.Log(pokemon[0].getLongID());
                                                            StopCoroutine(animatePlayer1);
                                                            animatePlayer1 =
                                                                StartCoroutine(BattleAnimator.AnimatePokemon(player1,
                                                                    pokemon[0].GetBackAnim_()));
                                                            yield return new WaitForSeconds(0.2f);
                                                            UpdatePokemonStatsDisplay(i);
                                                            yield return
                                                                StartCoroutine(BattleAnimator.ReleasePokemon(player1));
                                                            PlayCry(pokemon[0]);
                                                            yield return new WaitForSeconds(0.3f);
                                                            yield return
                                                                StartCoroutine(SlidePokemonStats(0,
                                                                    false));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        yield return
                                                            StartCoroutine(
                                                                DrawTextAndWait(
                                                                    SaveData.currentSave.PC.boxes[0][
                                                                        pokePartyPosition].getName() +
                                                                    " is already fighting!"));
                                                        Dialog.UnDrawDialogBox();
                                                    }
                                                }
                                                else
                                                {
                                                    yield return
                                                        StartCoroutine(
                                                            DrawTextAndWait(
                                                                SaveData.currentSave.PC.boxes[0][
                                                                    pokePartyPosition].getName() +
                                                                " is unable to fight!"));
                                                    Dialog.UnDrawDialogBox();
                                                }
                                            }
                                            else if (movesPosition == 5)
                                            {
//check summary
                                                UpdateCurrentTask(5);
                                                SfxHandler.Play(selectClip);
                                                yield return new WaitForSeconds(0.2f);
                                            }
                                            else if (movesPosition == 6)
                                            {
//back
                                                UpdateCurrentTask(3);
                                                SfxHandler.Play(selectClip);
                                                yield return new WaitForSeconds(0.2f);
                                            }
                                        }
                                        else if (Input.GetButtonDown("Back"))
                                        {
                                            UpdateCurrentTask(3);
                                            SfxHandler.Play(selectClip);
                                            yield return new WaitForSeconds(0.2f);
                                        }

                                        yield return null;
                                    }
                                }
                                else if (summaryPosition == 2)
                                {
//back
                                    UpdateCurrentTask(3);
                                    SfxHandler.Play(selectClip);
                                    yield return new WaitForSeconds(0.2f);
                                }
                            }
                            else if (Input.GetButtonDown("Back"))
                            {
                                UpdateCurrentTask(3);
                                SfxHandler.Play(selectClip);
                                yield return new WaitForSeconds(0.2f);
                            }

                            yield return null;
                        }
                    }
                }

                yield return null;
            }

            UpdateCurrentTask(-1);
        }
    }

    private IEnumerator ReplaceFaintedOpponentPokemon(Pokemon[] opponentParty, string opponentName)
    {
//replace fainted Opponent Pokemon (switch/set not yet implemented)
        for (int i = 0; i < pokemonPerSide; i++)
        {
            //replace each opponent
            if (pokemon[i + 3] != null) continue;
            //select the first able pokemon
            for (int i2 = 0; i2 < opponentParty.Length; i2++)
            {
                if (opponentParty[i2].getStatus() == Pokemon.Status.FAINTED) continue;
                //check that pokemon is not on the field
                bool notOnField = true;
                for (int i3 = 0; i3 < pokemonPerSide; i3++)
                {
                    //flexible faint animtions not yet implemented
                    if (opponentParty[i2] != pokemon[i3 + 3]) continue;
                    notOnField = false;
                    i3 = pokemonPerSide;
                }

                if (!notOnField) continue;
                BattleCalculation.SwitchPokemon(pokemon, i + 3, battleData[i + 3], opponentParty[i2]);

                yield return StartCoroutine(DrawTextAndWait(opponentName + " sent out "
                                                                         + pokemon[i + 3].getName() + "!", 1.5f, 1.5f));
                Dialog.UnDrawDialogBox();
                if (i == 0)
                {
                    //DEBUG
                    Debug.Log(pokemon[3].getLongID());
                    StopCoroutine(animateOpponent1);
                    animateOpponent1 =
                        StartCoroutine(BattleAnimator.AnimatePokemon(opponent1, pokemon[3].GetFrontAnim_()));
                    yield return new WaitForSeconds(0.2f);
                    UpdatePokemonStatsDisplay(i + 3);
                    yield return StartCoroutine(BattleAnimator.ReleasePokemon(opponent1));
                    PlayCry(pokemon[3]);
                    yield return new WaitForSeconds(0.3f);
                    yield return StartCoroutine(SlidePokemonStats(3, false));
                }

                i = pokemonPerSide;
                i2 = opponentParty.Length;
            }
        }
    }

    private IEnumerator AfterEffectsState()
    {
//running may be set to false by a successful flee
        //Apply after-moved effects
        for (int i = 0; i < 6; i++)
        {
            if (pokemon[i] != null)
            {
                if (pokemon[i].getStatus() == Pokemon.Status.BURNED ||
                    pokemon[i].getStatus() == Pokemon.Status.POISONED)
                {
                    pokemon[i].removeHP(Mathf.Floor(pokemon[i].getHP() / 8f));
                    if (pokemon[i].getStatus() == Pokemon.Status.BURNED)
                    { 
                        yield return StartCoroutine(DrawTextAndWait(GeneratePreString(i) + 
                                                pokemon[i].getName() + " is hurt by its burn!", 2.4f));
                    }
                    else if (pokemon[i].getStatus() == Pokemon.Status.POISONED)
                    {
                        yield return StartCoroutine(DrawTextAndWait(GeneratePreString(i) + 
                                                pokemon[i].getName() + " is hurt by poison!", 2.4f));
                    }

                    SfxHandler.Play(hitClip);

                    if (i == 0)
                    {
                        //if player pokemon 0 (only stats bar to display HP text)
                        yield return
                            StartCoroutine(BattleAnimator.StretchBar(statsHPBar[i],
                                Mathf.CeilToInt(pokemon[i].getPercentHP() * 48f), 32f, true, pokemon0CurrentHP,
                                pokemon0CurrentHPShadow, pokemon[i].getCurrentHP()));
                    }
                    else
                    {
                        yield return
                            StartCoroutine(BattleAnimator.StretchBar(statsHPBar[i],
                                Mathf.CeilToInt(pokemon[i].getPercentHP() * 48f), 32f, true));
                    }

                    yield return new WaitForSeconds(1.2f);

                    Dialog.UnDrawDialogBox();
                    UpdatePokemonStatsDisplay(i);
                }


                if (pokemon[i].getStatus() == Pokemon.Status.FAINTED)
                {
                    //debug = array of GUITextures not yet implemented
                    yield return
                        StartCoroutine(DrawTextAndWait(
                            GeneratePreString(i) + pokemon[i].getName() + " fainted!", 2.4f));
                    Dialog.UnDrawDialogBox();
                    yield return new WaitForSeconds(0.2f);
                    yield return new WaitForSeconds(PlayCry(pokemon[i]));
                    //flexible faint animtions not yet implemented
                    if (i == 0)
                    {
                        StartCoroutine(SlidePokemonStats(0, true));
                        yield return StartCoroutine(BattleAnimator.FaintPokemonAnimation(player1));
                    }
                    else if (i == 3)
                    {
                        StartCoroutine(SlidePokemonStats(3, true));
                        yield return StartCoroutine(BattleAnimator.FaintPokemonAnimation(opponent1));
                    }

                    pokemon[i] = null;
                }
            }
        }
    }

    private IEnumerator RunCommands(string opponentName)
    {
        //for each pokemon on field, in order of speed/priority, run their command
        for (int i = 0; i < 6; i++)
        {
            if (running)
            {
                //running may be set to false by a flee command
                int movingIndex = BattleCalculation.GetHighestSpeedIndex(pokemonHasMoved, battleData, pokemon);
                if (pokemon[movingIndex] != null)
                {
                    if (battleData[movingIndex].command == CommandType.Flee)
                    {
                        //RUN
                        if (movingIndex < 3)
                        {
                            //player attemps escape
                            playerFleeAttempts += 1;

                            int fleeChance = (pokemon[movingIndex].getSPE() * 128) / pokemon[3].getSPE() +
                                             30 * playerFleeAttempts;
                            if (Random.Range(0, 256) < fleeChance)
                            {
                                running = false;

                                SfxHandler.Play(runClip);
                                Dialog.DrawDialogBox();
                                yield return StartCoroutine(Dialog.DrawTextSilent("Got away safely!"));
                                while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back"))
                                {
                                    yield return null;
                                }

                                Dialog.UnDrawDialogBox();
                            }
                            else
                            {
                                yield return StartCoroutine(DrawTextAndWait("Can't escape!"));
                            }
                        }

                        pokemonHasMoved[movingIndex] = true;
                    }
                    else if (battleData[movingIndex].command == CommandType.Item)
                    {
                        //ITEM
                        //item effects not yet implemented fully

                        if (i < 3)
                        {
                            if (battleData[movingIndex].commandItem.getItemEffect() == ItemData.ItemEffect.BALL)
                            {
                                //debug autoselect targetIndex (target selection not yet implemented)
                                int targetIndex = 3;
                                //

                                //pokeball animation not yet implemented
                                yield return
                                    StartCoroutine(
                                        DrawTextAndWait(
                                            SaveData.currentSave.playerName + " used one " +
                                            battleData[movingIndex].commandItem.getName() + "!", 2.4f));
                                yield return new WaitForSeconds(1.2f);
                                if (trainerBattle)
                                {
                                    yield return
                                        StartCoroutine(DrawTextAndWait("The trainer blocked the ball!", 2.4f));
                                    yield return StartCoroutine(DrawTextAndWait("Don't be a theif!", 2.4f));
                                }
                                //calculate catch chance
                                else
                                {
                                    float ballRate = (float) battleData[movingIndex].commandItem.getFloatParameter();
                                    float catchRate =
                                        (float)
                                        PokemonDatabase.getPokemon(pokemon[targetIndex].getID()).getCatchRate();
                                    float statusRate = 1f;
                                    if ((pokemon[targetIndex].getStatus() != Pokemon.Status.NONE))
                                    {
                                        statusRate = (pokemon[targetIndex].getStatus() == Pokemon.Status.ASLEEP ||
                                                      pokemon[targetIndex].getStatus() == Pokemon.Status.FROZEN)
                                            ? 2.5f
                                            : 1.5f;
                                    }

                                    int modifiedRate =
                                        Mathf.FloorToInt(((3 * (float) pokemon[targetIndex].getHP() -
                                                           2 * (float) pokemon[targetIndex].getCurrentHP())
                                                          * catchRate * ballRate) /
                                                         (3 * (float) pokemon[targetIndex].getHP()) * statusRate);

                                    Debug.Log("modifiedRate: " + modifiedRate);

                                    //GEN VI
                                    //int shakeProbability = Mathf.FloorToInt(65536f / Mathf.Pow((255f/modifiedRate),0.1875f));
                                    //GEN V
                                    int shakeProbability =
                                        Mathf.FloorToInt(65536f / Mathf.Sqrt(Mathf.Sqrt(255f / modifiedRate)));

                                    int shakes = 0;

                                    string debugString = "";
                                    for (int shake = 0; shake < 4; shake++)
                                    {
                                        int shakeCheck = Random.Range(0, 65535);
                                        debugString += shake + ":(" + shakeCheck + "<" + shakeProbability + ")? ";
                                        if (shakeCheck < shakeProbability)
                                        {
                                            debugString += "Pass.   ";
                                            shakes += 1;
                                        }
                                        else
                                        {
                                            debugString += "Fail.   ";
                                            shake = 4;
                                        }
                                    }

                                    Debug.Log("(" + shakes + ")" + debugString);

                                    if (shakes == 4)
                                    {
                                        Debug.Log("Caught the " + pokemon[targetIndex].getName());
                                        running = false;

                                        //pokeball animation not yet implemented
                                        yield return StartCoroutine(BattleAnimator.FaintPokemonAnimation(opponent1));
                                        yield return new WaitForSeconds(1f);

                                        yield return
                                            StartCoroutine(
                                                DrawTextAndWait(
                                                    GeneratePreString(targetIndex) + pokemon[targetIndex].getName() +
                                                    " \\nwas caught!", 2.4f));

                                        Dialog.DrawDialogBox();
                                        yield return
                                            StartCoroutine(
                                                Dialog.DrawTextSilent(
                                                    "Would you like to give a nickname \\nto your new " +
                                                    pokemon[targetIndex].getName() + "?"));
                                        yield return StartCoroutine(Dialog.DrawChoiceBox());
                                        int chosenIndex = Dialog.chosenIndex;
                                        Dialog.UnDrawDialogBox();
                                        Dialog.UndrawChoiceBox();

                                        string nickname = null;
                                        if (chosenIndex == 1)
                                        {
                                            //give nickname
                                            SfxHandler.Play(selectClip);
                                            yield return StartCoroutine(ScreenFade.main.Fade(false, 0.4f));

                                            SceneScript.main.Typing.gameObject.SetActive(true);
                                            StartCoroutine(SceneScript.main.Typing.control(10, "",
                                                pokemon[targetIndex].getGender(), pokemon[targetIndex].GetIcons_()));
                                            while (SceneScript.main.Typing.gameObject.activeSelf)
                                            {
                                                yield return null;
                                            }

                                            if (SceneScript.main.Typing.typedString.Length > 0)
                                            {
                                                nickname = SceneScript.main.Typing.typedString;
                                            }

                                            yield return StartCoroutine(ScreenFade.main.Fade(true, 0.4f));
                                        }

                                        Debug.Log("CurrentHP" + pokemon[targetIndex].getCurrentHP());
                                        SaveData.currentSave.PC.addPokemon(new Pokemon(pokemon[targetIndex],
                                            nickname, battleData[movingIndex].commandItem.getName()));
                                    }

                                    Dialog.UnDrawDialogBox();
                                }
                            }
                            else if (battleData[movingIndex].commandItem.getItemEffect() == ItemData.ItemEffect.HP)
                            {
                                //commandTarget refers to the field position, healing a party member takes place before the turn.
                                if (battleData[movingIndex].commandTarget < 3)
                                {
                                    //if target is player
                                    yield return
                                        StartCoroutine(
                                            DrawTextAndWait(
                                                SaveData.currentSave.playerName + " used the " +
                                                battleData[movingIndex].commandItem.getName() + "!", 2.4f));
                                    yield return
                                        StartCoroutine(Heal(battleData[movingIndex].commandTarget,
                                            battleData[movingIndex].commandItem.getFloatParameter()));
                                }
                            }
                            else if (battleData[movingIndex].commandItem.getItemEffect() == ItemData.ItemEffect.STATUS)
                            {
                                //commandTarget refers to the field position, curing a party member takes place before the turn.
                                if (battleData[movingIndex].commandTarget < 3)
                                {
                                    //if target is player
                                    yield return
                                        StartCoroutine(
                                            DrawTextAndWait(
                                                SaveData.currentSave.playerName + " used the " +
                                                battleData[movingIndex].commandItem.getName() + "!", 2.4f));
                                    yield return StartCoroutine(Heal(battleData[movingIndex].commandTarget, -1, true));
                                }
                            }
                            else
                            {
                                //undefined effect
                                yield return StartCoroutine(DrawTextAndWait(SaveData.currentSave.playerName 
                                                                            + " used " + battleData[movingIndex].commandItem.getName() + "!", 2.4f));
                            }
                        }
                        else
                        {
                            yield return StartCoroutine(DrawTextAndWait(opponentName + " used " 
                                                                                     + battleData[movingIndex].commandItem.getName() + "!", 2.4f))
                                ;
                        }

                        pokemonHasMoved[movingIndex] = true;
                    }
                    else if (battleData[movingIndex].command == CommandType.Move)
                    {
                        //MOVE
                        //debug autoselect targetIndex (target selection not yet implemented)
                        int targetIndex = 3;
                        if (battleData[movingIndex].commandMove.getTarget() == MoveData.Target.SELF ||
                            battleData[movingIndex].commandMove.getTarget() == MoveData.Target.ADJACENTALLYSELF)
                        {
                            targetIndex = movingIndex;
                        }
                        else
                        {
                            if (movingIndex > 2)
                            {
                                targetIndex = 0;
                            }
                        }
                        //

                        if (pokemon[movingIndex].getStatus() != Pokemon.Status.FAINTED)
                        {
                            //calculate and test accuracy
                            float accuracy = battleData[movingIndex].commandMove.getAccuracy() *
                                             BattleCalculation.CalculateAccuracyModifier(battleData[movingIndex].pokemonStatsMod[5]);
                                             BattleCalculation.CalculateAccuracyModifier(battleData[movingIndex].pokemonStatsMod[6]);
                            bool canMove = true;
                            if (pokemon[movingIndex].getStatus() == Pokemon.Status.PARALYZED)
                            {
                                if (Random.value > 0.75f)
                                {
                                    yield return
                                        StartCoroutine(
                                            DrawTextAndWait(
                                                GeneratePreString(movingIndex) + pokemon[movingIndex].getName() +
                                                " is paralyzed! \\nIt can't move!", 2.4f));
                                    canMove = false;
                                }
                            }
                            else if (pokemon[movingIndex].getStatus() == Pokemon.Status.FROZEN)
                            {
                                if (Random.value > 0.2f)
                                {
                                    yield return
                                        StartCoroutine(
                                            DrawTextAndWait(
                                                GeneratePreString(movingIndex) + pokemon[movingIndex].getName() +
                                                " is \\nfrozen solid!", 2.4f));
                                    canMove = false;
                                }
                                else
                                {
                                    pokemon[movingIndex].setStatus(Pokemon.Status.NONE);
                                    UpdatePokemonStatsDisplay(movingIndex);
                                    yield return
                                        StartCoroutine(
                                            DrawTextAndWait(
                                                GeneratePreString(movingIndex) + pokemon[movingIndex].getName() +
                                                " thawed out!", 2.4f));
                                }
                            }
                            else if (pokemon[movingIndex].getStatus() == Pokemon.Status.ASLEEP)
                            {
                                pokemon[movingIndex].removeSleepTurn();
                                if (pokemon[movingIndex].getStatus() == Pokemon.Status.ASLEEP)
                                {
                                    yield return
                                        StartCoroutine(
                                            DrawTextAndWait(
                                                GeneratePreString(movingIndex) + pokemon[movingIndex].getName() +
                                                " is \\nfast asleep.", 2.4f));
                                    canMove = false;
                                }
                                else
                                {
                                    UpdatePokemonStatsDisplay(movingIndex);
                                    yield return
                                        StartCoroutine(
                                            DrawTextAndWait(
                                                GeneratePreString(movingIndex) + pokemon[movingIndex].getName() +
                                                " woke up!", 2.4f));
                                }
                            }

                            if (canMove)
                            {
                                //use the move
                                //deduct PP from the move
                                pokemon[movingIndex].removePP(battleData[movingIndex].commandMove.getName(), 1);
                                yield return
                                    StartCoroutine(
                                        DrawTextAndWait(
                                            GeneratePreString(movingIndex) + pokemon[movingIndex].getName() +
                                            " used " + battleData[movingIndex].commandMove.getName() + "!", 1.2f, 1.2f));

                                //adjust for accuracy
                                if (accuracy != 0 && Random.value > accuracy)
                                {
                                    //if missed, provide missed feedback
                                    yield return
                                        StartCoroutine(
                                            DrawTextAndWait(
                                                GeneratePreString(movingIndex) + pokemon[movingIndex].getName() +
                                                "'s attack missed!", 2.4f));
                                    canMove = false;
                                }
                            }

                            if (canMove)
                            {
                                //if didn't miss
                                //set up variables needed later
                                float damageToDeal = 0;
                                bool applyCritical = false;
                                float superEffectiveModifier = -1;

                                //check for move effects that change how damage is calculated (Heal / Set Damage / etc.) (not yet implemented fully)
                                if (battleData[movingIndex].commandMove.hasMoveEffect(MoveData.Effect.Heal))
                                {
                                    yield return
                                        StartCoroutine(Heal(targetIndex,
                                            battleData[movingIndex].commandMove.getMoveParameter(MoveData.Effect.Heal)));
                                }
                                else if (battleData[movingIndex].commandMove.hasMoveEffect(MoveData.Effect.SetDamage))
                                {
                                    damageToDeal =
                                        battleData[movingIndex].commandMove.getMoveParameter(MoveData.Effect.SetDamage);
                                    //if parameter is 0, then use the pokemon's level
                                    if (damageToDeal == 0)
                                    {
                                        damageToDeal = pokemon[movingIndex].getLevel();
                                    }

                                    //check for any ineffectivity
                                    superEffectiveModifier =
                                        BattleCalculation.GetSuperEffectiveModifier(battleData[movingIndex].commandMove.getType(),
                                            battleData[targetIndex].pokemonType1) *
                                        BattleCalculation.GetSuperEffectiveModifier(battleData[movingIndex].commandMove.getType(),
                                            battleData[targetIndex].pokemonType2) *
                                        BattleCalculation.GetSuperEffectiveModifier(battleData[movingIndex].commandMove.getType(),
                                            battleData[targetIndex].pokemonType3);
                                    //if able to hit, set to 1 to prevent super effective messages appearing
                                    if (superEffectiveModifier > 0f)
                                    {
                                        superEffectiveModifier = 1f;
                                    }
                                }
                                else
                                {
                                    //calculate damage
                                    damageToDeal = BattleCalculation.CalculateDamage(pokemon[movingIndex], 
                                        battleData[movingIndex].pokemonStats, battleData[targetIndex].pokemonStats,
                                        battleData[movingIndex].commandMove);
                                    applyCritical = BattleCalculation.CalculateCritical(battleData[movingIndex].focusEnergy, 
                                        battleData[movingIndex].commandMove);
                                    if (applyCritical)
                                    {
                                        damageToDeal *= 1.5f;
                                    }

                                    superEffectiveModifier =
                                        BattleCalculation.GetSuperEffectiveModifier(battleData[movingIndex].commandMove.getType(),
                                            battleData[targetIndex].pokemonType1) *
                                        BattleCalculation.GetSuperEffectiveModifier(battleData[movingIndex].commandMove.getType(),
                                            battleData[targetIndex].pokemonType2) *
                                        BattleCalculation.GetSuperEffectiveModifier(battleData[movingIndex].commandMove.getType(),
                                            battleData[movingIndex].pokemonType3);
                                    damageToDeal *= superEffectiveModifier;
                                    //apply offense/defense boosts.
                                    float damageBeforeMods = damageToDeal;
                                    if (battleData[movingIndex].commandMove.getCategory() == MoveData.Category.PHYSICAL)
                                    {
                                        if (applyCritical)
                                        {
                                            //if a critical lands
                                            if (battleData[movingIndex].pokemonStatsMod[0] > 0)
                                            {
                                                //only apply ATKmod if positive
                                                damageToDeal *=
                                                    BattleCalculation.CalculateStatModifier(battleData[movingIndex].pokemonStatsMod[0]);
                                            }

                                            if (battleData[targetIndex].pokemonStatsMod[1] < 0)
                                            {
                                                //only apply DEFmod if negative
                                                damageToDeal *=
                                                    BattleCalculation.CalculateStatModifier(battleData[targetIndex].pokemonStatsMod[1]);
                                            }
                                        }
                                        else
                                        {
                                            //apply ATK and DEF mods normally (also half damage if burned)
                                            damageToDeal *= BattleCalculation.CalculateStatModifier(battleData[movingIndex].pokemonStatsMod[0]);
                                            damageToDeal /= BattleCalculation.CalculateStatModifier(battleData[targetIndex].pokemonStatsMod[1]);
                                            if (pokemon[movingIndex].getStatus() == Pokemon.Status.BURNED)
                                            {
                                                damageToDeal /= 2f;
                                            }
                                        }
                                    }
                                    else if (battleData[movingIndex].commandMove.getCategory() == MoveData.Category.SPECIAL)
                                    {
                                        if (applyCritical)
                                        {
                                            //same as above, only using the Special varients
                                            if (battleData[movingIndex].pokemonStatsMod[2] > 0)
                                            {
                                                damageToDeal *=
                                                    BattleCalculation.CalculateStatModifier(battleData[movingIndex].pokemonStatsMod[2]);
                                            }

                                            if (battleData[targetIndex].pokemonStatsMod[3] < 0)
                                            {
                                                damageToDeal *=
                                                    BattleCalculation.CalculateStatModifier(battleData[movingIndex].pokemonStatsMod[3]);
                                            }
                                        }
                                        else
                                        {
                                            damageToDeal *= BattleCalculation.CalculateStatModifier(battleData[movingIndex].pokemonStatsMod[2]);
                                            damageToDeal /= BattleCalculation.CalculateStatModifier(battleData[movingIndex].pokemonStatsMod[3]);
                                        }
                                    }
                                }

                                //inflict damage
                                int DEBUG_beforeHP = pokemon[targetIndex].getCurrentHP();
                                pokemon[targetIndex].removeHP(damageToDeal);
                                Debug.Log(DEBUG_beforeHP + " - " + damageToDeal + " = " +
                                          pokemon[targetIndex].getCurrentHP());

                                if (damageToDeal > 0)
                                {
                                    if (superEffectiveModifier > 1.01f)
                                    {
                                        SfxHandler.Play(hitSuperClip);
                                    }
                                    else if (superEffectiveModifier < 0.99f)
                                    {
                                        SfxHandler.Play(hitPoorClip);
                                    }
                                    else
                                    {
                                        SfxHandler.Play(hitClip);
                                    }
                                }

                                if (targetIndex == 0)
                                {
                                    //if player pokemon 0 (only stats bar to display HP text)
                                    yield return
                                        StartCoroutine(BattleAnimator.StretchBar(statsHPBar[targetIndex],
                                            Mathf.CeilToInt(pokemon[targetIndex].getPercentHP() * 48f), 32f, true,
                                            pokemon0CurrentHP, pokemon0CurrentHPShadow,
                                            pokemon[targetIndex].getCurrentHP()));
                                }
                                else
                                {
                                    yield return
                                        StartCoroutine(BattleAnimator.StretchBar(statsHPBar[targetIndex],
                                            Mathf.CeilToInt(pokemon[targetIndex].getPercentHP() * 48f), 32f, true,
                                            null, null, 0));
                                }

                                yield return new WaitForSeconds(0.4f);

                                UpdatePokemonStatsDisplay(targetIndex);

                                //Feedback on the damage dealt
                                if (superEffectiveModifier == 0)
                                {
                                    yield return StartCoroutine(DrawTextAndWait("It had no effect...", 2.4f));
                                }
                                else if (battleData[movingIndex].commandMove.getCategory() != MoveData.Category.STATUS)
                                {
                                    if (applyCritical)
                                    {
                                        yield return StartCoroutine(DrawTextAndWait("A Critical Hit!", 2.4f));
                                    }

                                    if (superEffectiveModifier > 1)
                                    {
                                        yield return StartCoroutine(DrawTextAndWait("It's Super Effective!", 2.4f));
                                    }
                                    else if (superEffectiveModifier < 1)
                                    {
                                        yield return
                                            StartCoroutine(DrawTextAndWait("It's not very effective.", 2.4f));
                                    }
                                }

                                //Faint the target if nessecary
                                if (pokemon[targetIndex].getStatus() == Pokemon.Status.FAINTED)
                                {
                                    //debug = array of GUITextures not yet implemented
                                    yield return
                                        StartCoroutine(
                                            DrawTextAndWait(
                                                GeneratePreString(targetIndex) + pokemon[targetIndex].getName() +
                                                " fainted!", 2.4f));
                                    Dialog.UnDrawDialogBox();
                                    yield return new WaitForSeconds(0.2f);
                                    yield return new WaitForSeconds(PlayCry(pokemon[targetIndex]));
                                    //flexible faint animtions not yet implemented
                                    if (targetIndex == 0)
                                    {
                                        StartCoroutine(SlidePokemonStats(0, true));
                                        yield return StartCoroutine(BattleAnimator.FaintPokemonAnimation(player1));
                                    }
                                    else if (targetIndex == 3)
                                    {
                                        StartCoroutine(SlidePokemonStats(3, true));
                                        yield return StartCoroutine(BattleAnimator.FaintPokemonAnimation(opponent1));
                                    }

                                    //give EXP / add EXP
                                    if (targetIndex > 2)
                                    {
                                        for (int i2 = 0; i2 < pokemonPerSide; i2++)
                                        {
                                            if (pokemon[i2].getStatus() != Pokemon.Status.FAINTED)
                                            {
                                                float isWildMod = (trainerBattle) ? 1.5f : 1f;
                                                float baseExpYield =
                                                    PokemonDatabase.getPokemon(pokemon[targetIndex].getID())
                                                        .getBaseExpYield();
                                                float luckyEggMod = (pokemon[i2].getHeldItem() == "Lucky Egg") ? 1.5f : 1f;
                                                float OTMod = (pokemon[i2].getIDno() !=
                                                               SaveData.currentSave.playerID)
                                                    ? 1.5f
                                                    : 1f;
                                                float sharedMod = 1f; //shared experience
                                                float IVMod = 0.85f +
                                                              (float)
                                                              (pokemon[targetIndex].getIV_HP() +
                                                               pokemon[targetIndex].getIV_ATK() +
                                                               pokemon[targetIndex].getIV_DEF() +
                                                               pokemon[targetIndex].getIV_SPA() +
                                                               pokemon[targetIndex].getIV_SPD() +
                                                               pokemon[targetIndex].getIV_SPE()) / 480f;
                                                //IV Mod is unique to Pokemon Unity
                                                int exp =
                                                    Mathf.CeilToInt((isWildMod * baseExpYield * IVMod * OTMod *
                                                                     luckyEggMod *
                                                                     (float) pokemon[targetIndex].getLevel()) / 7 *
                                                                    sharedMod);

                                                yield return StartCoroutine(AddExp(i2, exp));
                                            }
                                        }
                                    }

                                    pokemon[targetIndex] = null;
                                }

                                //Move effects should not apply to those pokemon that are immune to that move. not yet implemented

                                //apply move effects
                                MoveData.Effect[] moveEffects = battleData[movingIndex].commandMove.getMoveEffects();
                                float[] moveEffectParameters = battleData[movingIndex].commandMove.getMoveParameters();

                                //track these and prevent multiple statUp/Down anims
                                bool statUpRun = false;
                                bool statDownRun = false;
                                bool statUpSelfRun = false;
                                bool statDownSelfRun = false;
                                for (int i2 = 0; i2 < moveEffects.Length; i2++)
                                {
                                    //Check for Chance effect. if failed, no further effects will run
                                    if (moveEffects[i2] == MoveData.Effect.Chance)
                                    {
                                        if (Random.value > moveEffectParameters[i2])
                                        {
                                            i2 = moveEffects.Length;
                                        }
                                    }
                                    else
                                    {
                                        //Check these booleans to prevent running an animation twice for one pokemon.
                                        bool animate = false;
                                        switch (moveEffects[i2])
                                        {
                                            //check if statUp/Down Effect
                                            case MoveData.Effect.ATK:
                                            case MoveData.Effect.DEF:
                                            case MoveData.Effect.SPA:
                                            case MoveData.Effect.SPD:
                                            case MoveData.Effect.SPE:
                                            case MoveData.Effect.ACC:
                                            //check if Self statUp/Down Effect
                                            case MoveData.Effect.EVA:
                                            {
                                                //if statUp, and haven't run statUp yet, set statUpRun bool to true;
                                                if (moveEffectParameters[i2] > 0 && !statUpRun)
                                                {
                                                    statUpRun = true;
                                                    animate = true;
                                                }
                                                else if (moveEffectParameters[i2] < 0 && !statDownRun)
                                                {
                                                    statDownRun = true;
                                                    animate = true;
                                                }

                                                break;
                                            }
                                            case MoveData.Effect.ATKself:
                                            case MoveData.Effect.DEFself:
                                            case MoveData.Effect.SPAself:
                                            case MoveData.Effect.SPDself:
                                            case MoveData.Effect.SPEself:
                                            case MoveData.Effect.ACCself:
                                            case MoveData.Effect.EVAself:
                                            {
                                                //if statUp, and haven't run statUp yet, set statUpRun bool to true;
                                                if (moveEffectParameters[i2] > 0 && !statUpSelfRun)
                                                {
                                                    statUpSelfRun = true;
                                                    animate = true;
                                                }
                                                else if (moveEffectParameters[i2] < 0 && !statDownSelfRun)
                                                {
                                                    statDownSelfRun = true;
                                                    animate = true;
                                                }

                                                break;
                                            }
                                            default:
                                                animate = true;
                                                break;
                                        }

                                        yield return
                                            StartCoroutine(ApplyEffect(movingIndex, targetIndex, moveEffects[i2],
                                                moveEffectParameters[i2], animate));
                                    }
                                }

                                UpdatePokemonStatsDisplay(targetIndex);
                            }
                        }

                        pokemonHasMoved[movingIndex] = true;
                    }
                    else if (battleData[movingIndex].command == CommandType.Switch)
                    {
                        //switch pokemon
                        //enemy switching not yet implemented

                        yield return
                            StartCoroutine(DrawTextAndWait(pokemon[movingIndex].getName() + ", come back!", 1.5f,
                                1.5f));
                        Dialog.UnDrawDialogBox();

                        StartCoroutine(SlidePokemonStats(0, true));
                        yield return StartCoroutine(BattleAnimator.WithdrawPokemon(player1));
                        yield return new WaitForSeconds(0.5f);

                        BattleCalculation.SwitchPokemon(pokemon, movingIndex, battleData[movingIndex], 
                            battleData[movingIndex].commandPokemon);

                        yield return new WaitForSeconds(0.5f);
                        yield return
                            StartCoroutine(DrawTextAndWait("Go! " + pokemon[movingIndex].getName() + "!", 1.5f,
                                1.5f));
                        Dialog.UnDrawDialogBox();

                        if (i == 0)
                        {
                            //DEBUG
                            Debug.Log(pokemon[0].getLongID());
                            StopCoroutine(animatePlayer1);
                            animatePlayer1 = StartCoroutine(BattleAnimator.AnimatePokemon(player1, pokemon[0].GetBackAnim_()));
                            yield return new WaitForSeconds(0.2f);
                            UpdatePokemonStatsDisplay(i);
                            yield return StartCoroutine(BattleAnimator.ReleasePokemon(player1));
                            PlayCry(pokemon[0]);
                            yield return new WaitForSeconds(0.3f);
                            yield return StartCoroutine(SlidePokemonStats(0, false));
                        }

                        pokemonHasMoved[movingIndex] = true;
                    }
                }
                else
                {
                    //count pokemon as moved as pokemon does not exist.
                    pokemonHasMoved[movingIndex] = true;
                }
            }
        }
    }

    private void AITurnSelection()
    {
//AI not yet implemented properly.
        //the following code randomly chooses a move to use with no further thought.
        for (int i = 0; i < pokemonPerSide; i++)
        {
            //do for every pokemon on enemy side
            int pi = i + 3;
            if (pokemon[pi] == null) continue;
            //check if struggle is to be used (no PP left in any move)
            if (pokemon[pi].getPP(0) == 0 && pokemon[pi].getPP(1) == 0 &&
                pokemon[pi].getPP(2) == 0 && pokemon[pi].getPP(3) == 0)
            {
                battleData[pi].commandMove = MoveDatabase.getMove("Struggle");
            }
            else
            {
                //Randomly choose a move from the moveset
                int AImoveIndex = Random.Range(0, 4);
                while (battleData[pi].pokemonMoveset != null &&
                       string.IsNullOrEmpty(battleData[pi].pokemonMoveset[AImoveIndex]) &&
                       pokemon[pi].getPP(AImoveIndex) == 0)
                {
                    AImoveIndex = Random.Range(0, 4);
                }

                battleData[pi].command = CommandType.Move;
                battleData[pi].commandMove = MoveDatabase.getMove(battleData[pi].pokemonMoveset[AImoveIndex]);
                Debug.Log(battleData[pi].commandMove.getName() + ", PP: " + pokemon[pi].getPP(AImoveIndex));
            }
        }
    }

    private IEnumerator ControlSelect()
    {
        //// NAVIGATE MOVESET OPTIONS ////
        int currentPokemon = 0;
        switch (taskPosition)
        {
            case 1:
            {
                yield return ControlFight(currentPokemon);
                break;
            }
            case 4:
            {
                yield return ControlFleeAttempt(currentPokemon);
                break;
            }
            case 0:
            case 3:
            {
                yield return ControlBagInterFace(currentPokemon);
                break;
            }
            case 2:
            case 5:
            {
                yield return ControlPokeMenu(currentPokemon);
                break;
            }
        }
    }

    private IEnumerator ControlPokeMenu(int currentPokemon)
    {
        UpdateCurrentTask(3);
        SfxHandler.Play(selectClip);
        yield return null;

        //while still in Poke menu
        while (currentTask == 3)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                if (pokePartyPosition < 6)
                {
                    if (pokePartyPosition == 5)
                    {
                        UpdateSelectedPokemonSlot(pokePartyPosition + 1);
                    }
                    else
                    {
                        UpdateSelectedPokemonSlot(pokePartyPosition + 2);
                    }

                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                if (pokePartyPosition < 6)
                {
                    UpdateSelectedPokemonSlot(pokePartyPosition + 1);
                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                if (pokePartyPosition > 0)
                {
                    UpdateSelectedPokemonSlot(pokePartyPosition - 1);
                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                if (pokePartyPosition > 1)
                {
                    if (pokePartyPosition == 6)
                    {
                        UpdateSelectedPokemonSlot(pokePartyPosition - 1);
                    }
                    else
                    {
                        UpdateSelectedPokemonSlot(pokePartyPosition - 2);
                    }

                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Input.GetButtonDown("Select"))
            {
                yield return ControlPokeMenuSelect(currentPokemon);
            }
            else if (Input.GetButtonDown("Back"))
            {
                SfxHandler.Play(selectClip);
                UpdateCurrentTask(0);
            }

            yield return null;
        }
    }

    private IEnumerator ControlPokeMenuSelect(int currentPokemon)
    {
        if (pokePartyPosition == 6)
        {
            //Back
            SfxHandler.Play(selectClip);
            UpdateCurrentTask(0);
            yield return new WaitForSeconds(0.2f);
        }
        else
        {
            UpdateCurrentTask(5);
            SfxHandler.Play(selectClip);
            int summaryPosition = UpdateSummaryPosition(0); //0 = Switch, 1 = Moves, 2 = Back

            yield return new WaitForSeconds(0.2f);
            while (currentTask == 5)
            {
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    if (pokePartyPosition < 5)
                    {
                        int positionBeforeModification = pokePartyPosition;
                        UpdateSelectedPokemonSlot(pokePartyPosition + 1, false);
                        if (positionBeforeModification != pokePartyPosition)
                        {
                            SfxHandler.Play(scrollClip);
                        }

                        UpdatePokemonSummaryDisplay(
                            SaveData.currentSave.PC.boxes[0][pokePartyPosition]);
                        yield return new WaitForSeconds(0.2f);
                    }
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    if (summaryPosition < 2)
                    {
                        summaryPosition = UpdateSummaryPosition(summaryPosition + 1);
                        SfxHandler.Play(scrollClip);
                        yield return new WaitForSeconds(0.2f);
                    }
                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    if (summaryPosition > 0)
                    {
                        summaryPosition = UpdateSummaryPosition(summaryPosition - 1);
                        SfxHandler.Play(scrollClip);
                        yield return new WaitForSeconds(0.2f);
                    }
                }
                else if (Input.GetAxisRaw("Vertical") > 0)
                {
                    if (pokePartyPosition > 0)
                    {
                        int positionBeforeModification = pokePartyPosition;
                        UpdateSelectedPokemonSlot(pokePartyPosition - 1, false);
                        if (positionBeforeModification != pokePartyPosition)
                        {
                            SfxHandler.Play(scrollClip);
                        }

                        UpdatePokemonSummaryDisplay(
                            SaveData.currentSave.PC.boxes[0][pokePartyPosition]);
                        yield return new WaitForSeconds(0.2f);
                    }
                }
                else if (Input.GetButtonDown("Select"))
                {
                    switch (summaryPosition)
                    {
                        case 0 when SaveData.currentSave.PC.boxes[0][pokePartyPosition].getStatus() !=
                                    Pokemon.Status.FAINTED:
                        {
                            //check that pokemon is not on the field
                            bool notOnField = true;
                            for (int i = 0; i < pokemonPerSide; i++)
                            {
                                if (SaveData.currentSave.PC.boxes[0][pokePartyPosition] ==
                                    pokemon[i])
                                {
                                    notOnField = false;
                                    i = pokemonPerSide;
                                }
                            }

                            if (notOnField)
                            {
                                //debug
                                battleData[currentPokemon].command = CommandType.Switch;
                                battleData[currentPokemon].commandPokemon =
                                    SaveData.currentSave.PC.boxes[0][pokePartyPosition];
                                runState = false;
                                UpdateCurrentTask(-1);
                                SfxHandler.Play(selectClip);
                                yield return new WaitForSeconds(0.2f);
                            }
                            else
                            {
                                yield return
                                    StartCoroutine(
                                        DrawTextAndWait(
                                            SaveData.currentSave.PC.boxes[0][pokePartyPosition]
                                                .getName() + " is already fighting!"));
                                Dialog.UnDrawDialogBox();
                            }

                            break;
                        }
                        case 0:
                            yield return
                                StartCoroutine(
                                    DrawTextAndWait(
                                        SaveData.currentSave.PC.boxes[0][pokePartyPosition]
                                            .getName() + " is unable to fight!"));
                            Dialog.UnDrawDialogBox();
                            break;
                        case 1:
                        {//check moves
                            yield return ControlPokeMenuMoves(currentPokemon);
                            break;
                        }
                        case 2:
//back
                            UpdateCurrentTask(3);
                            SfxHandler.Play(selectClip);
                            yield return new WaitForSeconds(0.2f);
                            break;
                    }
                }
                else if (Input.GetButtonDown("Back"))
                {
                    UpdateCurrentTask(3);
                    SfxHandler.Play(selectClip);
                    yield return new WaitForSeconds(0.2f);
                }

                yield return null;
            }
        }
    }

    private IEnumerator ControlPokeMenuMoves(int currentPokemon)
    {
        UpdateCurrentTask(6);
        SfxHandler.Play(selectClip);
        yield return new WaitForSeconds(0.2f);

        int movesPosition = 5; //0-3 = Moves, 4 = Switch, 5 = Summary, 6 = Back
        while (currentTask == 6)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                if (movesPosition < 4)
                {
                    movesPosition = movesPosition == 2
                        ? UpdateMovesPosition(movesPosition + 3)
                        : UpdateMovesPosition(movesPosition + 2);

                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                if (movesPosition != 1 || movesPosition != 3 ||
                    movesPosition != 6)
                {
                    movesPosition = UpdateMovesPosition(movesPosition + 1);
                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                if (movesPosition == 1 || movesPosition == 3 ||
                    movesPosition > 4)
                {
                    movesPosition = UpdateMovesPosition(movesPosition - 1);
                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                if (movesPosition > 1)
                {
                    if (movesPosition > 3)
                    {
                        movesPosition = UpdateMovesPosition(2);
                    }
                    else
                    {
                        movesPosition = UpdateMovesPosition(movesPosition - 2);
                    }

                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Input.GetButtonDown("Select"))
            {
                switch (movesPosition)
                {
                    case 4 when SaveData.currentSave.PC.boxes[0][pokePartyPosition]
                                    .getStatus() != Pokemon.Status.FAINTED:
                    {
                        //check that pokemon is not on the field
                        bool notOnField = true;
                        for (int i = 0; i < pokemonPerSide; i++)
                        {
                            if (
                                SaveData.currentSave.PC.boxes[0][
                                    pokePartyPosition] == pokemon[i])
                            {
                                notOnField = false;
                                i = pokemonPerSide;
                            }
                        }

                        if (notOnField)
                        {
                            //debug
                            battleData[currentPokemon].command = CommandType.Switch;
                            battleData[currentPokemon].commandPokemon =
                                SaveData.currentSave.PC.boxes[0][
                                    pokePartyPosition];
                            runState = false;
                            UpdateCurrentTask(-1);
                            SfxHandler.Play(selectClip);
                            yield return new WaitForSeconds(0.2f);
                        }
                        else
                        {
                            yield return
                                StartCoroutine(
                                    DrawTextAndWait(
                                        SaveData.currentSave.PC.boxes[0][
                                            pokePartyPosition].getName() +
                                        " is already fighting!"));
                            Dialog.UnDrawDialogBox();
                        }

                        break;
                    }
                    case 4:
                        yield return
                            StartCoroutine(
                                DrawTextAndWait(
                                    SaveData.currentSave.PC.boxes[0][
                                        pokePartyPosition].getName() +
                                    " is unable to fight!"));
                        Dialog.UnDrawDialogBox();
                        break;
                    case 5:
//check summary
                        UpdateCurrentTask(5);
                        SfxHandler.Play(selectClip);
                        yield return new WaitForSeconds(0.2f);
                        break;
                    case 6:
//back
                        UpdateCurrentTask(3);
                        SfxHandler.Play(selectClip);
                        yield return new WaitForSeconds(0.2f);
                        break;
                }
            }
            else if (Input.GetButtonDown("Back"))
            {
                UpdateCurrentTask(3);
                SfxHandler.Play(selectClip);
                yield return new WaitForSeconds(0.2f);
            }

            yield return null;
        }
    }

    private IEnumerator ControlBagInterFace(int currentPokemon)
    {
        UpdateCurrentTask(2);
        SfxHandler.Play(selectClip);
        yield return null;

        UpdateSelectedBagCategory(bagCategoryPosition);
        //while still in Bag menu
        while (currentTask == 2)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                if (bagCategoryPosition < 4)
                {
                    UpdateSelectedBagCategory(bagCategoryPosition + 2);
                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                if (bagCategoryPosition == 0 || bagCategoryPosition == 2 || bagCategoryPosition == 4)
                {
                    UpdateSelectedBagCategory(bagCategoryPosition + 1);
                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                if (bagCategoryPosition == 1 || bagCategoryPosition == 3 || bagCategoryPosition == 5)
                {
                    UpdateSelectedBagCategory(bagCategoryPosition - 1);
                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                if (bagCategoryPosition > 1)
                {
                    UpdateSelectedBagCategory(bagCategoryPosition - 2);
                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (Input.GetButtonDown("Select"))
            {
                yield return ControlBagInterfaceSelect(currentPokemon);
            }
            else if (Input.GetButtonDown("Back"))
            {
                SfxHandler.Play(selectClip);
                UpdateCurrentTask(0);
                yield return new WaitForSeconds(0.2f);
            }

            yield return null;
        }
    }

    private IEnumerator ControlBagInterfaceSelect(int currentPokemon)
    {
        if (bagCategoryPosition < 4)
        {
            //Item Category
            UpdateCurrentTask(4);
            SfxHandler.Play(selectClip);

            int itemListPosition = UpdateSelectedItemListSlot(0, 0);
            yield return new WaitForSeconds(0.2f);
            while (currentTask == 4)
            {
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    if (itemListPosition < 8)
                    {
                        int positionBeforeModification = itemListPosition;
                        if (itemListPosition < 7)
                        {
                            itemListPosition = UpdateSelectedItemListSlot(itemListPosition, 2);
                        }
                        else
                        {
                            itemListPosition = UpdateSelectedItemListSlot(itemListPosition, 1);
                        }

                        if (positionBeforeModification != itemListPosition)
                        {
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                        }
                    }
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    if (itemListPosition < 8)
                    {
                        if (itemListPosition % 2 == 0)
                        {
                            int positionBeforeModification = itemListPosition;
                            itemListPosition = UpdateSelectedItemListSlot(itemListPosition, 1);
                            if (positionBeforeModification != itemListPosition)
                            {
                                SfxHandler.Play(scrollClip);
                                yield return new WaitForSeconds(0.2f);
                            }
                        }
                        //go to next page of items
                        else if (itemListPagePosition < itemListPageCount - 1)
                        {
                            int positionBeforeModification = itemListPosition;
                            itemListPagePosition += 1;
                            UpdateItemListDisplay();
                            itemListPosition = UpdateSelectedItemListSlot(itemListPosition, -1);
                            if (positionBeforeModification != itemListPosition)
                            {
                                SfxHandler.Play(scrollClip);
                                yield return new WaitForSeconds(0.2f);
                            }
                        }
                    }
                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    if (itemListPosition < 8)
                    {
                        if (itemListPosition % 2 == 1)
                        {
                            int positionBeforeModification = itemListPosition;
                            itemListPosition = UpdateSelectedItemListSlot(itemListPosition, -1);
                            if (positionBeforeModification != itemListPosition)
                            {
                                SfxHandler.Play(scrollClip);
                                yield return new WaitForSeconds(0.2f);
                            }
                        }
                        //go to previous page of items
                        else if (itemListPagePosition > 0)
                        {
                            int positionBeforeModification = itemListPosition;
                            itemListPagePosition -= 1;
                            UpdateItemListDisplay();
                            itemListPosition = UpdateSelectedItemListSlot(itemListPosition, 1);
                            if (positionBeforeModification != itemListPosition)
                            {
                                SfxHandler.Play(scrollClip);
                                yield return new WaitForSeconds(0.2f);
                            }
                        }
                    }
                }
                else if (Input.GetAxisRaw("Vertical") > 0)
                {
                    if (itemListPosition > 1)
                    {
                        int positionBeforeModification = itemListPosition;
                        itemListPosition = UpdateSelectedItemListSlot(itemListPosition, -2);
                        if (positionBeforeModification != itemListPosition)
                        {
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                        }
                    }
                }
                else if (Input.GetButtonDown("Select"))
                {
                    if (itemListPosition == 8)
                    {
                        SfxHandler.Play(selectClip);
                        UpdateCurrentTask(2);
                        yield return new WaitForSeconds(0.2f);
                    }
                    else
                    {
                        //use item
                        ItemData selectedItem =
                            ItemDatabase.getItem(
                                itemListString[itemListPosition + (8 * itemListPagePosition)]);
                        //Check item can be used
                        if (selectedItem.getItemEffect() == ItemData.ItemEffect.HP)
                        {
                            //Check target pokemon's health is not full
                            int target = 0; //target selection not yet implemented
                            if (pokemon[target].getCurrentHP() < pokemon[target].getHP())
                            {
                                battleData[currentPokemon].commandItem = selectedItem;
                                battleData[currentPokemon].commandTarget = target;
                                SaveData.currentSave.Bag.removeItem(selectedItem.getName(), 1);
                                runState = false;
                            }
                            else
                            {
                                yield return
                                    StartCoroutine(DrawTextAndWait("It won't have any effect!"))
                                    ;
                                Dialog.UnDrawDialogBox();
                            }
                        }
                        else if (selectedItem.getItemEffect() == ItemData.ItemEffect.STATUS)
                        {
                            int target = 0; //target selection not yet implemented
                            //Check target pokemon has the status the item cures
                            string statusCurer = selectedItem.getStringParameter().ToUpper();
                            //if an ALL is used, set it to cure anything but FAINTED or NONE.
                            if (statusCurer == "ALL" &&
                                pokemon[target].getStatus().ToString() != "FAINTED" &&
                                pokemon[target].getStatus().ToString() != "NONE")
                            {
                                statusCurer = pokemon[target].getStatus().ToString();
                            }

                            if (pokemon[target].getStatus().ToString() == statusCurer)
                            {
                                battleData[currentPokemon].commandItem = selectedItem;
                                battleData[currentPokemon].commandTarget = target;
                                SaveData.currentSave.Bag.removeItem(
                                    itemListString[itemListPosition], 1);
                                runState = false;
                            }
                            else
                            {
                                yield return
                                    StartCoroutine(DrawTextAndWait("It won't have any effect!"))
                                    ;
                                Dialog.UnDrawDialogBox();
                            }
                        }
                        else
                        {
                            battleData[currentPokemon].commandItem =
                                ItemDatabase.getItem(itemListString[itemListPosition]);
                            SaveData.currentSave.Bag.removeItem(
                                itemListString[itemListPosition], 1);
                            runState = false;
                        }

                        if (!runState)
                        {
                            //if an item was chosen.
                            SfxHandler.Play(selectClip);
                            battleData[currentPokemon].command = CommandType.Item;
                            UpdateCurrentTask(-1);
                        }
                    }
                }
                else if (Input.GetButtonDown("Back"))
                {
                    SfxHandler.Play(selectClip);
                    UpdateCurrentTask(2);
                    yield return new WaitForSeconds(0.2f);
                }


                yield return null;
            }
        }
        else if (bagCategoryPosition == 4)
        {
            //Back
            UpdateCurrentTask(0);
            SfxHandler.Play(selectClip);
            yield return new WaitForSeconds(0.2f);
        }
        else if (bagCategoryPosition == 5)
        {
            //Item used last
            SfxHandler.Play(selectClip);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator ControlFight(int currentPokemon)
    {
        UpdateCurrentTask(1);
        SfxHandler.Play(selectClip);
        yield return null;

        //while still in Move Selection menu
        while (currentTask == 1)
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                if (movePosition == 1)
                {
                    if (canMegaEvolve)
                    {
                        UpdateSelectedMove(0);
                        SfxHandler.Play(scrollClip);
                        yield return new WaitForSeconds(0.2f);
                    }
                    else
                    {
                        UpdateSelectedMove(3);
                        SfxHandler.Play(scrollClip);
                        yield return new WaitForSeconds(0.2f);
                    }
                }
                else if (movePosition > 1 && movePosition != 3)
                {
                    if (movePosition == 5)
                    {
                        if (battleData[currentPokemon].pokemonMoveset[2] != null)
                        {
                            //check destination has a move there
                            UpdateSelectedMove(movePosition - 1);
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                        }
                    }
                    else
                    {
                        UpdateSelectedMove(movePosition - 1);
                        SfxHandler.Play(scrollClip);
                        yield return new WaitForSeconds(0.2f);
                    }
                }
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                if (movePosition < 5 && movePosition != 2)
                {
                    switch (movePosition)
                    {
                        case 1:
                        {
                            if (battleData[currentPokemon].pokemonMoveset[1] != null)
                            {
                                //check destination has a move there
                                UpdateSelectedMove(movePosition + 1);
                                SfxHandler.Play(scrollClip);
                                yield return new WaitForSeconds(0.2f);
                            }

                            break;
                        }
                        case 3 when battleData[currentPokemon].pokemonMoveset[2] != null:
                            //check destination has a move there
                            UpdateSelectedMove(movePosition + 1);
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                            break;
                        case 3:
                            UpdateSelectedMove(1);
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                            break;
                        case 4 when battleData[currentPokemon].pokemonMoveset[3] != null:
                            //check destination has a move there
                            UpdateSelectedMove(movePosition + 1);
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                            break;
                        case 4:
                        {
                            if (battleData[currentPokemon].pokemonMoveset[1] != null)
                            {
                                //check there is a move 2
                                UpdateSelectedMove(2);
                                SfxHandler.Play(scrollClip);
                                yield return new WaitForSeconds(0.2f);
                            }

                            break;
                        }
                        default:
                            UpdateSelectedMove(movePosition + 1);
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                            break;
                    }
                }
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                if (movePosition == 3)
                {
                    if (canMegaEvolve)
                    {
                        UpdateSelectedMove(movePosition - 3);
                        SfxHandler.Play(scrollClip);
                        yield return new WaitForSeconds(0.2f);
                    }
                    else
                    {
                        //otherwise, go down to return
                        UpdateSelectedMove(1);
                        SfxHandler.Play(scrollClip);
                        yield return new WaitForSeconds(0.2f);
                    }
                }
                else if (movePosition > 3)
                {
                    if (movePosition == 5)
                    {
                        if (battleData[currentPokemon].pokemonMoveset[1] != null)
                        {
                            //check destination has a move there
                            UpdateSelectedMove(movePosition - 3);
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                        }
                    }
                    else
                    {
                        UpdateSelectedMove(movePosition - 3);
                        SfxHandler.Play(scrollClip);
                        yield return new WaitForSeconds(0.2f);
                    }
                }
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                if (movePosition < 3)
                {
                    switch (movePosition)
                    {
                        case 1 when battleData[currentPokemon].pokemonMoveset[2] != null:
                            //check destination has a move there
                            UpdateSelectedMove(movePosition + 3);
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                            break;
                        case 1:
                            //otherwise, go down to return
                            UpdateSelectedMove(3);
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                            break;
                        case 2 when battleData[currentPokemon].pokemonMoveset[3] != null:
                            //check destination has a move there
                            UpdateSelectedMove(movePosition + 3);
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                            break;
                        case 2 when battleData[currentPokemon].pokemonMoveset[2] != null:
                            //check if there is a move 3
                            UpdateSelectedMove(4);
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                            break;
                        case 2:
                            //otherwise, go down to return
                            UpdateSelectedMove(3);
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                            break;
                        default:
                            UpdateSelectedMove(movePosition + 3);
                            SfxHandler.Play(scrollClip);
                            yield return new WaitForSeconds(0.2f);
                            break;
                    }
                }
            }
            else if (Input.GetButtonDown("Select"))
            {
                switch (movePosition)
                {
                    case 0:
                        //if mega evolution selected (mega evolution not yet implemented)
                        break;
                    case 3:
                        //if back selected
                        SfxHandler.Play(selectClip);
                        UpdateCurrentTask(0);
                        break;
                    default:
                    {
                        //if a move is selected
                        //check if struggle is to be used (no PP left in any move)
                        if (pokemon[currentPokemon].getPP(0) == 0 && pokemon[currentPokemon].getPP(1) == 0 &&
                            pokemon[currentPokemon].getPP(2) == 0 && pokemon[currentPokemon].getPP(3) == 0)
                        {
                            battleData[currentPokemon].commandMove = MoveDatabase.getMove("Struggle");
                            runState = false;
                        }
                        else
                        {
                            //convert movePosition to moveset index
                            int[] move = {0, 0, 1, 0, 2, 3};
                            if (pokemon[currentPokemon].getPP(move[movePosition]) > 0)
                            {
                                battleData[currentPokemon].commandMove =
                                    MoveDatabase.getMove(battleData[currentPokemon].pokemonMoveset[move[movePosition]]);
                                Debug.Log(battleData[currentPokemon].commandMove.getName() + ", PP: " +
                                          pokemon[currentPokemon].getPP(move[movePosition]));
                                runState = false;
                            }
                            else
                            {
                                yield return StartCoroutine(DrawTextAndWait("That move is out of PP!"));
                                Dialog.UnDrawDialogBox();
                            }
                        }

                        break;
                    }
                }

                if (!runState)
                {
                    //if a move was chosen.
                    SfxHandler.Play(selectClip);
                    battleData[currentPokemon].command = CommandType.Move;
                    UpdateCurrentTask(-1);
                }
            }
            else if (Input.GetButtonDown("Back"))
            {
                SfxHandler.Play(selectClip);
                UpdateCurrentTask(0);
            }

            yield return null;
        }
    }

    private IEnumerator ControlFleeAttempt(int currentPokemon)
    {
        SfxHandler.Play(selectClip);
        if (trainerBattle)
        {
            yield return
                StartCoroutine(DrawTextAndWait("No! There's no running from \\na Trainer Battle!"));
            Dialog.UnDrawDialogBox();
        }
        else
        {
            battleData[currentPokemon].command = CommandType.Flee;
            runState = false;
            UpdateCurrentTask(-1);
        }
    }

    private IEnumerator NavigateMainOptions()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (taskPosition > 0 && taskPosition != 3)
            {
                UpdateSelectedTask(taskPosition - 1);
                SfxHandler.Play(scrollClip);
                yield return new WaitForSeconds(0.2f);
            }
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (taskPosition < 5 && taskPosition != 2)
            {
                UpdateSelectedTask(taskPosition + 1);
                SfxHandler.Play(scrollClip);
                yield return new WaitForSeconds(0.2f);
            }
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            if (taskPosition > 2)
            {
                if (taskPosition == 4)
                {
                    //if run selected
                    UpdateSelectedTask(taskPosition - 3);
                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
                else
                {
                    taskPosition -= 3;
                }
            }
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            if (taskPosition < 3)
            {
                if (taskPosition == 1)
                {
                    //if fight selected
                    UpdateSelectedTask(taskPosition + 3);
                    SfxHandler.Play(scrollClip);
                    yield return new WaitForSeconds(0.2f);
                }
                else
                {
                    taskPosition += 3;
                }
            }
        }
    }

    private void SetDebugOverlayTextures()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(BattleAnimator.AnimateOverlayer(opponent1Overlay, overlayHealTex, -1f, 0, 1.2f, 0.3f));
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(BattleAnimator.AnimateOverlayer(opponent1Overlay, overlayStatUpTex, -1f, 0, 1.2f, 0.3f));
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(BattleAnimator.AnimateOverlayer(opponent1Overlay, overlayStatDownTex, 1f, 0, 1.2f, 0.3f));
        }
    }

    private void ResetTurnTasks()
    {
        foreach (var data in battleData)
        {
            data.commandMove = null;
            data.commandTarget = 0;
            data.commandItem = null;
            data.commandPokemon = null;
        }
    }

    private IEnumerator InitWildBattle()
    {
        player1.transform.parent.parent.gameObject.SetActive(false);
        opponent1.transform.parent.parent.gameObject.SetActive(false);

        StartCoroutine(ScreenFade.main.Fade(true, ScreenFade.slowedSpeed));
        StartCoroutine(BattleAnimator.SlidePokemon(opponentBase, opponent1,
            true, false, new Vector3(92, 0, 0)));
        yield return StartCoroutine(BattleAnimator.SlidePokemon(playerBase, player1,
            false, true, new Vector3(-80, -64, 0)));
        Dialog.DrawDialogBox();
        StartCoroutine(Dialog.DrawTextSilent("A wild " + pokemon[3].getName() + " appeared!"));
        PlayCry(pokemon[3]);
        yield return StartCoroutine(SlidePokemonStats(3, false));
        yield return new WaitForSeconds(1.2f);

        Dialog.DrawDialogBox();
        StartCoroutine(Dialog.DrawTextSilent("Go " + pokemon[0].getName() + "!"));
        StartCoroutine(BattleAnimator.AnimatePlayerThrow(playerTrainerSprite1, playerTrainer1Animation, true));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(BattleAnimator.SlideTrainer(playerBase, playerTrainerSprite1, false, true));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(BattleAnimator.ReleasePokemon(player1));
        PlayCry(pokemon[0]);
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(SlidePokemonStats(0, false));
        yield return new WaitForSeconds(0.9f);
        Dialog.UnDrawDialogBox();
    }

    private IEnumerator InitTrainerBattle(Pokemon[] opponentParty, string opponentName)
    {
        StartCoroutine(ScreenFade.main.Fade(true, ScreenFade.slowedSpeed));
        StartCoroutine(BattleAnimator.SlidePokemon(opponentBase, opponent1, false, false, new Vector3(100, 0, 0)));
        StartCoroutine(BattleAnimator.SlidePokemon(playerBase, player1, false, true, new Vector3(-80, -64, 0)));

        yield return new WaitForSeconds(0.9f);
        StartCoroutine(DisplayPartyBar(true, opponentParty));
        StartCoroutine(DisplayPartyBar(false, SaveData.currentSave.PC.boxes[0]));

        yield return StartCoroutine(DrawTextAndWait(opponentName + " wants to fight!", 2.6f, 2.6f));

        Dialog.DrawDialogBox();
        StartCoroutine(Dialog.DrawTextSilent(opponentName + " sent out " + pokemon[3].getName() + "!"));
        StartCoroutine(DismissPartyBar(true));
        yield return StartCoroutine(BattleAnimator.SlideTrainer(opponentBase, trainerSprite1, true, true));
        yield return StartCoroutine(BattleAnimator.ReleasePokemon(opponent1));
        PlayCry(pokemon[3]);
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(SlidePokemonStats(3, false));
        yield return new WaitForSeconds(0.9f);

        Dialog.DrawDialogBox();
        StartCoroutine(Dialog.DrawTextSilent("Go " + pokemon[0].getName() + "!"));
        StartCoroutine(BattleAnimator.AnimatePlayerThrow(playerTrainerSprite1, playerTrainer1Animation, true));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(DismissPartyBar(false));
        StartCoroutine(BattleAnimator.SlideTrainer(playerBase, playerTrainerSprite1, false, true));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(BattleAnimator.ReleasePokemon(player1));
        PlayCry(pokemon[0]);
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(SlidePokemonStats(0, false));
        yield return new WaitForSeconds(0.9f);
        Dialog.UnDrawDialogBox();
    }

    private void ShowActivePokemon()
    {
//Animate the Pokemon being released into battle
        player1.transform.parent.parent.gameObject.SetActive(false);
        opponent1.transform.parent.parent.gameObject.SetActive(false);
        player1Overlay.gameObject.SetActive(false);
        opponent1Overlay.gameObject.SetActive(false);


        animateOpponent1 = StartCoroutine(BattleAnimator.AnimatePokemon(opponent1, opponent1Animation));
        animatePlayer1 = StartCoroutine(BattleAnimator.AnimatePokemon(player1, player1Animation));

        pokemonStatsDisplay[0].gameObject.SetActive(false);
        pokemonStatsDisplay[3].gameObject.SetActive(false);

        HidePartyBar(true);
        HidePartyBar(false);
    }

    private void InitializePokemon(Pokemon[] opponentParty) 
    {
        for (int i = 0; i < 6; i++)
        {
            var switchPokemon = SaveData.currentSave.PC.boxes[0][i];
            if (switchPokemon != null && switchPokemon.getStatus() != Pokemon.Status.FAINTED && battleData[i] != null)
            {
                BattleCalculation.SwitchPokemon(pokemon, 0, battleData[0], switchPokemon,
                    false, true);
                i = 6;
            }
        }

        BattleCalculation.SwitchPokemon(pokemon, 3, battleData[3], opponentParty[0], false, true);

//        foreach (var pokemon1 in pokemon)
//        {
//            Debug.Log(pokemon1.getName());
//        }

        player1Animation = pokemon[0].GetBackAnim_();
        opponent1Animation = pokemon[3].GetFrontAnim_();

        UpdatePokemonStatsDisplay(0);
        UpdatePokemonStatsDisplay(3);

        if (pokemon[0] != null)
        {
            UpdateMovesetDisplay(battleData[0].pokemonMoveset, pokemon[0].getPP(), pokemon[0].getMaxPP());
        }
    }

    private void SetTrainerSprites(Trainer trainer)
    {
//Set Trainer Sprites
        trainer1Animation = new[] {Resources.Load<Sprite>("null")};
        if (trainerBattle)
        {
            trainer1Animation = trainer.GetSprites();
        }

        playerTrainer1Animation =
            Resources.LoadAll<Sprite>("PlayerSprites/" + SaveData.currentSave.getPlayerSpritePrefix() + "back");
        playerTrainerSprite1.sprite = playerTrainer1Animation[0];
        //Note: the player animation should NEVER have no sprites 
        if (trainer1Animation.Length > 0)
        {
            trainerSprite1.sprite = trainer1Animation[0];
        }
        else
        {
            trainerSprite1.sprite = Resources.Load<Sprite>("null");
        }

        //Set trainer sprites to the center of the platform initially
        var rectTransform = playerTrainerSprite1.rectTransform;
        rectTransform.localPosition = new Vector3(0,
            rectTransform.localPosition.y, 0);
        var rectTransform1 = trainerSprite1.rectTransform;
        rectTransform1.localPosition = new Vector3(0, rectTransform1.localPosition.y, 0);
    }

    private void GetBattleBackgrounds()
    {
        int currentTileTag = Player.player.currentMap.getTileTag(Player.player.transform.position);
        Debug.Log(currentTileTag);
        background.sprite = Player.player.accessedMapSettings.getBattleBackground(currentTileTag);

        playerBase.sprite = Player.player.accessedMapSettings.getBattleBase(currentTileTag);
        opponentBase.sprite = playerBase.sprite;
    }

    private static int[] GetInitialLevels()
    {
        return SaveData.currentSave.PC.boxes[0].Take(6).Where(p => p != null).Select(p => p.getLevel()).ToArray();
    }
}
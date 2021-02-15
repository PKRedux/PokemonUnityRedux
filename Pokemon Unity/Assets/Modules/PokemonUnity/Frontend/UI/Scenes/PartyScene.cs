/// Source: Pokémon Unity Redux
/// Purpose: Party UI (temporary) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio

using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PokemonUnity.Backend.Serializables;
using PokemonUnity.Frontend.Global;

namespace PokemonUnity.Frontend.UI.Scenes {
public class PartyScene : BaseScene
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
    private const int CANCEL_BUTTON_POSITION = 6;

    void Awake()
    {
        Dialog = SceneScript.main.Dialog;
        //sceneTransition = transform.GetComponent<SceneTransition>();
        PartyAudio = transform.GetComponent<AudioSource>();
        cancel = transform.Find("Cancel").GetComponent<Image>();
        for (int i = 0; i < 6; i++)
        {
            SetSlotComponents(i);
        }
    }

    private void SetSlotComponents(int i)
    {
        slot[i] = transform.Find("Slot" + i).GetComponent<Image>();

        selectBall[i] = slot[i].transform.Find("SelectBall").GetComponent<Image>();
        icon[i] = slot[i].transform.Find("Icon").GetComponent<Image>();
        pokemonName[i] = slot[i].transform.Find("Name").GetComponent<Text>();
        pokemonNameShadow[i] = pokemonName[i].transform.Find("NameShadow").GetComponent<Text>();
        gender[i] = slot[i].transform.Find("Gender").GetComponent<Text>();
        genderShadow[i] = gender[i].transform.Find("GenderShadow").GetComponent<Text>();
        HPBarBack[i] = slot[i].transform.Find("HPBarBack").GetComponent<Image>();
        HPBar[i] = slot[i].transform.Find("HPBar").GetComponent<Image>();
        lv[i] = slot[i].transform.Find("Lv.").GetComponent<Image>();
        level[i] = slot[i].transform.Find("Level").GetComponent<Text>();
        levelShadow[i] = level[i].transform.Find("LevelShadow").GetComponent<Text>();
        currentHP[i] = slot[i].transform.Find("CurrentHP").GetComponent<Text>();
        currentHPShadow[i] = currentHP[i].transform.Find("CurrentHPShadow").GetComponent<Text>();
        slash[i] = slot[i].transform.Find("Slash").GetComponent<Text>();
        slashShadow[i] = slash[i].transform.Find("SlashShadow").GetComponent<Text>();
        maxHp[i] = slot[i].transform.Find("MaxHP").GetComponent<Text>();
        maxHPShadow[i] = maxHp[i].transform.Find("MaxHPShadow").GetComponent<Text>();
        status[i] = slot[i].transform.Find("Status").GetComponent<Image>();
        item[i] = slot[i].transform.Find("Item").GetComponent<Image>();
    }

    void Start()
    {
        UpdateParty();
        gameObject.SetActive(false);
    }

    private void UpdateParty()
    {
        for (int i = 0; i < 6; i++)
        {
            Pokemon selectedPokemon = SaveData.currentSave.PC.boxes[0][i];
            if (selectedPokemon == null)
            {
                slot[i].gameObject.SetActive(false);
            }
            else
            {
                SetPokemonData(i, selectedPokemon);
            }
        }
    }

    private void SetPokemonData(int i, Pokemon selectedPokemon)
    {
        slot[i].gameObject.SetActive(true);
        selectBall[i].sprite = selectBallClosed;
        icon[i].sprite = selectedPokemon.GetIcons();
        pokemonName[i].text = selectedPokemon.getName();
        pokemonNameShadow[i].text = pokemonName[i].text;
        SetPokemonGender(selectedPokemon, i);
        SetPokemonHpBar(i, selectedPokemon);

        level[i].text = "" + selectedPokemon.getLevel();
        levelShadow[i].text = level[i].text;
        currentHP[i].text = "" + selectedPokemon.getCurrentHP();
        currentHPShadow[i].text = currentHP[i].text;
        maxHp[i].text = "" + selectedPokemon.getHP();
        maxHPShadow[i].text = maxHp[i].text;
        SetStatusSprite(selectedPokemon, i);
        item[i].enabled = !string.IsNullOrEmpty(selectedPokemon.getHeldItem());
    }

    private void SetStatusSprite(Pokemon selectedPokemon, int i)
    {
        if (selectedPokemon.getStatus() != Pokemon.Status.NONE)
        {
            status[i].gameObject.SetActive(true);
            status[i].sprite =
                Resources.Load<Sprite>("PCSprites/status" + selectedPokemon.getStatus());
        }
        else
        {
            status[i].gameObject.SetActive(false);
        }
    }

    private void SetPokemonHpBar(int i, Pokemon selectedPokemon)
    {
        HPBar[i].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
            Mathf.FloorToInt(48f * (selectedPokemon.getCurrentHP() / (float) selectedPokemon.getHP())));
        HPBar[i].color = GetPokemonHpBarColor(i, selectedPokemon);
    }

    private Color GetPokemonHpBarColor(int i, Pokemon selectedPokemon)
    {
        if (selectedPokemon.getCurrentHP() < (selectedPokemon.getHP() / 4f))
        {
            return new Color(1, 0.125f, 0, 1);
        }
        if (selectedPokemon.getCurrentHP() < (selectedPokemon.getHP() / 2f))
        {
            return new Color(1, 0.75f, 0, 1);
        }
        return new Color(0.125f, 1, 0.065f, 1);
    }

    private void SetPokemonGender(Pokemon selectedPokemon, int i)
    {
        if (selectedPokemon.getGender() == Pokemon.Gender.FEMALE)
        {
            genderShadow[i].text = "♀";
            genderShadow[i].color = new Color(1, 0.2f, 0.2f, 1);
        }
        else if (selectedPokemon.getGender() == Pokemon.Gender.MALE)
        {
            genderShadow[i].text = "♂";
            genderShadow[i].color = new Color(0.2f, 0.4f, 1, 1);
        }
        else
        {
            genderShadow[i].text = null;
        }

        gender[i].text = genderShadow[i].text;
    }

    private void ShiftPosition(int move)
    {
        int repetitions = Mathf.Abs(move);
        for (int i = 0; i < repetitions; i++)
        {
            if (move > 0)
            {
                //add
                if (currentPosition < 5)
                {
                    if (SaveData.currentSave.PC.boxes[0][currentPosition + 1] == null)
                    {
                        currentPosition = 6;
                    }
                    else
                    {
                        currentPosition += 1;
                    }
                }
                else if (currentPosition == 5)
                {
                    currentPosition = 6;
                }
            }
            else if (move < 0)
            {
                //subtract
                if (currentPosition == 6)
                {
                    currentPosition -= 1;
                    while (SaveData.currentSave.PC.boxes[0][currentPosition] == null)
                    {
                        currentPosition -= 1;
                    }
                }
                else if (currentPosition > 0)
                {
                    currentPosition -= 1;
                }
            }
        }
        UpdateFrames();
    }

    private void UpdateFrames()
    {
        for (var i = 0; i < 6; i++)
        {
            UpdateSlotFrame(i);
        }

        cancel.sprite = currentPosition == CANCEL_BUTTON_POSITION ? cancelHighlightTex : cancelTex;
    }

    private void UpdateSlotFrame(int i)
    {
        var selectedPokemon = SaveData.currentSave.PC.boxes[0][i];
        if (selectedPokemon == null) return;
        slot[i].sprite = GetSlotSprite(i, selectedPokemon);
        selectBall[i].sprite = i == currentPosition ? selectBallOpen : selectBallClosed;
    }

    private Sprite GetSlotSprite(int i, Pokemon selectedPokemon)
    {
        if (i == swapPosition)
        {
            if (i == 0)
            {
                return i == currentPosition ? panelRoundSwapSel : panelRoundSwap;
            }
            return i == currentPosition ? panelRectSwapSel : panelRectSwap;
        }
        if (selectedPokemon.getCurrentHP() == 0)
        {
            if (i == 0)
            {
                if (i == currentPosition)
                {
                    return switching ? panelRoundSwapSel : panelRoundFaintSel;
                }
                return panelRoundFaint;
            }
            if (i == currentPosition)
            {
                return switching ? panelRectSwapSel : panelRectFaintSel;
            }
            return panelRectFaint;
        }
        if (i == 0)
        {
            if (i == currentPosition)
            {
                return switching ? panelRoundSwapSel : panelRoundSel;
            }
            return panelRound;
        }
        if (i == currentPosition)
        {
            return switching ? panelRectSwapSel : panelRectSel;
        }
        return panelRect;
    }

    private void SwitchPokemon(int position1, int position2)
    { 
        SaveData.currentSave.PC.swapPokemon(0, position1, 0, position2);
        UpdateParty();
    }

    public IEnumerator Control()
    {
        StartCoroutine(ScreenFade.main.Fade(true, ScreenFade.defaultSpeed));

        running = true;
        currentPosition = 0;
        SaveData.currentSave.PC.packParty();

        yield return ResetDefaults();
        while (running)
        {
            if (Math.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f || Math.Abs(Input.GetAxisRaw("Vertical")) > 0.1f)
            {
                yield return ControlMovement();
            }
            else if (Input.GetButton("Select"))
            {
                yield return ControlSelect();
            }
            else if (Input.GetButton("Back"))
            {
                yield return ControlBack();
            }
            yield return null;
        }
        Dialog.UnDrawDialogBox();
        GlobalScript.global.sceneActivity.resetFollower();
        gameObject.SetActive(false);
    }

    private IEnumerator PromptChoosePokemon()
    {
        yield return StartCoroutine(Dialog.DrawTextInstant("Choose a Pokémon."));
    }

    private IEnumerator ControlBack()
    {
        if (switching)
        {
            yield return CancelSwitching();
        }
        else
        {
            currentPosition = CANCEL_BUTTON_POSITION;
            UpdateFrames();
            CancelRunning();
        }
    }

    private void CancelRunning()
    {
        SfxHandler.Play(selectClip);
        running = false;
    }

    private IEnumerator ControlSelect()
    {
        if (currentPosition == CANCEL_BUTTON_POSITION)
        {
            if (switching)
            {
                yield return CancelSwitching();
            }
            else
            {
                CancelRunning();
            }
        }
        else if (switching)
        {
            yield return ControlSwitching();
        }
        else
        {
            yield return ControlPokemonOptions();
        }
    }

    private IEnumerator ControlPokemonOptions()
    {
        var selectedPokemon = SaveData.currentSave.PC.boxes[0][currentPosition];
        var chosenIndex = -1;
        const int CANCEL_INDEX = 0;
        while (chosenIndex != CANCEL_INDEX)
        {
            const int ITEM_INDEX = 1;
            const int SWITCH_INDEX = 2;
            const int SUMMARY_INDEX = 3;
            SfxHandler.Play(selectClip);
            yield return PromptPokemonOptions(selectedPokemon);
            chosenIndex = Dialog.chosenIndex;
            switch (chosenIndex)
            {
                case SUMMARY_INDEX:
                    yield return ControlSummary();
                    chosenIndex = 0;
                    break;
                case SWITCH_INDEX:
                    yield return StartSwitching(selectedPokemon);
                    chosenIndex = 0;
                    break;
                case ITEM_INDEX:
                {
                    yield return StartItemMenu(selectedPokemon);
                    chosenIndex = 0;
                    break;
                }
            }
        }
        if (switching) yield break;
        Dialog.UndrawChoiceBox();
        Dialog.DrawDialogBox();
        yield return PromptChoosePokemon();
    }

    private IEnumerator ControlSwitching()
    {
        if (currentPosition != swapPosition)
        {
            SwitchPokemon(swapPosition, currentPosition);
        }
        Dialog.UndrawChoiceBox();
        yield return ResetDefaults();
        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator StartItemMenu(Pokemon selectedPokemon)
    {
        int chosenIndex;
        Dialog.UndrawChoiceBox();
        Dialog.DrawDialogBox();
        if (!string.IsNullOrEmpty(selectedPokemon.getHeldItem()))
        {
            yield return StartCoroutine(
                Dialog.DrawText(selectedPokemon.getName() + " is holding " +
                                selectedPokemon.getHeldItem() + "."));
            var choices = new[]
            {
                "Swap", "Take", "Cancel"
            };
            const int SWAP_INDEX = 2;
            const int TAKE_INDEX = 1;
            Dialog.DrawChoiceBox(choices);
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(Dialog.DrawChoiceBox(choices));

            chosenIndex = Dialog.chosenIndex;
            switch (chosenIndex)
            {
                case SWAP_INDEX:
                {
                    yield return StartSwappingItem(selectedPokemon);
                    break;
                }
                case TAKE_INDEX:
                {
                    yield return StartTakingItem(selectedPokemon);
                    break;
                }
            }
        }
        else
        {
            yield return
                StartCoroutine(
                    Dialog.DrawText(selectedPokemon.getName() + " isn't holding anything."));
            var choices = new[]
            {
                "Give", "Cancel"
            };
            const int GIVE_INDEX = 1;
            Dialog.DrawChoiceBox(choices);
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(Dialog.DrawChoiceBox(choices));
            chosenIndex = Dialog.chosenIndex;
            if (chosenIndex == GIVE_INDEX)
            {
                yield return StartGivingItem(selectedPokemon);
            }
        }
        yield return new WaitForSeconds(0.2f);
    }
    
    private IEnumerator StartTakingItem(Pokemon selectedPokemon)
    {
        Dialog.UndrawChoiceBox();
        string receivedItem = selectedPokemon.swapHeldItem("");
        SaveData.currentSave.Bag.addItem(receivedItem, 1);

        UpdateParty();
        UpdateFrames();

        Dialog.DrawDialogBox();
        yield return
            StartCoroutine(
                Dialog.DrawText("Took " + receivedItem + " from " +
                                selectedPokemon.getName() + "."));
        while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back"))
        {
            yield return null;
        }
    }

    private IEnumerator StartGivingItem(Pokemon selectedPokemon)
    {
        yield return ChooseItemFromBag();
//        string chosenItem = SceneScript.main.Bag.chosenItem;
        string chosenItem = "";
        if (string.IsNullOrEmpty(chosenItem))
        {
            yield return PromptChoosePokemon();
        }

        yield return StartCoroutine(ScreenFade.main.Fade(true, 0.4f));

        if (!string.IsNullOrEmpty(chosenItem))
        {
            selectedPokemon.swapHeldItem(chosenItem);
//            SaveData.currentSave.Bag.removeItem(chosenItem, 1);

            UpdateParty();
            UpdateFrames();

            Dialog.DrawDialogBox();
            yield return
                Dialog.StartCoroutine(
                    Dialog.DrawText("Gave " + chosenItem + " to " + selectedPokemon.getName() + "."));
            while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back"))
            {
                yield return null;
            }
        }
    }
    
    private IEnumerator StartSwappingItem(Pokemon selectedPokemon)
    {
        yield return ChooseItemFromBag();
//        string chosenItem = SceneScript.main.Bag.chosenItem;
        string chosenItem = "";
        if (string.IsNullOrEmpty(chosenItem))
        {
            yield return PromptChoosePokemon();
        }

        if (!string.IsNullOrEmpty(chosenItem))
        {
            Dialog.DrawDialogBox();
            yield return
                StartCoroutine(
                    Dialog.DrawText("Swap " + selectedPokemon.getHeldItem() + " for " +
                                    chosenItem + "?"));
            Dialog.DrawChoiceBox();
            yield return StartCoroutine(Dialog.DrawChoiceBox());

            var chosenIndex = Dialog.chosenIndex;
            Dialog.UndrawChoiceBox();

            if (chosenIndex == 1)
            {
                string receivedItem = selectedPokemon.swapHeldItem(chosenItem);
//                SaveData.currentSave.Bag.addItem(receivedItem, 1);
//                SaveData.currentSave.Bag.removeItem(chosenItem, 1);

                Dialog.DrawDialogBox();
                yield return
                    Dialog.StartCoroutine(Dialog.DrawText(
                        "Gave " + chosenItem + " to " + selectedPokemon.getName() + ","));
                while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back"))
                {
                    yield return null;
                }

                Dialog.DrawDialogBox();
                yield return
                    Dialog.StartCoroutine(Dialog.DrawText(
                        "and received " + receivedItem + " in return."));
                while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back"))
                {
                    yield return null;
                }
            }
        }
    }

    private IEnumerator ChooseItemFromBag()
    {
        yield return PromptNotImplemented();
//        SfxHandler.Play(selectClip);
//        //yield return new WaitForSeconds(sceneTransition.FadeOut(0.4f));
//        yield return StartCoroutine(ScreenFade.main.Fade(false, 0.4f));
//
//        SceneScript.main.Bag.gameObject.SetActive(true);
//        StartCoroutine(SceneScript.main.Bag.control(false, true));
//        while (SceneScript.main.Bag.gameObject.activeSelf)
//        {
//            yield return null;
//        }
//
//        Dialog.UndrawChoiceBox();
//        Dialog.DrawDialogBox();
//        //yield return new WaitForSeconds(sceneTransition.FadeIn(0.4f));
//        yield return StartCoroutine(ScreenFade.main.Fade(true, 0.4f));
    }

    private IEnumerator StartSwitching(Pokemon selectedPokemon)
    {
        switching = true;
        swapPosition = currentPosition;
        UpdateFrames();
        Dialog.UndrawChoiceBox();
        Dialog.DrawDialogBox();
        yield return StartCoroutine(Dialog.DrawTextInstant("Move " + selectedPokemon.getName() + " to where?"));
        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator PromptPokemonOptions(Pokemon selectedPokemon)
    {
        string[] choices = {
            "Summary", "Switch", "Item", "Cancel"
        };
        Dialog.DrawDialogBox();
        yield return StartCoroutine(Dialog.DrawTextInstant("Do what with " + selectedPokemon.getName() + "?"));
        Dialog.DrawChoiceBox(choices);
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(Dialog.DrawChoiceBox(choices));
    }

    private IEnumerator ControlSummary()
    {
        SfxHandler.Play(selectClip);
//        yield return StartCoroutine(ScreenFade.main.Fade(false, 0.4f));
//        SceneScript.main.Summary.gameObject.SetActive(true);
//        StartCoroutine(SceneScript.main.Summary.control(SaveData.currentSave.PC.boxes[0], currentPosition));
        //Start an empty loop that will only stop when SceneSummary is no longer active (is closed)
//        while (SceneScript.main.Summary.gameObject.activeSelf)
//        {
//            yield return null;
//        }
        yield return PromptNotImplemented();
        Dialog.UndrawChoiceBox();
        Dialog.DrawDialogBox();
        yield return PromptChoosePokemon();
        //yield return new WaitForSeconds(sceneTransition.FadeIn(0.4f));
//        yield return StartCoroutine(ScreenFade.main.Fade(true, 0.4f));
    }

    private object PromptNotImplemented()
    {
        Dialog.DrawDialogBox();
        return StartCoroutine(SceneScript.main.Dialog.DrawText("This menu has not yet been implemented."));
    }

    private IEnumerator ResetDefaults()
    {
        switching = false;
        swapPosition = -1;
        UpdateFrames();
        Dialog.DrawDialogBox();
        yield return PromptChoosePokemon();
    }

    private IEnumerator CancelSwitching()
    {
        switching = false;
        swapPosition = -1;
        UpdateFrames();
        Dialog.UndrawChoiceBox();
        Dialog.DrawDialogBox();
        yield return PromptChoosePokemon();
        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator ControlMovement()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (currentPosition >= CANCEL_BUTTON_POSITION) yield break;
            ShiftPosition(1);
            yield return MakeSoundAndWait();
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (currentPosition <= 0) yield break;
            ShiftPosition(-1);
            yield return MakeSoundAndWait();
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            if (currentPosition <= 0) yield break;
            if (currentPosition == CANCEL_BUTTON_POSITION)
            {
                ShiftPosition(-1);
            }
            else
            {
                ShiftPosition(-2);
            }
            yield return MakeSoundAndWait();
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            if (currentPosition >= CANCEL_BUTTON_POSITION) yield break;
            ShiftPosition(2);
            yield return MakeSoundAndWait();
        }
    }

    private object MakeSoundAndWait()
    {
        SfxHandler.Play(selectClip);
        return new WaitForSeconds(0.2f);
    }
}
}

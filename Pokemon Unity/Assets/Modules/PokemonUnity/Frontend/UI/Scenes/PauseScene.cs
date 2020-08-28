/// Source: Pokémon Unity Redux
/// Purpose: Pause UI (currently has a broken carousel) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PokemonUnity.Frontend.UI.Scenes;
using System.Collections;
using PokemonUnity.Frontend.Overworld;
using PokemonUnity.Frontend.UI;
using PokemonUnity.Frontend.Global;
using PokemonUnity.Backend.Datatypes;
using PokemonUnity.Backend.FileIO;
using PokemonUnity.Backend.Serializables;
using System.Collections.Generic;
using System;

namespace PokemonUnity.Frontend.UI.Scenes {
[System.Serializable]
public class PauseCarousel {
    public Image[] icons;
    public Image[] references;
    public int position; //start at pokedex -> 0
}
[System.Serializable]
public class PauseSetup {
    public PauseCarousel carousel;
    public Image pauseBottom;
    public Image saveDataDisplay;
    public Image selectArrow;
    public Text mapName;
    public Text dataText;
    public DialogBox Dialog;
    public AudioSource PauseAudio;
    public AudioClip selectClip;
    public AudioClip openClip;
    public Text selectedText;
    public Text time;
}
[System.Serializable] //makes sure this shows up in the inspector
public class PositionedImage {
    public Sprite image;
    public Vector3 position;
    public string name;
    public PauseScene.ImageMode mode = PauseScene.ImageMode.RunScene;
    public SceneScript.SceneEnum scene;
    public UnityEvent activatorEvent;
}
public class PauseScene : BaseScene
{
    public enum ImageMode {
        None,
        RunScene,
        RunEvent,
        Save
    }
    public PositionedImage[] pauseIcons = new PositionedImage[]
    {
        new PositionedImage()
        {
            name = "Pokédex",
            mode = ImageMode.None
        },
        new PositionedImage()
        {
            name = "Pokémon Party",
            scene = SceneScript.SceneEnum.Party
        },
        new PositionedImage()
        {
            name = "Bag",
            scene = SceneScript.SceneEnum.Bag
        },
        new PositionedImage()
        {
            name = "Trainer",
            scene = SceneScript.SceneEnum.Trainer
        },
        new PositionedImage()
        {
            name = "Save",
            mode = ImageMode.Save
        },
        new PositionedImage()
        {
            name = "Settings",
            scene = SceneScript.SceneEnum.Settings
        },
        new PositionedImage()
        {
            name = "Credits",
            mode = ImageMode.None
        },
    };
    public enum PauseState {
        Closed,
        Opening,
        Open,
        Closing
    }
    private int lastPosition;
    private int selectedPosition;
    private int nextPosition;
    private int[] carouselIndexes;
    public PauseSetup setup;

    public PauseState state = PauseState.Closed;
    void FixedUpdate()
    {
        setup.time.text = System.DateTime.Now.Hour+":"+(System.DateTime.Now.Minute >= 10 ? System.DateTime.Now.Minute.ToString() : "0"+System.DateTime.Now.Minute.ToString());
    }
    void Start()
    {
        setup.pauseBottom.rectTransform.anchoredPosition = new Vector3(0,-96f,0);
        setup.pauseBottom.gameObject.SetActive(false);
        setup.selectArrow.gameObject.SetActive(false);
        //setSelectedText("");

        selectedPosition = 0;

        setup.saveDataDisplay.gameObject.SetActive(false);
        carouselIndexes = new int[setup.carousel.icons.Length];
    }
    private void setSelectedText(string text)
    {
        setup.selectedText.text = text;
    }
    private IEnumerator openAnim()
    {
        setup.pauseBottom.gameObject.SetActive(true);
        float speed = 250f;
        state = PauseState.Opening;
        while(setup.pauseBottom.rectTransform.anchoredPosition.y < 0f && state == PauseState.Opening) {
            setup.pauseBottom.rectTransform.anchoredPosition = Vector3.MoveTowards(setup.pauseBottom.rectTransform.anchoredPosition, Vector3.zero, Time.deltaTime * speed);
            yield return null;
        }
        state = PauseState.Open;
    }
    private IEnumerator closeAnim()
    {
        state = PauseState.Closing;
        float speed = 200f;
        setup.selectArrow.gameObject.SetActive(false);
        while(setup.pauseBottom.rectTransform.anchoredPosition.y > -96f && state == PauseState.Closing) {
            setup.pauseBottom.rectTransform.anchoredPosition = Vector3.MoveTowards(setup.pauseBottom.rectTransform.anchoredPosition, new Vector3(0,-96,0), Time.deltaTime * speed);
            yield return null;
        }
        setup.pauseBottom.gameObject.SetActive(false);
        state = PauseState.Closed;
    }
    public int wrapAround(int i, int limit, int start)
    {
        if(i >= limit)
            return start;
        else if(i < start)
            return limit-1;
        return i;
    }
    public IEnumerator shiftIcon(int index)
    {
        Image icon = setup.carousel.icons[index];
        Image icon2 = setup.carousel.references[wrapAround(index+1,setup.carousel.icons.Length,0)];
        float speed = 250f;
        Debug.Log(icon.rectTransform.anchoredPosition.x);
        Debug.Log(icon.rectTransform.anchoredPosition.x);
        Debug.Log("BUFFER");
        while(icon.rectTransform.anchoredPosition.x != icon2.rectTransform.anchoredPosition.x) {
            icon.rectTransform.anchoredPosition = Vector3.MoveTowards(icon.rectTransform.anchoredPosition, icon2.rectTransform.anchoredPosition, Time.deltaTime * speed);
            Vector2 toberounded = Vector2.MoveTowards(icon.rectTransform.sizeDelta, icon2.rectTransform.sizeDelta, Time.deltaTime * speed);
            //toberounded.x = (float)Math.Ceiling(toberounded.x);
            //toberounded.y = (float)Math.Ceiling(toberounded.y);
            icon.rectTransform.sizeDelta = toberounded;
            yield return null;
        }
        yield return null;
    }
    public IEnumerator updatePosition(float direction)
    {
        selectedPosition = wrapAround(selectedPosition+(direction > 0 ? 1 : -1),pauseIcons.Length,0);
        setSelectedText(pauseIcons[selectedPosition].name);
        setup.selectArrow.rectTransform.anchoredPosition = pauseIcons[selectedPosition].position;
        //visual
        for(int i = 0; i < setup.carousel.icons.Length; i++)
        {
            yield return StartCoroutine(shiftIcon(setup.carousel.position));
            setup.carousel.position++;
            yield return null;
        }
        setup.carousel.position = 0;
    }
    public IEnumerator control()
    {
        selectedPosition = 0;
        setSelectedText("");
        SfxHandler.Play(setup.openClip);
        yield return StartCoroutine(openAnim());
        state = PauseState.Open; //set elsewhere not just here
        while (state == PauseState.Open)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                StartCoroutine(updatePosition(Input.GetAxisRaw("Horizontal")));
                SfxHandler.Play(setup.selectClip);
                yield return new WaitForSeconds(0.2f);
            }
            else if (Input.GetButton("Select"))
            {
                switch(pauseIcons[selectedPosition].mode) {
                    case ImageMode.RunScene:
                        SfxHandler.Play(setup.selectClip);
                        yield return StartCoroutine(ScreenFade.main.Fade(false, 0.4f));
                        yield return StartCoroutine(runSceneUntilDeactivated(SceneScript.main.CastToScene(pauseIcons[selectedPosition].scene)));
                        break;
                    case ImageMode.RunEvent:
                        pauseIcons[selectedPosition].activatorEvent.Invoke();
                        break;
                    case ImageMode.Save:
                        setup.saveDataDisplay.gameObject.SetActive(true);
                        setup.saveDataDisplay.sprite =
                            Resources.Load<Sprite>("Frame/choice" + PlayerPrefs.GetInt("frameStyle"));

                        int badgeTotal = 0;
                        for (int i = 0; i < 12; i++)
                        {
                            if (SaveData.currentSave.gymsBeaten[i])
                            {
                                badgeTotal += 1;
                            }
                        }
                        string playerTime = "" + SaveData.currentSave.playerMinutes;
                        if (playerTime.Length == 1)
                        {
                            playerTime = "0" + playerTime;
                        }
                        playerTime = SaveData.currentSave.playerHours + " : " + playerTime;

                        setup.mapName.text = Player.player.accessedMapSettings.mapName;
                        setup.dataText.text = SaveData.currentSave.playerName + "\n" +
                                        badgeTotal + "\n" +
                                        "0" + "\n" + //pokedex not yet implemented
                                        playerTime;

                        setup.Dialog.DrawDialogBox();
                        yield return StartCoroutine(setup.Dialog.DrawText("Would you like to save the game?"));
                        yield return StartCoroutine(setup.Dialog.DrawChoiceBox(0));
                        int chosenIndex = setup.Dialog.chosenIndex;
                        if (chosenIndex == 1)
                        {
                            //update save file
                            //Dialog.UndrawChoiceBox();
                            SaveData.currentSave.levelName = SceneManager.GetActiveScene().name;
                            SaveData.currentSave.playerPosition = new SeriV3(Player.player.transform.position);
                            SaveData.currentSave.playerDirection = (int)Player.player.direction;
                            SaveData.currentSave.mapName = Player.player.accessedMapSettings.mapName;

                            NonResettingHandler.saveDataToGlobal();

                            SaveLoad.Save();
                            setup.Dialog.DrawDialogBox();
                            yield return
                                StartCoroutine(setup.Dialog.DrawText(SaveData.currentSave.playerName + " saved the game!"));
                            while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back"))
                            {
                                yield return null;
                            }
                        }
                        setup.Dialog.UnDrawDialogBox();
                        setup.Dialog.UndrawChoiceBox();
                        setup.saveDataDisplay.gameObject.SetActive(false);
                        yield return new WaitForSeconds(0.2f);
                        break;
                }
            }

            if (Input.GetButton("Start") || Input.GetButton("Back"))
            {
                state = PauseState.Closing;
            }
            yield return null;
        }
        yield return StartCoroutine(closeAnim());
        setup.pauseBottom.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    
    /// Only runs the default scene (no parameters)
    private IEnumerator runSceneUntilDeactivated(BaseScene sceneInterface)
    {
        disableAll();
        sceneInterface.gameObject.SetActive(true);
        sceneInterface.gameObject.SendMessage("control");
        yield return new WaitForSeconds(0.05f);
        while (sceneInterface.gameObject.activeSelf)
        {
            yield return null;
        }
        enableAll();
    }
    private void disableAll()
    {
       //setSelectedText("");
       setup.pauseBottom.gameObject.SetActive(false);
       setup.selectArrow.gameObject.SetActive(false);
    }
    private void enableAll()
    {
       //setSelectedText("");
       //selectedPosition = 0;
       setup.pauseBottom.gameObject.SetActive(true);
       setup.selectArrow.gameObject.SetActive(true);
    }
}
}

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
using TMPro;

namespace PokemonUnity.Frontend.UI.Scenes {
[System.Serializable]
public class PauseCarousel {
    public Image[] icons;
    public Image[] references;
    public int position;
    public int selectedPosition;
    public PositionedImage selectedImage;
}
[System.Serializable]
public class PauseSetup {
    public PauseCarousel carousel;
    public Image pauseBottom;
    public Image saveDataDisplay;
    public TextMeshProUGUI mapName;
    public TextMeshProUGUI dataText;
    public AudioSource PauseAudio;
    public AudioClip selectClip;
    public AudioClip openClip;
    public Text selectedText;
    public Text time;
}
[System.Serializable] //makes sure this shows up in the inspector
public class PositionedImage {
    public Sprite image;
    public string name;
    public PauseScene.ImageMode mode = PauseScene.ImageMode.NotImplemented;
    public SceneScript.SceneEnum scene;
    public UnityEvent activatorEvent;
}
public class PauseScene : BaseScene
{
    public enum ImageMode {
        None,
        RunScene,
        RunEvent,
        Save,
        NotImplemented
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
        //setSelectedText("");

        setup.carousel.position = 0;
        setup.carousel.icons = new Image[setup.carousel.references.Length];
        for(int i = 0; i < setup.carousel.references.Length;i++)
        {
            GameObject instance = Instantiate(setup.carousel.references[i].gameObject,setup.pauseBottom.transform.Find("Carousel"));
            setup.carousel.icons[i] = instance.GetComponent<Image>();
        }
        setup.saveDataDisplay.gameObject.SetActive(false);
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
        while(setup.pauseBottom.rectTransform.anchoredPosition.y > -96f && state == PauseState.Closing) {
            setup.pauseBottom.rectTransform.anchoredPosition = Vector3.MoveTowards(setup.pauseBottom.rectTransform.anchoredPosition, new Vector3(0,-96,0), Time.deltaTime * speed);
            yield return null;
        }
        setup.pauseBottom.gameObject.SetActive(false);
        state = PauseState.Closed;
    }
    public enum WrapState {
        Negative,
        Positive,
        TimesTwo
    }
    public IEnumerator shiftIcon(int index,int direction)
    {
        Image icon2;
        if(setup.carousel.position+(direction > 0 ? 1 : -1) >= setup.carousel.references.Length)
            icon2 = setup.carousel.references[0];
        else if(setup.carousel.position+(direction > 0 ? 1 : -1) < 0)
            icon2 = setup.carousel.references[setup.carousel.references.Length-1];
        else
            icon2 = setup.carousel.references[setup.carousel.position+(direction > 0 ? 1 : -1)];
        Image icon = setup.carousel.icons[index];
        //icon.transform.SetSiblingIndex(direction*-1);
        Debug.Log("Directioned: "+(index+direction)+"; Non: "+index);
        float speed = 250f;
        if(index >= setup.carousel.references.Length-1)
            speed = 999f;
        if(index == 0)
            speed = 999f;
        while(icon.rectTransform.anchoredPosition.x != icon2.rectTransform.anchoredPosition.x) {
            icon.rectTransform.anchoredPosition = Vector3.MoveTowards(icon.rectTransform.anchoredPosition, icon2.rectTransform.anchoredPosition, Time.deltaTime * speed);
            Vector2 toberounded = Vector2.MoveTowards(icon.rectTransform.sizeDelta, icon2.rectTransform.sizeDelta, Time.deltaTime * speed);
            //toberounded.x = (float)Math.Ceiling(toberounded.x);
            //toberounded.y = (float)Math.Ceiling(toberounded.y);
            icon.rectTransform.sizeDelta = toberounded;
            yield return null;
        }
        icon2.sprite = icon.sprite;
        yield return null;
    }
    public IEnumerator updatePosition(float direction)
    {
        setup.carousel.position = 0;
        if(setup.carousel.selectedPosition+(direction > 0 ? 1 : -1) >= pauseIcons.Length)
            setup.carousel.selectedPosition = 0;
        else if(setup.carousel.selectedPosition+(direction > 0 ? 1 : -1) < 0)
            setup.carousel.selectedPosition = pauseIcons.Length-1;
        else
            setup.carousel.selectedPosition += (direction > 0 ? 1 : -1);
        setup.carousel.selectedImage = pauseIcons[setup.carousel.selectedPosition];
        setSelectedText(setup.carousel.selectedImage.name);
        setup.carousel.references[0].transform.parent.gameObject.SetActive(false);
        setup.carousel.icons[0].sprite = setup.carousel.icons[setup.carousel.references.Length-2].sprite;
        setup.carousel.icons[setup.carousel.references.Length-1].sprite = setup.carousel.icons[1].sprite;
        for(int i = 0; i < setup.carousel.references.Length; i++)
        {
            PositionedImage repIcon;
            if(setup.carousel.selectedPosition >= pauseIcons.Length)
                repIcon = pauseIcons[0];
            else if(setup.carousel.selectedPosition < 0)
                repIcon = pauseIcons[(pauseIcons.Length-1)];
            else
                repIcon = pauseIcons[setup.carousel.selectedPosition];
            StartCoroutine(shiftIcon(setup.carousel.position,(direction > 0 ? -1 : 1)));
            setup.carousel.position++;
            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        setup.carousel.references[0].transform.parent.gameObject.SetActive(true);
        foreach(Image img in setup.carousel.icons)
        {
            Destroy(img.gameObject);
        }
        setup.carousel.icons = new Image[setup.carousel.references.Length];
        for(int i = 0; i < setup.carousel.references.Length;i++)
        {

            GameObject instance = Instantiate(setup.carousel.references[i].gameObject,setup.pauseBottom.transform.Find("Carousel"));
            setup.carousel.icons[i] = instance.GetComponent<Image>();
        }
        setup.carousel.position = 0;
    }
    public IEnumerator control()
    {
        setup.carousel.position = 0;
        setSelectedText("Bag");
        SfxHandler.Play(setup.openClip);
        yield return StartCoroutine(openAnim());
        state = PauseState.Open; //set elsewhere not just here
        while (state == PauseState.Open)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                SfxHandler.Play(setup.selectClip);
                yield return StartCoroutine(updatePosition(Input.GetAxisRaw("Horizontal")));
            }
            else if (Input.GetButton("Select"))
            {
                switch(pauseIcons[setup.carousel.selectedPosition].mode) {
                    case ImageMode.RunScene:
                        SfxHandler.Play(setup.selectClip);
                        yield return StartCoroutine(ScreenFade.main.Fade(false, 0.4f));
                        yield return StartCoroutine(runSceneUntilDeactivated(SceneScript.main.CastToScene(pauseIcons[setup.carousel.position].scene)));
                        break;
                    case ImageMode.RunEvent:
                        pauseIcons[setup.carousel.position].activatorEvent.Invoke();
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

                        SceneScript.main.Dialog.DrawDialogBox();
                        yield return StartCoroutine(SceneScript.main.Dialog.DrawText("Would you like to save the game?"));
                        yield return StartCoroutine(SceneScript.main.Dialog.DrawChoiceBox(0));
                        int chosenIndex = SceneScript.main.Dialog.chosenIndex;
                        if (chosenIndex == 1)
                        {
                            //update save file
                            SceneScript.main.Dialog.UndrawChoiceBox();
                            SaveData.currentSave.levelName = SceneManager.GetActiveScene().name;
                            SaveData.currentSave.playerPosition = new SeriV3(Player.player.transform.position);
                            SaveData.currentSave.playerDirection = (int)Player.player.direction;
                            SaveData.currentSave.mapName = Player.player.accessedMapSettings.mapName;

                            NonResettingHandler.saveDataToGlobal();

                            SaveLoad.Save();
                            SceneScript.main.Dialog.DrawDialogBox();
                            yield return
                                StartCoroutine(SceneScript.main.Dialog.DrawText(SaveData.currentSave.playerName + " saved the game!"));
                            while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back"))
                            {
                                yield return null;
                            }
                        }
                        SceneScript.main.Dialog.UnDrawDialogBox();
                        SceneScript.main.Dialog.UndrawChoiceBox();
                        setup.saveDataDisplay.gameObject.SetActive(false);
                        yield return new WaitForSeconds(0.2f);
                        break;
                    case ImageMode.NotImplemented:
                        SceneScript.main.Dialog.DrawDialogBox();
                        yield return StartCoroutine(SceneScript.main.Dialog.DrawText("This menu has not yet been implemented."));
                        while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back"))
                        {
                            yield return null;
                        }
                        SceneScript.main.Dialog.UnDrawDialogBox();
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
    }
    private void enableAll()
    {
       //setSelectedText("");
       //position = 0;
       setup.pauseBottom.gameObject.SetActive(true);
    }
}
}

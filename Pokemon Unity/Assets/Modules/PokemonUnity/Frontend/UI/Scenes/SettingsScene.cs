/// Source: Pokémon Unity Redux
/// Purpose: Settings UI (temporary) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using UnityEngine.UI;
namespace PokemonUnity.Frontend.UI.Scenes {
public class SettingsScene : BaseScene
{
    private DialogBox Dialog;

    private Image selectRow;

    private Text
        textSpeed,
        textSpeedShadow,
        textSpeedHighlight,
        musicVolume,
        musicVolumeShadow,
        musicVolumeHighlight,
        sfxVolume,
        sfxVolumeShadow,
        sfxVolumeHighlight,
        frameStyle,
        frameStyleShadow,
        battleScene,
        battleSceneShadow,
        battleSceneHighlight,
        battleStyle,
        battleStyleShadow,
        battleStyleHighlight,
        screenSize,
        screenSizeShadow,
        screenSizeHighlight,
        fullscreen,
        fullscreenShadow,
        fullscreenHighlight;

    private bool running;
    private int selectedOption;

    private int[] selectedOptionSize = new int[]
    {
        3, 21, 21, 2, 2, 2, 5, 3
    };

    private int[] selectedOptionIndex = new int[]
    {
        2, 7, 14, 0, 1, 0, 0, 0
    };

    private string[] selectedOptionText = new string[]
    {
        "How quickly to draw text to the screen.",
        "Adjust the volume of the music.",
        "Adjust the volume of the sound effects.",
        "Change the appearance of the text boxes.",
        "Display animations during battles.",
        "Switch before opponent's next Pokemon?",
        "Adjust the resolution of the screen.",
        "Set the fullscreen mode."
    };

    private AudioSource SettingsAudio;
    public AudioClip selectClip;

    private GameObject DialogBox;
    private Text DialogBoxText;
    private Text DialogBoxTextShadow;
    private Image DialogBoxBorder;
}
}

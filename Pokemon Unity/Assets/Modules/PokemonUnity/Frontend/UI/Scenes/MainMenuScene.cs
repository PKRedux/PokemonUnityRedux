/// Source: Pokémon Unity Redux
/// Purpose: Main menu UI (temporary) for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using UnityEngine.UI;
namespace PokemonUnity.Frontend.UI.Scenes {
public class MainMenuScene : BaseScene
{
    public int selectedButton = 0;
    public int selectedFile = 0;

    public Texture buttonSelected;
    public Texture buttonDimmed;

    private GameObject fileDataPanel;
    private GameObject continueButton;

    private Image[] button = new Image[3];
    private Image[] buttonHighlight = new Image[3];
    private Text[] buttonText = new Text[3];
    private Text[] buttonTextShadow = new Text[3];

    private Text fileNumbersText;
    private Text fileNumbersTextShadow;
    private Text fileSelected;

    private Text mapNameText;
    private Text mapNameTextShadow;
    private Text dataText;
    private Text dataTextShadow;
    private Image[] pokemon = new Image[6];
}
}

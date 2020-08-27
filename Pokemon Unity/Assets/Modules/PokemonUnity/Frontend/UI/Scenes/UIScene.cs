/// Source: Pokémon Unity Redux
/// Purpose: UI scene director for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using System.Collections;
namespace PokemonUnity.Frontend.UI.Scenes {
public class UIScene : MonoBehaviour
{
    public static UIScene main;

    public BagScene Bag;
    public BattleScene Battle;
    public EvolutionScene Evolution;
    public PartyScene Party;
    public PauseScene Pause;
    public PCScene PC;
    public SettingsScene Settings;
    public SummaryScene Summary;
    public TrainerScene Trainer;
    public TypingScene Typing;


    void Awake()
    {
        if (main == null)
        {
            main = this;
        }

        Bag.gameObject.SetActive(true);
        Battle.gameObject.SetActive(true);
        Evolution.gameObject.SetActive(true);
        Party.gameObject.SetActive(true);
        Pause.gameObject.SetActive(true);
        PC.gameObject.SetActive(true);
        Settings.gameObject.SetActive(true);
        Summary.gameObject.SetActive(true);
        Trainer.gameObject.SetActive(true);
        Typing.gameObject.SetActive(true);
    }
}
}
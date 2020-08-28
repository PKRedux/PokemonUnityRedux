/// Source: Pokémon Unity Redux
/// Purpose: UI scene director for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using System.Collections;
namespace PokemonUnity.Frontend.UI.Scenes {
public class BaseScene : MonoBehaviour
{
    //needed for other serializables
}
public class SceneScript : MonoBehaviour
{
    public static SceneScript main;
    public enum SceneEnum {
        None,
        Bag,
        Battle,
        Evolution,
        Party,
        Pause,
        PC,
        Settings,
        Summary,
        Trainer,
        Typing,
    }
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
    public BaseScene CastToScene(SceneEnum caster)
    {
        switch(caster)
        {
            case SceneEnum.Bag:
                return Bag;
            case SceneEnum.Battle:
                return Battle;
            case SceneEnum.Evolution:
                return Evolution;
            case SceneEnum.Party:
                return Party;
            case SceneEnum.Pause:
                return Pause;
            case SceneEnum.PC:
                return Pause;
            case SceneEnum.Summary:
                return Summary;
            case SceneEnum.Trainer:
                return Trainer;
            case SceneEnum.Typing:
                return Typing;
            default:
                return new BaseScene();
        }
    }
    void Awake()
    {
        if(main == null)
            main = this;
        if(Bag != null)
            Bag.gameObject.SetActive(true);
        if(Battle != null)
            Battle.gameObject.SetActive(true);
        if(Evolution != null)
            Evolution.gameObject.SetActive(true);
        if(Party != null)
            Party.gameObject.SetActive(true);
        if(Pause != null)
            Pause.gameObject.SetActive(true);
        if(PC != null)
            PC.gameObject.SetActive(true);
        if(Settings != null)
            Settings.gameObject.SetActive(true);
        if(Settings != null)
            Summary.gameObject.SetActive(true);
        if(Trainer != null)
            Trainer.gameObject.SetActive(true);
        if(Typing != null)
            Typing.gameObject.SetActive(true);
    }
}
}
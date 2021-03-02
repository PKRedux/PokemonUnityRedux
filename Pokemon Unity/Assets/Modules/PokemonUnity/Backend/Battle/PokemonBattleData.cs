using PokemonUnity.Backend.Datatypes;
using PokemonUnity.Backend.Serializables;

namespace Modules.PokemonUnity.Backend.Battle
{
    public class PokemonBattleData
    {
        //[movePosition]
        public string[] pokemonMoveset = new string[4];

        //Stats can be changed in battle. These changes never persist after swapping out.
        public int[] pokemonStats = new int[5];

        //Ability can be changed in battle. These changes never persist after swapping out.
        public string pokemonAbility;
        //Types can be changed in battle. These changes never persist after swapping out.
        public PokemonData.Type pokemonType1 = PokemonData.Type.NONE;

        public PokemonData.Type pokemonType2 = PokemonData.Type.NONE;

        public PokemonData.Type pokemonType3 = PokemonData.Type.NONE;

        //pokemon stat boost data
        public int[] pokemonStatsMod = {
            0, //ATK
            0, //DEF
            0, //SPA
            0, //SPD
            0, //SPE
            0, //ACC
            0, //EVA
        };

        public CommandType command;
        public int commandTarget;
        public MoveData commandMove;
        public ItemData commandItem;
        public Pokemon commandPokemon;

        //Pokemon Effects
        public bool confused;
        public int infatuatedBy = -1;
        public bool flinched;
        public int statusEffectTurns;
        public int lockedTurns;
        public int partTrappedTurns;
        public bool trapped;
        public bool charging;
        public bool recharging;
        public bool protect;
        //specific moves
        public int seededBy = -1;
        public bool focusEnergy;
        public bool destinyBond;
        public bool minimized;
        public bool defenseCurled;


    }
}

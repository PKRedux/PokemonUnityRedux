using PokemonUnity.Backend.Databases;
using PokemonUnity.Backend.Datatypes;
using PokemonUnity.Backend.Serializables;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.PokemonUnity.Backend.Battle
{
    public static class BattleCalculation
    {
        //////////////////////////////////
        /// BATTLE DATA MANAGEMENT
        //
        /// Calculates the base damage of an attack (before modifiers are applied).
        public static float CalculateDamage(Pokemon attackerPokemon, int[] attackerStats, int[] targetStats, MoveData move)
        {
            float baseDamage = 0;
            if (move.getCategory() == MoveData.Category.PHYSICAL)
            {
                baseDamage = ((2f * attackerPokemon.getLevel() + 10f) / 250f) *
                             (attackerStats[0] / (float) targetStats[1]) *
                             move.getPower() + 2f;
            }
            else if (move.getCategory() == MoveData.Category.SPECIAL)
            {
                baseDamage = ((2f * attackerPokemon.getLevel() + 10f) / 250f) *
                             (attackerStats[2] / (float) targetStats[3]) *
                             move.getPower() + 2f;
            }

            baseDamage *= Random.Range(0.85f, 1f);
            return baseDamage;
        }

        /// Uses the attacker's total critical ratio to randomly determine whether a Critical Hit should happen or not
        public static bool CalculateCritical(bool attackerFocusEnergy, MoveData move)
        {
            int attackerCriticalRatio = 0;
            if (attackerFocusEnergy)
            {
                attackerCriticalRatio += 1;
            }
            if (move.hasMoveEffect(MoveData.Effect.Critical))
            {
                attackerCriticalRatio += 1;
            }
            bool applyCritical = false;
            if (move.getCategory() == MoveData.Category.STATUS) return false;
            switch (attackerCriticalRatio)
            {
                case 0:
                {
                    if (Random.value <= 0.0625)
                    {
                        applyCritical = true;
                    }

                    break;
                }
                case 1:
                {
                    if (Random.value <= 0.125)
                    {
                        applyCritical = true;
                    }

                    break;
                }
                case 2:
                {
                    if (Random.value <= 0.5)
                    {
                        applyCritical = true;
                    }

                    break;
                }
                default:
                {
                    if (attackerCriticalRatio > 2)
                    {
                        applyCritical = true;
                    }

                    break;
                }
            }
            return applyCritical;
        }


        /// TEMPORARILY USED FOR EVERYTHING NOT COVERED BY AN EXISTING METHOD
        public static float CalculateModifiedDamage(PokemonData.Type attackerType1, PokemonData.Type attackerType2, 
            PokemonData.Type attackerType3, int attackerPosition, int targetPosition, int[][] pokemonStatsMod, 
            int[] reflectTurns, int[] lightScreenTurns, MoveData move, float baseDamage, bool applyCritical)
        {
            float modifiedDamage = baseDamage;

            //apply STAB
            if (attackerType1 == move.getType() ||
                attackerType2 == move.getType() ||
                attackerType3 == move.getType())
            {
                modifiedDamage *= 1.5f;
            }

            //apply Offence/Defence boosts 
            if (move.getCategory() == MoveData.Category.PHYSICAL)
            {
                modifiedDamage *= CalculateStatModifier(pokemonStatsMod[0][attackerPosition]);
                if (!applyCritical)
                {
                    //exclude defensive buffs in a critical hit
                    modifiedDamage /= CalculateStatModifier(pokemonStatsMod[1][targetPosition]);
                }
            }
            else if (move.getCategory() == MoveData.Category.SPECIAL)
            {
                modifiedDamage *= CalculateStatModifier(pokemonStatsMod[2][attackerPosition]);
                if (!applyCritical)
                {
                    //exclude defensive buffs in a critical hit
                    modifiedDamage /= CalculateStatModifier(pokemonStatsMod[3][targetPosition]);
                }
            }
            //not yet implemented
            //apply held item
            //apply ability
            //apply field advantages
            //reflect/lightScreen
            if (!applyCritical)
            {
                if (move.getCategory() == MoveData.Category.PHYSICAL)
                {
                    if (reflectTurns[Mathf.FloorToInt((float) targetPosition / 3f)] > 0)
                    {
                        modifiedDamage *= 0.5f;
                    }
                }
                else if (move.getCategory() == MoveData.Category.SPECIAL)
                {
                    if (lightScreenTurns[Mathf.FloorToInt((float) targetPosition / 3f)] > 0)
                    {
                        modifiedDamage *= 0.5f;
                    }
                }
            }

            //apply multi-target debuff 

            return Mathf.Floor(modifiedDamage);
        }

        public static float CalculateStatModifier(int modifier)
        {
            if (modifier > 0)
            {
                return ((2f + (float) modifier) / 2f);
            }
            else if (modifier < 0)
            {
                return (2f / (2f + Mathf.Abs((float) modifier)));
            }
            return 1f;
        }

        public static float CalculateAccuracyModifier(int modifier)
        {
            if (modifier > 0)
            {
                return ((3f + modifier) / 3f);
            }
            else if (modifier < 0)
            {
                return (3f / (3f + modifier));
            }
            return 1f;
        }

        /// returns the modifier of a type vs. type. returns as 0f-2f
        public static float GetSuperEffectiveModifier(PokemonData.Type attackingType, PokemonData.Type targetType)
        {
            switch (attackingType)
            {
                case PokemonData.Type.BUG:
                    switch (targetType)
                    {
                        case PokemonData.Type.DARK:
                        case PokemonData.Type.GRASS:
                        case PokemonData.Type.PSYCHIC:
                            return 2f;
                        case PokemonData.Type.FAIRY:
                        case PokemonData.Type.FIGHTING:
                        case PokemonData.Type.FIRE:
                        case PokemonData.Type.FLYING:
                        case PokemonData.Type.GHOST:
                        case PokemonData.Type.POISON:
                        case PokemonData.Type.STEEL:
                            return 0.5f;
                    }
                    break;
                case PokemonData.Type.DARK:
                    switch (targetType)
                    {
                        case PokemonData.Type.GHOST:
                        case PokemonData.Type.PSYCHIC:
                            return 2f;
                        case PokemonData.Type.DARK:
                        case PokemonData.Type.FAIRY:
                        case PokemonData.Type.FIGHTING:
                            return 0.5f;
                    }
                    break;
                case PokemonData.Type.DRAGON:
                    switch (targetType)
                    {
                        case PokemonData.Type.DRAGON:
                            return 2f;
                        case PokemonData.Type.STEEL:
                            return 0.5f;
                        case PokemonData.Type.FAIRY:
                            return 0f;
                    }
                    break;
                case PokemonData.Type.ELECTRIC:
                    switch (targetType)
                    {
                        case PokemonData.Type.FLYING:
                        case PokemonData.Type.WATER:
                            return 2f;
                        case PokemonData.Type.DRAGON:
                        case PokemonData.Type.ELECTRIC:
                        case PokemonData.Type.GRASS:
                            return 0.5f;
                        case PokemonData.Type.GROUND:
                            return 0f;
                    }
                    break;
                case PokemonData.Type.FAIRY:
                    switch (targetType)
                    {
                        case PokemonData.Type.DARK:
                        case PokemonData.Type.DRAGON:
                        case PokemonData.Type.FIGHTING:
                            return 2f;
                        case PokemonData.Type.FIRE:
                        case PokemonData.Type.POISON:
                        case PokemonData.Type.STEEL:
                            return 0.5f;
                    }
                    break;
                case PokemonData.Type.FIGHTING:
                    switch (targetType)
                    {
                        case PokemonData.Type.DARK:
                        case PokemonData.Type.ICE:
                        case PokemonData.Type.NORMAL:
                        case PokemonData.Type.ROCK:
                        case PokemonData.Type.STEEL:
                            return 2f;
                        case PokemonData.Type.BUG:
                        case PokemonData.Type.FAIRY:
                        case PokemonData.Type.FLYING:
                        case PokemonData.Type.POISON:
                        case PokemonData.Type.PSYCHIC:
                            return 0.5f;
                        case PokemonData.Type.GHOST:
                            return 0f;
                    }

                    break;
                case PokemonData.Type.FIRE:
                    switch (targetType)
                    {
                        case PokemonData.Type.BUG:
                        case PokemonData.Type.GRASS:
                        case PokemonData.Type.ICE:
                        case PokemonData.Type.STEEL:
                            return 2f;
                        case PokemonData.Type.DRAGON:
                        case PokemonData.Type.FIRE:
                        case PokemonData.Type.ROCK:
                        case PokemonData.Type.WATER:
                            return 0.5f;
                    }
                    break;
                case PokemonData.Type.FLYING:
                    switch (targetType)
                    {
                        case PokemonData.Type.BUG:
                        case PokemonData.Type.FIGHTING:
                        case PokemonData.Type.GRASS:
                            return 2f;
                        case PokemonData.Type.ELECTRIC:
                        case PokemonData.Type.ROCK:
                        case PokemonData.Type.STEEL:
                            return 0.5f;
                    }
                    break;
                case PokemonData.Type.GHOST:
                    switch (targetType)
                    {
                        case PokemonData.Type.GHOST:
                        case PokemonData.Type.PSYCHIC:
                            return 2f;
                        case PokemonData.Type.DARK:
                            return 0.5f;
                        case PokemonData.Type.NORMAL:
                            return 0f;
                    }
                    break;
                case PokemonData.Type.GRASS:
                    switch (targetType)
                    {
                        case PokemonData.Type.GROUND:
                        case PokemonData.Type.ROCK:
                        case PokemonData.Type.WATER:
                            return 2f;
                        case PokemonData.Type.BUG:
                        case PokemonData.Type.DRAGON:
                        case PokemonData.Type.FIRE:
                        case PokemonData.Type.FLYING:
                        case PokemonData.Type.GRASS:
                        case PokemonData.Type.POISON:
                        case PokemonData.Type.STEEL:
                            return 0.5f;
                    }
                    break;
                case PokemonData.Type.GROUND:
                    switch (targetType)
                    {
                        case PokemonData.Type.ELECTRIC:
                        case PokemonData.Type.FIRE:
                        case PokemonData.Type.POISON:
                        case PokemonData.Type.ROCK:
                        case PokemonData.Type.STEEL:
                            return 2f;
                        case PokemonData.Type.BUG:
                        case PokemonData.Type.GRASS:
                            return 0.5f;
                        case PokemonData.Type.FLYING:
                            return 0f;
                    }
                    break;
                case PokemonData.Type.ICE:
                    switch (targetType)
                    {
                        case PokemonData.Type.DRAGON:
                        case PokemonData.Type.FLYING:
                        case PokemonData.Type.GRASS:
                        case PokemonData.Type.GROUND:
                            return 2f;
                        case PokemonData.Type.FIRE:
                        case PokemonData.Type.ICE:
                        case PokemonData.Type.STEEL:
                        case PokemonData.Type.WATER:
                            return 0.5f;
                    }
                    break;
                case PokemonData.Type.NORMAL:
                    switch (targetType)
                    {
                        case PokemonData.Type.ROCK:
                        case PokemonData.Type.STEEL:
                            return 0.5f;
                        case PokemonData.Type.GHOST:
                            return 0f;
                    }
                    break;
                case PokemonData.Type.POISON:
                    switch (targetType)
                    {
                        case PokemonData.Type.FAIRY:
                        case PokemonData.Type.GRASS:
                            return 2f;
                        case PokemonData.Type.POISON:
                        case PokemonData.Type.GROUND:
                        case PokemonData.Type.ROCK:
                        case PokemonData.Type.GHOST:
                            return 0.5f;
                        case PokemonData.Type.STEEL:
                            return 0f;
                    }
                    break;
                case PokemonData.Type.PSYCHIC:
                    switch (targetType)
                    {
                        case PokemonData.Type.FIGHTING:
                        case PokemonData.Type.POISON:
                            return 2f;
                        case PokemonData.Type.PSYCHIC:
                        case PokemonData.Type.STEEL:
                            return 0.5f;
                        case PokemonData.Type.DARK:
                            return 0f;
                    }
                    break;
                case PokemonData.Type.ROCK:
                    switch (targetType)
                    {
                        case PokemonData.Type.BUG:
                        case PokemonData.Type.FIRE:
                        case PokemonData.Type.FLYING:
                        case PokemonData.Type.ICE:
                            return 2f;
                        case PokemonData.Type.FIGHTING:
                        case PokemonData.Type.GROUND:
                        case PokemonData.Type.STEEL:
                            return 0.5f;
                    }
                    break;
                case PokemonData.Type.STEEL:
                    switch (targetType)
                    {
                        case PokemonData.Type.FAIRY:
                        case PokemonData.Type.ICE:
                        case PokemonData.Type.ROCK:
                            return 2f;
                        case PokemonData.Type.ELECTRIC:
                        case PokemonData.Type.FIRE:
                        case PokemonData.Type.STEEL:
                        case PokemonData.Type.WATER:
                            return 0.5f;
                    }
                    break;
                case PokemonData.Type.WATER:
                    switch (targetType)
                    {
                        case PokemonData.Type.FIRE:
                        case PokemonData.Type.GROUND:
                        case PokemonData.Type.ROCK:
                            return 2f;
                        case PokemonData.Type.DRAGON:
                        case PokemonData.Type.GRASS:
                        case PokemonData.Type.WATER:
                            return 0.5f;
                    }
                    break;
            }
            return 1f;
        }

        /// Returns the Pokemon index that should move first, that hasn't moved this turn.
        public static int GetHighestSpeedIndex(bool[] pokemonHasMoved, PokemonBattleData[] battleDatas, Pokemon[] pokemon)
        {
            int topSpeed = 0;
            int topPriority = -7;
            int topSpeedPosition = 0;

            //calculate the highest speed remaining in the list, the set that speed to -1
            for (int i = 0; i < 6; i++)
            {
                if (pokemonHasMoved[i]) continue;
                //calculate speed
                float calculatedPokemonPriority = 0;
                var calculatedPokemonSpeed = battleDatas[i].pokemonStats[4] * CalculateStatModifier(battleDatas[i].pokemonStatsMod[4]);
                if (pokemon[i] != null)
                {
                    if (pokemon[i].getStatus() == Pokemon.Status.PARALYZED)
                    {
                        calculatedPokemonSpeed /= 4f;
                    }
                }

                switch (battleDatas[i].command)
                {
                    //only the player gets increased priority on fleeing
                    case CommandType.Flee when i < 3:
                    case CommandType.Item:
                        calculatedPokemonPriority = 6;
                        break;
                    case CommandType.Move:
                        calculatedPokemonPriority = battleDatas[i].commandMove.getPriority();
                        break;
                    case CommandType.Switch:
                        //6 priority for regular swapping out.
                        calculatedPokemonPriority = 6;
                        break;
                }

                //if the top speed is greater than the old, AND the priority is no lower than       OR   the priority is greater than the old
                if ((calculatedPokemonSpeed >= topSpeed && calculatedPokemonPriority >= topPriority) ||
                    calculatedPokemonPriority > topPriority)
                {
                    if (calculatedPokemonSpeed == topSpeed && calculatedPokemonPriority == topPriority)
                    {
                        //if the speed/priority is exactly equal to the current highest, then to randomize the order
                        //in which speed is chosen (to break a speed tie), update the topSpeed position ONLY when a
                        //random float value (0f-1f) is greater than 0.5f.
                        if (Random.value > 0.5f)
                        {
                            topSpeedPosition = i;
                        }
                    }
                    else
                    {
                        topSpeed = Mathf.FloorToInt(calculatedPokemonSpeed);
                        topPriority = Mathf.FloorToInt(calculatedPokemonPriority);
                        topSpeedPosition = i;
                    }
                }
            }
            return topSpeedPosition;
        }
    

        public static void SetHpBarColor(Image bar, float maxSize)
        {
            if (bar.rectTransform.sizeDelta.x < maxSize / 4f)
            {
                bar.color = new Color(0.625f, 0.125f, 0, 1);
            }
            else if (bar.rectTransform.sizeDelta.x < maxSize / 2f)
            {
                bar.color = new Color(0.687f, 0.562f, 0, 1);
            }
            else
            {
                bar.color = new Color(0.125f, 0.625f, 0, 1);
            }
        }

        /// Switch Pokemon
        public static bool SwitchPokemon(Pokemon[] pokemons, int index, PokemonBattleData switchBattleData, 
            Pokemon newPokemon, bool batonPass = false, bool forceSwitch = false)
        {
            if (newPokemon == null)
            {
                return false;
            }

            if (newPokemon.getStatus() == Pokemon.Status.FAINTED)
            {
                return false;
            }

            // Return false if any condition is preventing the pokemon from switching out
            if (!forceSwitch)
            {
                //no condition can stop a fainted pokemon from switching out
                if (pokemons[index] != null && pokemons[index].getStatus() != Pokemon.Status.FAINTED)
                {
                }
            }

            pokemons[index] = newPokemon;
            switchBattleData.pokemonMoveset = newPokemon.getMoveset();
            //set PokemonData
            UpdatePokemonStats(switchBattleData.pokemonStats, pokemons[index] );
            switchBattleData.pokemonAbility =
                PokemonDatabase.getPokemon(newPokemon.getID()).getAbility(newPokemon.getAbility());
            switchBattleData.pokemonType1 = PokemonDatabase.getPokemon(newPokemon.getID()).getType1();
            switchBattleData.pokemonType2 = PokemonDatabase.getPokemon(newPokemon.getID()).getType2();
            switchBattleData.pokemonType3 = PokemonData.Type.NONE;

            //reset Pokemon Effects
            switchBattleData.confused = false;
            switchBattleData.infatuatedBy = -1;
            switchBattleData.flinched = false;
            switchBattleData.statusEffectTurns = 0;
            switchBattleData.lockedTurns = 0;
            switchBattleData.partTrappedTurns = 0;
            switchBattleData.trapped = false;
            switchBattleData.charging = false;
            switchBattleData.recharging = false;
            switchBattleData.protect = false;
            //specific moves
            switchBattleData.seededBy = -1;
            switchBattleData.destinyBond = false;
            switchBattleData.minimized = false;
            switchBattleData.defenseCurled = false;
            if (!batonPass)
            {
                switchBattleData.pokemonStatsMod[0] = 0;
                switchBattleData.pokemonStatsMod[1] = 0;
                switchBattleData.pokemonStatsMod[2] = 0;
                switchBattleData.pokemonStatsMod[3] = 0;
                switchBattleData.pokemonStatsMod[4] = 0;
                switchBattleData.pokemonStatsMod[5] = 0;
                switchBattleData.pokemonStatsMod[6] = 0;

                //Pokemon Effects
                switchBattleData.focusEnergy= false;
            }
            return true;
        }


        public static void UpdatePokemonStats(int[] pokemonStats, Pokemon pokemon)
        {
            //set PokemonData
            pokemonStats[0] = pokemon.getATK();
            pokemonStats[1] = pokemon.getDEF();
            pokemonStats[2] = pokemon.getSPA();
            pokemonStats[3] = pokemon.getSPD();
            pokemonStats[4] = pokemon.getSPE();
        }
    }
}

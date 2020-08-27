/// Source: Pokémon Unity Redux
/// Purpose: C variable MonoBehaviour activation for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using System.Collections;
using PokemonUnity.Backend.Serializables;
namespace PokemonUnity.Frontend {
public class ActivateOnCVariable : MonoBehaviour
{
    public string cVariable;

    public enum Check
    {
        Equal,
        LessThan,
        GreaterThan
    }
    public bool not = false;
    public Check check;
    public float cNumber;

    public GameObject target;

    void Awake()
    {
        if (target == null)
        {
            target = this.transform.GetChild(0).gameObject;
        }
    }

    void Start()
    {
        CheckActivation();
    }


    private void CheckActivation()
    {
        bool checkResult = false;
        switch(check) {
            case Check.Equal:
                if (SaveData.currentSave.getCVariable(cVariable) == cNumber)
                {
                    checkResult = true;
                }
                break;
            case Check.GreaterThan:
                if (SaveData.currentSave.getCVariable(cVariable) > cNumber)
                {
                    checkResult = true;
                }
                break;
            case Check.LessThan:
                if (SaveData.currentSave.getCVariable(cVariable) < cNumber)
                {
                    checkResult = true;
                }
                break;
        }
        if (not)
        {
            //invert bool
            checkResult = !checkResult;
        }
        target.SetActive(checkResult);
    }
}
}

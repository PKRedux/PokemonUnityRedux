/// Source: Pokémon Unity Redux
/// Purpose: Map name box UI for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace PokemonUnity.Frontend.UI {
public class MapNameBoxHandler : MonoBehaviour
{
    private Transform mapName;
    private Image mapNameBox;
    private Text mapNameText;
    private Text mapNameTextShadow;

    private Coroutine mainDisplay;

    public float speed;
    private float increment;

    void Awake()
    {
        mapName = transform.Find("MapName");
        mapNameBox = mapName.GetComponent<Image>();
        mapNameText = mapName.Find("BoxText").GetComponent<Text>();
        mapNameTextShadow = mapName.Find("BoxTextShadow").GetComponent<Text>();
    }

    void Start()
    {
        mapName.position = new Vector3(0, 0.17f, mapName.position.z);
    }

    public void display(Sprite boxTexture, string name, Color textColor)
    {
        //do not display when on a map of the same name
        if (mapNameText.text != name)
        {
            if (mainDisplay != null)
            {
                StopCoroutine(mainDisplay);
            }
            mainDisplay = StartCoroutine(displayCoroutine(boxTexture, name, textColor));
        }
    }

    private IEnumerator displayCoroutine(Sprite boxTexture, string name, Color textColor)
    {
        if (mapName.position.y != 0.17f)
        {
            increment = mapName.position.y / 0.17f;
            while (increment < 1)
            {
                increment += (1 / speed) * Time.deltaTime;
                if (increment > 1)
                {
                    increment = 1;
                }
                mapName.position = new Vector3(0, 0.17f * increment, mapName.position.z);
                yield return null;
            }
        }
        mapNameBox.sprite = boxTexture;
        mapNameText.text = name;
        mapNameTextShadow.text = name;
        mapNameText.color = textColor;

        increment = 0f;
        while (increment < 1)
        {
            increment += (1 / speed) * Time.deltaTime;
            if (increment > 1)
            {
                increment = 1;
            }
            mapName.position = new Vector3(0, 0.17f - (0.17f * increment), mapName.position.z);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        increment = 0f;
        while (increment < 1)
        {
            increment += (1 / speed) * Time.deltaTime;
            if (increment > 1)
            {
                increment = 1;
            }
            mapName.position = new Vector3(0, 0.17f * increment, mapName.position.z);
            yield return null;
        }
    }
}
}

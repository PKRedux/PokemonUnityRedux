/// Source: Pokémon Unity Redux
/// Purpose: Player's follower Pokémon sprite for Pokémon Unity frontend
/// Author: IIColour_Spectrum
/// Contributors: TeamPopplio
using UnityEngine;
using System.Collections;
using PokemonUnity.Frontend.UI;
using PokemonUnity.Backend.Serializables;
using PokemonUnity.Backend.Databases;
namespace PokemonUnity.Frontend.Overworld {
public class PlayerFollower : CharacterBase
{
    public static PlayerFollower follower;
    private DialogBox Dialog;
    private Player Player;

    private Vector3 startPosition;
    private Vector3 destinationPosition;

    public bool hasLight;
    public Color lightColor;
    public float lightIntensity;

    public bool moving = false;
    public float speed;

    public int pokemonID = 6;
    private int followerIndex = 0;

    private Transform pawnLight;
    public Transform pawnLightReflection;

    private Light followerLight;
    private SpriteRenderer sRenderer;
    private SpriteRenderer sLRenderer;
    private SpriteRenderer sReflectionRenderer;
    private SpriteRenderer sLReflectionRenderer;
    private Sprite[] lightSheet;

    private SpriteRenderer pawnShadow;

    public bool hide;
    public bool canMove = true;
    public Sprite pokeBall;

    // Use this for initialization
    public override void Awake()
    {
        Dialog = GameObject.Find("GUI").GetComponent<DialogBox>();
        Player = Player.player;

        pawn = transform.Find("Pawn");
        pawnLight = transform.Find("PawnLight");
        pawnReflection = transform.Find("PawnReflection");
        pawnLightReflection = transform.Find("PawnLightReflection");

        hitBox = transform.Find("Follower_Transparent");

        sRenderer = pawn.GetComponent<SpriteRenderer>();
        sLRenderer = pawnLight.GetComponent<SpriteRenderer>();
        sReflectionRenderer = pawnReflection.GetComponent<SpriteRenderer>();
        sLReflectionRenderer = pawnLightReflection.GetComponent<SpriteRenderer>();

        pawnShadow = transform.Find("PawnShadow").GetComponent<SpriteRenderer>();

        followerLight = GetComponentInChildren<Light>();
    }

    public override void Start()
    {
        Player = Player.player;
        startPosition = transform.position;

        followerLight.color = lightColor;
        if (hasLight)
        {
            followerLight.intensity = lightIntensity;
        }
        else
        {
            followerLight.intensity = 0;
        }

        switch(direction)
        {
            case Direction.Down:
                transform.Translate(Vector3.back);
                break;
            case Direction.Left:
                transform.Translate(Vector3.left);
                break;
            case Direction.Up:
                transform.Translate(Vector3.forward);
                break;
            case Direction.Right:
                transform.Translate(Vector3.right);
                break;
        }
        transform.position = startPosition;
        changeFollower(followerIndex);
        StartCoroutine("animateSprite");
    }

    public IEnumerator move(Vector3 destination, float sentSpeed)
    {
        if (canMove)
        {
            hide = false;
            followerLight.enabled = true;
            pawnShadow.enabled = true;
            speed = sentSpeed;
            startPosition = transform.position; //add follower's position offset
            destinationPosition = destination;
            Vector3 movement = destinationPosition - startPosition;
            if (Mathf.Round(movement.x) > 0)
            {
                direction = Direction.Right;
            }
            else if (Mathf.Round(movement.x) < 0)
            {
                direction = Direction.Left;
            }
            else if (Mathf.Round(movement.z) > 0)
            {
                direction = Direction.Up;
            }
            else if (Mathf.Round(movement.z) < 0)
            {
                direction = Direction.Down;
            }
            while (Player.increment < 1)
            {
                //because fak trying to use this thing's own increment. shit doesn't work for some reason.
                transform.position = startPosition + (destinationPosition - startPosition) * Player.increment;
                hitBox.position = destinationPosition;
                yield return null;
            }
            transform.position = destinationPosition;
            hitBox.position = destinationPosition;
        }
        else if (hide)
        {
            while (Player.increment < 1)
            {
                transform.position = Player.transform.position;
                hitBox.position = Player.transform.position;
                yield return null;
            }
        }
        else
        {
            startPosition = transform.position;
            while (Player.increment < 1)
            {
                transform.position = startPosition;
                hitBox.position = startPosition;
                yield return null;
            }
        }
    }

    public void hideFollower()
    {
        hide = true;
        transform.position = Player.transform.position;
    }

    public IEnumerator withdrawToBall()
    {
        StopCoroutine("animateSprite");
        canMove = false;
        followerLight.enabled = false;
        sRenderer.sprite = pokeBall;
        sLRenderer.sprite = null;
        sReflectionRenderer.sprite = pokeBall;
        sLReflectionRenderer.sprite = null;
        float increment = 0f;
        float time = 0.4f;
        Vector3 lockedPosition = transform.position;
        while (increment < 1)
        {
            increment += (1 / time) * Time.deltaTime;
            if (increment > 1)
            {
                increment = 1;
            }
            transform.position = lockedPosition;
            yield return null;
        }
        pawnShadow.enabled = false;
        sRenderer.sprite = null;
        sLRenderer.sprite = null;
        sReflectionRenderer.sprite = null;
        sLReflectionRenderer.sprite = null;
        hide = true;
        transform.position = Player.transform.position;
        StartCoroutine("animateSprite");
    }


    public void changeFollower(int index)
    {
        if (followerLight == null)
        {
            followerLight = GetComponentInChildren<Light>();
        }
        followerIndex = index;
        pokemonID = SaveData.currentSave.PC.boxes[0][followerIndex].getID();
        spriteSheet = SaveData.currentSave.PC.boxes[0][followerIndex].GetSprite(false);

        hasLight = PokemonDatabase.getPokemon(pokemonID).hasLight();
        lightIntensity = PokemonDatabase.getPokemon(pokemonID).getLuminance();
        lightColor = PokemonDatabase.getPokemon(pokemonID).getLightColor();
        lightSheet = SaveData.currentSave.PC.boxes[0][followerIndex].GetSprite(true);

        followerLight.color = lightColor;
        followerLight.intensity = lightIntensity;
    }

    public void reflect(bool setState)
    {
        //Debug.Log ("F Reflect");
        sReflectionRenderer.enabled = setState;
        sLReflectionRenderer.enabled = setState;
    }

    public override IEnumerator animateSprite()
    {
        int frame = 0;
        while (true)
        {
            for (int i = 0; i < 6; i++)
            {
                if (!hide)
                {
                    sRenderer.sprite = spriteSheet[(int)direction * 2 + frame];
                    sLRenderer.sprite = lightSheet[(int)direction * 2 + frame];
                    pawnShadow.enabled = true;
                }
                else
                {
                    sRenderer.sprite = null;
                    sLRenderer.sprite = null;
                    pawnShadow.enabled = false;
                }
                sReflectionRenderer.sprite = sRenderer.sprite;
                sLReflectionRenderer.sprite = sLRenderer.sprite;
                if (i > 2)
                {
                    pawn.localPosition = new Vector3(0, 0.17f, -0.36f);
                    pawnLight.localPosition = new Vector3(0, 0.171f, -0.36f);
                }
                else
                {
                    pawn.localPosition = new Vector3(0, 0.2f, -0.305f);
                    pawnLight.localPosition = new Vector3(0, 0.201f, -0.305f);
                }
                yield return new WaitForSeconds(0.055f);
            }
            frame = (frame == 0) ? 1 : 0;
        }
    }

    public IEnumerator interact()
    {
        if (!hide)
        {
            if (Player.setCheckBusyWith(this.gameObject))
            {
                //calculate Player's position relative to target object's and set direction accordingly. (Face the player)
                float xDistance = this.transform.position.x - Player.gameObject.transform.position.x;
                float zDistance = this.transform.position.z - Player.gameObject.transform.position.z;
                if (xDistance >= Mathf.Abs(zDistance))
                {
                    //Mathf.Abs() converts zDistance to a positive always.
                    direction = Direction.Left; //this allows for better accuracy when checking orientation.
                }
                else if (xDistance <= Mathf.Abs(zDistance) * -1)
                {
                    direction = Direction.Right;
                }
                else if (zDistance >= Mathf.Abs(xDistance))
                {
                    direction = Direction.Down;
                }
                else
                {
                    direction = Direction.Up;
                }

                Dialog.DrawDialogBox();
                yield return
                    Dialog.StartCoroutine(Dialog.DrawText(
                        SaveData.currentSave.PC.boxes[0][followerIndex].getName() +
                        " is enjoying walking around \\out of their ball."));
                while (!Input.GetButtonDown("Select") && !Input.GetButtonDown("Back"))
                {
                    yield return null;
                }
                Dialog.UnDrawDialogBox();
                yield return new WaitForSeconds(0.2f);
                Player.unsetCheckBusyWith(this.gameObject);
            }
        }
    }
    public void resetFollower()
    {
        for (int i = 0; i < 6; i++)
        {
            if (SaveData.currentSave.PC.boxes[0][i] != null)
            {
                if (SaveData.currentSave.PC.boxes[0][i].getStatus() != Pokemon.Status.FAINTED)
                {
                    changeFollower(i);
                    break;
                }
            }
        }
    }
}
}

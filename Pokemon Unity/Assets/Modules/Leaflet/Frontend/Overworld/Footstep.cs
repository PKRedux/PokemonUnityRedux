/// Source: Leaflet
/// Purpose: Footstep sound object for Leaflet
/// Author: TeamPopplio
/// Contributors: TeamPopplio
using UnityEngine;
using PokemonUnity.Frontend.Global;
namespace Leaflet.Frontend.Overworld {
/// <summary>
/// Footstep sound class - MonoBehaviour
/// </summary>
public class Footstep : MonoBehaviour
{
    private AudioSource walkSound;
    public AudioClip walkClip;
    public bool stepOnAnyObject = false;

    void Awake()
    {
        walkSound = transform.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("_Object") || other.name.Contains("_Transparent"))
        {
            if (other.transform.parent.name == "Player" || stepOnAnyObject)
            {
                SfxHandler.Play(walkClip, Random.Range(0.85f, 1.1f));
            }
        }
    }
}
}
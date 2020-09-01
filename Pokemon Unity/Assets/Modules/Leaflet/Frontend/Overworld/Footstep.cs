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
    public Color gizmoColor;

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

    private void OnDrawGizmos(){
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position+new Vector3(0,0.5f,0), new Vector3(0.4f, 1, 0.4f));
    }
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages sound files so that other classes/objects can call this class and its methods to play sounds
//this is intended to make playing sounds easier and cleaner
//For sounds heard anywhere. If you want audio to have spacial settings apply a AudioSource and clip to that gameobject
public class AudioManager : MonoBehaviour {
    /*
     * HOW TO ADD NEW SOUNDS
     * 1. Upload new audio file to unity and PUT IT IN THE RESOURCES FOLDER (IMPORTANT!!!)
     * 2. Make a new public static AudioClip variable and name it something. Delcare it above this message next to the other variables
     * 3. Assign the new variable in Start with Resources.Load<AudioClip>(fileName) 
     * 4. In the PlaySound method add a new case to the switch statement. Make the case a string the same as the file name
     * 5. Apply any audio changes (volume, pitch, loop, etc.) you want. Apply changes to audioSrc variable not the AudioClip itself
     * 6. using audioSrc call the PlayOneShot method and pass the variable for the sound you want to play
     * 
     * HOW TO PLAY SOUND IN OTHER SCRIPTS
     * 1. Follow the steps above to add a new sound
     * 2. Find the method or place you want to play the new sound in code (ex: Lighting campfire -> Campfire's LightFire method)
     * 3. Put AudioManager.PlaySound(file name) in the spot, where file name is a string of the file name
     */

    //declare audio variables and audiosource
    public static AudioClip breakIce;
    public static AudioClip igniteFire;
    public static AudioClip iceIndicator;
    public static AudioClip fallingIce;
    public static AudioClip snuffFire;
    public static AudioClip droplet;
    public static AudioClip tumbleWind;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start() {
        //assign audio clip variables audio files found in the Resources folder
        breakIce = Resources.Load<AudioClip>("breakingIce");
        igniteFire = Resources.Load<AudioClip>("ignite");
        iceIndicator = Resources.Load<AudioClip>("iceIndicator");
        fallingIce = Resources.Load<AudioClip>("IceFall");
        snuffFire = Resources.Load<AudioClip>("extinguish");
        droplet = Resources.Load<AudioClip>("droplet");
        tumbleWind = Resources.Load<AudioClip>("wind");

        //get the audiosource we're playing from 
        audioSrc = GetComponent<AudioSource>();
    }

    //plays audio based on passed variable. Called by other scripts
    public static void PlaySound(string clipName) {
        //switch case for which audio is bing played
        switch (clipName) {
            case "breakingIce":
                audioSrc.volume = 0.5f;
                audioSrc.PlayOneShot(breakIce);
                break;
            case "ignite":
                audioSrc.volume = 0.5f;
                audioSrc.PlayOneShot(igniteFire);
                break;
            case "iceIndicator":
                audioSrc.PlayOneShot(iceIndicator);
                break;
            case "IceFall":
                audioSrc.PlayOneShot(fallingIce);
                break;
            case "snuffFire":
                audioSrc.PlayOneShot(snuffFire);
                break;
            case "droplet":
                audioSrc.spatialBlend = 1;
                audioSrc.PlayOneShot(droplet);
                audioSrc.spatialBlend = 0;
                break;
            case "wind":
                audioSrc.PlayOneShot(tumbleWind);
                break;
            default:
                //do nothing
                break;
        }
    }
}
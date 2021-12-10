using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrippingBehavior : MonoBehaviour
{

    CampfireTracker campfireTracker;
    [SerializeField] int firesRequiredStg1;
    [SerializeField] int firesRequiredStg2;

    float timePassed;
    [SerializeField] float dripFrequencyStg1;
    [SerializeField] float dripFrequencyStg2;
    [SerializeField] GameObject prefab;

    [SerializeField] float zRotation;

    GameObject tempDrop;

    // Start is called before the first frame update
    void Start()
    {
        // will be used to get the number of fires lit in the level
        campfireTracker = GameObject.Find("ScriptHolder").GetComponent<CampfireTracker>();
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // if the number of fires lit is at least the value of stg1 and under the value of stg2
        if (campfireTracker.getNumfiresLit() >= firesRequiredStg1 && campfireTracker.getNumfiresLit() < firesRequiredStg2)
        {
            //unmute the continuous sound
            GetComponent<AudioSource>().mute = false;
            
            timePassed += Time.deltaTime; // track time
            if (timePassed > dripFrequencyStg1) // if enough time has passed
            {
                timePassed = 0; // reset timer
                tempDrop = Instantiate(prefab, transform.position, transform.rotation); // create water drop
                tempDrop.transform.Rotate(0f, 0f, zRotation);
                // set the melting point of the drop
                tempDrop.GetComponent<DropBehavior>().meltingPoint = firesRequiredStg1;
                
            }
        }
        else if (campfireTracker.getNumfiresLit() >= firesRequiredStg2) // same thing but for stage 2
        {
            timePassed += Time.deltaTime;
            if (timePassed > dripFrequencyStg2)
            {
                timePassed = 0;
                tempDrop = Instantiate(prefab, transform.position, transform.rotation);
                tempDrop.transform.Rotate(0f, 0f, zRotation);
                // set the melting point of the drop 
                tempDrop.GetComponent<DropBehavior>().meltingPoint = firesRequiredStg1;
            }
        }

    }
}

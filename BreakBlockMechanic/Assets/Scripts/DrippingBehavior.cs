using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrippingBehavior : MonoBehaviour
{

    CampfireTracker campfireTracker;
    [SerializeField] int firesRequired;
    float timePassed;
    [SerializeField] float dripFrequency;
    // Start is called before the first frame update
    void Start()
    {
        campfireTracker = GameObject.Find("ScriptHolder").GetComponent<CampfireTracker>();
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(campfireTracker.getNumfiresLit() >= firesRequired)
        {
            timePassed += Time.deltaTime;
            if(timePassed > dripFrequency)
            {
                timePassed = 0;
                Debug.Log("Drip Drip Drip Bitch");
            }
        }
        
    }
}

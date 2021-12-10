using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFirstFire : MonoBehaviour
{
    bool firstFireLit;
    // Start is called before the first frame update
    void Start()
    {
        firstFireLit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool isFirstFireLit()
    {
        return firstFireLit;
    }
   

    public void lightFirstFire(Campfire firstFire)
    {
        if (!firstFireLit)
        {
            firstFire.lightFire();
            firstFireLit = true;
        }
    }
}

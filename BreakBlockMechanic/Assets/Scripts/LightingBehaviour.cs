using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class NewBehaviourScript : MonoBehaviour {

    Light2D lightEngine;
    // Start is called before the first frame update
    void Start() {
        lightEngine = gameObject.GetComponent<Light2D>();
    }



}

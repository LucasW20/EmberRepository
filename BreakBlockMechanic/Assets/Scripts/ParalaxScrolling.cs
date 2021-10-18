using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxScrolling : MonoBehaviour {

    private float lastX;
    public float paralaxSpeed;


    
    // Start is called before the first frame update
    void Start() {
        lastX = Camera.main.transform.position.x;
    }

    // Update is called once per frame
    void Update() {
        float deltaX = Camera.main.transform.position.x - lastX;

        transform.position += Vector3.right * (deltaX * paralaxSpeed);

        lastX = Camera.main.transform.position.x;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxScrolling : MonoBehaviour {

    private float lastX;
    private float lastY;
    public float paralaxSpeed;
    
    // Start is called before the first frame update
    void Start() {
        lastX = Camera.main.transform.position.x;
        lastY = Camera.main.transform.position.y;
    }

    // Update is called once per frame
    void Update() {
        float deltaX = Camera.main.transform.position.x - lastX;
        float deltaY = Camera.main.transform.position.y - lastY;

        transform.position += Vector3.right * (deltaX * paralaxSpeed);
        transform.position += Vector3.up * (deltaY * paralaxSpeed);


        lastX = Camera.main.transform.position.x;
        lastY = Camera.main.transform.position.y;
        
    }
}

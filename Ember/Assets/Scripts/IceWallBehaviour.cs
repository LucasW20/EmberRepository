using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Manages the behaviour of the ice wall to be broken by frozen blocks
 * 
 * @author Lucas_C_Wright
 * @start 11-07-2021
 * @version 11-07-2021
 */
public class IceWallBehaviour : MonoBehaviour {

    public bool special = false;
    NotificationManager ntManager;

    // Start is called before the first frame update
    void Start() {
        ntManager = GameObject.Find("MainUICanvas").GetComponent<NotificationManager>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void DestroyHandler(GameObject colObj) {
        if (!special) {
            //diable the breaker object
            gameObject.SetActive(false);

            //play breaking sound
            AudioManager.PlaySound("breakingIce");
        } else if (special) {
            if (colObj.CompareTag("WindProjectile1")) {
                ntManager.SetNewNotification("Hard Ice cannot be destroyed by wind!");
            } else if (colObj.CompareTag("Drip")) {
                gameObject.SetActive(false);

                //play breaking sound
                AudioManager.PlaySound("breakingIce");
            }
        }
    }
}

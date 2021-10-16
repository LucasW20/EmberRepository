using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxPlHealth = 25;
    private float timeMultiplier = 1;
    private float timePassed = 25f;
    private bool isAlive = false;
    public Campfire lastFire;

    GameObject mainCamera;
    private FollowPlayer followPlayer;
    private Vector3 mapCenter;

    [SerializeField] HealthBar healthBar;

    bool touchingCampfire = false;

    void Start()
    {
        healthBar.setMaxHealth(maxPlHealth);
        healthBar.setHealth(timePassed);

        mainCamera = GameObject.Find("Main Camera"); 
        followPlayer = mainCamera.GetComponent<FollowPlayer>();
        mapCenter = mainCamera.transform.position; // save the center of the map as the initial position of the camera
        // the main camera should always be centered on the map to start each level, theoretically
        
    }

    void Update()
    {
        if (isAlive)
        {
            updateTime();

            if (timePassed >= maxPlHealth)
            {
                deathEffect();
            }
        }
        else
        {

        }

        if(touchingCampfire) // if the player is touching a campfire
        {
            // reset the time on health
            GameObject.Find("Ember").GetComponent<PlayerHealth>().resetTime();
        }

    }

    private void updateTime() 
    {
        if (timePassed < maxPlHealth)
        {
            timePassed += Time.deltaTime * timeMultiplier;
            healthBar.setHealth(maxPlHealth - timePassed);
        }
    }

    public void deathEffect()
    {
        Debug.Log("Times up!");
        isAlive = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().Sleep();

        //Tells the camera to stop following the player
        followPlayer.setTrackPlayer(false);

        followPlayer.setGoToCenter(true); // tells camera to go to center
    }

    public void revive(Vector2 location)
    {
        Debug.Log("Revived");
        transform.position = location;
        timePassed = 0f;
        isAlive = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().WakeUp();
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 25);

        
        followPlayer.setGoToCenter(false); // tells camera to stop going to center
        followPlayer.setTrackPlayer(true); //Tells the camera to follow the player again
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Breaker")) { 
            Debug.Log("Touching Ground!");
            timeMultiplier = 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CampFire")) // if player is touching a campfire
        {
            touchingCampfire = true; // track it
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Breaker")) {
            Debug.Log("No Longer Touching Ground!");
            timeMultiplier = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CampFire"))
        {
            touchingCampfire = false;
        }
    }

    public void resetTime() {
        timePassed = 0f;
    }

    // returns the center position of the map
    public Vector3 getMapCenter()
    {
        return mapCenter;
    }
}

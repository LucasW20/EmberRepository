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

        GetComponent<PlayerLives>().loseLives(1); // lose one life
    }

    public void revive(Vector2 location)
    {
        Debug.Log("Revived");
        transform.position = location;
        timePassed = 0f;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().WakeUp();
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 25);

        
        followPlayer.setGoToCenter(false); // tells camera to stop going to center
        followPlayer.setTrackPlayer(true); //Tells the camera to follow the player again

        if (isAlive)
        {
            GetComponent<PlayerLives>().loseLives(1); // lose one life
        }

        isAlive = true;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Breaker") || col.gameObject.CompareTag("Drip")) { 
            Debug.Log("Touching Ground!");
            timeMultiplier = 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CampFire")) // if player is touching a campfire
        {
            touchingCampfire = true; // track it
            StartCoroutine(ViewMapCoroutine(7));
            
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Breaker") || col.gameObject.CompareTag("Drip")) {
            Debug.Log("No Longer Touching Ground!");
            timeMultiplier = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CampFire"))
        {
            touchingCampfire = false;
            followPlayer.setTrackPlayer(true); // follow player
            followPlayer.setGoToCenter(false); // and stop going to center
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

     public IEnumerator ViewMapCoroutine(float seconds)
    {
        //yield on a new YieldInstruction that waits for 0.14 seconds before executing what is below
        yield return new WaitForSeconds(seconds);

        //Collider is disabled to start so it doesn't delete itself when it flies through the wall. 
        //It is enabled once the 0.14 seconds pass
        if (touchingCampfire)
        {
            Debug.Log("Zoom Out Camera");
            followPlayer.setTrackPlayer(false); // stop following player
            followPlayer.setGoToCenter(true); // and go to center
        }
    }
}

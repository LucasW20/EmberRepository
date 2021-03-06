using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int maxPlHealth = 25;
    private float timePassed = 25f;
    private bool isAlive = false;
    public Campfire lastFire;

    GameObject mainCamera;
    private FollowPlayer followPlayer;
    private Vector3 mapCenter;

    [SerializeField] HealthBar healthBar; // The health bar UI

    bool touchingCampfire = false;
    bool touchingWall = false;
    private bool freeze = false;
    private bool shieldEnabled = false;
    private bool canTakeDamage = true;

    int frostResist = 0;

    [SerializeField] ParticleSystem partSys;
    [SerializeField] ParticleSystem partSysTrail;

    void Start()
    {
        healthBar.setMaxHealth(maxPlHealth); // sets the Health Bar UI elements Max health
        healthBar.setHealth(timePassed); // sets the Health Bar UI elements current health

        mainCamera = GameObject.Find("Main Camera");
        followPlayer = mainCamera.GetComponent<FollowPlayer>();
        mapCenter = mainCamera.transform.position; // save the center of the map as the initial position of the camera
        // the main camera should always be centered on the map to start each level, theoretically

        frostResist = GameObject.Find("SaveObject").GetComponent<PassingScene>().getFrostResist();

    }

    void Update()
    {
        if (isAlive && !freeze)
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

        if (touchingCampfire) // if the player is touching a campfire
        {
            // reset the time on health
            GameObject.Find("Ember").GetComponent<PlayerHealth>().resetTime();
        }
        if (Input.GetKeyDown("z"))
        {
            if (touchingCampfire && !followPlayer.getGoToCenter())
            {
                Debug.Log("Zoom Out Camera");
                viewWholeMap();
            }
            else if (followPlayer.getGoToCenter())
            {
                followPlayer.setTrackPlayer(true); // stop following player
                followPlayer.setGoToCenter(false); // and go to center
            }
        }

    }

    // updates the amount of health the player has.
    private void updateTime()
    {
        if (timePassed < maxPlHealth)
        {
            timePassed += Time.deltaTime;
            healthBar.setHealth(maxPlHealth - timePassed);
        }
    }

    // handles all the effects of the player death
    // sets isAlive bool to false, disables ember's sprite visual, collider, and rigidbody,
    // tells camera to view entire map, and player loses one life.
    public void deathEffect()
    {
        if (!shieldEnabled)
        {
            Debug.Log("Times up!");
            isAlive = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Light2D>().enabled = false;
            GetComponent<Rigidbody2D>().Sleep();

            partSys.Clear();
            ParticleSystem.EmissionModule em = partSys.emission;
            em.enabled = false;

            partSysTrail.Clear();
            ParticleSystem.EmissionModule emT = partSysTrail.emission;
            emT.enabled = false;

            //Tells the camera to stop following the player
            viewWholeMap();

            GetComponent<PlayerLives>().loseLives(1); // lose one life
        }
    }

    // handles all the effects when a player revieves
    // teleports player to passed location, sets player health to max,
    // enables ember's sprite, collider, and rigibody, Shoots player slightly upwards from the fire,
    // tell camera to track the player again. If the player was already alive, lose one life, and lastly
    // set isAlive bool to true
    public void revive(Vector2 location)
    {
        Debug.Log("Revived");
        transform.position = location;
        timePassed = 0f;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<Light2D>().enabled = true;
        GetComponent<Rigidbody2D>().WakeUp();
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 25);

        partSys.Play();
        ParticleSystem.EmissionModule em = partSys.emission;
        em.enabled = true;

        partSysTrail.Play();
        ParticleSystem.EmissionModule emT = partSysTrail.emission;
        emT.enabled = true;

        trackPlayer();

        if (isAlive)
        {
            GetComponent<PlayerLives>().loseLives(1); // lose one life
        }

        isAlive = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // if the player collides with a wall or a break block
        if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Breaker"))
        {
            Debug.Log("Touching Ground!");
            touchingWall = true; // record the touching
            StartCoroutine(LoseHealthCoroutine(5 - frostResist, 1)); // lose 5 - frostResist health every 1 second
            Debug.Log("Frost Resistance is: " + frostResist);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        // if the player stops touching a wall or a break block
        if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Breaker"))
        {
            Debug.Log("No Longer Touching Ground!");
            touchingWall = false; // record that they are no longer touching
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CampFire")) // if player is touching a campfire
        {
            touchingCampfire = true; // record it
        }

        if (collision.gameObject.CompareTag("Spray"))
        {
            StartCoroutine(LoseHealthCoroutine(2, 1));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CampFire")) // if player stops touching a campfire
        {
            touchingCampfire = false; // record the change
            trackPlayer(); // tell camera to track player
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }
    }

    // function to reset the player's time (life)
    public void resetTime()
    {
        timePassed = 0f;
    }

    // returns the center position of the map
    public Vector3 getMapCenter()
    {
        return mapCenter;
    }

    public bool isEmberAlive() { return isAlive; }

    // tells the camera to zoom out and view the entire map
    public void viewWholeMap()
    {
        Debug.Log("Zoom Out Camera");
        followPlayer.setTrackPlayer(false); // stop following player
        followPlayer.setGoToCenter(true); // and go to center 
    }

    // tells the camera to track the player
    public void trackPlayer()
    {
        followPlayer.setTrackPlayer(true);
        followPlayer.setGoToCenter(false);
    }

    // handles health loss if the player is touching a wall. lose damage health every waitTime seconds
    public IEnumerator LoseHealthCoroutine(float damage, float waitTime)
    {
        if (!shieldEnabled && canTakeDamage)
        {
            StartCoroutine(canTakeDamageCoroutine());
            timePassed += damage;

            //yield on a new YieldInstruction that waits for 0.14 seconds before executing what is below
            yield return new WaitForSeconds(waitTime);

            //Collider is disabled to start so it doesn't delete itself when it flies through the wall. 
            //It is enabled once the 0.14 seconds pass
            if (touchingWall)
            {
                StartCoroutine(LoseHealthCoroutine(3, 1));
            }
        }
    }

    public IEnumerator canTakeDamageCoroutine()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
    }

    public void FreezeHealth() { freeze = true; }
    public void UnfreezeHealth() { freeze = false; }
    public void toggleShield(bool tf) { shieldEnabled = tf; }

    // getter for frost resist
    public int getFrostResist()
    {
        return frostResist;
    }

    // adjuster for frost resist
    public void adjustFrostReist(int resistChange)
    {
        frostResist += resistChange;
    }
    // setter for frost resist
    public void setFrostResist(int nResist)
    {
        frostResist = nResist;
    }

    public float getHealth() { return timePassed; }
}

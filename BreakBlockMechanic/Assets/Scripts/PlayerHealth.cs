using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxPlHealth = 25;
    private float timeMultiplier = 1;
    private float timePassed = 25f;
    private bool isAlive = false;
    Vector3 campPos;
    public Campfire lastFire;

    [SerializeField] HealthBar healthBar;

    void Start()
    {
        healthBar.setMaxHealth(maxPlHealth);
        healthBar.setHealth(timePassed);
        campPos = GameObject.Find("CampBotLeft").transform.position;
        //lastFire = GameObject.Find("CampBotLeft");
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

        //need a variable here so that we can change the z coord for the map. It has to be -10 so that it isn't covered
        //by other things.
        campPos = lastFire.transform.position;
        campPos.z = -10;

        //set the camera to the last fire lit
        GameObject.Find("Main Camera").transform.position = campPos;
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

        //set the respawn location for the camera and change the z coord to -10 so it doesn't get covered
        Vector3 temp = location;
        temp.z = -10;

        GameObject.Find("Main Camera").transform.position = temp;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Breaker")) { 
            Debug.Log("Touching Ground!");
            timeMultiplier = 5;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Breaker")) {
            Debug.Log("No Longer Touching Ground!");
            timeMultiplier = 1;
        }
    }

    public void resetTime() {
        timePassed = 0f;
    }

    // this is a comment to see how github works
}

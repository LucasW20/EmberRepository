using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShieldBehavior : MonoBehaviour
{
    private float durationOfShield = 2;
    PassingScene passingScene;
    GameObject shieldVisual;
    PlayerPoints playerPoints;
    GameObject ember;
    private int uses = 4;
    private bool isActive = false;
    private int requiredPoints = 12;
    // Start is called before the first frame update
    void Start()
    {
        ember = GameObject.Find("Ember");
        passingScene = GameObject.Find("SaveObject").GetComponent<PassingScene>();
        durationOfShield = passingScene.getShieldDuration();
        shieldVisual = GameObject.Find("BubbleShieldVisual");
        playerPoints = ember.GetComponent<PlayerPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s") && uses > 0 && !isActive && playerPoints.getTotalPoints() >= requiredPoints)
        {
            StartCoroutine(startShieldCoroutine());
            GameObject.Find("ShieldCounter").GetComponent<ShieldCounter>().useShield();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }
    }

    public IEnumerator startShieldCoroutine()
    {
        // toggles a boolean in playerhealth, stopping health loss from collisions
        isActive = true;
        GetComponent<PlayerHealth>().toggleShield(true);
        shieldVisual.GetComponent<SpriteRenderer>().enabled = true;
        // increase the collider radius
        GetComponent<CircleCollider2D>().radius = 1.5f;
        yield return new WaitForSeconds(durationOfShield);
        // disable shield and set radius back to normal
        GetComponent<PlayerHealth>().toggleShield(false);
        shieldVisual.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().radius = .76f;
        isActive = false;
        uses--;
    }

    public void adjustShieldDuration(float n)
    {
        durationOfShield += n;
    }

    public float getShieldDuration()
    {
        return durationOfShield;
    }
}

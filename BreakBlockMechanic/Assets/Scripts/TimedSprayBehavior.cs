using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSprayBehavior : MonoBehaviour
{
    Collider2D playerCollider;
    BoxCollider2D boxCol;
    AreaEffector2D areaAffector;
    [SerializeField] string direction; // the direction of the spray, accepts values of "up", "down", "left", and "right". case sensitive
    [SerializeField] float sprayHangTime; // how long the full hitbox of the spray remains
    [SerializeField] float timeBetweenSprays; // how long the wait is between sprays

    [SerializeField] float sprayWidth; // width of the sprays
    [SerializeField] float sprayLength; // length of the spray
    [SerializeField] float spraySpeed; // how fast the spray comes out
    float currentLength = 0.01f;

    bool spraying = true;
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GameObject.Find("Ember").GetComponent<Collider2D>();
        areaAffector = GetComponent<AreaEffector2D>();
        boxCol = GetComponent<BoxCollider2D>();
        setDirection(); // update the direction of the spray (based on direction variable)
    }

    // Update is called once per frame
    void Update()
    {
        if (spraying)
        {
            // incriment the theoretical length of the box collider
            currentLength += sprayLength * spraySpeed * Time.deltaTime;
            // change the actual length of the collider
            spray(sprayWidth, currentLength);
            if (currentLength >= sprayLength) // if the length overshoots the intended max length
            {
                spray(sprayWidth, sprayLength); // set the length of collider to the actual max length
                spraying = false; // stop spraying
                StartCoroutine(WaitForSprayCoroutine());
            }
        }
    }
    // handles the spray timing
    public IEnumerator WaitForSprayCoroutine()
    {
        yield return new WaitForSeconds(sprayHangTime); // wait for the hangtime
        currentLength = 0; // reset length 
        boxCol.size = new Vector2(0.01f, 0.01f); // reset length of box collider
        boxCol.offset = new Vector2(0, 0); // reset offset of collider
        yield return new WaitForSeconds(timeBetweenSprays); // wait for timeBetweenSprays seconds
        spraying = true; // start spraying again
    }

    // function that sets the angle the spray shoots the player
    private void setDirection()
    {
        switch (direction)
        {
            case "up":
                areaAffector.forceAngle = 90;
                break;
            case "down":
                areaAffector.forceAngle = 270;
                break;
            case "right":
                areaAffector.forceAngle = 0;
                break;
            case "left":
                areaAffector.forceAngle = 180;
                break;
            default:
                break;

        }
    }

    // function that updates the size of the box collider of the spray
    private void spray(float nSprayWidth, float nSprayLength)
    {
        switch (direction)
        {
            case "up":
                boxCol.size = new Vector2(nSprayWidth, nSprayLength);
                boxCol.offset = new Vector2(0, nSprayLength / 2);
                break;
            case "down":
                boxCol.size = new Vector2(nSprayWidth, nSprayLength);
                boxCol.offset = new Vector2(0, -nSprayLength / 2);
                break;
            case "right":
                boxCol.size = new Vector2(nSprayLength, nSprayWidth);
                boxCol.offset = new Vector2(nSprayLength / 2, 0);
                break;
            case "left":
                boxCol.size = new Vector2(nSprayLength, nSprayWidth);
                boxCol.offset = new Vector2(-nSprayLength / 2, 0);
                break;
            default:
                break;
        }
    }

    public string getDirection()
    {
        return direction;
    }

}

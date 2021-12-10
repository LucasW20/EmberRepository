using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongJump : MonoBehaviour
{
    GameObject ember;
    PassingScene passingScene;
    PlayerPoints playerPoints;
    Vector2 mouseCurr;

    float timePassed = 0; // tracks time
    bool moving = false; // tracks when to move the ember
    int direction = 0; // tracks what direction to move. 1 is up, 2 is right, 3 is left, and 4 is down

    float speed = 40; // velocity of the jump

    int jumpsUsed = 0; // tracks how many jumps the player has used
    int totalJumps = 3;
    int pointsRequired = 1; // how many points are needed before player can jump


    // Start is called before the first frame update
    void Start()
    {
        ember = GameObject.Find("Ember");
        playerPoints = ember.GetComponent<PlayerPoints>();
        passingScene = GameObject.Find("SaveObject").GetComponent<PassingScene>();
        totalJumps = passingScene.getTotalJumps();
        Debug.Log("Total Jumps = " + totalJumps);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("g")) // while 'g' is held down, track where the mouse is
        {
            mouseCurr = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        // if 'g' is released and we have jumps left, and we have enough points
        if (Input.GetKeyUp("g") && jumpsUsed < totalJumps && playerPoints.getTotalPoints() >= pointsRequired) 
        {
            // calculate the direction of the 'jump' based on the ember's location and the mouses position
            Debug.Log("Jump Attempted");
            direction = calculateDirection(transform.position, mouseCurr);
            moving = true; // set moving equal to true (for the if statement below)
            jumpsUsed++; // remove a jump
            
        }
        if(moving)
        {
            timePassed += Time.deltaTime; // track time
            applyMovement(direction); // apply the movement in the calculated direction
            if(timePassed > 1.5) // after 1.5 seconds has passed
            {
                timePassed = 0; // reset time
                moving = false; // stop movement
            }
        }
        
    }

    // if the ember collides with anything, stop all movement and reset the move timer
    private void OnCollisionEnter2D(Collision2D collision)
    {
        timePassed = 0;
        moving = false;
    }

    // applies the movement based on the direction.
    // 1 means up, 2 means right, 3 means left, and 4 means down
    private void applyMovement(int nDirection)
    {
        switch (nDirection)
        {
            case 1:
                // NOTE: this directly changes velocity, meaning the player
                // can not influence the ember with wind while the dash is happening
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
                break;
            case 2:
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
                break;
            case 3:
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
                break;
            case 4:
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
                break;
            default:
                //do nothing
                break;
        }
    }

    // calculates the direction for the jump using the slope of the line between the
    // ember's location and the current mouse position. returns an int.
    //     1
    //     v
    // 3 >(e)< 2
    //     ^
    //     4
    // diagram to show directions above
    private int calculateDirection(Vector2 point1, Vector2 point2)
    {
        float slope = calculateSlope(point1, point2);
        if(mouseIsAbove(point1, point2))
        {
            if(slope >= 1 || slope <= -1 )
            {
                return 1; // up
            }
            if((slope < 1 && slope > 0))
            {
                return 2; // right
            }
            if ((slope > -1 && slope < 0))
            {
                return 3; // left
            }
        }
        else
        {
            if(slope > -1 && slope < 0)
            {
                return 2; // right
            }
            if(slope < 1 && slope > 0)
            {
                return 3; // left
            }
            if(slope >= 1 || slope <= -1)
            {
                return 4; // down
            }
        }
        if(slope == 0)
        {
            if (point1.x > point2.x)
            {
                return 3; // left
            }
            else
            {
                return 2; // right
            }
        }
        return 0;
    }

    // function to determine if the mouse is above or below the ember. Pass it the embers location and the mouses current position
    private bool mouseIsAbove(Vector2 point1, Vector2 point2) 
    {
        // if the 2nd location is above the first, return true
        if(point2.y > point1.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // calculates the slope between two points
    private float calculateSlope(Vector2 point1, Vector2 point2)
    {
        float slope = 0;

        // slope of line between mouse origin and mouse current
        if (point2.x != point1.x)
        {
            slope = ((point2.y - point1.y) / (point2.x - point1.x));
        }

        // if the x values of the two points are equal and the 2nd point is above the first, set the slope to maxint
        else if (point2.y > point1.y)
        {
            slope = int.MaxValue;
        }
        // if the 2nd point is below the first point, set slope to -maxint
        else if (point2.y <= point1.y)
        {
            slope = int.MaxValue * -1;
        }
        // return the slope
        return slope;
    }

    public void adjustTotalJumps(int n){ totalJumps += n; }

    public int getTotalJumps() { return totalJumps; }


}

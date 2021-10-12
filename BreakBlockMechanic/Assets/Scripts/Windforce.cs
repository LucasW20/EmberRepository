using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windforce : MonoBehaviour
{
    // Start is called before the first frame update
    // create variables to store where the mouse was when left mouse button is clicked
    // and where mouse currently is while being dragged
    Vector2 mouseOrigin;
    Vector2 mouseCurrent;
    // the width of the windbox
    float width = 2;

    GameObject windForceM1;
    PolygonCollider2D windForceCollider;
    AreaEffector2D windForceEffector;
    LineRenderer windVectorVisual;

    void Start()
    {
        windForceM1 = GameObject.Find("WindForceM1");
        windForceCollider = windForceM1.GetComponent<PolygonCollider2D>();
        windForceEffector = windForceM1.GetComponent<AreaEffector2D>();
        windVectorVisual = windForceM1.GetComponent<LineRenderer>();

        //The wind force is set to automatically be on layer 0. Ice is set to layer 1.
        //This ignores any collisions that would happen between anything on layer 0 and 1. 
        //The ember, in this, is set to layer 2 so they can still collide.
        //Physics2D.IgnoreLayerCollision(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            mouseCurrent = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // get the current mouse position.
            //"Input.mousePosition" return the mouses x and y position in pixels
            // Camera.main.ScreenToWorldPoint converts that position into units based on what our main camera sees

            mouseOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) {
            // enable polygon collider
            windForceCollider.enabled = true;
            // enable line renderer
            windVectorVisual.enabled = true;
            // enable area effector
            windForceEffector.enabled = true;
            // get mouse position
            mouseCurrent = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // calculate the 4 corner points for the polygon collider with calculatePoints function and store them as vector2's
            Vector2[] pointsList = calculatePoints(mouseOrigin, mouseCurrent, width);
            // set the path of the polygon collider to the Vector2s in the points list array
            windForceCollider.SetPath(0, pointsList);
            // sets the angle from the x axis that force is applied to objects
            windForceEffector.forceAngle = calculateForceAngle(mouseOrigin, mouseCurrent);
            // sets the magnitude of the force relative to the length of the line
            windForceEffector.forceMagnitude = calculateForceMagnitude(mouseOrigin, mouseCurrent, 20);

            Vector3[] lrPos = new Vector3[2];
            // store mouse origin nd mouse current as Vector3s
            lrPos[0] = mouseOrigin;
            lrPos[1] = mouseCurrent;
            // set the 2 positions of the line renderer
            windVectorVisual.SetPositions(lrPos);
        }

        if (Input.GetMouseButtonUp(0)) {
            // disable collider, effecftor, and line renderer
            windForceCollider.enabled = false;
            windVectorVisual.enabled = false;
            windForceEffector.enabled = false;
        }
    }

    /**
     * @brief calcuates mganitude of a vector
     * 
     *  calculates the magnitude of a vector based on the distance
     *  between two given points
     * 
     * @param nPoint1 the first point
     * @param nPoint2 the second point
     * @param weight the value to weight the mag by
     * @return fMag the magnitude of the vector times the weight value
     */
    private float calculateForceMagnitude(Vector2 nPoint1, Vector2 nPoint2, int weight)
    {
        float fMag = 0;

        // mag = sqrt((x2-x1)^2 + (y2 - y1)^2)
        float deltaXsq = Mathf.Pow((nPoint2.x - nPoint1.x), 2);
        float deltaYsq = Mathf.Pow((nPoint2.y - nPoint1.y), 2);

        fMag = Mathf.Sqrt((deltaXsq + deltaYsq));
        fMag *= weight;

        return fMag;
    }

    /**
     * @brief calcuates the angle between given two points
     * 
     *  calculates the slope of the line between two vector2's.
     *  returns the ArcTan(slope) in degrees.
     * 
     * @param point1 the first point
     * @param point2 the second point
     * @return angle the angle
     */
    private float calculateForceAngle(Vector2 point1, Vector2 point2) {
        // set default angle to 90 (maybe bad practice)
        float angle = 90;
        float slope;

        // slope of line between mouse origin and mouse current
        if (point2.x != point1.x) {
            slope = ((point2.y - point1.y) / (point2.x - point1.x));

            // if the line is going to the left or straight up, angle = 0 + angle
            if (point1.x - point2.x <= 0) {
                // calculates angle as ArcTan of slope. Atan returns answer in radians. Multiplies by 180/pi to convert to degrees
                angle = (Mathf.Atan(slope) * 180 / Mathf.PI);
            }

            // if the line is going to the right, angle = 180 + angle
            else {
                // calculates angle as ArcTan of slope. Atan returns answer in radians. Multiplies by 180/pi to convert to degrees
                angle = 180 + Mathf.Atan(slope) * 180 / Mathf.PI;
            }
        }

        else if (point2.y > point1.y) {
            angle = 90;
        }
        else if (point2.y <= point1.y) {
            angle = 270;
        }


        // return the angle
        return angle;
    }

    /** @brief Creates a rectangle of a given width between two given points
     * 
     *  given two points in x/y space, calculates where the four corner points of
     *  a rectangle with a given width should be.
     *  
     *  @param nPoint1 - the first point of the center line of the rectangle
     *  @param nPoint2 - the second point ----
     *  @param nWidth - the width of the rectangle
     *  
     *  @return tPoints - a Vector2 Array containing the four corner points of the rectangle
     */
    private Vector2[] calculatePoints(Vector2 nPoint1, Vector2 nPoint2, float nWidth)
    {
        Vector2[] tPoints = new Vector2[4];
        float xOff;
        float yOff;

        if (nPoint2.x != nPoint1.x)
        {
            // calculates the slope using the formula (y2 - y1) / (x2 - x1)
            float slope = ((nPoint2.y - nPoint1.y) / (nPoint2.x - nPoint1.x));


            xOff = ((nWidth / 2f) * slope / Mathf.Pow(slope * slope + 1, .5f));
            yOff = ((nWidth / 2f) * 1 / Mathf.Pow(slope * slope + 1, .5f));
        }

        // if the x values from both points are equal the line in straight up or down.
        // this means the formula above would be (y2-y1)/0, this catches that.
        else
        {
            xOff = nWidth / 2;
            yOff = 0;
        }

        // using the two calculated offset values, determine the four corner points of the collider
        tPoints[0] = new Vector2(nPoint1.x + xOff, nPoint1.y - yOff);
        tPoints[1] = new Vector2(nPoint1.x - xOff, nPoint1.y + yOff);
        tPoints[2] = new Vector2(nPoint2.x - xOff, nPoint2.y + yOff);
        tPoints[3] = new Vector2(nPoint2.x + xOff, nPoint2.y - yOff);

        // return as an vector2 array because thats what PolygonCollider2D's SetPath function takes as a paramater
        return tPoints;
    }

}

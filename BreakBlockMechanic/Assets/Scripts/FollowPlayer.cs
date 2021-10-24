using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Script that controlls camera movement. Allows the camera to follow the player
 * or go to the center of the map
 */
public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform target; // stores our target (the player)

    // variable to control how smoothly the camera moves
    private float smoothSpeed = 2f;
    Vector3 desiredPosition; // vector3 to track the 'target' (the player)

    Vector3 offset; // TODO, dynamically offset the camera from the player if it to close to edge of map;

    bool trackPlayer = false; // bool to let the camera know if it should follow the player
    bool goToCenter = true; // bool to let the camera know if it should go to center

    [SerializeField] int maxCameraSize; // stores set the max camera size (map size)
    [SerializeField] int minCameraSize; // stores min camera size (15 unless we have a niche reason to change it)

    [SerializeField] float resetSize = 26;

    [SerializeField] Vector3 mapCenter;
    [SerializeField] float mapHeight;
    [SerializeField] float mapWidth;

    float mapTopLimit;
    float mapBottomLimit;
    float mapLeftLimit;
    float mapRightLimit;

    float cameraTopLimit;
    float cameraBottomLimit;
    float cameraLeftLimit;
    float cameraRightLimit;

    const float pixelRatio = 16f / 9f;

    private void Start()
    {
        calculateMapLimits(); // calculate the edges of the map
    }

    // calculates the x and y values of the edges of the map based on the map's center position and the map size
    // map size is stored in [SerializeField] variables which need to be changed for each map
    private void calculateMapLimits()
    {
        mapTopLimit = mapCenter.y + mapHeight / 2;
        mapBottomLimit = mapCenter.y - mapHeight / 2;
        mapLeftLimit = mapCenter.x - mapWidth / 2;
        mapRightLimit = mapCenter.x + mapWidth / 2;
    }

    // calculates the x and y values of the edges of the camera based on the cameras position and orthographic size
    private void calculateCameraLimits()
    {
        // position equals camera's y position + the orthographic size (orthosize = 1/2 of the camera height in worldspace units)
        cameraTopLimit = transform.position.y + (GetComponent<Camera>().orthographicSize);
        cameraBottomLimit = transform.position.y - (GetComponent<Camera>().orthographicSize);
        // same as above, but for width we need to multiply scale by the pixel ratio (16/9)
        cameraLeftLimit = transform.position.x - (GetComponent<Camera>().orthographicSize * pixelRatio);
        cameraRightLimit = transform.position.x + (GetComponent<Camera>().orthographicSize * pixelRatio);
    }

    // called each frame right after update
    private void FixedUpdate()
    {
        calculateCameraLimits(); // find the edges of the camera
        calculateOffset(); // find what the offset should be (so the camera doesn't look over the edge of the map)
        if (trackPlayer) // if the camera should be following the player
        {
            desiredPosition = target.position + offset; // store targets position so we can edit z value (offset variable unused so far)

            goToLocation(desiredPosition, smoothSpeed);
            // Lerp(a, b, t) returns a vector of value (a + (b - a) * t. See comments in goToLocation() below for further explanation
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 15, 1 * Time.deltaTime);
        }
        else
        {
            goToLocation(mapCenter, 1);
            //GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 26, 1 * Time.deltaTime);
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, resetSize, 1 * Time.deltaTime);
        }
    }

    // changes whether or not the player should be tracked by the camera based on the bool paramater
    public void setTrackPlayer(bool tf)
    {
        trackPlayer = tf;
    }

    // changes whether or not the camera should go to the center of the map based on the bool paramater
    public void setGoToCenter(bool tf)
    {
        goToCenter = tf;
    }

    // tells the camera to go the the Vector3 location at a given speed
    private void goToLocation(Vector3 location, float speed)
    {
        location.z = -10; // back the camera up so we can see

        // Lerp(a, b, t) returns a vector of value (a + (b - a) * t;
        // essentially, it sets the position of the camera partially closer to the desired position based on t.
        // So if t is .5 the camera will move halfway towards the desiredposition each frame.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, location, speed * Time.deltaTime);

        // sets the position of the camera to the smoothed position
        transform.position = smoothedPosition;
    }

    // calculates the offset of the camera so that it doesn't look beyond the edge of the map
    private void calculateOffset()
    {
        if (cameraTopLimit > mapTopLimit) // if the top of the camera passes the top of the map
        {
            offset.y = (mapTopLimit - cameraTopLimit) * 9; // set the offset in the y = to the top of the maps y value - the top of the cameras y value;
            // multiply by nine because the camera is still trying to go the player, so it needs some extra offset to overpower its base tracking
        }
        else if (cameraBottomLimit < mapBottomLimit)
        {
            offset.y = (mapBottomLimit - cameraBottomLimit) * 9;
        }
        else
        {
            offset.y = 0;
        }

        if (cameraRightLimit > mapRightLimit)
        {
            offset.x = (mapRightLimit - cameraRightLimit) * 9;
        }
        else if (cameraLeftLimit < mapLeftLimit)
        {
            offset.x = (mapLeftLimit - cameraLeftLimit) * 9;
        }
        else
        {
            offset.x = 0;
        }
    }
}

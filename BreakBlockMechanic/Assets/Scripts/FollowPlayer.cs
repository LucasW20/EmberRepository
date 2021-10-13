using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Script that controlls camera movement. Allows the camera to follow the player
 * or go to the center of the map
 */
public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform target; // stores our target (the player)
    private PlayerHealth playerHealth;

    // variable to control how smoothly the camera moves
    private float smoothSpeed = 2f;
    Vector3 desiredPosition; // vector3 to track the 'target' (the player)

    Vector3 offset; // TODO, dynamically offset the camera from the player if it to close to edge of map;

    bool trackPlayer = false; // bool to let the camera know if it should follow the player
    bool goToCenter = false; // bool to let the camera know if it should go to center

    [SerializeField] int maxCameraSize; // stores set the max camera size (map size)
    [SerializeField] int minCameraSize; // stores min camera size (15 unless we have a niche reason to change it)

    private void Start()
    {
        playerHealth = GameObject.Find("Ember").GetComponent<PlayerHealth>();

    }

    // called each frame right after update
    private void FixedUpdate()
    {
        if (trackPlayer) // if the camera should be following the player
        {
            desiredPosition = target.position + offset; // store targets position so we can edit z value (offset variable unused so far)

            goToLocation(desiredPosition, smoothSpeed);
            // Lerp(a, b, t) returns a vector of value (a + (b - a) * t. See comments in goToLocation() below for further explanation
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 15, 1 * Time.deltaTime);
        }
        else
        {
            goToLocation(playerHealth.getMapCenter(), 1);
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 26, 1 * Time.deltaTime);
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
}

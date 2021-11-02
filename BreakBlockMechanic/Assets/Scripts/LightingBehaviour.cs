using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/***
 * Handles the lighting effects of the ember as the player lights more campfires
 * @author Lucas_C_Wright
 * @start 10-30-2021
 * @version 10-30-2021
 */
public class LightingBehaviour : MonoBehaviour {

    Light2D lightEngine;    //the variable for the light component on the ember

    // Start is called before the first frame update
    void Start() {
        lightEngine = gameObject.GetComponent<Light2D>();

        //set the default values for the ember at the start of the scene
        resetLight();
    }

    public void increaseLighting() { StartCoroutine(ChangingLightCoroutine(lightEngine.intensity + 0.25f, lightEngine.pointLightOuterRadius + 2.5f)); }

    public void decreaseLighting() { StartCoroutine(ChangingLightCoroutine(lightEngine.intensity - 0.25f, lightEngine.pointLightOuterRadius - 2.5f)); }

    private IEnumerator ChangingLightCoroutine(float targetInt, float targetRad) {
        float time = 0;
        float deltaTime = 2.5f;

        float startingInt = lightEngine.intensity;
        float startingRad = lightEngine.pointLightOuterRadius;

        while (time < deltaTime) {
            time += Time.unscaledDeltaTime;

            lightEngine.intensity = Mathf.Lerp(startingInt, targetInt, time / deltaTime);
            lightEngine.pointLightOuterRadius = Mathf.Lerp(startingRad, targetRad, time / deltaTime);

            yield return null;
        }
    }

    //reset the default values for the ember at the start of the scene
    public void resetLight() {
        lightEngine.intensity = 2.5f;
        lightEngine.pointLightOuterRadius = 10f;
    }
}

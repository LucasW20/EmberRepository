using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Handles the mechnics of the waterfall
 * 
 * @author Lucas_C_Wright
 * @start 11-04-2021
 * @version 11-04-2021
 */
public class WaterfallBehaviour : MonoBehaviour {
    [SerializeField] private int meltingPoints = 5;
    private PolygonCollider2D fallCollider;
    private CampfireTracker fireTracker;
    private GameObject ember;
    private bool frozen = true;

    // Start is called before the first frame update
    void Start() {
        ember = GameObject.Find("Ember");
        fallCollider = gameObject.GetComponent<PolygonCollider2D>();
        fireTracker = GameObject.Find("ScriptHolder").GetComponent<CampfireTracker>();
    }

    // Update is called once per frame
    void Update() {
        //if the melting points has been reached then the waterfall melts. if not then freeze
        if (fireTracker.getNumfiresLit() >= meltingPoints && frozen) {
            MeltWaterfall();
        } else if (fireTracker.getNumfiresLit() < meltingPoints && !frozen) {
            FreezeWaterfall();
        }
    }

    private void MeltWaterfall() {
        fallCollider.isTrigger = true;
        frozen = false;


    }

    private void FreezeWaterfall() {
        fallCollider.isTrigger = false;
        frozen = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        StartCoroutine(ember.GetComponent<PlayerHealth>().LoseHealthCoroutine(8, 1));
    }
}

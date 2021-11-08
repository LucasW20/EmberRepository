using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Handles the mechanics of the waterfall
 * 
 * @author Lucas_C_Wright
 * @start 11-04-2021
 * @version 11-7-2021
 */
public class WaterfallBehaviour : MonoBehaviour {
    [SerializeField] private int meltingPoints;
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
            StartCoroutine(MeltWaterfall());
        } else if (fireTracker.getNumfiresLit() < meltingPoints && !frozen) {
            StartCoroutine(FreezeWaterfall());
        }
    }

    private IEnumerator MeltWaterfall() {
        float time = 0;
        float fadeTime = 2f;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer waterfallAni = GameObject.Find("WaterfallAnimation").GetComponent<SpriteRenderer>();

        while (time < fadeTime) {
            time += Time.unscaledDeltaTime;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(1f, 0f, time / fadeTime));
            waterfallAni.color = new Color(waterfallAni.color.r, waterfallAni.color.g, waterfallAni.color.b, Mathf.Lerp(0f, 1f, time / fadeTime));
            yield return null;
        }

        fallCollider.isTrigger = true;
        frozen = false;
    }

    private IEnumerator FreezeWaterfall() {
        float time = 0;
        float fadeTime = 2f;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();

        while (time < fadeTime) {
            time += Time.unscaledDeltaTime;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(0f, 1f, time / fadeTime));
            yield return null;
        }

        fallCollider.isTrigger = false;
        frozen = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        StartCoroutine(ember.GetComponent<PlayerHealth>().LoseHealthCoroutine(5, 1));
        ember.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -20), ForceMode2D.Impulse);
    }

    //private void OnTriggerStay2D(Collider2D collision) {

    //    StartCoroutine(ember.GetComponent<PlayerHealth>().LoseHealthCoroutine(5, 1));
    //    ember.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -20), ForceMode2D.Impulse);
    //}
}

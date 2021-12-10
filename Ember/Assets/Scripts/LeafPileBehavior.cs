using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafPileBehavior : MonoBehaviour
{

    [SerializeField] Sprite burnSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(startBurnCoroutine());
            GameObject.Find("Ember").GetComponent<PlayerHealth>().resetTime();
        }
    }


    private IEnumerator startBurnCoroutine()
    {
        GetComponent<ParticleSystem>().Play();
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = burnSprite;
        yield return new WaitForSeconds(6);
        this.gameObject.SetActive(false);
    }
}

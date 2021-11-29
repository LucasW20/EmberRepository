using UnityEngine;

/***
 * Handles the behaviour if SaveObject is not in the current scene
 * @author Lucas_C_Wright
 * @start 11-17-21
 * @version 11-29-21
 */
public class MissingCatch : MonoBehaviour {

    [SerializeField] public GameObject prefab;
    private GameObject newObj;

    // Start is called before the first frame update
    void Start() {
        if (GameObject.Find("SaveObject") == null) {
            newObj = Instantiate(prefab);
            newObj.name = "SaveObject";
        }
    }
}

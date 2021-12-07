using UnityEngine;

/***
 * Handles the behaviour if SaveObject is not in the current scene
 * @author Lucas_C_Wright
 * @start 11-17-21
 * @version 12-06-21
 */
public class MissingCatch : MonoBehaviour {

    [SerializeField] public GameObject prefab;
    private GameObject newObj;

    // Start is called before the first frame update
    void Start() {
        //when there's no SaveObject (because we loaded later levels w/o initially creating SaveObject)
        if (GameObject.Find("SaveObject") == null) {
            //create a new SaveObject from the prefab
            newObj = Instantiate(prefab);
            newObj.name = "SaveObject";
            newObj.SetActive(true);
        }
    }
}

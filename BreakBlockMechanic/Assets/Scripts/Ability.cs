using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Class for abilities that the ember can have
 * @author Lucas_C_Wright
 * @start 10/13/2021
 * @version 10/13/2021
 */
public class Ability {
    public string abilityName;
    public int pointRequirement;
    private bool unlocked;

    //does nothing. overwrite in subclass
    public void AbilityEffect() { }

    //runs when the player reaches a certain amount of points.
    public void Unlock() {

    }
}

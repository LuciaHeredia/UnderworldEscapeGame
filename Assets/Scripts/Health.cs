using UnityEngine;

/*
* Change Health here +
* in HealthSystem Prefab, add another heart image  +
* in Spacecraft prefab, in CollisionHandler Script, in Hearts field, add another heart image.
*/
public class Health : MonoBehaviour
{
    public static int health = 5;
    public static int numOfHearts = 5;
    public static bool didPlayerWin = false;
}

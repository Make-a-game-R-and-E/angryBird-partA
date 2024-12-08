using UnityEngine;

public class Pig2 : Enemy
{

    public override void OnHit()
    {
        // הרס את החזיר ברגע שהוא נפגע
        Debug.Log("Pig2 exploded!");
        Destroy(gameObject);
    }
    
}
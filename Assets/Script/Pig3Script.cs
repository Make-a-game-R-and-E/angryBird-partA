using UnityEngine;

public class Pig3 : Enemy
{
    public override void OnHit()
    {
        // התנהגות עבור סוג חזיר 3
        Debug.Log("Pig3 exploded!");
        Destroy(gameObject);  // הרס החזיר
    }
    
}
using UnityEngine;

public class Pig1 : Enemy
{
    public override void OnHit()
    {
        // התנהגות עבור סוג חזיר 1
        Debug.Log("Pig1 exploded!");
        Destroy(gameObject);  // הרס החזיר
    }

    
}

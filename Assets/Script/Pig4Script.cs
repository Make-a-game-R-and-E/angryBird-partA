using UnityEngine;

public class Pig4 : Enemy
{
    public override void OnHit()
    {
        // התנהגות עבור סוג חזיר 4
        Debug.Log("Pig4 exploded!");
        Destroy(gameObject);  // הרס החזיר
    }
    
}
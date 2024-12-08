using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual void OnHit()
    {
        // התנהגות כללית: הרס החזיר
        Destroy(gameObject);
    }

}

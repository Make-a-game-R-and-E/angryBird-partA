using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public virtual void OnHit()
    {
        Debug.Log("Enemy exploded!");
        Destroy(gameObject);
    }
}
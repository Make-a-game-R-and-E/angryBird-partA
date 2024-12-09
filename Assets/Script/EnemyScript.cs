using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public void OnHit()
    {
        Debug.Log("Enemy exploded!");
        Destroy(gameObject);

        // Notify scene manager that a pig died
        GameSceneManager.instance.PigDied();
    }
}

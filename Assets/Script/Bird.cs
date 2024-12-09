using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    private float stationaryTime = 0f;
    [SerializeField] private float stationaryThresholdTime = 2f; // How long the bird can remain stationary before dying
    [SerializeField] private float velocityThreshold = 0.1f;     // Consider the bird "stationary" if below this speed

    private bool hasBeenLaunched = false;
    BirdManager birdManager;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        birdManager = FindFirstObjectByType<BirdManager>();

    }

    void Update()
    {
        // Only start counting stationary time after the bird has been launched
        if (!hasBeenLaunched) return;

        // Check if the bird is moving slowly
        if (rb.linearVelocity.magnitude < velocityThreshold)
        {
            stationaryTime += Time.deltaTime;
            if (stationaryTime >= stationaryThresholdTime)
            {
                Destroy(gameObject);
                if (birdManager != null)
                {
                    birdManager.SpawnNextBird();
                }
            }
        }
        else
        {
            // Reset timer if the bird is moving
            stationaryTime = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the bird hits a pig
        if (collision.gameObject.CompareTag("Pig"))
        {
            // Pig dies
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.OnHit();
            }

            // Destroy this bird
            Destroy(gameObject);
            if (birdManager != null)
            {
                birdManager.SpawnNextBird();
            }
        }
    }

    // Call this method from the slingshot/launcher script once the bird is released
    public void OnLaunched()
    {
        hasBeenLaunched = true;
    }
}

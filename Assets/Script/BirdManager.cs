using UnityEngine;

public class BirdManager : MonoBehaviour
{
    [SerializeField] GameObject birdPrefab;  // Assign in inspector
    [SerializeField] Transform spawnPoint;   // Assign in inspector (the slingshot pivot or close to it)
    [SerializeField] int maxBirdCount;
    int currentBirdCount = 0;

    public void SpawnNextBird()
    {
        if (currentBirdCount < maxBirdCount)
        {
            Instantiate(birdPrefab, spawnPoint.position, Quaternion.identity);
            currentBirdCount++;
        }
        else
        {
            WaitForSeconds wait = new WaitForSeconds(.5f); // for a case when you kill the last bird and the last pig together
            // No more birds left
            // If no more birds and still pigs alive -> lose scene
            Debug.Log("Game Over: No more birds left!");
            GameSceneManager.instance.GoToLoseScene();
        }
    }

    void Start()
    {
        SpawnNextBird();
    }
}

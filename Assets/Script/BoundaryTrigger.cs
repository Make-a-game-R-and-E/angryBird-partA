using UnityEngine;

public class BoundaryTrigger : MonoBehaviour
{
    private BirdManager birdManager;

    void Start()
    {
        birdManager = FindObjectOfType<BirdManager>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // בדוק אם הציפור יצאה מהגבול
        if (other.CompareTag("Bird"))
        {
            // השבת את הציפור הנוכחית
            other.gameObject.SetActive(false);

            // בקש מהמנהל להפעיל את הציפור הבאה
            birdManager.SpawnNextBird();
        }
    }
}

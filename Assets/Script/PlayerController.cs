// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     private Rigidbody2D rb;
//     private Vector2 startDragPosition;
//     private Vector2 releasePosition;
//     private bool isDragging = false;

//     public float throwForce = 10f; // עוצמת הזריקה
//     private LineRenderer lineRenderer;

//     private BirdManager birdManager;
//     private bool hasBeenReleased = false; // בדיקה אם הציפור שוחררה


//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         lineRenderer = GetComponent<LineRenderer>();
//         lineRenderer.enabled = false;
//         birdManager = FindObjectOfType<BirdManager>(); // מציאת מנהל הציפורים
//     }

//     void Update()
//     {
//         if (Input.GetMouseButtonDown(0)) // לחיצה התחלתית
//         {
//             isDragging = true;
//             startDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             lineRenderer.enabled = true;
//         }

//         if (isDragging)
//         {
//             Vector2 currentDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             UpdateLineRenderer(startDragPosition, currentDragPosition);
//         }

//         if (Input.GetMouseButtonUp(0) && isDragging && !hasBeenReleased) // שחרור לחיצה
//         {
//             isDragging = false;
//             releasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             Vector2 throwDirection = startDragPosition - releasePosition;
//             rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

//             lineRenderer.enabled = false; // מחיקת הקו לאחר השחרור
//             hasBeenReleased = true; // ציפור זו כבר שוחררה

//         // לאחר שחרור הציפור, בקש מהמנהל להביא את הבאה
//         birdManager.Invoke("SpawnNextBird", 2.0f); // עיכוב של 2 שניות
//         }
//     }

//     private void UpdateLineRenderer(Vector2 start, Vector2 end)
//     {
//         lineRenderer.positionCount = 2; // הקו מורכב מ-2 נקודות
//         lineRenderer.SetPosition(0, start); // נקודת ההתחלה
//         lineRenderer.SetPosition(1, end);   // נקודת הסיום
//     }
// }


// עובד חלק
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startDragPosition;
    private Vector2 releasePosition;
    private bool isDragging = false;

    public float throwForce = 10f; // עוצמת הזריקה
    private LineRenderer lineRenderer;

    private BirdManager birdManager;
    private bool hasBeenReleased = false; // בדיקה אם הציפור שוחררה

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        birdManager = FindObjectOfType<BirdManager>(); // מציאת מנהל הציפורים
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasBeenReleased) // לחיצה התחלתית
        {
            isDragging = true;
            startDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.enabled = true;
        }

        if (isDragging)
        {
            Vector2 currentDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UpdateLineRenderer(startDragPosition, currentDragPosition);
        }

        if (Input.GetMouseButtonUp(0) && isDragging && !hasBeenReleased) // שחרור לחיצה
        {
            isDragging = false;
            releasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 throwDirection = startDragPosition - releasePosition;
            rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

            lineRenderer.enabled = false; // מחיקת הקו לאחר השחרור
            hasBeenReleased = true; // ציפור זו כבר שוחררה
        }
    }

    private void UpdateLineRenderer(Vector2 start, Vector2 end)
    {
        lineRenderer.positionCount = 2; // הקו מורכב מ-2 נקודות
        lineRenderer.SetPosition(0, start); // נקודת ההתחלה
        lineRenderer.SetPosition(1, end);   // נקודת הסיום
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ברגע שהשחקן נוגע במשהו (חזיר, מדרגה וכו')
        if (collision.gameObject.CompareTag("Pig") || collision.gameObject.CompareTag("Steps"))
        {
            birdManager.SpawnNextBird(); // קרא לציפור הבאה
            Destroy(gameObject); // השמד את הציפור הנוכחית
        }
    }
}



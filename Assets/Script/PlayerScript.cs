using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;  // מהירות השחקן
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // לדוגמה, להזיז את השחקן בעזרת כיוון ולטווח מסוים
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // דחיפה של השחקן בכיוון מסוים
            rb.AddForce(new Vector2(10, 5), ForceMode2D.Impulse);
        }
    }

    // פונקציה שתופסת את ההתנגשות עם אובייקטים
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // כאשר השחקן פוגע במדרגה (נניח שיש לה Tag "StairCase")
        if (collision.gameObject.CompareTag("StairCase"))
        {
            // מקבל את ה-Rigidbody2D של המדרגה
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // הפעלת כוח על המדרגה (כדי שהיא תזוז)
                rb.gravityScale = 1; // מתחילים את הגרביטציה על המדרגה
                rb.AddForce(new Vector2(10, 0), ForceMode2D.Impulse); // הוסף כוח כלפי ימינה (אפשר לשנות)
            }
        }
        // בדוק אם השחקן פוגע בחזיר
        if (collision.gameObject.CompareTag("Pig"))
        {
            // אם השחקן פוגע בחזיר, הוא יתפוצץ (יש להרוס את החזיר)
            collision.gameObject.GetComponent<Enemy>().OnHit(); // הפעל את הפונקציה שמוגדרת בחזיר
        }
    }
}

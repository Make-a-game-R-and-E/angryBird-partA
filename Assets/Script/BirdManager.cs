// using System.Collections.Generic;
// using UnityEngine;

// public class BirdManager : MonoBehaviour
// {
//     public List<GameObject> birds; // רשימה של כל הציפורים
//     private int currentBirdIndex = 0;

//     // public void SpawnNextBird()
//     // {
//     //     if (currentBirdIndex < birds.Count)
//     //     {
//     //         birds[currentBirdIndex].SetActive(true); // הפעיל את הציפור הנוכחית
//     //         currentBirdIndex++;
//     //     }
//     //     else
//     //     {
//     //         Debug.Log("Game Over: No more birds left!");
//     //     }
//     // }

// public void SpawnNextBird()
// {
//     if (currentBirdIndex < birds.Count)
//     {
//         birds[currentBirdIndex].SetActive(true); // הפעל את הציפור הנוכחית
//         currentBirdIndex++;
//     }
//     else
//     {
//         Debug.Log("Game Over: No more birds left!");
//     }
// }


//     public void ResetAllBirds()
//     {
//         foreach (var bird in birds)
//         {
//             bird.SetActive(false);
//         }
//         currentBirdIndex = 0;
//         SpawnNextBird();
//     }

//     void Start()
// {
//     for (int i = 1; i < birds.Count; i++)
//     {
//         birds[i].SetActive(false); // כיבוי כל הציפורים מלבד הראשונה
//     }
// }

// }

// עובד 
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public List<GameObject> birds; // רשימה של כל הציפורים
    private int currentBirdIndex = 0;

    public void SpawnNextBird()
    {
        if (currentBirdIndex < birds.Count)
        {
            GameObject nextBird = birds[currentBirdIndex];
            nextBird.SetActive(true); // הפעל את הציפור הבאה
            currentBirdIndex++;
        }
        else
        {
            Debug.Log("Game Over: No more birds left!");
        }
    }

    void Start()
    {
        // כבה את כל הציפורים מלבד הראשונה
        for (int i = 0; i < birds.Count; i++)
        {
            birds[i].SetActive(false);
        }

        // הפעל את הציפור הראשונה
        SpawnNextBird();
    }
}

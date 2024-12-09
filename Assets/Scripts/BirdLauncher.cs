using UnityEngine;

public class BirdLauncher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform pivot;            // The pivot point of the slingshot
    [SerializeField] LineRenderer slingshotLine; // The line representing the slingshot band
    [SerializeField] LineRenderer trajectoryLine; // The line representing the predicted trajectory

    [Header("Configuration")]
    [SerializeField] float maxStretchDistance = 2f;  // Maximum distance the bird can be stretched
    [SerializeField] float launchPower = 5f;          // Multiplier for the initial launch velocity
    [SerializeField] int trajectorySegments = 50;     // Number of points in the trajectory line
    [SerializeField] float trajectoryTimeStep = 0.1f; // Time step used for calculating the trajectory

    [Header("Bird Settings")]
    [SerializeField] float birdSelectionRadius = 0.5f; // Radius around the bird for selection

    [Header("slingshot")]
    [SerializeField] int slingshotLineLength = 2; // The pivot point of the slingshot

    Camera mainCamera;
    Rigidbody2D currentBirdRigidbody;
    bool isDragging = false;
    Vector2 dragVector;
    BirdManager birdManager;

    void Start()
    {
        mainCamera = Camera.main;
        birdManager = FindFirstObjectByType<BirdManager>(); // Available in Unity 2023.1+; otherwise use FindObjectOfType<BirdManager>()
    }

    void Update()
    {
        // Attempt to find a bird if none is currently tracked
        if (currentBirdRigidbody == null)
        {
            FindCurrentBird();
        }

        // If still no bird found, no actions needed this frame
        if (currentBirdRigidbody == null) return;

        HandleInput();
    }

    /// Handles player input for dragging and releasing the bird.
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryBeginDragging();
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            DragBird();
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            ReleaseBird();
        }
    }

    /// Attempts to start dragging the bird if the mouse is close enough.
    private void TryBeginDragging()
    {
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float distanceToBird = Vector2.Distance(mouseWorldPos, currentBirdRigidbody.position);

        if (distanceToBird < birdSelectionRadius)
        {
            isDragging = true;
            currentBirdRigidbody.isKinematic = true; // Disable physics for precise dragging
        }
    }

    /// Drags the bird, updating its position and the visual elements accordingly.
    private void DragBird()
    {
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        dragVector = mouseWorldPos - (Vector2)pivot.position;

        // Limit the stretch distance
        if (dragVector.magnitude > maxStretchDistance)
        {
            dragVector = dragVector.normalized * maxStretchDistance;
        }

        // Move the bird to the dragged position
        currentBirdRigidbody.position = (Vector2)pivot.position + dragVector;

        // Update the slingshot band line
        UpdateSlingshotLine();
        // Update the trajectory line to show predicted path
        UpdateTrajectory();
    }

    /// Releases the bird, re-enabling physics and setting its initial velocity.
    private void ReleaseBird()
    {
        isDragging = false;
        currentBirdRigidbody.isKinematic = false;
        currentBirdRigidbody.gravityScale = 1f; // Ensure gravity is enabled on release

        Vector2 initialVelocity = -dragVector * launchPower;
        currentBirdRigidbody.linearVelocity = initialVelocity;

        // Hide the lines after launch
        slingshotLine.enabled = false;
        trajectoryLine.enabled = false;

        // Notify the bird it has been launched (if such a method exists in the Bird script)
        Bird birdScript = currentBirdRigidbody.GetComponent<Bird>();
        if (birdScript != null)
        {
            birdScript.OnLaunched();
        }
    }

    /// Updates the slingshot line to stretch from the pivot to the bird's current position.
    private void UpdateSlingshotLine()
    {
        slingshotLine.enabled = true;
        slingshotLine.positionCount = slingshotLineLength;
        slingshotLine.SetPosition(0, pivot.position);
        slingshotLine.SetPosition(1, currentBirdRigidbody.position);
    }

    /// Updates the trajectory line based on the current drag vector and launch power.
    private void UpdateTrajectory()
    {
        Vector2 initialVelocity = -dragVector * launchPower;
        DisplayTrajectory((Vector2)pivot.position, initialVelocity);
    }

    /// Displays the predicted trajectory line given a start position and an initial velocity.
    /// This considers the current bird's gravity scale for more accurate prediction.
    private void DisplayTrajectory(Vector2 startPosition, Vector2 initialVelocity)
    {
        trajectoryLine.enabled = true;
        trajectoryLine.positionCount = trajectorySegments;


        for (int i = 0; i < trajectorySegments; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector2 position = startPosition + initialVelocity * t + 0.5f * Physics2D.gravity * t * t;
            trajectoryLine.SetPosition(i, position);
        }
    }

    /// Finds the current bird in the scene by looking for a GameObject with the "Bird" tag.
    private void FindCurrentBird()
    {
        GameObject birdObj = GameObject.FindGameObjectWithTag("Bird");
        if (birdObj != null)
        {
            currentBirdRigidbody = birdObj.GetComponent<Rigidbody2D>();
        }
    }
}

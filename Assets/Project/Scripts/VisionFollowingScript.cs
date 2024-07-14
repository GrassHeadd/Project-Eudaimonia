using UnityEngine;

public class VisionFollower : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float distance = 3.0f;
    private RectTransform myRectTransform;

    private void Awake()
    {
        myRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Continuously update the position of the UI element to always be in front of the camera
        Vector3 targetPosition = FindTargetPosition();
        MoveTowards(targetPosition);

        // Make the UI element face the camera
        myRectTransform.rotation = Quaternion.LookRotation(myRectTransform.position - cameraTransform.position);
    }

    private Vector3 FindTargetPosition()
    {
        // Get a position in front of the camera
        return cameraTransform.position + (cameraTransform.forward * distance);
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        // Move the UI element to the target position smoothly
        myRectTransform.position = Vector3.Lerp(myRectTransform.position, targetPosition, Time.deltaTime * 5f);
    }
}

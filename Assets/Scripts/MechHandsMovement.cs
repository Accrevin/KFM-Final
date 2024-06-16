using UnityEngine;

public class MechHandController : MonoBehaviour
{
    public Transform vrLeftHand;
    public Transform vrRightHand;
    public Transform mechLeftHand;
    public Transform mechRightHand;
    public Vector3 leftHandOffset;
    public Vector3 rightHandOffset;
    public Quaternion leftHandRotationOffset = Quaternion.identity; // Rotation offset for left hand
    public Quaternion rightHandRotationOffset = Quaternion.identity; // Rotation offset for right hand
    public float distanceMultiplier = 1.5f; // Adjust this value as needed
    public float punchForce = 10f; // Adjust the punch force as needed
    public float punchThreshold = 0.1f; // Threshold for detecting a punch

    private Vector3 previousLeftHandPos;
    private Vector3 previousRightHandPos;
    private Rigidbody leftHandRb;
    private Rigidbody rightHandRb;

    void Start()
    {
        // Initialize previous positions
        previousLeftHandPos = vrLeftHand.position;
        previousRightHandPos = vrRightHand.position;

        // Get or add Rigidbody components to the mech hands
        leftHandRb = mechLeftHand.GetComponent<Rigidbody>();
        rightHandRb = mechRightHand.GetComponent<Rigidbody>();

        if (leftHandRb == null)
        {
            leftHandRb = mechLeftHand.gameObject.AddComponent<Rigidbody>();
            leftHandRb.isKinematic = true; // Ensure it's kinematic for manual control
        }

        if (rightHandRb == null)
        {
            rightHandRb = mechRightHand.gameObject.AddComponent<Rigidbody>();
            rightHandRb.isKinematic = true;
        }
    }

    void Update()
    {
        // Update the left hand position and rotation
        UpdateHandPositionAndRotation(vrLeftHand, mechLeftHand, leftHandOffset, leftHandRotationOffset, ref previousLeftHandPos, leftHandRb);

        // Update the right hand position and rotation
        UpdateHandPositionAndRotation(vrRightHand, mechRightHand, rightHandOffset, rightHandRotationOffset, ref previousRightHandPos, rightHandRb);
    }

    void UpdateHandPositionAndRotation(Transform vrHand, Transform mechHand, Vector3 handOffset, Quaternion rotationOffset, ref Vector3 previousHandPos, Rigidbody handRb)
    {
        // Calculate the target position
        Vector3 targetHandPos = vrHand.position + handOffset;

        // Calculate the forward direction vector and apply the distance multiplier
        Vector3 handForward = vrHand.forward * distanceMultiplier;

        // Apply the forward distance multiplier to the target position
        targetHandPos += handForward;

        // Calculate transition speed
        float transitionSpeed = Time.deltaTime * 5f;

        // Smoothly interpolate the MechHand position to the target position
        mechHand.position = Vector3.Lerp(mechHand.position, targetHandPos, transitionSpeed);

        // Apply the rotation offset to the VR hand rotation
        Quaternion targetRotation = vrHand.rotation * rotationOffset;

        // Directly set the rotation of the MechHand to match the adjusted VR hand rotation
        mechHand.rotation = targetRotation;

        // Detect and apply punch force
        Vector3 handVelocity = (vrHand.position - previousHandPos) / Time.deltaTime;
        if (handVelocity.magnitude > punchThreshold)
        {
            handRb.isKinematic = false; // Temporarily make the Rigidbody non-kinematic to apply force
            handRb.AddForce(handVelocity * punchForce, ForceMode.VelocityChange);
            handRb.isKinematic = true; // Reset to kinematic after applying force
        }

        // Update previous hand position
        previousHandPos = vrHand.position;
    }
}

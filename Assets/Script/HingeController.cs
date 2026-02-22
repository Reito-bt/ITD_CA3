using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HingeController : MonoBehaviour
{
    [SerializeField] private GameObject door; // The door GameObject with XRGrabInteractable
    [SerializeField] private int correctKeyID = 1;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable doorGrabInteractable;
    private HingeJoint doorHinge;

    private void Start()
    {
        if (door == null)
        {
            Debug.LogError("HingeController: door is not assigned in Inspector!");
            return;
        }

        if (socket == null)
        {
            Debug.LogError("HingeController: socket is not assigned in Inspector!");
            return;
        }

        doorGrabInteractable = door.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (doorGrabInteractable == null)
        {
            Debug.LogError("HingeController: door does not have an XRGrabInteractable component!");
            return;
        }

        doorHinge = door.GetComponent<HingeJoint>();
        if (doorHinge == null)
        {
            Debug.LogWarning("HingeController: door does not have a HingeJoint component!");
        }

        socket.selectEntered.AddListener(OnKeyInserted);
        
        // Lock the door
        doorGrabInteractable.enabled = false;
        if (doorHinge != null)
        {
            doorHinge.useLimits = true;
            JointLimits limits = doorHinge.limits;
            limits.min = 0;
            limits.max = 0;
            doorHinge.limits = limits;
        }
    }

    private void OnKeyInserted(SelectEnterEventArgs args)
    {
        string keyName = args.interactableObject.transform.name;
        Debug.Log($"Key inserted: '{keyName}' | Expected: 'Key_{correctKeyID}'");
        
        if (keyName == $"Key_{correctKeyID}")
        {
            Debug.Log("Correct key! Unlocking door.");
            UnlockDoor();
        }
        else
        {
            Debug.Log("Wrong key!");
        }
    }

    private void UnlockDoor()
    {
        doorGrabInteractable.enabled = true;
        
        if (doorHinge != null)
        {
            // Unlock the hinge by setting limits to allow rotation
            JointLimits limits = doorHinge.limits;
            limits.min = 0;
            limits.max = 90; // Adjust angle as needed
            doorHinge.limits = limits;
            Debug.Log("Door HingeJoint unlocked!");
        }
    }
}

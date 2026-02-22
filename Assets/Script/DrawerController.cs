using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerController : MonoBehaviour
{
    [SerializeField] private GameObject drawer; // The drawer GameObject with XRGrabInteractable
    [SerializeField] private int correctKeyID = 1;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable drawerGrabInteractable;
    private ConfigurableJoint drawerJoint;

    private void Start()
    {
        if (drawer == null)
        {
            Debug.LogError("DrawerController: drawer is not assigned in Inspector!");
            return;
        }

        if (socket == null)
        {
            Debug.LogError("DrawerController: socket is not assigned in Inspector!");
            return;
        }

        drawerGrabInteractable = drawer.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (drawerGrabInteractable == null)
        {
            Debug.LogError("DrawerController: drawer does not have an XRGrabInteractable component!");
            return;
        }

        drawerJoint = drawer.GetComponent<ConfigurableJoint>();
        if (drawerJoint == null)
        {
            Debug.LogWarning("DrawerController: drawer does not have a ConfigurableJoint component!");
        }

        socket.selectEntered.AddListener(OnKeyInserted);
        
        // Lock the drawer
        drawerGrabInteractable.enabled = false;
        if (drawerJoint != null)
        {
            drawerJoint.xMotion = ConfigurableJointMotion.Locked;
            drawerJoint.yMotion = ConfigurableJointMotion.Locked;
            drawerJoint.zMotion = ConfigurableJointMotion.Locked;
        }
    }

    private void OnKeyInserted(SelectEnterEventArgs args)
    {
        string keyName = args.interactableObject.transform.name;
        Debug.Log($"Key inserted: '{keyName}' | Expected: 'Key_{correctKeyID}'");
        
        if (keyName == $"Key_{correctKeyID}")
        {
            Debug.Log("Correct key! Unlocking drawer.");
            UnlockDrawer();
        }
        else
        {
            Debug.Log("Wrong key!");
        }
    }

    private void UnlockDrawer()
    {
        drawerGrabInteractable.enabled = true;
        
        if (drawerJoint != null)
        {
            // Unlock the axis the drawer slides on
            drawerJoint.zMotion = ConfigurableJointMotion.Limited;
            Debug.Log("Drawer ConfigurableJoint unlocked!");
        }
    }
}

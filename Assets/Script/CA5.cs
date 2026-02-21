using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerController : MonoBehaviour
{
    [SerializeField] private GameObject drawerHandle;
    [SerializeField] private int correctKeyID = 1;

    private void Start()
    {
        if (drawerHandle == null)
        {
            Debug.LogError("DrawerController: drawerHandle is not assigned in Inspector!");
            return;
        }

        var socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
        if (socket == null)
        {
            Debug.LogError("DrawerController: XRSocketInteractor component not found on this GameObject!");
            return;
        }

        socket.selectEntered.AddListener(OnKeyInserted);
        drawerHandle.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = false;
    }

    private void OnKeyInserted(SelectEnterEventArgs args)
    {
        string keyName = args.interactableObject.transform.name;
        if (keyName == $"Key_{correctKeyID}")
        {
            drawerHandle.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = true;
        }
    }
}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class CombineItems : MonoBehaviour
{
    [Header("Assembly Sockets")]
    [SerializeField] private List<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor> weaponPieceSockets = new List<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    
    [Header("Assembled Weapon")]
    [SerializeField] private GameObject assembledWeapon; // The complete weapon object
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable weaponGrabInteractable;
    
    [Header("Pedestal")]
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor pedestalSocket;
    
    [Header("UI")]
    [SerializeField] private GameObject congratsUI;

    private int socketsWithItems = 0;
    private bool isFullyAssembled = false;

    private void Start()
    {
        if (congratsUI != null)
        {
            congratsUI.SetActive(false);
        }

        // Disable the assembled weapon grab at start
        if (weaponGrabInteractable != null)
        {
            weaponGrabInteractable.enabled = false;
        }

        // Subscribe to weapon piece socket events
        foreach (var socket in weaponPieceSockets)
        {
            if (socket != null)
            {
                socket.selectEntered.AddListener(OnPieceAttached);
                socket.selectExited.AddListener(OnPieceRemoved);
            }
        }

        // Subscribe to pedestal socket event
        if (pedestalSocket != null)
        {
            pedestalSocket.selectEntered.AddListener(OnWeaponPlacedOnPedestal);
        }
    }

    private void OnPieceAttached(SelectEnterEventArgs args)
    {
        socketsWithItems++;
        Debug.Log($"Piece attached! Total pieces: {socketsWithItems}/{weaponPieceSockets.Count}");
        CheckIfFullyAssembled();
    }

    private void OnPieceRemoved(SelectExitEventArgs args)
    {
        socketsWithItems--;
        Debug.Log($"Piece removed! Total pieces: {socketsWithItems}/{weaponPieceSockets.Count}");
        
        // Disable grabbing if weapon becomes incomplete
        if (socketsWithItems < weaponPieceSockets.Count)
        {
            isFullyAssembled = false;
            if (weaponGrabInteractable != null)
            {
                weaponGrabInteractable.enabled = false;
            }
        }
    }

    private void CheckIfFullyAssembled()
    {
        if (socketsWithItems >= weaponPieceSockets.Count && !isFullyAssembled)
        {
            isFullyAssembled = true;
            Debug.Log("Weapon fully assembled! You can now grab it and place it on the pedestal.");
            
            // Enable the assembled weapon to be grabbed
            if (weaponGrabInteractable != null)
            {
                weaponGrabInteractable.enabled = true;
            }
        }
    }

    private void OnWeaponPlacedOnPedestal(SelectEnterEventArgs args)
    {
        GameObject placedObject = args.interactableObject.transform.gameObject;
        Debug.Log($"Object placed on pedestal: '{placedObject.name}' - Showing UI!");
        
        // Show UI immediately when anything is placed on pedestal
        if (congratsUI != null)
        {
            congratsUI.SetActive(true);
        }
        else
        {
            Debug.LogError("Congrats UI is not assigned!");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        foreach (var socket in weaponPieceSockets)
        {
            if (socket != null)
            {
                socket.selectEntered.RemoveListener(OnPieceAttached);
                socket.selectExited.RemoveListener(OnPieceRemoved);
            }
        }

        if (pedestalSocket != null)
        {
            pedestalSocket.selectEntered.RemoveListener(OnWeaponPlacedOnPedestal);
        }
    }
}

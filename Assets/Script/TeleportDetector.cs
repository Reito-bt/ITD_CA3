using UnityEngine;

public class TeleportDetector : MonoBehaviour
{
    public CA4 manager;
    private bool hasBeenUsed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenUsed && other.CompareTag("Player"))
        {
            hasBeenUsed = true;
            manager.TeleportUsed();
        }
    }
}

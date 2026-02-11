using UnityEngine;

public class SimpleTrigger : MonoBehaviour
{
    public CA4 manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.NextStep();
        }
    }
}

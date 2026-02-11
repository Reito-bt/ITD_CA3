using UnityEngine;
using UnityEngine.UI;

public class CA4 : MonoBehaviour
{
    public GameObject trigger1;
    public GameObject trigger2;
    public GameObject trigger3;
    public GameObject teleportArea1;
    public GameObject teleportArea2;
    public GameObject congratsUI;
    public Button dismissButton;

    private int step = 0;
    private int teleportsUsed = 0;

    void Start()
    {
        trigger1.SetActive(true);
        trigger2.SetActive(false);
        trigger3.SetActive(false);
        teleportArea1.SetActive(false);
        teleportArea2.SetActive(false);
        congratsUI.SetActive(false);
        dismissButton.onClick.AddListener(() => congratsUI.SetActive(false));
    }

    public void NextStep()
    {
        step++;
        
        if (step == 1)
        {
            trigger1.SetActive(false);
            trigger2.SetActive(true);
        }
        else if (step == 2)
        {
            trigger2.SetActive(false);
            trigger3.SetActive(true);
        }
        else if (step == 3)
        {
            trigger3.SetActive(false);
            teleportArea1.SetActive(true);
            Invoke("EnableTeleport2", 1f);
        }
    }

    void EnableTeleport2()
    {
        teleportArea2.SetActive(true);
    }

    public void TeleportUsed()
    {
        teleportsUsed++;
        
        if (teleportsUsed >= 2)
        {
            congratsUI.SetActive(true);
        }
    }
}

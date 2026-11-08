using UnityEngine;

public class PoliceStunning : MonoBehaviour
{
    [SerializeField] public PoliceManager policeManager;
    [SerializeField] public ArcadeCarController arcadeCarController;
    [SerializeField] public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && policeManager.policeState == PoliceManager.PoliceState.Chase)
        {
            arcadeCarController.GetStunned();
            policeManager.EndChase();
        }
    }
}

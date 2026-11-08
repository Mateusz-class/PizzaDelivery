using UnityEngine;

public class PoliceVision : MonoBehaviour
{
    [SerializeField] public PoliceManager policeManager;
    [SerializeField] public ArcadeCarController arcadeCarController;
    [SerializeField] public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" 
        && arcadeCarController.playerState == ArcadeCarController.PlayerState.Regular 
        && policeManager.policeState == PoliceManager.PoliceState.Patrol
        && gameManager.gameState == GameManager.GameState.Delivery)
        {
            policeManager.ChasePlayer();
        }
    }
}

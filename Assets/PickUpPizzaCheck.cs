using UnityEngine;

public class PickUpPizzaCheck : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.PickUpPizza();
        }
    }
}

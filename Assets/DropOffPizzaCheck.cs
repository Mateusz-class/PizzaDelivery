using UnityEngine;

public class DropOffPizzaCheck : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.DropOffPizza();
        }
    }
}

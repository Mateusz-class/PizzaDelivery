using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        None, Pickup, Delivery, GameOver
    }
    public GameState gameState;
    public List<Transform> DropOffLocations = new List<Transform>();
    public int CurrentIndex;
    public GameObject DropOffArea;
    public GameObject PizzaPickup;
    public float DeliveryTimer, DeliveryTimeLimit;
    public GameObject TimerTextTMP;
    public TMP_Text TimerText;
    public GameObject EndScreenWin, EndScreenLose;
    public DirectionIndicator directionIndicator;

    private void Start()
    {
        gameState = GameState.Pickup;
        DeliveryTimer = 0;
        CurrentIndex = 0;
        ShuffleList(DropOffLocations);
        directionIndicator.target = PizzaPickup.transform;
    }
    void Update()
    {
        if (DeliveryTimer > 0)
        {
            DeliveryTimer = Mathf.Clamp(DeliveryTimer - Time.deltaTime, 0, DeliveryTimeLimit);
            UpdateTimerUI();
            if (DeliveryTimer <= 0 && gameState == GameState.Delivery)
            {
                EndGame(false);
            }
        }
        if (gameState == GameState.GameOver && Keyboard.current.rKey.wasPressedThisFrame)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Playtest quick restart
        if (Keyboard.current.pKey.wasPressedThisFrame)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SetActiveState(GameObject tempObject, bool activeState)
    {
        tempObject.SetActive(activeState);
    }
    public void PickUpPizza()
    {
        gameState = GameState.Delivery;
        DropOffArea.transform.position = DropOffLocations[CurrentIndex].position;
        SetActiveState(DropOffArea, true);
        SetActiveState(PizzaPickup, false);
        SetActiveState(TimerTextTMP, true);
        DeliveryTimer = DeliveryTimeLimit;
        directionIndicator.target = DropOffArea.transform;
    }
    public void DropOffPizza()
    {
        gameState = GameState.Pickup;
        CurrentIndex++;
        if (CurrentIndex > DropOffLocations.Count - 1)
        {
            EndGame(true);
            return;
        }
        SetActiveState(DropOffArea, false);
        SetActiveState(PizzaPickup, true);
        SetActiveState(TimerTextTMP, false);
        directionIndicator.target = PizzaPickup.transform;
        DeliveryTimer = 0;
    }
    public void EndGame(bool Won)
    {
        gameState = GameState.GameOver;
        if (Won)
        {
            SetActiveState(EndScreenWin, true);
            SetActiveState(TimerTextTMP, false);
            DeliveryTimer = 0;
        }
        else
        {
            SetActiveState(EndScreenLose, true);
            SetActiveState(TimerTextTMP, false);
            DeliveryTimer = 0;
        }
    }
    private void UpdateTimerUI()
    {
        TimerText.text = DeliveryTimer.ToString("N0");
        //Learned how to round the timer from here:
        //https://stackoverflow.com/questions/67160505/how-can-i-remove-decimal-at-timer-on-c-sharp
    }
        public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Modified ShuffleList code from:
    //https://medium.com/@occasoftware/how-to-shuffle-a-list-in-unity-267940dba432
    public void ShuffleList(List<Transform> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            Transform temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

}

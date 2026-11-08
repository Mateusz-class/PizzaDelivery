using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
            SceneManager.LoadScene(1);
    }
}

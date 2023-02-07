using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameoverController : MonoBehaviour
{
    void Update()
    {
        //リトライ
        if (Gamepad.current.buttonSouth.isPressed)
        {
            SceneManager.LoadScene("Game");
        }

        //タイトルへ戻る
        if(Gamepad.current.buttonNorth.isPressed)
        {
            SceneManager.LoadScene("Title");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
    void Update()
    {
        //タイトルへ戻る
        if(Gamepad.current.buttonNorth.isPressed)
        {
            SceneManager.LoadScene("Title");
        }
    }
}

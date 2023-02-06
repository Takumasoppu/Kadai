using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameStartController : MonoBehaviour
{
    /// <summary>
    /// タイトル処理
    /// </summary>
    public void Update()
    {
        //Aボタンを押したときにゲームシーンに移動する
        if(Gamepad.current.buttonSouth.isPressed)
        {
            SceneManager.LoadScene("Game");
        }

        //Bボタンを押したときにゲームを終了する
        if(Gamepad.current.buttonEast.isPressed)
        {
            Application.Quit();
        }
    }

}

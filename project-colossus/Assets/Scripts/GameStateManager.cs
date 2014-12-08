using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour{
    public static bool playerWin = false;
    public static bool playerLose = false;
    public Text WinLabel;
    public Text LostLabel;

    public void Update()
    {
        if( playerWin )
        {
            WinLabel.enabled = true;
        }

        if( playerLose )
        {
            LostLabel.enabled = true;
        }

        if( Input.GetKey(KeyCode.Escape) )
        {
            Application.Quit();
        }
    }

    public static void PlayersWin()
    {
        playerWin = true;
    }

    public static void PlayersLose()
    {
        playerLose = true;
    }

}

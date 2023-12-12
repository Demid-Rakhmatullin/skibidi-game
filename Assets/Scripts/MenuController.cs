using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject StartBtn;
    [SerializeField] GameObject RestartBtn;
    [SerializeField] GameObject WinText;

    public void BtnClick_Start()
        => LevelController.Instance.StartGame();

    public void BtnClick_Restart()
        => LevelController.Instance.RestartGame();


    public void ShowRestart()
    {
        StartBtn.SetActive(false);
        RestartBtn.SetActive(true);
    }

    public void ShowStart()
    {
        StartBtn.SetActive(true);
        RestartBtn.SetActive(false);
        WinText.SetActive(false);
    }

    public void ShowWin()
    {
        StartBtn.SetActive(false);
        RestartBtn.SetActive(true);
        WinText.SetActive(true);
    }
}

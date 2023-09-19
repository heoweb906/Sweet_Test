using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuUIControl : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerInformation playerInformation;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerInformation = FindObjectOfType<PlayerInformation>();
        playerInformation.IsMenu = true;
        playerInformation.IsGame = false;
    }

    public void Play() // ���� ���� ���� ���µ� ���� �� ����
    {
        gameManager.bulletCount = 10; // ���� ù ���۽��� �Ѿ�

        SceneManager.LoadScene("Play1"); // "YourSceneName"�� �̵��ϰ��� �ϴ� ���� �̸����� �ٲ��ּ���.
        gameManager.soundManager.Play();
        gameManager.GameStart();
        playerInformation.IsMenu = false;
        playerInformation.IsGame = true;

        gameManager.isReload = false;
        gameManager.ActivateImage(playerInformation.WeponColor);
        gameManager.ActivateHpImage(3);
    }

    public void SceneTurnTiming()
    {
        SceneManager.LoadScene("Timing");
        gameManager.soundTime.Play();
    }




    public void QuitGame() // ���� ���� ��ư Ŭ��(���� ����)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TestButton : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerInformation playerInformation;

    public TMP_Text cruBulletCount;

    public void Awake()
    {
        playerInformation = FindObjectOfType<PlayerInformation>();
    }

    public void Update()
    {
        if(playerInformation.IsGame)
        {
            ShowBulletCount();
        }

       

        if (Input.GetKeyDown(KeyCode.T))
        {
            GoMenu();
        }
    }



    private void ShowBulletCount()
    {
        // #. ���� �� ���� �� ǥ���� ��
        if(gameManager.isReload)
        {
            cruBulletCount.text = "X";
        }
        else
        {
            // #. ���� ���� �ƴ� ���� �ִ� źâ�� ���� ǥ��
            cruBulletCount.text = gameManager.bulletCount.ToString();
        }
    }



    public void GoMenu()
    {
        if (!(playerInformation.IsMenu))  // �޴�ȭ���� �ƴ� ��쿡��
        {
            UnlockCursor(); // Ŀ�� �� ����

            SceneManager.LoadScene("Menu"); // "YourSceneName"�� �̵��ϰ��� �ϴ� ���� �̸����� �ٲ��ּ���.
            gameManager.soundManager.Stop();
            playerInformation.IsMenu = true;
            playerInformation.IsGame = false;
            gameManager.iconOn = false;
        }
    }
    private void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
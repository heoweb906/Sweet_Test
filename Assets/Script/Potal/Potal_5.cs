using UnityEngine.SceneManagement;
using UnityEngine;

public class Potal_5 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // "Player" �±׸� ���� ������Ʈ�� �浹���� ��
        {
            SceneManager.LoadScene("PlayBoss"); // Play �� 2�� 
        }
    }
}
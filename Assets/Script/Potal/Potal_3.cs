using UnityEngine.SceneManagement;
using UnityEngine;

public class Potal_3 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // "Player" �±׸� ���� ������Ʈ�� �浹���� ��
        {
            SceneManager.LoadScene("Play4"); // Play �� 2�� 
        }
    }
}
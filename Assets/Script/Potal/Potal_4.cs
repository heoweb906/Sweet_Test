using UnityEngine.SceneManagement;
using UnityEngine;

public class Potal_4 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // "Player" �±׸� ���� ������Ʈ�� �浹���� ��
        {
            SceneManager.LoadScene("Play5"); // Play �� 2�� 
        }
    }
}
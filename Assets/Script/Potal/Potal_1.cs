using UnityEngine.SceneManagement;
using UnityEngine;

public class Potal_1 : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // "Player" �±׸� ���� ������Ʈ�� �浹���� ��
        {
            SceneManager.LoadScene("Play2"); // Play �� 2�� 
        }
    }

}
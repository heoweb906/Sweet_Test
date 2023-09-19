using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public int damage = 1;
    public float destroyDelay = 15f;

    private void Start()
    {
        // ���� �ð��� ���� �Ŀ� �ڵ����� ����
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject playerObject = other.gameObject;
            Player playerScript = playerObject.GetComponent<Player>();

            // �÷��̾� ��ũ��Ʈ�� �����ϸ� �÷��̾��� ü���� ���ҽ�Ŵ
            if (playerScript != null)
            {
                playerScript.OnDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
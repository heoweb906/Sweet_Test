using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Long : Monster
{
    [Header("���� ������Ʈ / ����")]
    public bool isAttack;
    public GameObject bulletPrefab; // �Ѿ� �������� �����ؾ� �մϴ�.

    private Animator anim;
    private Transform player;
    private Rigidbody rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (currentHealth <= 0 && !doDie)
        {
            anim.SetBool("boolDie",true);
            doDie = true;
            anim.SetTrigger("doDie");
        }

        if (!isAttack && !doDie && gameManager.bpmCount % 4 == 0 && gameManager.bpmCount != 0)  // 4��° bpm ���� �ѹ��� ����
        {
            isAttack = true;
              StartCoroutine(AttackAfterDelay(0.35f));  // 0.35�� �ڿ� ���� �ڷ�ƾ ���� - �ִϸ��̼� �ӵ��� �����ؼ� ���ݰ� BPM�� ��ġ��Ű�� ���ؼ�
        }

        LookAtPlayer(); // �÷��̾� �������� ȸ����Ű�� �Լ�
    }

    private IEnumerator AttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 0.4�� ���
        yield return StartCoroutine(Attack()); // Attack �ڷ�ƾ ����
    }
    IEnumerator Attack()
    {
        anim.SetTrigger("doAttack");
        yield return new WaitForSeconds(1.5f);
        isAttack = false;
    }


    public void LookAtPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0f; // y�� ȸ���� ������� ����
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }


    public void ShootAtPlayer() // �ִϸ��̼� �̺�Ʈ�� �����س���
    {
        if (bulletPrefab != null)
        {
            // �÷��̾� ��ġ�� ���� ȸ��
            Vector3 playerPosition = player.position;
            Vector3 offset = new Vector3(0f, -1f, 0f); // �Ʒ��� 1 ������ŭ ������ ������
            Vector3 direction = (playerPosition + offset) - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);

            // �Ѿ��� �����ϰ� �߻�
            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            // �Ѿ˿� ���� ���� �߻� (���ϴ� ���� �������� �����ؾ� ��)
            float bulletSpeed = 10f; // �Ѿ� �߻� �ӵ�
            bulletRb.velocity = direction.normalized * bulletSpeed;

            // �Ѿ��� �߻��� �� �� �� �Ŀ� �ڵ����� ���� (���ϴ� �ð����� ���� ����)
            float bulletDestroyDelay = 5f; // �� �� �Ŀ� �������� ����
            Destroy(bullet, bulletDestroyDelay);
        }
    }
}
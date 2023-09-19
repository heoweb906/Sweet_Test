using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameManager gameManager;

    [Header("���� ����")]
    public int currentHealth;
    public int monsterColor;
    public bool doDie; 


    // �ϴ� ������ �����Ű�� ���� �־����
    public new Renderer renderer; // ������ ������Ʈ
    public Color originalColor; // ���� ��Ƽ����

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>(); // GameManager�� ã�Ƽ� �Ҵ�
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        renderer.material.color = Color.black;

        if (currentHealth <= 0)
        {
            Invoke("Die", 1.5f);
        }

        Invoke("ColorBack", 0.1f);
    }
    private void ColorBack()
    {
        renderer.material.color = originalColor;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
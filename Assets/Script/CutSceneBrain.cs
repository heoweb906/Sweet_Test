using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class CutSceneBrain : MonoBehaviour
{
    [SerializeField] UnityEvent enterEvent;
    [SerializeField] UnityEvent exitEvent;

    [Space(10f)]
    [SerializeField] bool CutSceneShowing;
    [SerializeField] int cutIndex = 0;
    [Space(10f)]
    [SerializeField] SingleCutInfo[] cutInfos;
    [SerializeField] float nextCutDelay;
    WaitForSeconds nextCutDelayWFS;

    private bool isExiting = false;

    void Awake()
    {
        nextCutDelayWFS = new WaitForSeconds(nextCutDelay);
        for (int i = 0; i < cutInfos.Length; i++)
        {
            cutInfos[i].Init();
        }

        EnterCutScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TryNextCut();
        if (Input.GetKeyDown(KeyCode.Escape) && !isExiting) // isExiting ������ Ȯ���Ͽ� �ߺ����� ExitCutScene() ȣ�� ����
            ExitCutScene();
    }

    [ContextMenu("EnterCutScene")]
    public void EnterCutScene()
    {
        if (!CutSceneShowing)
        {
            CutSceneShowing = true;
            enterEvent.Invoke();

            cutIndex = -1;
            ShowNextCut();
        }
    }

    [ContextMenu("ExitCutScene")]
    public void ExitCutScene()
    {
        if (CutSceneShowing && !isExiting) // isExiting ������ Ȯ���Ͽ� �ߺ����� ó������ �ʵ��� ��
        {
            isExiting = true; // �ⱸ ���·� ����

            // �ð� ���� ���� �ٷ� ���� ������ �̵�
            SceneManager.LoadScene("Menu");
        }
    }

    [ContextMenu("TryNextCut")]
    public void TryNextCut()
    {
        if (CutSceneShowing)
        {
            if (cutIndex >= cutInfos.Length)
                ExitCutScene();

            else
                ShowNextCut();
        }
    }

    void ShowNextCut()
    {
        cutIndex++;

        if (cutIndex < cutInfos.Length)
        {
            //���� �� ��ŵ
            if (cutIndex > 0)
                cutInfos[cutIndex - 1].DoEnter_Skip();

            if (CutShowing_C != null)
                StopCoroutine(CutShowing_C);

            //���� �� �����ֱ�
            cutInfos[cutIndex].DoEnter();
            CutShowing_C = CutShowingc();
            StartCoroutine(CutShowing_C);
        }
    }

    //Duration �ð� �Ѿ�� �ڿ������� ������ �Ѿ��
    IEnumerator CutShowing_C;
    IEnumerator CutShowingc()
    {
        float waitT = cutInfos[cutIndex].Duration;
        while (waitT > 0)
        {
            waitT -= Time.deltaTime;
            yield return null;
        }

        yield return nextCutDelayWFS;
        ShowNextCut();
    }
}

[System.Serializable]
public class SingleCutInfo
{
    Image CutImg;
    public RectTransform CutRtf;
    public float Duration;
    public Vector2 StartOffset;
    Vector2 goalPos;

    Color whiteClear = new Color(1, 1, 1, 0);

    public void Init()
    {
        CutRtf.TryGetComponent(out CutImg);
        CutImg.color = Color.clear;
    }

    public void DoEnter()
    {
        goalPos = CutRtf.anchoredPosition;

        CutRtf.anchoredPosition = goalPos + StartOffset;
        CutRtf.DOAnchorPos(goalPos, Duration);

        CutImg.color = whiteClear;
        CutImg.DOColor(Color.white, Duration);
    }

    public void DoEnter_Skip()
    {
        CutRtf.DOKill();
        CutImg.DOKill();

        CutRtf.anchoredPosition = goalPos;
        CutImg.color = Color.white;
    }

    public void DoExit()
    {
        CutRtf.DOKill();
        CutImg.DOKill();

        CutImg.DOColor(whiteClear, 0.5f);
    }
}
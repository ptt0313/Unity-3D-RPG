using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : Singleton<SceneManagement>
{
    [SerializeField] Image screenImage;
    public void OnClickTown()
    {
        SceneManagement.Instance.StartCoroutine(AsyncLoad(0));
    }
    public void OnClickForest()
    {
        SceneManagement.Instance.StartCoroutine(AsyncLoad(1));
    }
    public void OnClickDungeon()
    {
        SceneManagement.Instance.StartCoroutine(AsyncLoad(2));
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public IEnumerator FadeIn()
    {
        screenImage.gameObject.SetActive(true);
        Color color = screenImage.color;
        color.a = 1f;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime;
            screenImage.color = color;
            if (color.a <= 0)
            {
                screenImage.gameObject.SetActive(false);
            }
        }
        yield return null;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("SceneLoaded");
        StartCoroutine(FadeIn());
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }
    public IEnumerator AsyncLoad(int index)
    {
        screenImage.gameObject.SetActive(true);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;
        // <asyncOperation.allowSceneActivation>
        // ����� �غ�� ��� ����� Ȱ��ȭ�Ǵ� ���� ����ϴ� �����Դϴ�.
        Color color = screenImage.color;
        color.a = 0;

        // <asyncOperation.isDone>
        // �ش� ������ �Ϸ�Ǿ������� ��Ÿ���� �����Դϴ�.(�б�����)
        while (asyncOperation.isDone == false)
        {
            color.a += Time.deltaTime;

            screenImage.color = color;

            // <asyncOperation.progress>
            // �۾��� ���� ���¸� ��Ÿ���� �����Դϴ�.(�б�����)
            if (asyncOperation.progress >= 0.9f)
            {
                color.a = Mathf.Lerp(color.a, 1f, Time.deltaTime);

                screenImage.color = color;
                if (color.a >= 1.0f)
                {
                    asyncOperation.allowSceneActivation = true;
                    Debug.Log("SceneLoad");
                    yield break;
                }
            }

            yield return null;
        }

    }
}

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
        // 장면이 준비된 즉시 장면이 활성화되는 것을 허용하는 변수입니다.
        Color color = screenImage.color;
        color.a = 0;

        // <asyncOperation.isDone>
        // 해당 동작이 완료되었는지를 나타내는 변수입니다.(읽기전용)
        while (asyncOperation.isDone == false)
        {
            color.a += Time.deltaTime;

            screenImage.color = color;

            // <asyncOperation.progress>
            // 작업의 진행 상태를 나타내는 변수입니다.(읽기전용)
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

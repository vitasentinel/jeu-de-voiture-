using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changementscene : MonoBehaviour
{
    public string sceneName;
    public GameObject loadingscreen;
    
    
    public void Changementscene()
    {
        StartCoroutine(LoadingScene());
    }

    private IEnumerator LoadingScene()
    {
        loadingscreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName);

    }

    private void OnTriggerEnter(Collider other)
    {
        Changementscene();
    }
}

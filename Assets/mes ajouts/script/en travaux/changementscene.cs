using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changementscene : MonoBehaviour
{
    public string sceneName;
    
    
    void OnClick()
    {
        Debug.Log("clic");
        SceneManager.LoadScene(sceneName);
        
    }
}

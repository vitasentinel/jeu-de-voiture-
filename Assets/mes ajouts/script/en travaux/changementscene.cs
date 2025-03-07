using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class changementscene : MonoBehaviour
{
    public string sceneName;
    
    
    public void loadscene()
    {
        SceneManager.LoadScene(sceneName);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    public void btnClick_GoTutorial()
    {
            SceneManager.LoadScene("tutorial");
    }
    public void btnClick_GoIngame()
    {
        SceneManager.LoadScene("Ingame");
    }
    
}

using UnityEngine;
using UnityEngine.SceneManagement;  

public class MenuManage : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("O jogo foi fechado");
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
   
    private void Start()
    {
       
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene("Game");
    }
}

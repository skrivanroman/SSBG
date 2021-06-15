using UnityEngine;
using UnityEngine.SceneManagement;


public class prepinaniScen : MonoBehaviour
{
    public void Konec()
    {
        Debug.Log("Konec");       //zatim pro overeni zda Quit funguje
        Application.Quit();
    }

    public void NovaHra()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //posune se na dalsi scenu v seznamu (v build setting)
    }
        
    public void Napoveda()
    {
        SceneManager.LoadScene(3);
    }

    public void Autori()
    {
        SceneManager.LoadScene(4);
    }

    public void ZpetDoMenu()
    {
        SceneManager.LoadScene(0);
    }

}

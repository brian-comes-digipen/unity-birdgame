using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit(0);
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
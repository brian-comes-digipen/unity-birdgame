using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;

    public GameObject ScoreText;

    public float GameSpeed = 1;

    public bool[] activeTiles = new bool[30];

    // Start is called before the first frame update
    private void Start()
    {
        ScoreText = GameObject.Find("ScoreText (TMP)");
        for (int i = 0; i < activeTiles.Length; ++i)
        {
            activeTiles[i] = true;
        }
        StartCoroutine(ChangeBeanSpawnState());
        StartCoroutine(IncreaseGameSpeed());
    }

    // Update is called once per frame
    private void Update()
    {
        ScoreText.GetComponent<TextMeshProUGUI>().SetText($"Score: {score}");

        int i = 0;
        while (i < activeTiles.Length)
        {
            //print("checking block #" + i);
            //print($"block #{i} should be {!activeTiles[i]}");
            GameObject.Find("block" + i).GetComponent<BlockObject>().blockPlayer = !activeTiles[i];
            ++i;
        }
    }

    public void AddToScore(int s)
    {
        score += s;
    }

    private IEnumerator ChangeBeanSpawnState()
    {
        while (true)
        {
            print("Bean Spawner is spawning beans randomly.");
            GameObject.Find("BeanSpawner").GetComponent<BeanSpawner>().movementMode = (int)BeanSpawner.MovementModes.Randomly;
            yield return new WaitForSeconds(60);
            print("Bean Spawner is spawning beans near the player.");
            GameObject.Find("BeanSpawner").GetComponent<BeanSpawner>().movementMode = (int)BeanSpawner.MovementModes.NearPlayerX;
            yield return new WaitForSeconds(10);
        }
        yield return null;
    }

    private IEnumerator IncreaseGameSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
            GameSpeed += 0.125f;
        }
        yield return null;
    }

    private IEnumerator TurnOnMagicBeans()
    {
        yield return new WaitForSeconds(300);
        GameObject.Find("BeanSpawner").GetComponent<BeanSpawner>().CanSpawnMagicBeans = true;
        yield return null;
    }

    public void RegenerateTiles(int numTiles)
    {
        System.Random r = new System.Random();
        int regeneratedTiles = 0;
        int j = 0;
        while (regeneratedTiles < numTiles)
        {
            activeTiles[r.Next(0, activeTiles.Length)] = true;
            ++j;
            if (j == activeTiles.Length)
            {
                break;
            }
        }
    }
}
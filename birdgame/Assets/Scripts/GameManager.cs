using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool[] activeTiles = new bool[30];
    public float GameSpeed = 1;
    public int score;

    public GameObject ScoreText;

    private BeanSpawner BS;

    public void AddToScore(int s)
    {
        score += s;
    }

    public void RegenerateTiles(int numTiles)
    {
        System.Random r = new System.Random();
        int regeneratedTiles = 0;
        while (regeneratedTiles <= numTiles)
        {
            activeTiles[r.Next(0, activeTiles.Length)] = true;
            ++regeneratedTiles;
            if (regeneratedTiles >= activeTiles.Length)
            {
                break;
            }
        }
    }

    private IEnumerator ChangeBeanSpawnState()
    {
        while (true)
        {
            print("Bean Spawner is spawning beans randomly.");
            BS.movementMode = (int)BeanSpawner.MovementModes.Randomly;
            yield return new WaitForSecondsRealtime(60);
            print("Bean Spawner is spawning beans near the player.");
            BS.movementMode = (int)BeanSpawner.MovementModes.NearPlayerX;
            yield return new WaitForSecondsRealtime(10);
        }
        yield return null;
    }

    private IEnumerator IncreaseGameSpeed()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(30);
            GameSpeed += 0.125f;
        }
        yield return null;
    }

    // Start is called before the first frame update
    private void Start()
    {
        BS = GameObject.Find("BeanSpawner").GetComponent<BeanSpawner>();
        ScoreText = GameObject.Find("ScoreText (TMP)");
        for (int i = 0; i < activeTiles.Length; ++i)
        {
            activeTiles[i] = true;
        }
        StartCoroutine(ChangeBeanSpawnState());
        StartCoroutine(IncreaseGameSpeed());
    }

    private IEnumerator TurnOnMagicBeans()
    {
        yield return new WaitForSecondsRealtime(300);
        BS.CanSpawnMagicBeans = true;
        yield return null;
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
}
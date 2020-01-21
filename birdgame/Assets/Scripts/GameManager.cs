using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;

    public GameObject ScoreText;

    public bool[] activeTiles = new bool[30];

    // Start is called before the first frame update
    void Start()
    {
        ScoreText = GameObject.Find("ScoreText (TMP)");
        for (int i = 0; i < activeTiles.Length; ++i)
        {
            activeTiles[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timeElapsed;

    bool[] activeTiles = new bool[32];

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        for (int i = 0; i < activeTiles.Length; ++i)
        {
            activeTiles[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
    }
}

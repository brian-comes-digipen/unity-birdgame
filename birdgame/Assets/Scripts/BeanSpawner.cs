using System.Collections;
using System.Linq;
using UnityEngine;

public class BeanSpawner : MonoBehaviour
{
    public readonly float[] validXpositions = { 7.25f, 6.75f, 6.25f, 5.75f, 5.25f, 4.75f, 4.25f, 3.75f, 3.25f, 2.75f, 2.25f, 1.75f, 1.25f, 0.75f, 0.25f };

    public GameObject beanPrefab;
    public float beansPerSecond;

    public bool CanSpawnMagicBeans;
    public int movementMode;
    private int frames;
    private GameManager GM;

    private bool CanSpawnBeansAtAll = false;

    private float secondsElapsed = 0;

    public enum MovementModes { Randomly = 0, NearPlayerX = 1 };

    // Returns 1 or -1
    private int CoinFlipNegative()
    {
        System.Random r = new System.Random();
        if (r.Next(0, 2) == 1)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    private float RandomFloatFromArray(float[] x)
    {
        return x[Random.Range(0, x.Length)];
    }

    private float RandomOffset(float f)
    {
        System.Random r = new System.Random();
        int x = r.Next(0, 3);
        if (x == 0)
        {
            return 0;
        }
        else if (x == 1)
        {
            return 2 * f * CoinFlipNegative();
        }
        else
            return f * CoinFlipNegative();
    }

    // Start is called before the first frame update
    private void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    private void Update()
    {
        frames++;
        if (CanSpawnBeansAtAll)
        {
            if (movementMode == (int)MovementModes.Randomly)
            {
                transform.position = new Vector2(RandomFloatFromArray(validXpositions) * CoinFlipNegative(), transform.position.y);
            }
            else if (movementMode == (int)MovementModes.NearPlayerX)
            {
                float playerX = GameObject.Find("Player").GetComponent<Transform>().position.x;
                float xNearestToPlayerX = Mathf.Abs(validXpositions.Aggregate((a, b) => Mathf.Abs(a - Mathf.Abs(playerX)) < Mathf.Abs(b - Mathf.Abs(playerX)) ? a : b)) + RandomOffset(.5f);
                //print($"nearest x: {xNearestToPlayerX}");
                if (playerX < 0)
                {
                    xNearestToPlayerX *= -1;
                }
                transform.position = new Vector3(xNearestToPlayerX, transform.position.y, transform.position.z);
            }

            if ((secondsElapsed % 200 / GM.GameSpeed == 0) && CanSpawnMagicBeans)
            {
                beanPrefab.GetComponent<BeanFall>().BeanType = 2;
                Instantiate(beanPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            }
            else if (secondsElapsed % 50 / GM.GameSpeed == 0)
            {
                beanPrefab.GetComponent<BeanFall>().BeanType = 1;
                Instantiate(beanPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            }
            else if (secondsElapsed % 5 / GM.GameSpeed == 0)
            {
                beanPrefab.GetComponent<BeanFall>().BeanType = 0;
                Instantiate(beanPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            }
        }
        CanSpawnBeansAtAll = false;
    }

    private IEnumerator Timer()
    {
        secondsElapsed = 0;
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.25f);
            secondsElapsed += 0.25f;
            CanSpawnBeansAtAll = true;
        }
        yield return null;
    }
}
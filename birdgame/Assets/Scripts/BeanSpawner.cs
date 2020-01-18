using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanSpawner : MonoBehaviour
{
    public readonly float[] validXpositions = { 7.25f, 6.75f, 6.25f, 5.75f, 5.25f, 4.75f, 4.25f, 3.75f, 3.25f, 2.75f, 2.25f, 1.75f, 1.25f, 0.75f, 0.25f };

    public float beansPerSecond;

    int frames;

    bool goRight;

    int movementMode;

    enum movementModes { Randomly = 0, NearPlayerX = 1 };

public GameObject beanPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frames++;
        if (frames % 6000 == 0)
        {
            beanPrefab.GetComponent<BeanFall>().BeanType = 1;
            Instantiate(beanPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            beanPrefab.GetComponent<BeanFall>().BeanType = 0;
        }
        else if (frames % 600 == 0)
        {
            beanPrefab.GetComponent<BeanFall>().BeanType = 0;
            Instantiate(beanPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            beanPrefab.GetComponent<BeanFall>().BeanType = 0;
        }

        if (goRight)
        {
            transform.position = transform.position + (Vector3)new Vector2(.5f * Time.deltaTime, 0);
            if (transform.position.x >= 7.25f)
                goRight = false;
        }
        else
        {
            transform.position = transform.position - (Vector3)new Vector2(.5f * Time.deltaTime, 0);
            if (transform.position.x <= -7.25f)
                goRight = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject crow;

    private float crowHalfHeight;
    private float crowWidth;

    // Start is called before the first frame update
    void Start()
    {
        crowHalfHeight = crow.gameObject.GetComponent<BoxCollider2D>().size.y / 2;
        crowWidth = crow.gameObject.GetComponent<BoxCollider2D>().size.x ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(float platformHalfWidth, float platformHalfHeight, Vector3 platformPosition)
    {
        GameObject temp = Instantiate(crow);
        float centreXPoint = platformPosition.x;
        float centreYPoint = platformPosition.y;
        Debug.Log(platformHalfWidth);
        temp.transform.position = new Vector3(UnityEngine.Random.Range(centreXPoint - platformHalfWidth + crowWidth, centreXPoint + platformHalfWidth - crowWidth), 
            platformPosition.y + platformHalfHeight, 0);
    }
}

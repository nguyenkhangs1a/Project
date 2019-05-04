using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatZB : MonoBehaviour {
    [SerializeField]
    private Transform[] PointZB;
    [SerializeField]
    private GameObject ZBPrefabs;
    private int FullZB;
    public static int Count;
    int i;
	// Use this for initialization
	void Start () {
        StartCoroutine(DelayCreat());
        Count = 0;
         i = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator DelayCreat()
    {
        var timerand = Random.Range(2, 5);
        yield return new WaitForSeconds(timerand);
        if(Count<=4)
        {
            CreatZomBie();
            Count++;
        }
        else
        {
        }
        StartCoroutine(DelayCreat());
        
    }
    void CreatZomBie()
    {
        if (i >= PointZB.Length)
        {
            i = 0;
        }
        Instantiate(ZBPrefabs, PointZB[i].position, Quaternion.identity);
        i++;
    }
    
}

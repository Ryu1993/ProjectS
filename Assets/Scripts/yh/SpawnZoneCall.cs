using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneCall : MonoBehaviour
{
    [SerializeField]
    GameObject mountain;
    [SerializeField]
    GameObject lake;
    [SerializeField]
    GameObject forest;
    [SerializeField]
    GameObject yard;
    void Start()
    {
        StartCoroutine(TurnOn());
    }

    
    IEnumerator TurnOn()
    {
        while(true)
        {
            mountain.SetActive(true);
            yield return new WaitForSeconds(5f);
            lake.SetActive(true);
            yield return new WaitForSeconds(5f);
            forest.SetActive(true);
            yield return new WaitForSeconds(5f);
            yard.SetActive(true);
            yield return new WaitForSeconds(5f);
        }
    }
}

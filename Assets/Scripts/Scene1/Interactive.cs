using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public SpriteRenderer mySR;
    public GameObject my;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.CompareTag("Player"))
        {
            mySR.sortingOrder = 6;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mySR.sortingOrder = 2;
        }

    }

}

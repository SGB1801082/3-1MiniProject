/*using UnityEngine;

public class ChangeSortingOrder : MonoBehaviour
{
    public SpriteRenderer mySR;

    private bool enterPlayer = false;

    *//*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTrigger Potal");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player In Potal");
            mySR.sortingOrder = 6;
        }
    }*//*

    private void Update()
    {
        if (enterPlayer)
        {
            mySR.sortingOrder = 6;
            //mySR.order
        }
        else
        {
            mySR.sortingOrder = 2;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTrigger Potal - Detected something");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player In Potal");
        }
        else
        {
            Debug.Log("Other object: " + other.gameObject.name);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 여기에 충돌 중 지속적으로 호출할 코드를 추가할 수 있습니다.
            // 예를 들어, 애니메이션 상태를 확인하거나 추가적인 로직을 처리할 수 있습니다.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exit");
            mySR.sortingOrder = 2;
        }
    }

}
*/
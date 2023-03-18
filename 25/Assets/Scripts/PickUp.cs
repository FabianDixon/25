using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private int pickUpType; // 1: candle, 2: pet, 3: slice of cake


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Points addPoints = other.gameObject.GetComponent<Points>();

            switch (pickUpType)
            {
                case 1:
                    addPoints.takeCandle();
                    break;
                case 2:
                    addPoints.tamePet();
                    break;
                case 3:
                    addPoints.takeSlice();
                    break;
                default:
                    break;
            }

            if (pickUpType != 2)
            {
                Destroy(this.gameObject);
            }
        }
    }

}

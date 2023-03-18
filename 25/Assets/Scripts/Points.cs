using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField]
    private float score = 0f;

    public int numCandles;
    public int numPets;
    public int numSlices;

    public void takeCandle()
    {
        numCandles += 1;

        score += 100f;
    }

    public void tamePet()
    {
        numPets += 1;
    }

    public void takeSlice()
    {
        numSlices += 1;

        score += 1000f;
    }

}

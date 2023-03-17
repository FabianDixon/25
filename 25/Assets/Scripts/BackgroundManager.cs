using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public Transform mainCam;
    public Transform middleBG;
    public Transform sideBG;

    public float BG_Length = 12f;

    // Update is called once per frame
    void Update()
    {
        if (mainCam.position.x > middleBG.position.x)
        {
            sideBG.position = middleBG.position + Vector3.right * BG_Length;
        }

        if (mainCam.position.x < middleBG.position.x)
        {
            sideBG.position = middleBG.position + Vector3.left * BG_Length;
        }

        if (mainCam.position.x < sideBG.position.x)
        {
            Transform placeHolder = middleBG;

            middleBG = sideBG;
            sideBG = placeHolder;
        }
    }
}

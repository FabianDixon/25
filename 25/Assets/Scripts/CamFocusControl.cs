using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamFocusControl : MonoBehaviour
{
    public Transform camFocus;
    public CinemachineVirtualCamera vcam;
    public Transform _player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            vcam.Follow = camFocus;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            vcam.Follow = _player;
        }
    }
}


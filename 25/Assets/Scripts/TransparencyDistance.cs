using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyDistance : MonoBehaviour
{
    public Transform _player;
    public Transform thisObject;
    public SpriteRenderer sp;
    public SpriteRenderer child_sp;
    public CircleCollider2D thisCollider;

    public bool isHiding;

    private Color originalAlpha;
    private Color originalAlphaChild;
    private float fullDistance;

    void Start()
    {
        originalAlpha = sp.color;
        if (child_sp != null)
        {
            originalAlphaChild = child_sp.color;
        }

        fullDistance = thisCollider.radius;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 relativeDist = new Vector3(_player.position.x - thisObject.position.x, _player.position.y - thisObject.position.y, 0f);

            var tmp = sp.color;
            var tmp2 = sp.color;
            if (child_sp != null)
            {
                tmp2 = child_sp.color;
            }

            if (isHiding)
            {
                tmp.a = fullDistance / (relativeDist.magnitude * 2f);
                sp.color = tmp;
                if (child_sp != null)
                {
                    tmp2.a = fullDistance / (relativeDist.magnitude * 2f);
                    child_sp.color = tmp2;
                }
            }
            else
            {
                tmp.a = (relativeDist.magnitude) / fullDistance;
                sp.color = tmp;
                if (child_sp != null)
                {
                    tmp2.a = (relativeDist.magnitude) / fullDistance;
                    child_sp.color = tmp2;
                }
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sp.color = originalAlpha;
            if (child_sp != null)
            {
                child_sp.color = originalAlphaChild;
            }
        }
    }
}

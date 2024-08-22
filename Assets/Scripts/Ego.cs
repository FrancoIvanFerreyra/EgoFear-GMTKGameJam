using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class Ego : Companion
{
    float _currentScale = .99f;
    public float changeSpeed = 1, delta = 1;
    Animator animator;
    public float currentScale
    {
        get
        {
            return _currentScale;
        }
        set
        {
            //Update ego scale
            if(_currentScale != value)
            {
                if(value >= .9f)
                {
                    StartCoroutine(ChangeSize(_currentScale, value));
                }
                else
                {
                    StartCoroutine(ChangeSize(_currentScale, .9f));
                }
                _currentScale = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override IEnumerator ChangeSize(float start, float newValue)
    {
        
        //Update animator booleans
        //Grow
        if(start < newValue)
        {
            animator.SetBool("isGrowing", true);
            animator.SetBool("isShrinking", false);
        }
        //Shrink
        else
        {
            animator.SetBool("isGrowing", false);
            animator.SetBool("isShrinking", true);
        }

        float t = 0;
        //Gradually increase the scale of ego's transform
        while (transform.localScale != Vector3.one * newValue)
        {
            transform.localScale = Vector3.one * Mathf.Lerp(start, newValue, t);
            t += changeSpeed * Time.deltaTime;
            yield return null;
        }

        //Reset animation to default
        animator.SetBool("isGrowing", false);
        animator.SetBool("isShrinking", false);
        yield return null;
    }
}

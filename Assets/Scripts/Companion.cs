using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    public virtual IEnumerator ChangeSize(float start, float newValue)
    {
        yield return null;
    }
}

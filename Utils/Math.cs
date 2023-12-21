using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class MathExt
{
    public static float GetDistanceBetween(Vector3 pos1, Vector3 pos2)
    {
        return Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.y - pos2.y, 2));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Math
{
    public static float GetDistanceBetween(Vector3 pos1, Vector3 pos2)
    {
        return Vector2.Distance(new Vector2(pos1.x, pos1.y), new Vector2(pos2.x, pos2.y));
    }
}

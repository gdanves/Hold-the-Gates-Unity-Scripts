using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Util
{
    public static int GetTimeMillis()
    {
        return (int)Mathf.Round(Time.time * 1000);
    }
}

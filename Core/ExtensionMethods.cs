using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
    public static Game AsGame(this GameObject gameObject)
    {
        return gameObject.GetComponent<Game>();
    }

    public static Creature AsCreature(this GameObject gameObject)
    {
        return gameObject.GetComponent<Creature>();
    }
}

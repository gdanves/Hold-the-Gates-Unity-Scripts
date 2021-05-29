using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnoll : Monster
{
    protected override void Awake()
    {
        base.Awake();
        SetStats(new List<int>{38, 25, 35, 27, 45}); // atk, hp, def, defm, spd
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}

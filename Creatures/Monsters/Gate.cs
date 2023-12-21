using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : Unit
{
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        //base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //base.Update();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        m_gameManager.EndGame();
    }
}

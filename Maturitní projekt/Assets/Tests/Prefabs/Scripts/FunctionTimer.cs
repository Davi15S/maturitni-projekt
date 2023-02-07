using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FunctionTimer
{
    public static FunctionTimer Create(Action action, float timer)
    {
        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));
        FunctionTimer functionTimer = new FunctionTimer(action, timer, gameObject);

        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;

        return functionTimer;
    }
    public class MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            if (onUpdate != null) onUpdate();
        }
    }
    private Action action;
    private float timer;
    private bool isDestroy = false;
    private GameObject gameObject;
    private FunctionTimer(Action action, float timer, GameObject gameObject)
    {
        this.action = action;
        this.timer = timer;
        this.gameObject = gameObject;
    }

    public void Update()
    {
        if (!isDestroy)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                action();
                DestroySelf();
            }
        }
    }

    private void DestroySelf()
    {
        isDestroy = true;
        UnityEngine.Object.Destroy(gameObject);
    }
}

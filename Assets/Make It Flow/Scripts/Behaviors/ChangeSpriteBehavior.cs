﻿using System.Collections.Generic;
using Make_It_Flow.Scripts.Core.Behavior;
using Make_It_Flow.Scripts.Core.Objects;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteBehavior : Behavior
{
    [SerializeField] Sprite sprite;
    public Sprite Sprite { get => sprite; set => sprite = value; }

    Dictionary<MFObject, Image> dictMFObjToAct = new Dictionary<MFObject, Image>();

    public override void InitializeBehavior()
    {
        dictMFObjToAct.Clear();
        foreach (MFObject objAct in MFObjectsToAct)
        {
            GameObject objActGO = objAct.Transform.gameObject;
            Image image = objActGO.GetComponent<Image>();
            if (!image)
            {
                image = objActGO.AddComponent<Image>();
            }

            dictMFObjToAct.Add(objAct, image);
        }
    }
    public override void StartBehavior()
    {
        behaviorEvents.OnBehaviorStart.Invoke();
        foreach (var item in dictMFObjToAct)
        {
            Image itemValue = item.Value;
            if (itemValue)
                itemValue.sprite = Sprite;
        }
        behaviorEvents.OnBehaviorEnd.Invoke();
    }

    public override void InterruptBehavior()
    {
        behaviorEvents.OnBehaviorInterrupt.Invoke();
    }
}

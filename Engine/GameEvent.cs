﻿using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedOwl.Core
{
    public class WaitForNextEvent : CustomYieldInstruction
    {
        private bool _keepWaiting;
        public override bool keepWaiting => _keepWaiting;

        private readonly GameEvent _evt;

        public WaitForNextEvent(GameEvent evt)
        {
            _evt = evt;
            _evt.On += HandleEvent;
            _keepWaiting = true;
        }

        private void HandleEvent()
        {
            _evt.On -= HandleEvent;
            _keepWaiting = false;
            
        }
    }
    
    [HideMonoScript]
    [CreateAssetMenu(menuName = "Red Owl/Game Event")]
    public class GameEvent : ScriptableObject
    {
        public event Action On;

        [Button(ButtonSizes.Large)]
        public void Raise()
        {
            Log.Always($"Raising Event: {name}");
            On?.Invoke();
        }

        public WaitForNextEvent OnNext()
        {
            return new WaitForNextEvent(this);
        }
    }

    [HideMonoScript]
    public abstract class GameEvent<T1> : ScriptableObject
    {
        public event Action<T1> On;

        [Button(ButtonSizes.Large)]
        public void Raise(T1 t1)
        {
            On?.Invoke(t1);
        }
    }  
    
    [HideMonoScript]
    public abstract class GameEvent<T1, T2> : ScriptableObject
    {
        public event Action<T1, T2> On;

        [Button(ButtonSizes.Large)]
        public void Raise(T1 t1, T2 t2)
        {
            On?.Invoke(t1, t2);
        }
    } 
    
    [HideMonoScript]
    public abstract class GameEvent<T1, T2, T3> : ScriptableObject
    {
        public event Action<T1, T2, T3> On;

        [Button(ButtonSizes.Large)]
        public void Raise(T1 t1, T2 t2, T3 t3)
        {
            On?.Invoke(t1, t2, t3);
        }
    }
    
    [HideMonoScript]
    public abstract class GameEvent<T1, T2, T3, T4> : ScriptableObject
    {
        public event Action<T1, T2, T3, T4> On;

        [Button(ButtonSizes.Large)]
        public void Raise(T1 t1, T2 t2, T3 t3, T4 t4)
        {
            On?.Invoke(t1, t2, t3, t4);
        }
    }
}

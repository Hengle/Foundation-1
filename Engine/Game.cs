using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = Unity.Mathematics.Random;

namespace RedOwl.Core
{
    public class RedOwlException : Exception
    {
        public RedOwlException() {}

        public RedOwlException(string message) : base(message) {}

        public RedOwlException(string message, Exception inner) : base(message, inner) {}
    }
    
    public partial class RedOwlSettings
    {
        [SerializeField]
        private StateMachineConfig gameStateMachine;

        public static StateMachineConfig GameStateMachine => I.gameStateMachine;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void InitializeGame() => Game.Initialize();
    }
    
    public static class Game
    {
        internal static void Initialize()
        {
            Log.Always("Initialize RedOwl Game!");
            
            InitializeRandom();
            InitializeServices();
            InitializeStateMachine();
        }

        #region Random
        
        public static Random Random { get; private set; }

        private static void InitializeRandom()
        {
            Random = new Random((uint)Environment.TickCount);
        }
        
        #endregion
        
        #region Services
        
        public static ServiceCache Services { get; private set; }

        private static void InitializeServices()
        {
            Services = new ServiceCache();
        }
        
        public static void Bind<T>(T instance) => Services.Bind(instance);
        public static void Inject(object obj) => Services.Inject(obj);
        
        #endregion
        
        #region StateMachine
        
        public static StateMachine StateMachine { get; private set; }
        
        private static void InitializeStateMachine()
        {
            if (RedOwlSettings.GameStateMachine == null) return;
            Log.Always("Initialize RedOwl Game State Machine");
            
            var obj = new GameObject("Game");
            Object.DontDestroyOnLoad(obj);
            StateMachine = new StateMachine(obj, RedOwlSettings.GameStateMachine);

            StateMachine.EnterInitialState();
        }
        public static CoroutineWrapper SetState(State nextState) => StateMachine.SetState(nextState);
        
        #endregion
    }
}
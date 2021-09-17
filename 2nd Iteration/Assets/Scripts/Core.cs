using Leopotam.Ecs;
using UnityEngine;

namespace Gradient {
    sealed class Core : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;

        public Configuration _config;
        void Start () {

            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                // register your InitSystems here, for example:
                .Add(new InitUILayout())
                .Add(new InitWorkArea())
                .Add(new ColirizeTiles())
                .Add(new SelectBlankTiles())
                // register your RunSystems here, for example:
                .Add(new TrackInput())
                .Add(new ColorSwipe())
                .Add(new GameConditions())
                .Add(new DestroyMarks())
                // register one-frame components (order is important), for example:
                .OneFrame<Selected>()
                .OneFrame<GameCheck>()
                // inject service instances here (order doesn't important), for example:
                .Inject(_config)
                .Init ();
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}
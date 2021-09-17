using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gradient
{
    internal class TrackInput : IEcsRunSystem
    {
        private Configuration _config;
        private PointerEventData _eventData;
        private EcsFilter<Checking> _filter;

        public void Run()
        {
            if (_filter.IsEmpty())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _eventData = new PointerEventData(_config.EventSystem);
                    _eventData.position = Input.mousePosition;

                    List<RaycastResult> workAreaResults = new List<RaycastResult>();

                    _config.WorkAreaRaycaster.Raycast(_eventData, workAreaResults);

                    if (workAreaResults.Count == 0) return;

                    if (workAreaResults[0].gameObject.TryGetComponent<TileScript>(out var tmpTS))
                    {
                        var selected = tmpTS.Entity;

                        if (selected.Has<Blank>())
                        {
                            selected.Get<Selected>();
                        }
                    }
                }
                else
                {
                    _config.Checking = true;
                }
            }
        }
    }
}



    

    
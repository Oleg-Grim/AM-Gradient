/*using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro.EditorUtilities;
using UnityEditor.Timeline.Actions;

namespace Client
{
    internal class GameConditionsSystem : IEcsRunSystem
    {
        private Configuration _config;
        private EcsFilter<GameCheck> _filter;
        private EcsFilter<Blank> _blankFilter;

        private List<Transform> _correctTweenTargets = new List<Transform>();
        private List<Transform> _wrongTweenTargets = new List<Transform>();
        

        public void Run()
        {
            if (_filter.IsEmpty()) return;
            
            foreach (var index in _blankFilter)
            {
                var tile = _blankFilter.GetEntity(index);

                if (tile.Get<TileAvatar>()._avatar.GetComponent<Image>().color ==
                    tile.Get<TileAvatar>()._correctColor)
                {
                    _correctTweenTargets.Add(tile.Get<TileAvatar>()._avatar.transform);
                    ScaleUp();                    
                }
                else
                {
                    _wrongTweenTargets.Add(tile.Get<TileAvatar>()._avatar.transform);
                    ShakeTile();
                }
            }
        }

        private void ShakeTile()
        {
            foreach (var target in _wrongTweenTargets)
            {
                target.DOShakePosition(1f, new Vector3(10, 0, 0), 10, 90f, true, false);
            }
            
            _wrongTweenTargets.Clear();
            
        }
        
        private void ScaleUp()
        {
            foreach (var target in _correctTweenTargets)
            {
                target.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f).OnComplete(ScaleDown);
            }
        }
        
        private void ScaleDown()
        {

            foreach (var target in _correctTweenTargets)
            {
                target.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(DisableTile);
            }
        }

        private void DisableTile()
        {
            foreach (var target in _correctTweenTargets)
            {
                target.GetComponent<TileScript>()._entity.Del<Blank>();
                _correctTweenTargets.Remove(target);

                if (_blankFilter.IsEmpty())
                {
                    ResetGame();
                }
            }
        }

        private void ResetGame()
        {
            _config._replay = true;
        }
    }
}*/
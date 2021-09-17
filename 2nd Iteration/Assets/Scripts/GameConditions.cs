using System.Collections.Generic;
using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Gradient
{
    internal class GameConditions : IEcsRunSystem
    {
        private EcsWorld _world;
        private Configuration _config;
        private EcsFilter<GameCheck> _filter;
        private EcsFilter<Blank> _blankFilter;
        private EcsFilter<Checking> _checkFilter;

        private List<Transform> _correctTweenTargets = new List<Transform>();
        private List<Transform> _wrongTweenTargets = new List<Transform>();

        private float _timer = 2f;
        
        public void Run()
        {
            if (!_checkFilter.IsEmpty())
            {
                if (_timer > 0)
                {
                    _timer -= Time.deltaTime;
                }
                else
                {
                    _checkFilter.GetEntity(0).Del<Checking>();
                    _timer = 2f;
                }
            }
            
            if (_blankFilter.IsEmpty())
            {
                ResetGame();
            }
            
            if(_filter.IsEmpty()) return;
            
            foreach (var index in _blankFilter)
            {
                var tile = _blankFilter.GetEntity(index);

                if (tile.Get<TileAvatar>().Avatar.GetComponent<Image>().color ==
                    tile.Get<TileAvatar>().CorrectColor)
                {
                    _correctTweenTargets.Add(tile.Get<TileAvatar>().Avatar.transform);
                    ScaleUp();
                }
                else
                {
                    _wrongTweenTargets.Add(tile.Get<TileAvatar>().Avatar.transform);
                    ShakeTile();
                }
            }
        }

        private void ShakeTile()
        {
            foreach (var target in _wrongTweenTargets)
            {
                var crossMark = GameObject.Instantiate(_config.CrossMarkPrefab, target);//_config.WorkAreaCanvas);
                var markRect = crossMark.GetComponent<RectTransform>();
                markRect.sizeDelta = target.GetComponent<RectTransform>().sizeDelta;
                
                var entity = _world.NewEntity();
                    entity.Get<TileAvatar>().Avatar = crossMark;
                    entity.Get<Mark>();

                    crossMark.transform.DOScale(Vector3.zero, 0.7f).From();
                target.DOShakePosition(_config.MarkTimer, new Vector3(0.5f, 0, 0), 5, 0f, false, true);
            }
            
            _wrongTweenTargets.Clear();
        }

       private void ScaleUp()
        {
            foreach (var target in _correctTweenTargets)
            {
                var checkMark = GameObject.Instantiate(_config.CheckMarkPrefab, target);
                var markRect = checkMark.GetComponent<RectTransform>();
                markRect.sizeDelta = target.GetComponent<RectTransform>().sizeDelta;
                
                var entity = _world.NewEntity();
                entity.Get<TileAvatar>().Avatar = checkMark;
                entity.Get<Mark>();
                
                checkMark.transform.DOScale(Vector3.zero, _config.MarkTimer).From().OnComplete(DisableTile);
            }
        }

        private void DisableTile()
        {
            foreach (var target in _correctTweenTargets)
            {
                target.GetComponent<TileScript>().Entity.Del<Blank>();
                _correctTweenTargets.Remove(target);
            }
        }

        private void ResetGame()
        {
            _config.Replay = true;
            _config.Score += 100;
        }
    }
}
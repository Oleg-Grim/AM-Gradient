using System.Collections.Generic;
using System.Diagnostics;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Gradient
{
    internal class SelectBlankTiles : IEcsInitSystem, IEcsRunSystem
    {
        private Configuration _config;
        private EcsFilter<TileAvatar>.Exclude<Blank> _filter;
        private EcsFilter<TileAvatar, Blank> _blankFilter;

        private List<Color> _availableColors;

        public void Init()
        {
            var randomBlankIndex = Random.Range(_config.BlankMin, _config.Blankax);
            _availableColors = new List<Color>();

            for (int i = 0; i < randomBlankIndex; i++)
            {
                var tile = _filter.GetEntity(Random.Range(0, _filter.GetEntitiesCount()));
                tile.Get<Blank>();
                tile.Get<TileAvatar>().Avatar.GetComponent<Image>().color = Color.gray;
            }

            foreach (var index in _blankFilter)
            {
                var tile = _blankFilter.GetEntity(index);
                var tileColors = tile.Get<TileAvatar>().AvailableColors = new List<Color>();

                for (int i = 0; i < _blankFilter.GetEntitiesCount(); i++)
                {
                    tileColors.Add(_blankFilter.GetEntity(i).Get<TileAvatar>().CorrectColor);
                }

                tileColors.Remove(tile.Get<TileAvatar>().CorrectColor);
                
                tile.Get<TileAvatar>().WrongOne = tileColors[Random.Range(0, tileColors.Count)];
                tileColors.Remove(tile.Get<TileAvatar>().WrongOne);

                tile.Get<TileAvatar>().WrongTwo = tileColors[Random.Range(0, tileColors.Count)];
                tileColors.Clear();

                tile.Get<TileAvatar>().AvailableColors.Add(tile.Get<TileAvatar>().WrongOne);
                tile.Get<TileAvatar>().AvailableColors.Add(tile.Get<TileAvatar>().WrongTwo);
                tile.Get<TileAvatar>().AvailableColors.Add(tile.Get<TileAvatar>().CorrectColor);
            }

            _config.Rearrange = false;
        }

        public void Run()
        {
            if (_config.Rearrange)
                Init();
        }
    }
}
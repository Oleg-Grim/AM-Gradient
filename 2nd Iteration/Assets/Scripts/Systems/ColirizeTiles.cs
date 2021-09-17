using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Gradient
{
    internal class ColirizeTiles : IEcsInitSystem, IEcsRunSystem
    {
        private Configuration _config;
        private EcsFilter<TileAvatar, Position> _filter;

        public void Init()
        {
            _config.ScoreText.GetComponentInChildren<Text>().text = _config.Score.ToString();

            var randomColorIndex= Random.Range(0, _config.Colors.Length);
            
            foreach (var index in _filter)
            {
                var tile = _filter.GetEntity(index);

                tile.Get<TileAvatar>().Avatar.GetComponent<Image>().color = Color.Lerp(_config.Colors[randomColorIndex],
                    _config.BottomCOlor, (1f/ (_config.Rows + 1) * tile.Get<Position>().Row));

                tile.Get<TileAvatar>().Avatar.GetComponent<Image>().color = Color.Lerp(
                    tile.Get<TileAvatar>().Avatar.GetComponent<Image>().color, _config.SideColor,
                    (1f / (_config.Columns + 1) * tile.Get<Position>().Column));

                tile.Get<TileAvatar>().CorrectColor = tile.Get<TileAvatar>().Avatar.GetComponent<Image>().color;
            }
            
            _config.Replay = false;
            _config.Rearrange = true;
        }

        public void Run()
        {
            if (_config.Replay)
            {
                Init();
            }
        }
    }
}
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Gradient
{
    internal class InitWorkArea : IEcsInitSystem
    {
        private EcsWorld _world;
        private Configuration _config;

        public void Init()
        {
            Vector2 WorkAreaSize = new Vector2(_config.WorkAreaCanvas.GetComponent<RectTransform>().rect.width,
                _config.WorkAreaCanvas.GetComponent<RectTransform>().rect.height);
            
            for (int i = 0; i < _config.Rows; i++)
            {
                for (int j = 0; j < _config.Columns; j++)
                {
                    var tile = _world.NewEntity();
                    var tmp = GameObject.Instantiate(_config.ImagePrefab, _config.WorkAreaCanvas);
                    
                    tile.Get<TileAvatar>().Avatar = tmp;
                    tile.Get<Position>().Row = i;
                    tile.Get<Position>().Column = j;
                    tile.Get<TileAvatar>().Avatar.AddComponent<TileScript>();
                    tile.Get<TileAvatar>().Avatar.GetComponent<TileScript>().Entity = tile;
                }
            }
            
            var controlEntity = _world.NewEntity();
            controlEntity.Get<Control>();

            _config.CheckButton.gameObject.GetComponent<ButtonScript>().Control = controlEntity;
            
            var grid = _config.WorkAreaCanvas.GetComponent<GridLayoutGroup>();

            grid.constraintCount = _config.Columns;
            grid.cellSize = new Vector2(WorkAreaSize.x / _config.Columns, WorkAreaSize.y / _config.Rows);
        }
    }
}
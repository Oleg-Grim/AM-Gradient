using Leopotam.Ecs;
using UnityEngine.UI;

namespace Gradient
{
    internal class ColorSwipe : IEcsRunSystem
    {
        private EcsFilter<Selected> _filter;

        public void Run()
        {
            if (_filter.IsEmpty()) return;
            
            foreach (var index in _filter)
            {
                var tile = _filter.GetEntity(index);
                ref var tileAvatar = ref _filter.GetEntity(index).Get<TileAvatar>();


                if (tileAvatar.CurrentIndex + 1 < tileAvatar.AvailableColors.Count)
                {
                    tileAvatar.CurrentIndex++;
                }
                else
                {
                    tileAvatar.CurrentIndex = 0;
                }

                tile.Get<TileAvatar>().Avatar.GetComponent<Image>().color =
                    tileAvatar.AvailableColors[tileAvatar.CurrentIndex];
            }
        }
    }
}
using Leopotam.Ecs;
using UnityEngine;

namespace Gradient
{
    internal class DestroyMarks : IEcsRunSystem
    {
        private Configuration _config;
        private EcsFilter<Mark> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var mark = _filter.GetEntity(index);

                if (!mark.Get<Mark>().IsActive)
                {
                    mark.Get<Mark>().Timer = _config.MarkTimer;
                    mark.Get<Mark>().IsActive = true;
                }
                else
                {

                    if (mark.Get<Mark>().Timer >= 0)
                    {
                        mark.Get<Mark>().Timer -= Time.deltaTime;
                    }
                    else
                    {
                        GameObject.Destroy(mark.Get<TileAvatar>().Avatar);
                        mark.Destroy();
                    }
                }
            }
        }
    }
}

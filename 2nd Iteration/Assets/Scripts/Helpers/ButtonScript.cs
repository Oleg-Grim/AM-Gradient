using Gradient;
using Leopotam.Ecs;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public EcsEntity Control;

    public void SetCheckGame()
    {
        if (!Control.Has<Checking>())
        {
            Control.Get<Checking>();
            Control.Get<GameCheck>();
        }
        
    }
}

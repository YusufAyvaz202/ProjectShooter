using System;
using Misc;
namespace Interfaces
{
    public interface ICollectible
    {
        void Collect(Action<CollectibleType> onCollect);
    }
}
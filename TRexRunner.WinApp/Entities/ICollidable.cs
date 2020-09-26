using Microsoft.Xna.Framework;

namespace TRexRunner.WinApp.Entities
{
    public interface ICollidable
    {
        Rectangle CollisionBox { get; }
    }
}

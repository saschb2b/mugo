using OpenTK;
using Engine.cgimin.object3d;

namespace Mugo
{
    public interface ITunnelSegementElementModel
    {
        Matrix4 Transformation { get; set; }
        Matrix4 DefaultTransformation { get; }

		float Radius { get; }

		BaseObject3D BaseObject { get; }

        void Draw();
        void UnLoad();
    }
}
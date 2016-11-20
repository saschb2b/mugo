using OpenTK;

namespace Mugo
{
    public interface ITunnelSegementElementModel
    {
        Matrix4 Transformation { get; set; }
        Matrix4 DefaultTransformation { get; }

		float Radius { get; }

        void Draw();
        void UnLoad();
    }
}
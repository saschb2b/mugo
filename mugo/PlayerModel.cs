using System;
using Engine.cgimin.object3d;
using Engine.cgimin.texture;
using OpenTK;

namespace Mugo
{
    class PlayerModel : ObjLoaderObject3D
    {
        private const String objFilePath = "data/objects/lumberJack.model";
        private const String texturePath = "data/textures/lumberJack_diffuse.png";
        private const String normalTexturePath = "data/textures/lumberJack_normal.png";

        public PlayerModel() : base(objFilePath)
        {
            TextureId = TextureManager.LoadTexture(texturePath);
            normalTextureId = TextureManager.LoadTexture(normalTexturePath);
            Transformation *= Matrix4.CreateScale(0.6f);
            Transformation *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(180));
            Transformation *= Matrix4.CreateTranslation(2.95f, 0.7f, -0.8f);

			DefaultTransformation = Transformation;
        }

        public int TextureId { get; private set; }
        public int normalTextureId { get; private set; }

        public Matrix4 DefaultTransformation { get; }

		public void ResetZTransformation()
		{
            Vector3 trans = Transformation.ExtractTranslation();
            Transformation = Transformation.ClearTranslation();
            Transformation *= Matrix4.CreateTranslation(trans.X, trans.Y, -0.8f);
        }
    }
}

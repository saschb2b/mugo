using Engine.cgimin.object3d;
using Engine.cgimin.texture;
using OpenTK;
using System;

namespace Examples.Tutorial
{
    class PlayerModel : ObjLoaderObject3D
    {
        private const String objFilePath = "data/objects/lumberJack.model";
        private const String texturePath = "data/textures/lumberJack_diffuse.png";

        public PlayerModel() : base(objFilePath)
        {
            TextureId = TextureManager.LoadTexture(texturePath);
            Transformation *= Matrix4.CreateScale(0.4f);
            Transformation *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(180));
            Transformation *= Matrix4.CreateTranslation(1, 0.4f, -0.5f);
        }

        public int TextureId { get; private set; }
    }
}

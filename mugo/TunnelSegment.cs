using System;
using System.Collections.Generic;
using Engine.cgimin.material.normalmapping;
using Engine.cgimin.object3d;
using Engine.cgimin.texture;
using OpenTK;

using static Mugo.TunnelSegmentConfig;
using Engine.cgimin.material.normalmappingfog;

namespace Mugo
{
	static class TunnelSegmentConfig
	{
		public const float Width = 6;
		public const int RailCount = 3;
		public const int Height = 3;
		public const int Depth = 20;
		public const float RailWidth = Width / RailCount;
    }

	class TunnelSegment : ClonedObject<TunnelSegmentInternal>
	{
		private readonly Dictionary<int, ITunnelSegementElementModel> elements = new Dictionary<int, ITunnelSegementElementModel> (RailCount);
        private string[] textures = { "textures.jpg", "textures2.jpg", "textures3.jpg" };
        Random rand = new Random();

        public TextureHolder Textures {
			get;
			set;
		}

		public NormalMappingMaterialFog Material {
			get;
			set;
		}

		public IReadOnlyDictionary<int, ITunnelSegementElementModel> Elements => elements;

		public TunnelSegment ()
		{
			Textures = TextureLoader.Load ("data/textures/" + textures[rand.Next(0, textures.Length)]);
			Material = new NormalMappingMaterialFog ();
		}

		public void SetElementAtPosition(int index, ITunnelSegementElementModel element)
		{
			if (index < 0 || index >= TunnelSegmentConfig.RailCount)
				throw new ArgumentOutOfRangeException (nameof (index));

			ITunnelSegementElementModel previousElement;

			if (elements.TryGetValue (index, out previousElement)) {
				previousElement.UnLoad ();
				elements.Remove (index);
			}

			if (element != null) {
				element.Transformation *= Matrix4.CreateTranslation (RailWidth * index + (RailWidth / 2), 0f, 0f);
				elements [index] = element;
			}
		}

		public void Draw()
		{
			Material.Draw(this, Textures.TextureId, Textures.NormalMapId, 1f);

			foreach (var element in elements.Values) {
				element.Draw();
			}
		}

		public override void UnLoad()
		{
			foreach (var element in elements.Values) {
				element.UnLoad();
			}

			base.UnLoad();
		}
	}

	internal class TunnelSegmentInternal : BaseObject3D
	{
        private const float parts = Depth/10;
		public TunnelSegmentInternal()
        {
            for (int i = 1; i <= parts; i++)
            {
                float z = Depth / parts * i * -1;
                float zPrev = Depth / parts * (i-1) * -1;
                //left
                addTriangle(
                    new Vector3(0, Height, z), new Vector3(0, Height, zPrev), new Vector3(0, 0, zPrev),
                    Vector3.One, Vector3.One, Vector3.One,
                    new Vector2(0.5f, 0.4f), new Vector2(0, 0.4f), new Vector2(0f, 0.7f));

                addTriangle(
                    new Vector3(0, 0, zPrev), new Vector3(0, 0, z), new Vector3(0, Height, z),
                    Vector3.One, Vector3.One, Vector3.One,
                    new Vector2(0f, 0.7f), new Vector2(0.5f, 0.7f), new Vector2(0.5f, 0.4f));

                //right
                addTriangle(
                    new Vector3(Width, 0, zPrev), new Vector3(Width, Height, zPrev), new Vector3(Width, Height, z),
                    Vector3.One, Vector3.One, Vector3.One,
                    new Vector2(0f, 0.7f), new Vector2(0f, 0.4f), new Vector2(0.5f, 0.4f));

                addTriangle(
                    new Vector3(Width, Height, z), new Vector3(Width, 0, z), new Vector3(Width, 0, zPrev),
                    Vector3.One, Vector3.One, Vector3.One,
                    new Vector2(0.5f, 0.4f), new Vector2(0.5f, 0.7f), new Vector2(0f, 0.7f));

                //bottom
                for (int j = 0; j < RailCount; j++)
                {
                    addTriangle(
                    new Vector3(RailWidth + (RailWidth * j), 0, zPrev), new Vector3(RailWidth * j, 0, z), new Vector3(RailWidth * j, 0, zPrev),
                    Vector3.One, Vector3.One, Vector3.One,
                    new Vector2(0.3f, 0.4f), new Vector2(0, 0), new Vector2(0, 0.4f));

                    addTriangle(
                        new Vector3(RailWidth + (RailWidth * j), 0, zPrev), new Vector3(RailWidth + (RailWidth * j), 0, z), new Vector3(RailWidth * j, 0, z),
                        Vector3.One, Vector3.One, Vector3.One,
                        new Vector2(0.3f, 0.4f), new Vector2(0.3f, 0f), new Vector2(0f, 0f));
                }


                //top
                addTriangle(
                    new Vector3(0, Height, zPrev), new Vector3(0, Height, z), new Vector3(Width, Height, zPrev),
                    Vector3.One, Vector3.One, Vector3.One,
                    new Vector2(0f, 0.4f), new Vector2(0.5f, 0.4f), new Vector2(0f, 0.7f));

                addTriangle(
                    new Vector3(0, Height, z), new Vector3(Width, Height, z), new Vector3(Width, Height, zPrev),
                    Vector3.One, Vector3.One, Vector3.One,
                    new Vector2(0.5f, 0.4f), new Vector2(0.5f, 0.7f), new Vector2(0f, 0.7f));
            }

            averageTangents();

            CreateVAO();
        }
    }
}
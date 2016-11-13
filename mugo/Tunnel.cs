using System;
using Engine.cgimin.material.normalmapping;
using Engine.cgimin.material;
using OpenTK;
using System.Collections.Generic;
using OpenTK.Graphics.ES10;

namespace Mugo
{
	public class Tunnel
	{
		private readonly List<TunnelSegment> segments;
		private readonly NormalMappingMaterial normalMappingMaterial;

		public Tunnel()
        {
            segments = new List<TunnelSegment>() {
                new TunnelSegment(),
                new TunnelSegment(),
                new TunnelSegment(),
                new TunnelSegment(),
                new TunnelSegment(),
                new TunnelSegment(),
                new TunnelSegment(),
                new TunnelSegment(),
            };

            normalMappingMaterial = new NormalMappingMaterial();

            CalculateTransformations();
        }

        private void CalculateTransformations()
        {
            for (var i = 0; i < segments.Count; i++)
            {
                var segment = segments[i];
                var zOffset = Matrix4.CreateTranslation(0, 0, (i - 2) * -TunnelSegment.Depth);

                segment.Transformation = zOffset;

                foreach (var element in segment.Elements.Values)
                {
                    var baseTranslation = Matrix4.CreateTranslation(element.DefaultTransformation.ExtractTranslation());
                    baseTranslation *= Matrix4.CreateTranslation(element.Transformation.ExtractTranslation().X, 0f, 0f);

                    element.Transformation = element.Transformation.ClearTranslation() * baseTranslation * zOffset;
                }
            }
        }

	    public void GenerateNextSegment(TunnelSegment nextSegement)
	    {
            segments[0].UnLoad();
            segments.RemoveAt(0);

			segments.Add(nextSegement ?? new TunnelSegment());

            CalculateTransformations();
	    }

        public void Draw()
		{
			foreach(var segment in segments) {
				segment.Draw();
            }
		}

		public void UnLoad() 
		{
			foreach(var segment in segments) {
				segment.UnLoad();
			}
		}
	}
}


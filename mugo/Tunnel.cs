using System;
using Engine.cgimin.material.normalmapping;
using Engine.cgimin.material;
using OpenTK;
using System.Collections.Generic;

namespace Mugo
{
	class Tunnel
	{
		private readonly List<TunnelSegment> segments;
		private const int backSegments = 2;

		public Tunnel()
        {
            segments = new List<TunnelSegment>() {
                new TunnelSegment(),
                new TunnelSegment(),
                new TunnelSegment(),
                new TunnelSegment(),
                new TunnelSegment(),
                new TunnelSegment(),
            };

            CalculateTransformations();
        }

	    public IReadOnlyList<TunnelSegment> Segements => segments;

		public float Length => (Segements.Count - backSegments) * TunnelSegmentConfig.Depth;

	    private void CalculateTransformations()
        {
            for (var i = 0; i < segments.Count; i++)
            {
                var segment = segments[i];
				var zOffset = Matrix4.CreateTranslation(0, 0, (i - backSegments) * -TunnelSegmentConfig.Depth);

                segment.Transformation = zOffset;

                foreach (var element in segment.Elements.Values)
                {
                    var baseTranslation = Matrix4.CreateTranslation(element.DefaultTransformation.ExtractTranslation());
                    baseTranslation *= Matrix4.CreateTranslation(element.Transformation.ExtractTranslation().X, 0f, 0f);

                    element.Transformation = element.Transformation.ClearTranslation() * baseTranslation * zOffset;
                }
            }
        }

		public TunnelSegment CurrentSegment => Segements[2];

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


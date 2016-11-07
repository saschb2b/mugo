﻿using System;
using Engine.cgimin.material.simpletexture;
using Engine.cgimin.material;
using OpenTK;
using System.Collections.Generic;
using OpenTK.Graphics.ES10;

namespace Mugo
{
	public class Tunnel
	{
		private readonly List<TunnelSegment> segments;
		private readonly SimpleTextureMaterial material;

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

            material = new SimpleTextureMaterial();

            CalculateTransformations();
        }

        private void CalculateTransformations()
        {
            for (var i = 0; i < segments.Count; i++)
            {
                segments[i].Transformation = Matrix4.CreateTranslation(0, 0, (i - 2) * -TunnelSegment.Depth);
            }
        }

	    public void GenerateNextSegment(Action<TunnelSegment> segementModifier)
	    {
            segments[0].UnLoad();
            segments.RemoveAt(0);

            var newSegment = new TunnelSegment();
            segementModifier?.Invoke(newSegment);
            segments.Add(newSegment);

            CalculateTransformations();
	    }

        public void Draw()
		{
			foreach(var segment in segments) {
				material.Draw(segment, segment.TextureId);
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


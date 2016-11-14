﻿
// This code was written for the OpenTK library and has been released
// to the Public Domain.
// It is provided "as is" without express or implied warranty of any kind.
using Engine;

#region --- Using Directives ---

using System;
using System.IO;
using System.Linq;
using Engine.cgimin.camera;
using Engine.cgimin.material.simpletexture;
using Engine.cgimin.material.normalmapping;
using Engine.cgimin.texture;
using Engine.cgimin.light;
using Engine.cgimin.object3d;
using OpenTK;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

#endregion --- Using Directives ---

namespace Mugo
{
    public class MugoGame : GameWindow
	{
        private PlayerModel player;
        private CartModel cart;

        private SimpleTextureMaterial simpleTextureMaterial;
        private NormalMappingMaterial normalMappingMaterial;

        private KeyboardState keyboardState, lastKeyboardState;

        private Tunnel tunnel;
        
        private float zMover = 0.0f;
        private float xMover = 0.0f;

		private readonly Matrix4 railStep = Matrix4.CreateTranslation(TunnelSegment.RailWidth, 0, 0);
		private readonly Matrix4 railStepNegative = Matrix4.CreateTranslation(-TunnelSegment.RailWidth, 0, 0);

		private int playerLaneIndex = 1;

        private Random random = new Random();

		private Sound backgroundMusic;

		public MugoGame()
			: base(800, 600, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 3, GraphicsContextFlags.Default)
		{
		}
        
       
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			Camera.Init();
			Camera.SetWidthHeightFov(800, 600, 60);

            player = new PlayerModel();

			cart = new CartModel();

			tunnel = new Tunnel();

            simpleTextureMaterial = new SimpleTextureMaterial();
            normalMappingMaterial = new NormalMappingMaterial();

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);

            GL.Enable(EnableCap.DepthTest);
			GL.ClearColor(0.5f, 0.5f, 0.5f, 0.5f);
            
			Camera.SetLookAt(new Vector3(1f, 0.5f, 1.5f), new Vector3(1f, 0.5f, -10), new Vector3(0, 1, 0));

			Sound.Init();
			backgroundMusic = new Sound("data/audio/background.wav", looping: true);
			backgroundMusic.Play();

			Camera.SetLookAt(new Vector3(3f, 0.5f, 1.5f), new Vector3(3f, 0.5f, -10), new Vector3(0, 1, 0));
		}

		protected override void OnUnload(EventArgs e)
		{
			tunnel.UnLoad();
            player.UnLoad();
            cart.UnLoad();

			backgroundMusic.UnLoad();
        }
       
		protected override void OnResize(EventArgs e)
		{
			GL.Viewport(0, 0, Width, Height);

			float aspect_ratio = Width / (float)Height;
			Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref perpective);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
            // Get current state
            keyboardState = OpenTK.Input.Keyboard.GetState();
            if (Keyboard[Key.Escape])
				this.Exit();

			if (Keyboard[Key.F11])
				if (WindowState != WindowState.Fullscreen)
					WindowState = WindowState.Fullscreen;
				else
					WindowState = WindowState.Normal;

            const float step = 0.1f;

            if (KeyWasPressed(Key.Right))
            {
                if (xMover <= 0 && xMover <= 1.0f)
                {
                    xMover += 1.0f;
					player.Transformation *= railStep;
					cart.Transformation *= railStep;

					playerLaneIndex += 1;
                }
                else
                {
                    xMover = 1.0f;
                }
            }
            else if (KeyWasPressed(Key.Left))
            {
                if (xMover >= 0) 
				{
					xMover -= 1.0f;
					player.Transformation *= railStepNegative;
					cart.Transformation *= railStepNegative;

					playerLaneIndex -= 1;
                } 
				else
                {
                    xMover = -1.0f;
                }
            }

			if (KeyWasPressed(Key.M))
			{
				if(backgroundMusic.State == ALSourceState.Playing)
				{
					backgroundMusic.Pause();
				} 
				else
				{
					backgroundMusic.Play();
				}
			}

            // Store current state for next comparison;
            lastKeyboardState = keyboardState;

            if (zMover <= -TunnelSegment.Depth)
            {
                zMover = 0.0f;
				player.ResetZTransformation();
				cart.ResetZTransformation();

                var nextSegement = new TunnelSegment();
                nextSegement.SetElementAtPosition(random.Next(TunnelSegment.RailCount), new PizzaModel());
                tunnel.GenerateNextSegment(nextSegement);
            } 

			ITunnelSegementElementModel element;

			if (tunnel.CurrentSegment.Elements.TryGetValue(playerLaneIndex, out element))
			{
				if (Math.Abs(player.Transformation.ExtractTranslation().Z - element.Transformation.ExtractTranslation().Z) < 0.00001)
				{
					tunnel.CurrentSegment.SetElementAtPosition(playerLaneIndex, null);
				}
			}

            zMover -= step;

            Camera.SetLookAt(new Vector3(3f, 2.0f, 3.0f + zMover), new Vector3(3f +xMover, 0.5f, -5 + zMover), new Vector3(0, 1, 0));
            // Licht setzen
            Light.SetDirectionalLight(new Vector3(1f, 0.5f, -5), new Vector4(1.0f, 0.94f, 0.9f, 0.1f), new Vector4(1.0f, 1.0f, 1.0f, 0.0f), new Vector4(0.2f, 0.2f, 0.2f, 0.1f));

            player.Transformation *= Matrix4.CreateTranslation(0, 0, -step);
            cart.Transformation *= Matrix4.CreateTranslation(0, 0, -step);
        }

        private bool KeyWasPressed(Key key)
        {
            return (keyboardState[key] && (keyboardState[key] != lastKeyboardState[key]));
        }

        protected override void OnRenderFrame(FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit |
				ClearBufferMask.DepthBufferBit);

            var pizzas =
                from segment in tunnel.Segements
                from element in segment.Elements.Values
                where element is PizzaModel
                select element;

            foreach (var pizza in pizzas)
            {
                var transformation = pizza.Transformation;
                pizza.Transformation = transformation.ClearTranslation() * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(1)) * Matrix4.CreateTranslation(transformation.ExtractTranslation());
            }

            tunnel.Draw();
            normalMappingMaterial.Draw(player, player.TextureId, player.normalTextureId, 1.0f);
            simpleTextureMaterial.Draw(cart, cart.TextureId);

            SwapBuffers();
		}
        
		[STAThread]
		public static void Main()
		{
			using (MugoGame example = new MugoGame()) {
				// Get the title and category  of this example using reflection.
				//ExampleAttribute info = ((ExampleAttribute)example.GetType().GetCustomAttributes(false)[0]);
				//example.Title = String.Format("OpenTK | {0} {1}: {2}", info.Category, info.Difficulty, info.Title);
				example.Run(30.0, 0.0);
			}
		}
	}
}
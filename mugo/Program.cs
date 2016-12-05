
// This code was written for the OpenTK library and has been released
// to the Public Domain.
// It is provided "as is" without express or implied warranty of any kind.
using Engine;
using Engine.cgimin.sound;
using Engine.cgimin.text;

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
using System.Drawing;

#endregion --- Using Directives ---

namespace Mugo
{
    public class MugoGame : GameWindow
	{
		private static readonly CommandLineOptions options = new CommandLineOptions ();
		private static Random random;
		private static String hudInformationFormat = "speed: {0}, distance: {1}, pizza: {2}";

        private PlayerModel player;
        private CartModel cart;

        private SimpleTextureMaterial simpleTextureMaterial;
        private NormalMappingMaterial normalMappingMaterial;

        private KeyboardState keyboardState, lastKeyboardState;

        private Tunnel tunnel;
        
        private float zMover = 0.0f;
        private float xMover = 0.0f;
        private float xMoverAppr = 0.0f;
        private float yMover = 0.0f;
        private float yMoverVelocity = 0.0f;
        private float yMoverAppr = 0.0f;

        private float step = 0.1f;
        private int fov = 60;

		private int playerLaneIndex = 1;

		private Sound backgroundMusic;
		private Sound pizzaCollectSound;
		private Sound cartLandingSound;

	    private DrawableString pizzaCounterString;
	    private int pizzaCounter;
		private int distanceCounter;

		public MugoGame()
			: base(800, 600, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 3, 3, GraphicsContextFlags.Default)
		{
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			Camera.Init();
			Camera.SetWidthHeightFov(800, 600, fov);
			InitFog ();

            player = new PlayerModel();

			cart = new CartModel();

			tunnel = new Tunnel();

			RockModel.Init ();
			PizzaModel.Init ();

			simpleTextureMaterial = new SimpleTextureMaterial();
            normalMappingMaterial = new NormalMappingMaterial();

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);

            GL.Enable(EnableCap.DepthTest);
			GL.ClearColor(Color.Black);
            
			Camera.SetLookAt(new Vector3(1f, 0.5f, 1.5f), new Vector3(1f, 0.5f, -10), new Vector3(0, 1, 0));

			Sound.Init();
			backgroundMusic = new Sound("data/audio/background.wav", looping: true);
			backgroundMusic.Gain = 0.5f;
			if (!options.NoBackgroundMusic) {
				backgroundMusic.Play ();
			}

			pizzaCollectSound = new Sound("data/audio/pizza_collect.wav");

			cartLandingSound = new Sound("data/audio/cart_landing.wav");

		    pizzaCounter = 0;
			distanceCounter = 0;
			CreateHud ();

			Camera.SetLookAt(new Vector3(3f, 0.5f, 1.5f), new Vector3(3f, 0.5f, -10), new Vector3(0, 1, 0));
		}

		protected override void OnUnload(EventArgs e)
		{
			tunnel.UnLoad();
            player.UnLoad();
            cart.UnLoad();

			backgroundMusic.UnLoad();
			pizzaCollectSound.UnLoad();
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
            if (KeyWasPressed(Key.Escape))
				this.Exit();

			if (KeyWasPressed(Key.F11))
				if (WindowState != WindowState.Fullscreen)
					WindowState = WindowState.Fullscreen;
				else
					WindowState = WindowState.Normal;

            if (KeyWasPressed(Key.Right) || KeyWasPressed(Key.D))
            {
                if (yMoverAppr == 0f)
                {
                    yMover = 0.2f;
                    yMoverVelocity = 4f;
                }
                if (xMover < 1)
                {
                    xMover += 1.0f;
					playerLaneIndex += 1;
                }
                else
                {
                    xMover = 1.0f;
                }
            }
            else if (KeyWasPressed(Key.Left) || KeyWasPressed(Key.A))
            {
                if(yMoverAppr == 0f)
                {
                    yMover = 0.2f;
                    yMoverVelocity = 4f;
                }
                if (xMover > -1) 
				{
					xMover -= 1.0f;
					playerLaneIndex -= 1;
                } 
				else
                {
                    xMover = -1.0f;
                }
            }
            else if (KeyWasPressed(Key.Up) || KeyWasPressed(Key.W))
            {
                if (yMoverAppr == 0f)
                {
                    yMover = 1.5f;
                    yMoverVelocity = 10f;
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

			CollisionCheck();

            // Store current state for next comparison;
            lastKeyboardState = keyboardState;

            if (zMover <= -TunnelSegmentConfig.Depth)
            {
                zMover = 0.0f;
				player.ResetZTransformation();
				cart.ResetZTransformation();
				InitFog ();

                GenerateNextTunnelSegment ();
                step *= 1.05f;
                if(fov < 90)
                {
                    fov += 2;
                    Camera.SetWidthHeightFov(800, 600, fov);
                }

				distanceCounter++;
            } 

            const float xMoverStep = 1f/3;
            if (xMoverAppr < xMover * TunnelSegmentConfig.RailWidth - 0.01f)
            {
                xMoverAppr += xMoverStep;
                player.Transformation *= Matrix4.CreateTranslation(xMoverStep, 0, 0);
                cart.Transformation *= Matrix4.CreateTranslation(xMoverStep, 0, 0);

                if (Math.Abs(xMoverAppr - xMover * TunnelSegmentConfig.RailWidth) < 0.001f)
                {
                    cartLandingSound.Play();
                }
            }
            else if (xMoverAppr > xMover * TunnelSegmentConfig.RailWidth + 0.01f)
            {
                xMoverAppr -= xMoverStep;
                player.Transformation *= Matrix4.CreateTranslation(-xMoverStep, 0, 0);
                cart.Transformation *= Matrix4.CreateTranslation(-xMoverStep, 0, 0);

                if (Math.Abs(xMoverAppr - xMover * TunnelSegmentConfig.RailWidth) < 0.001f)
                {
                    cartLandingSound.Play();
                }
            }

            float yMoverStep = yMover / yMoverVelocity;
            if (Math.Abs(yMoverAppr - yMover * 2) > 0.01f)
            {
                yMoverAppr += yMoverStep;
                
                    if (yMoverAppr < yMover + 0.01f)
                    {
                        player.Transformation *= Matrix4.CreateTranslation(0, yMoverStep, 0);
                        cart.Transformation *= Matrix4.CreateTranslation(0, yMoverStep, 0);
                    }
                    else
                    {
                        player.Transformation *= Matrix4.CreateTranslation(0, yMoverStep * -1f, 0);
                        cart.Transformation *= Matrix4.CreateTranslation(0, yMoverStep * -1f, 0);
                    }
                if (Math.Abs(yMoverAppr - yMover * 2) < 0.01f)
                {
                    yMoverAppr = 0.0f;
                    yMover = 0.0f;

                    cartLandingSound.Play();
                }
            }
          
            zMover -= step;

            Camera.SetLookAt(new Vector3(3f, 2.0f, 3.0f + zMover), new Vector3(3f + xMoverAppr, 0.5f, -5 + zMover), new Vector3(0, 1, 0));
            // Licht setzen
            Light.SetDirectionalLight(new Vector3(1f, 0.5f, -5), new Vector4(1.0f, 0.94f, 0.9f, 0.1f), new Vector4(1.0f, 1.0f, 1.0f, 0.0f), new Vector4(0.2f, 0.2f, 0.2f, 0.1f));

            player.Transformation *= Matrix4.CreateTranslation(0, 0, -step);
            cart.Transformation *= Matrix4.CreateTranslation(0, 0, -step);

			Camera.FogStart += step;
			Camera.FogEnd += step;

			CreateHud ();
        }

        private bool KeyWasPressed(Key key)
        {
            return (keyboardState[key] && (keyboardState[key] != lastKeyboardState[key]));
        }

		private void CollisionCheck()
		{
			ITunnelSegementElementModel element;

			if (tunnel.CurrentSegment.Elements.TryGetValue (playerLaneIndex, out element)) {
				if (Math.Abs (player.Transformation.ExtractTranslation ().Z - element.Transformation.ExtractTranslation ().Z) <= element.Radius) {
					tunnel.CurrentSegment.SetElementAtPosition (playerLaneIndex, null);

					if (element is PizzaModel)
                    {
                        pizzaCollectSound.Play();

                        pizzaCounter++;
                    }
                    else if (element is RockModel) {
						Exit ();
					}
				}
			}
		}

		private void GenerateNextTunnelSegment()
		{
            var nextSegement = new TunnelSegment ();
			var obstaclePosition = random.Next (TunnelSegmentConfig.RailCount);

			ITunnelSegementElementModel nextObstacle;

			if (random.Next (2) != 0) {
				nextObstacle = new RockModel ();
			}
			else {
				nextObstacle = new PizzaModel ();
			}

			nextSegement.SetElementAtPosition (obstaclePosition, nextObstacle);
			tunnel.GenerateNextSegment (nextSegement);
		}

		private void CreateHud()
		{
			const float scale = 0.055f;

			pizzaCounterString?.UnLoad ();
			pizzaCounterString = new DrawableString (String.Format (hudInformationFormat, (int)(step * 100), distanceCounter, pizzaCounter));
			pizzaCounterString.Transformation *= Matrix4.CreateScale (scale);
			pizzaCounterString.Transformation *= Matrix4.CreateTranslation (-1f, 1f - scale, 0);
		}

		private void InitFog() 
		{
			Camera.SetupFog (15f, 65f, new Vector3 (0f, 0f, 0f));
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

			GL.BlendColor (Color.Black);
			pizzaCounterString.Draw(blendDest: BlendingFactorDest.ConstantColor);

            SwapBuffers();
		}
        
		[STAThread]
		public static void Main(String[] args)
		{
			CommandLine.Parser.Default.ParseArgumentsStrict (args, options);

			if (options.Seed.HasValue) {
				random = new Random (options.Seed.Value);
			} 
			else {
				random = new Random ();
			}

			using (MugoGame example = new MugoGame()) {
				// Get the title and category  of this example using reflection.
				//ExampleAttribute info = ((ExampleAttribute)example.GetType().GetCustomAttributes(false)[0]);
				//example.Title = String.Format("OpenTK | {0} {1}: {2}", info.Category, info.Difficulty, info.Title);
				example.Run(30.0, 0.0);
			}
		}
	}
}
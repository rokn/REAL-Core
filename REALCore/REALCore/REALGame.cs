using Microsoft.Xna.Framework;

namespace REALCore
{
	public class REALGame : Game
	{
		private readonly GraphicsDeviceManager _graphics;
		private readonly EntityWorld _currentScene;

		public REALGame(int resolutionWidth, int resolutionHeight, bool fullscreen)
			: this(resolutionWidth, resolutionHeight, fullscreen, new EntityWorld())
		{
		}

		public REALGame(int resolutionWidth, int resolutionHeight, bool fullscreen, EntityWorld startingScene)
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			_graphics.PreferredBackBufferWidth = resolutionWidth;
			_graphics.PreferredBackBufferHeight = resolutionHeight;
			_graphics.IsFullScreen = fullscreen;
			_graphics.ApplyChanges();

			_currentScene = startingScene;
		}

		protected override void Initialize()
		{
			Input.Initialize(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, 100f);
			_currentScene.GraphicsDevice = GraphicsDevice;
			_currentScene.Content = Content;
			_currentScene.Start();
			base.Initialize();
		}

		protected override void LoadContent()
		{
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			_currentScene.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_currentScene.Draw();
			_currentScene.GuiDraw();
			base.Draw(gameTime);
		}
	}
}

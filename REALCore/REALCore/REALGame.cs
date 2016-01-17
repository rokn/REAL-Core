using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace REALCore
{
	public class REALGame : Game
	{
		private readonly GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private readonly EntityWorld _world;

		public REALGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			_world = new EntityWorld();
		}

		protected override void Initialize()
		{
			Input.Initialize(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, 100f);
			_world.Start();
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			_world.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_world.Draw();
			_world.GuiDraw();
			base.Draw(gameTime);
		}
	}
}

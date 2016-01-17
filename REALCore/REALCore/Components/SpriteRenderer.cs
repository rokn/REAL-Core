using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace REALCore.Components
{
	public class SpriteRenderer : Component
	{
		public int DrawLayer { get; set; }
		public Texture2D Texture { get; set; }
		public Vector2 Origin { get; set; }
		public Color Color { get; set; }
		public float Transperency { get; set; }
		private Transform _transform;
		private readonly string _filename;

		public SpriteRenderer()
		{
			Texture = null;
			Color = Color.White;
			Transperency = 1.0f;
		}

		public int Width => Texture?.Width ?? 0;
		public int Height => Texture?.Height ?? 0;

		public override void Start()
		{
			_transform = Entity.GetComponent<Transform>();
			Texture = World.Content.Load<Texture2D>(_filename);
			Origin = new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f);
		}

		public SpriteRenderer(string filename)
		{
			_filename = filename;
			Color = Color.White;
			Transperency = 1.0f;
		}

		public override bool Draw()
		{
			if (Texture == null || _transform == null || !IsInView()) return false;

			World.MainCamera.CurrentBatch.Draw(Texture, _transform.Position, null, Color * Transperency, (float)_transform.Rotation, Origin, _transform.Scale, SpriteEffects.None, 1f);

			return true;
		}

		private bool IsInView()
		{
			var maxDimension = Math.Max(Width, Height);
			var pos = _transform.Position;

			//TODO: Make Camera check for point in view with transofrmations

			Rectangle container = new Rectangle((int)(pos.X - Origin.X), (int)(pos.Y - Origin.Y), maxDimension, maxDimension);

			return World.MainCamera.IsInView(container);
		}
	}
}

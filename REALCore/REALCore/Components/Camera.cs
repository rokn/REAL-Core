using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace REALCore.Components
{
	public class Camera : Component
	{
		private Transform _transform;
		private Point _cameraSize;
		private uint _layerMask;
		public Rectangle ViewRectangle;
		private bool _isActive;

		public Camera(Point cameraSize)
		{
			_cameraSize = cameraSize;
			ViewRectangle = new Rectangle(0,0,cameraSize.X,cameraSize.Y);
			_isActive = false;
		}

		public Matrix TransformMatrix { get; private set; }
		public SpriteBatch CurrentBatch { get;private set; }

		public override void Start()
		{
			_transform = Entity.GetComponent<Transform>();
			_transform.Position = new Vector2();
			ViewRectangle.UpdateViaVector(_transform.Position);
			CurrentBatch = new SpriteBatch(World.GraphicsDevice);
		}

		public override void Update()
		{
			CalculateMatrix();
			ViewRectangle = ViewRectangle.UpdateViaVector(TransformScreenCoordinates(new Vector2()));
			ViewRectangle.Width = (int)(_cameraSize.X * 1/_transform.Scale.X);
			ViewRectangle.Height = (int)(_cameraSize.Y * 1/_transform.Scale.Y);
		}

		public override bool Draw()
		{
			if (!_isActive) return base.Draw();

			CurrentBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,null,null,null,null,TransformMatrix);

			foreach (var component in WorldLayer.GetAllLayers()
				.OrderBy(layer => layer.Id)
				.Where(IsDrawingLayer)
				.SelectMany(World.GetEntitiesInLayer)
				.SelectMany(entity => entity.GetComponents()))
			{
				component.Draw();
			}

			CurrentBatch.End();

			return true;
		}

		public bool IsDrawingLayer(WorldLayer layer)
		{
			return (_layerMask & layer.Id) != 0;
		}

		public void AddDrawingLayer(WorldLayer layer)
		{
			if(layer.Id == Entity?.Layer)
				throw new ArgumentException("Can't add draw layer to camera which is equal to it's own layer");

			_layerMask |= layer.Id;
		}

		public void SetActive()
		{
			foreach (var camera in World.GetEntitiesWithComponent<Camera>().Select(entity => entity.GetComponent<Camera>()))
			{
				camera._isActive = false;
			}

			_isActive = true;
		}

		/// <summary>
		/// Move the camera by a given amount
		/// </summary>
		/// <param name="amount">By how much to move the camera</param>
		public void Move(Vector2 amount)
		{
			_transform.Position += amount;
		}

		/// <summary>
		/// Update the dimensions of the camera
		/// </summary>
		/// <param name="newScreenSize"></param>
		public void UpdateScreenDimensions(Point newScreenSize) => _cameraSize = newScreenSize;

		/// <summary>
		/// Transform screen coords into game coords
		/// </summary>
		/// <param name="screenPos">The coordinates on the screen</param>
		/// <returns>Coordinates in the game</returns>
		public Vector2 TransformScreenCoordinates(Vector2 screenPos)
		{
			return Vector2.Transform(screenPos, Matrix.Invert(TransformMatrix));
		}

		public bool IsInView(Vector2 point)
		{
			return ViewRectangle.Contains(point);
		}

		public bool IsInView(Rectangle rect)
		{
			return ViewRectangle.Intersects(rect);
		}

		/// <summary>
		/// Calculates the transform matrix of the camera
		/// </summary>
		private void CalculateMatrix()
		{
			if (_transform == null) return;

			TransformMatrix = Matrix.CreateTranslation(new Vector3(-_transform.Position.X, -_transform.Position.Y, 0)) *
										 Matrix.CreateRotationZ((float) _transform.Rotation) *
										 Matrix.CreateScale(new Vector3(_transform.Scale.X, _transform.Scale.Y, 1)) *
										 Matrix.CreateTranslation(new Vector3(_cameraSize.X/2.0f, _cameraSize.Y/2.0f, 0));
		}
	}
}

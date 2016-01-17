﻿using Microsoft.Xna.Framework;

namespace REALCore.Components
{
	public class Transform : Component
	{
		public Vector2 Position { get; set; }
		public double Rotation { get; set; }
		public Vector2 Scale { get; set; }

		public Transform()
		{
			Position = new Vector2();
			Rotation = 0.0;
			Scale = new Vector2(1.0f, 1.0f);
		}

		/// <summary>
		/// Set xScale and yScale to the same amount
		/// </summary>
		/// <param name="amount">The amount</param>
		public void SetScale(float amount)
		{
			Scale = new Vector2(amount);
		}
	}
}

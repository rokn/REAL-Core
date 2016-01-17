using System;
using Microsoft.Xna.Framework;

namespace REALCore.Components
{
	public class CircleCollider : Collider
	{
		public float Radius
		{
			get
			{
				return _radius;
			}

			set
			{
				if (value < 0)
				{
					throw new InvalidOperationException("Radius must be non negative.");
				}

				_radius = value;
			}
		}
		
		private float _radius;

		public override bool CheckCollision(Collider other)
		{
			var collided = false;

			var collider = other as CircleCollider;
			if (collider != null)
			{
				var distance = Vector2.Distance(Entity.GetComponent<Transform>().Position + Offset,
					collider.Entity.GetComponent<Transform>().Position + collider.Offset);

				if (distance <= Radius + collider.Radius)
				{
					OnCollideEvent(this, collider);
					collided = true;
				}
			}

			base.CheckCollision(other);

			return collided;
		}
	}
}

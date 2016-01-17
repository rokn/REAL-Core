using Microsoft.Xna.Framework;

namespace REALCore.Components
{
	public class SmoothFollowScript : Component
	{
		public bool IsFollowing { get; set; }
		public Transform Target { get; set; }
		public Vector2 Offset { get; set; }
		public float SmoothTime { get; set; }

		private Transform _transform;

		public SmoothFollowScript()
		{
			Target = new Transform();
			IsFollowing = false;
		}

		public SmoothFollowScript(Transform target)
		{
			Target = target;
			IsFollowing = true;
		}

		public override void Start()
		{
			_transform = Entity.GetComponent<Transform>();
		}

		public override void Update()
		{
			if (!IsFollowing || Target == null || _transform == null) return;


			Vector2 goalPos = Target.Position + Offset;
//			_transform.Position = Vector2.SmoothStep(_transform.Position, goalPos, SmoothTime * (float)deltaTime); // TODO : Delta time fix
		}
	}
}

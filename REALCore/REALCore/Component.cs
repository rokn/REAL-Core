namespace REALCore
{

	#region Component Interfaces
//
//	public interface IComponent
//	{
//		Entity Entity{ get; set; }
//		EntityWorld World{ get; set; }
//
//		void Start();
//		void Destroy();
//	}
//
//	public interface IUpdateableComponent : IComponent
//	{
//	}
//
//	public interface IDrawableComponent : IComponent
//	{
//	}
//
//	public interface IGuiDrawableComponent : IComponent
//	{
//	}

	#endregion

	public abstract class Component
	{
		#region Properties

		public Entity Entity { get; set; }
		public EntityWorld World { get; set; }

		#endregion

		#region Main functions

		public virtual void Start()
		{
		}

		public virtual void Update()
		{
		}

		public virtual void Destroy()
		{
		}

		public virtual bool Draw()
		{
			return false;
		}

		public virtual void GuiDraw()
		{
			
		}

		#endregion

		#region Equality functions

		public override bool Equals(object obj)
		{
			Component other = obj as Component;

			if (other == null)
				return false;

			return Entity.Equals(other.Entity) && GetType() == obj.GetType();
		}

		public override int GetHashCode()
		{
			var hash = 17;
			hash = hash*23 + GetType().GetHashCode();
			hash = hash*23 + Entity.GetHashCode();
			return hash;
		}

		#endregion

	}
}

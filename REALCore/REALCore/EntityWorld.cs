using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using REALCore.Components;

namespace REALCore
{
	public class EntityWorld
	{
		#region Fields

		private readonly List<Entity> _entities;
//		private readonly List<Entity> _entitiesInView;
		private readonly List<Entity> _removeList;
		private readonly List<Entity> _additionList;

		#endregion

		#region Constructors

		public EntityWorld()
		{
			_entities = new List<Entity>();
			_removeList = new List<Entity>();
			DrawCalls = 0;
//			_entitiesInView = new List<Entity>();
			_additionList = new List<Entity>();
		}

		#endregion

		public int DrawCalls { get; private set; }
		public bool Started { get; private set; }
		public Camera MainCamera { get; set; }
		public GraphicsDevice GraphicsDevice { get; set; }
		public ContentManager Content { get; set; }
		public int WindowWidth => GraphicsDevice.Viewport.Width;
		public int WindowHeight => GraphicsDevice.Viewport.Height;

		#region Entity Managment

		public void AddEntity(Entity entity)
		{
			_additionList.Add(entity);
			entity.World = this;
			entity.Started = Started;

			foreach (var component in entity.GetComponents())
			{
				component.World = this;

				if(Started)
					component.Start();
			}
		}

		//TODO: Make add/remove entity event

		public Entity GetEntityWithTag(string tag)
		{
			return _entities.FirstOrDefault(entity => tag == entity.Tag);
		}

		public IEnumerable<Entity> GetEntitiesWithTag(string tag)
		{
			return _entities.Where(entity => tag == entity.Tag);
		}

		public Entity GetEntityWithComponent<T>()
			where T : Component
		{
			return _entities.FirstOrDefault(entity => entity.HasComponent<T>());
		}

		public IEnumerable<Entity> GetEntitiesWithComponent<T>()
			where T : Component
		{
			return _entities.Where(entity => entity.HasComponent<T>());
		}

		public IEnumerable<Entity> GetEntitiesInLayer(WorldLayer layer)
		{
			return from entity in _entities
				   where entity.Layer == layer.Id
				   select entity;
		}

		public IEnumerable<Entity> GetEntitiesInLayers(uint layerMask)
		{
			return from entity in _entities
				   where (entity.Layer & layerMask) != 0
				   select entity;
		}

		//		public ReadOnlyCollection<Entity> EntitiesInView => _entitiesInView.AsReadOnly();	

		public void RemoveEntity(Entity entity)
		{
			_removeList.Add(entity);
		}

		#endregion

		#region Main Functionality

		public void Start()
		{
			UpdateAdditionList();

			foreach (var entity in _entities)
			{
				entity.Started = true;

				foreach(var component in entity.GetComponents())
				{
					component.Start();
				}
			}

			Started = true;
		}

		public void Update(GameTime gameTime)
		{
			foreach (var component in _entities.SelectMany(entity => entity.GetComponents()))
			{
				component.Update();
			}

			UpdateRemoveList();
			UpdateAdditionList();
		}

		public void Draw()
		{
			MainCamera?.Draw();

			if (MainCamera == null)
			{
				MainCamera = GetEntityWithComponent<Camera>().GetComponent<Camera>();
				MainCamera.SetActive(); //TODO Fix main camera selecting
			}
//			_entitiesInView.Clear();
//			DrawCalls = 0;
//
//			var query = _entities
//				.SelectMany(entity => entity.GetComponents()
//				.OfType<IDrawableComponent>());
//
//			var ordered = query.OrderBy(component => component.DrawLayer);
//
//			foreach (var component in ordered.Where(component => component.Draw()))
//			{
//				DrawCalls ++;
////				_entitiesInView.Add(component.Entity);
//			}
		}

		public void GuiDraw()
		{
			foreach (var component in _entities.OrderBy(entity => entity.Id)
											   .SelectMany(entity => entity.GetComponents()))
			{
				component.GuiDraw();
			}
		}

		public void Destroy()
		{
			foreach (var entity in _entities)
			{
				entity.Destroy();
			}

			UpdateRemoveList();
		}

		#endregion

		private void UpdateRemoveList()
		{
			foreach(var entity in _removeList)
			{
				_entities.Remove(entity);
			}

			_removeList.Clear();
		}

		private void UpdateAdditionList()
		{
			foreach(var entity in _additionList)
			{
				_entities.Add(entity);
			}

			_additionList.Clear();
		}
	}
}
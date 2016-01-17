using System;
using System.Linq;
using Microsoft.Xna.Framework;
using REALCore.Components;

namespace REALCore
{
	public class CameraP : Entity
	{
		public CameraP(string name) : base(name,"Camera")
		{
			Tag = "Camera";
			StartEvent += CameraP_StartEvent;
		}

		public CameraP()
			: this("MainCamera")
		{
		}

		private void CameraP_StartEvent(object sender, EventArgs e)
		{
			AttachComponent(new Transform());
			var camera = new Camera(new Point(World.WindowWidth, World.WindowHeight));
			AttachComponent(camera);

			foreach(var worldLayer in WorldLayer.GetAllLayers().Where(layer => layer.Id != Layer))
			{
				camera.AddDrawingLayer(worldLayer);
			}

		}
	}
}

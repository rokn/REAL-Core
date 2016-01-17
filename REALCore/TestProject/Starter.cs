using REALCore;
using REALCore.Components;

namespace TestProject
{
	class Starter
	{
		static void Main()
		{
			var testEntity = new Entity();
			testEntity.AttachComponent(new Transform());
			testEntity.AttachComponent(new SpriteRenderer("box"));
			var scene = new EntityWorld();
			var cameraEntity = new CameraP();
			scene.AddEntity(testEntity);
			scene.AddEntity(cameraEntity);
			scene.MainCamera = cameraEntity.GetComponent<Camera>();
			using (var game = new REALGame(800,480,false,scene))
			{
				game.Run();
			}
		}
	}
}
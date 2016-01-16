using REALCore;

namespace TestProject
{
	class Starter
	{
		static void Main()
		{
			using (var game = new REALGame())
			{
				game.Run();
			}
		}
	}
}

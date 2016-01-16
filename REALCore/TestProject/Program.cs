using REALCore;

namespace TestProject
{
	class Program
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

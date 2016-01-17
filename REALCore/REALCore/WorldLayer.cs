using System;
using System.Collections.Generic;

namespace REALCore
{
	public class WorldLayer
	{
		private const int MaxLayers = 32;
		private static int _layerCount = 0;
		private static uint _idCounter = 1;
		private static readonly Dictionary<string, WorldLayer> _layers;

		public uint Id { get; private set; }
		public string Name { get; private set; }

		static WorldLayer()
		{
			_layers = new Dictionary<string, WorldLayer>();
			AddLayer("Misc");
			AddLayer("Player");
			AddLayer("Background");
			AddLayer("Camera");
		}

		public static IEnumerable<WorldLayer> GetAllLayers()
		{
			return _layers.Values;
		}  

		public static WorldLayer GetLayer(string name)
		{
			return _layers.ContainsKey(name) ? _layers[name] : null;
		}

		public static uint AddLayer(string name)
		{
			if (++_layerCount >= MaxLayers)
			{
				throw new IndexOutOfRangeException("Layers count can't exceed the maximum of " + MaxLayers);
			}

			var id = _idCounter << 1;
			_layers.Add(name, new WorldLayer() {Id = _idCounter, Name = name});
			return (_idCounter = id);
		}
	}
}

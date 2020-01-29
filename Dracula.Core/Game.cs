using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dracula.Core
{
	public class Game
	{
		public List<Player> Players;
	}

	public class Player
	{
		public string Name;
		public int TopOffset;
		public int LeftOffset;
		public Character? SelectedCharacter;
	}

	public enum Character
	{
		Dracula,
		LordGodalming,
		DrJohnStewart,
		VanHelsing,
		MinaHarker,
	}
}

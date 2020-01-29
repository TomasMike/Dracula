using System.Collections.Generic;

namespace Dracula.Web
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

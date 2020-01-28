using System.Collections.Generic;
using System.Runtime.Serialization;

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
		//[EnumMember(Value = "Dracula")]
		Dracula,
		//[EnumMember(Value = "LordGodalming")]
		LordGodalming,
		//[EnumMember(Value = "DrJohnStewart")]
		DrJohnStewart,
		//[EnumMember(Value = "VanHelsing")]
		VanHelsing,
		//[EnumMember(Value = "MinaHarker")]
		MinaHarker
	}
}

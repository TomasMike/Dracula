using System.Collections.Generic;

namespace Dracula.Web
{
    public class Game
    {
        public Game()
        {
           
        }

        private void InitLocations()
        {
            Locations = new List<Location>()
            {
                new Location("",LocationType.LargeCity),

            };
        }

        private List<Player> Players;
        private List<Location> Locations;
        private int Influence;
        private object Time;
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

    public class Location
    {
        public Location(string name, LocationType locationType)
        {
            Name = name;
            LocationType = locationType;
        }

        public string Name;
        public List<(Location, LocationConnectionType)> ConnectedLocations;
        public LocationType LocationType;
    }



    public enum LocationType
    {
        LargeCity,
        SmallCity,
        SeaZone
    }

    public enum LocationConnectionType
    {
        Road,
        RailwayWhite,
        RailwayYellow
    }

    public enum MapZone
    {
        SpainPortugal,
        France,
        Italy,
        GreatBritain,
        GreeceRomania,
        HungaryCroatiaCzechSlovakia,
        Germany,
    }



}

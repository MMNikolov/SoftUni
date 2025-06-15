namespace Horizons.GCommon
{
    public static class ValidationConstants
    {
        public static class Destination
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 80;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 250;

            public const string DateFormat = "dd-MM-yyyy"; 
        }

        public static class Terrain
        {
            public const int TerrainNameMinLength = 3;
            public const int TerrainNameMaxLength = 20;
        }
    }
}

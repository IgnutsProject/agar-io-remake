public static class GameConfig
{
    public static int StartFoodCount => 4000;
    public static int StartEnemiesCount => 60;
    public static float IncreaseScaleFactor => 0.05f;
    
    public static float TargetScaleFactor => 0.2f;

    private static string[] _botsNames = { "Killer", 
        "Hunter1337", 
        "Bob", "Jojo", 
        "Arbyz",
        "Orange juice",
        "Poedatel",
        "Ubivatel",
        "Pro gamer",
        "Zoro",
        "Zoro 133",
        "Pavel",
        "S1mi;e",
        "sgsdg",
    };
    public static string[] BotsNames => _botsNames;
}

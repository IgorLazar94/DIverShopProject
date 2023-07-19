public struct FishburgerRecipe
{
    public int FishA { get; }
    public int FishB { get; }
    public int FishC { get; }

    public FishburgerRecipe(int fishA, int fishB, int fishC)
    {
        FishA = fishA;
        FishB = fishB;
        FishC = fishC;
    }

    public int CookIngredientOne()
    {
        int requiredFishA = 0;
        int resultA = FishA - requiredFishA;
        return resultA;
    }
    public int CookIngredientTwo()
    {
        int requiredFishB = 2;
        int resultB = FishB - requiredFishB;
        return resultB;
    }
    public int CookIngredientThree()
    {
        int requiredFishC = 2;
        int resultC = FishC - requiredFishC;
        return resultC;
    }
}

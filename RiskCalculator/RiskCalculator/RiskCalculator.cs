using System;
using System.Collections.Generic;

public class RiskCalc
{
    public static Random random;
    public RiskCalc()
    {

    }

    public int AArmyRemaining;
    public int DArmyRemaining;

    public bool Battle(int AArmy, int DArmy, int ADice, int DDice)
    {
        AArmy--; //the attacking army must leave one behind
        while (AArmy > 0 && DArmy > 0)
        {
            int ATempDice = AArmy > ADice ? ADice : AArmy;
            int DTempDice = DArmy > DDice ? DDice : DArmy;
            List<int> AResults = PlayerDiceRole(ATempDice);
            List<int> DResults = PlayerDiceRole(DTempDice);

            while (AResults.Count > 0 && DResults.Count > 0)
            {
                if (AResults[0] > DResults[0])
                {
                    //Attacker has won this die
                    DArmy--;
                }
                else
                {
                    //Defender has won this die
                    AArmy--;
                }
                DResults.RemoveAt(0);
                AResults.RemoveAt(0);
            }
        }
        AArmyRemaining = AArmy;
        DArmyRemaining = DArmy;
        return AArmy > DArmy;
    }
    public List<int> PlayerDiceRole(int count)
    {
        List<int> result = new List<int>();
        for (int x = 0; x < count; x++)
        {
            result.Add(DiceRoll);
        }
        result.Sort(delegate(int x, int y)
        {
           return y - x;
        });
        return result;
    }
    public static int DiceRoll
    {
        get
        {
            return Rand.Next(1, 7);
        }
    }
    public static Random Rand
    {
        get
        {
            if (random == null) random = new Random();
            return random;
        }
    }
}

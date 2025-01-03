[System.Serializable]
public class SubParty
{
    public string subPartyName; // Название подгруппы
    public int subPartyPopularity; // Популярность подгруппы (целое число)
    public bool isSubPartyRuling; // Является ли подгруппа правящей (true/false)
}

[System.Serializable]
public class PoliticalParty
{
    public string partyName; // Название глобальной партии
    public int partyPopularity; // Популярность глобальной партии (целое число)
    public bool isPartyRuling; // Является ли партия правящей (true/false)
    public SubParty[] subParties; // Массив подгрупп внутри глобальной партии
}

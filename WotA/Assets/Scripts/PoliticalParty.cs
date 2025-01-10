using UnityEngine;

[System.Serializable]
public class PoliticalParty
{
    public string partyName; // Название глобальной партии
    public int partyPopularity; // Популярность глобальной партии (целое число)
    public int partyLoyalty; // Лояльность глобальной партии, от -100 до 100
    public bool isPartyRuling; // Является ли партия правящей (true/false)
    public SubParty[] subParties; // Массив подгрупп внутри глобальной партии

    [System.Serializable]
    public class SubParty
    {
        public string subPartyName; // Название подгруппы
        public int subPartyPopularity; // Популярность подгруппы (целое число)
        public int subPartyLoyalty; // Лояльность подгруппы, от -100 до 100
        public bool isSubPartyRuling; // Является ли подгруппа правящей (true/false)

        // Вычисляемая лояльность: собственная лояльность + лояльность глобальной партии
        public int EffectiveLoyalty => subPartyLoyalty + (party != null ? party.partyLoyalty : 0);

        private PoliticalParty party; // Ссылка на родительскую партию

        public SubParty(string name, PoliticalParty parentParty)
        {
            subPartyName = name;
            subPartyPopularity = 0;
            subPartyLoyalty = 0; // Начальная лояльность
            isSubPartyRuling = false;
            party = parentParty; // Сохраняем ссылку на родительскую партию
        }
    }

    public PoliticalParty(string name)
    {
        partyName = name;
        partyPopularity = 0;
        partyLoyalty = 0; // Начальная лояльность
        isPartyRuling = false;
        subParties = new SubParty[0]; // Изначально нет подгрупп
    }

    // Метод для добавления подгруппы
    public void AddSubParty(string name)
    {
        var newSubParty = new SubParty(name, this);
        var tempList = new List<SubParty>(subParties) { newSubParty };
        subParties = tempList.ToArray();
    }
}

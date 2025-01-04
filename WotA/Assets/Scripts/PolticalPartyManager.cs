using UnityEngine;

public class PoliticalPartyManager : MonoBehaviour
{
    public PoliticalParty[] parties; // Массив глобальных партий

    void Start()
    {
        // Пример увеличения популярности первой подгруппы первой партии на 5
        AdjustPopularity(0, 0, 5); // Увеличиваем популярность первой подгруппы первой партии

        // Выводим результаты
        DisplayPartiesInfo();
    }

    public void AdjustPopularity(int partyIndex, int? subPartyIndex, int increaseAmount)
    {
        // Находим глобальную партию по индексу
        if (partyIndex < 0 || partyIndex >= parties.Length)
        {
            Debug.LogError($"Индекс партии {partyIndex} вне диапазона.");
            return;
        }

        PoliticalParty targetParty = parties[partyIndex];

        if (subPartyIndex.HasValue)
        {
            // Увеличиваем популярность конкретной подгруппы
            targetParty.subParties[subPartyIndex.Value].subPartyPopularity += increaseAmount;
        }
        else
        {
            // Увеличиваем или уменьшаем популярность глобальной партии
            targetParty.partyPopularity += increaseAmount;
        }

        // Нормализуем популярности подгрупп относительно глобальной партии
        NormalizeSubParties(partyIndex, subPartyIndex);
    }

    private void NormalizeSubParties(int partyIndex, int? changedSubPartyIndex)
    {
        PoliticalParty targetParty = parties[partyIndex];

        int totalSubPartyPopularity = 0;

        foreach (var subParty in targetParty.subParties)
        {
            totalSubPartyPopularity += subParty.subPartyPopularity;
        }

        // Если сумма подгрупп не равна популярности глобальной партии, нормализуем
        if (totalSubPartyPopularity != targetParty.partyPopularity)
        {
            float scalingFactor = (float)targetParty.partyPopularity / totalSubPartyPopularity;

            foreach (var subParty in targetParty.subParties)
            {
                // Если это изменённая подгруппа, пропускаем её
                if (changedSubPartyIndex.HasValue && subParty == targetParty.subParties[changedSubPartyIndex.Value])
                {
                    continue;
                }
                // Пропорционально изменяем популярность подгруппы
                subParty.subPartyPopularity = Mathf.FloorToInt(subParty.subPartyPopularity * scalingFactor);
            }

            // После перерасчета, если сумма все еще не равна, корректируем одну из подгрупп
            int newTotal = 0;
            foreach (var subParty in targetParty.subParties)
            {
                newTotal += subParty.subPartyPopularity;
            }

            // Корректируем последнюю подгруппу для достижения точного соответствия
            if (newTotal != targetParty.partyPopularity)
            {
                int difference = targetParty.partyPopularity - newTotal;
                for (int i = 0; i < targetParty.subParties.Length; i++)
                {
                    if (changedSubPartyIndex.HasValue && i == changedSubPartyIndex.Value)
                    {
                        continue; // Пропускаем изменённую подгруппу
                    }
                    targetParty.subParties[i].subPartyPopularity += difference;
                    break; // Вносим корректировку только в первую подходящую подгруппу
                }
            }
        }
    }

    public void SetRulingStatusForSubParty(int partyIndex, int subPartyIndex, bool isRuling)
    {
        // Устанавливаем статус правящей подгруппы
        if (isRuling)
        {
            // Сбрасываем статус правящей подгруппы для всех других подгрупп в этой партии
            foreach (var subParty in parties[partyIndex].subParties)
            {
                if (subParty != parties[partyIndex].subParties[subPartyIndex])
                {
                    subParty.isSubPartyRuling = false;
                }
            }
        }

        parties[partyIndex].subParties[subPartyIndex].isSubPartyRuling = isRuling;

        // Если подгруппа становится правящей, устанавливаем статус правящей партии
        if (isRuling)
        {
            parties[partyIndex].isPartyRuling = true;
        }
    }

    public void SetRulingStatusForMainParty(int partyIndex, bool isRuling)
    {
        // Устанавливаем статус правящей глобальной партии
        if (isRuling)
        {
            // Сбрасываем статус правящей глобальной партии для всех других партий
            foreach (var party in parties)
            {
                if (party != parties[partyIndex])
                {
                    party.isPartyRuling = false; // Сбрасываем статус других партий
                }
            }
        }

        parties[partyIndex].isPartyRuling = isRuling;

        // Здесь мы не сбрасываем статус подгрупп
    }

    private void DisplayPartiesInfo()
    {
        foreach (var party in parties)
        {
            Debug.Log($"{party.partyName} Popularity: {party.partyPopularity}, Ruling: {party.isPartyRuling}");
            foreach (var subParty in party.subParties)
            {
                Debug.Log($"  {subParty.subPartyName} Popularity: {subParty.subPartyPopularity}, Ruling: {subParty.isSubPartyRuling}");
            }
        }
    }
}

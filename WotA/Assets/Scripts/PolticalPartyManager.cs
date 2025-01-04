using UnityEngine;

public class PoliticalPartyManager : MonoBehaviour
{
    public PoliticalParty[] parties; // ������ ���������� ������

    void Start()
    {
        // ������ ���������� ������������ ������ ��������� ������ ������ �� 5
        AdjustPopularity(0, 0, 5); // ����������� ������������ ������ ��������� ������ ������

        // ������� ����������
        DisplayPartiesInfo();
    }

    public void AdjustPopularity(int partyIndex, int? subPartyIndex, int increaseAmount)
    {
        // ������� ���������� ������ �� �������
        if (partyIndex < 0 || partyIndex >= parties.Length)
        {
            Debug.LogError($"������ ������ {partyIndex} ��� ���������.");
            return;
        }

        PoliticalParty targetParty = parties[partyIndex];

        if (subPartyIndex.HasValue)
        {
            // ����������� ������������ ���������� ���������
            targetParty.subParties[subPartyIndex.Value].subPartyPopularity += increaseAmount;
        }
        else
        {
            // ����������� ��� ��������� ������������ ���������� ������
            targetParty.partyPopularity += increaseAmount;
        }

        // ����������� ������������ �������� ������������ ���������� ������
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

        // ���� ����� �������� �� ����� ������������ ���������� ������, �����������
        if (totalSubPartyPopularity != targetParty.partyPopularity)
        {
            float scalingFactor = (float)targetParty.partyPopularity / totalSubPartyPopularity;

            foreach (var subParty in targetParty.subParties)
            {
                // ���� ��� ��������� ���������, ���������� �
                if (changedSubPartyIndex.HasValue && subParty == targetParty.subParties[changedSubPartyIndex.Value])
                {
                    continue;
                }
                // ��������������� �������� ������������ ���������
                subParty.subPartyPopularity = Mathf.FloorToInt(subParty.subPartyPopularity * scalingFactor);
            }

            // ����� �����������, ���� ����� ��� ��� �� �����, ������������ ���� �� ��������
            int newTotal = 0;
            foreach (var subParty in targetParty.subParties)
            {
                newTotal += subParty.subPartyPopularity;
            }

            // ������������ ��������� ��������� ��� ���������� ������� ������������
            if (newTotal != targetParty.partyPopularity)
            {
                int difference = targetParty.partyPopularity - newTotal;
                for (int i = 0; i < targetParty.subParties.Length; i++)
                {
                    if (changedSubPartyIndex.HasValue && i == changedSubPartyIndex.Value)
                    {
                        continue; // ���������� ��������� ���������
                    }
                    targetParty.subParties[i].subPartyPopularity += difference;
                    break; // ������ ������������� ������ � ������ ���������� ���������
                }
            }
        }
    }

    public void SetRulingStatusForSubParty(int partyIndex, int subPartyIndex, bool isRuling)
    {
        // ������������� ������ �������� ���������
        if (isRuling)
        {
            // ���������� ������ �������� ��������� ��� ���� ������ �������� � ���� ������
            foreach (var subParty in parties[partyIndex].subParties)
            {
                if (subParty != parties[partyIndex].subParties[subPartyIndex])
                {
                    subParty.isSubPartyRuling = false;
                }
            }
        }

        parties[partyIndex].subParties[subPartyIndex].isSubPartyRuling = isRuling;

        // ���� ��������� ���������� ��������, ������������� ������ �������� ������
        if (isRuling)
        {
            parties[partyIndex].isPartyRuling = true;
        }
    }

    public void SetRulingStatusForMainParty(int partyIndex, bool isRuling)
    {
        // ������������� ������ �������� ���������� ������
        if (isRuling)
        {
            // ���������� ������ �������� ���������� ������ ��� ���� ������ ������
            foreach (var party in parties)
            {
                if (party != parties[partyIndex])
                {
                    party.isPartyRuling = false; // ���������� ������ ������ ������
                }
            }
        }

        parties[partyIndex].isPartyRuling = isRuling;

        // ����� �� �� ���������� ������ ��������
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

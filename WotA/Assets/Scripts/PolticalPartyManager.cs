using UnityEngine;

public class PoliticalPartyManager : MonoBehaviour
{
    public PoliticalParty[] parties; // ������ ���������� ������

    void Start()
    {
        // ������ ���������� ������������ ������ ��������� ������ ������ �� 5
        AdjustSubPartyPopularity(0, 0, 5); // ����������� ������������ ������ ��������� ������ ������

        // ������� ����������
        foreach (var party in parties)
        {
            Debug.Log($"{party.partyName} Popularity: {party.partyPopularity}");
            foreach (var subParty in party.subParties)
            {
                Debug.Log($"  {subParty.subPartyName} Popularity: {subParty.subPartyPopularity}");
            }
        }
    }

    public void AdjustSubPartyPopularity(int partyIndex, int subPartyIndex, int increaseAmount)
    {
        // ����������� ������������ ��������� ���������
        parties[partyIndex].subParties[subPartyIndex].subPartyPopularity += increaseAmount;

        // ��������� ����� ������������ ���������� ������
        UpdateGlobalPartyPopularity(partyIndex);

        // ����������� ������������ ���� ������
        NormalizePopularity();
    }

    private void UpdateGlobalPartyPopularity(int partyIndex)
    {
        int totalSubPartyPopularity = 0;

        foreach (var subParty in parties[partyIndex].subParties)
        {
            totalSubPartyPopularity += subParty.subPartyPopularity;
        }

        parties[partyIndex].partyPopularity = totalSubPartyPopularity;
    }

    private void NormalizePopularity()
    {
        foreach (var party in parties)
        {
            while (party.partyPopularity > 100)
            {
                for (int i = 0; i < party.subParties.Length && party.partyPopularity > 100; i++)
                {
                    if (party.subParties[i].subPartyPopularity > 0)
                    {
                        party.subParties[i].subPartyPopularity--;
                        party.partyPopularity--;
                    }
                }
            }

            while (party.partyPopularity < 100)
            {
                for (int i = 0; i < party.subParties.Length && party.partyPopularity < 100; i++)
                {
                    party.subParties[i].subPartyPopularity++;
                    party.partyPopularity++;
                }
            }

            // ��������� ����� ������������ ���������� ������
            UpdateGlobalPartyPopularity(System.Array.IndexOf(parties, party));
        }
    }
}

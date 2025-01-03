[System.Serializable]
public class SubParty
{
    public string subPartyName; // �������� ���������
    public int subPartyPopularity; // ������������ ��������� (����� �����)
    public bool isSubPartyRuling; // �������� �� ��������� �������� (true/false)
}

[System.Serializable]
public class PoliticalParty
{
    public string partyName; // �������� ���������� ������
    public int partyPopularity; // ������������ ���������� ������ (����� �����)
    public bool isPartyRuling; // �������� �� ������ �������� (true/false)
    public SubParty[] subParties; // ������ �������� ������ ���������� ������
}

using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int _time = 1;
    private int _timeEvent = 0;
    public int TimeBetweenEvents = 90;

    private void Start()
    {
        // ��������� �������� ��� ���������� �������
        StartCoroutine(AddTime());
    }

    private IEnumerator AddTime()
    {
        while (true) // ����������� ����
        {
            yield return new WaitForSeconds(1f); // ��� 1 �������
            _time += 1;
            _timeEvent += 1; // ����������� ����� �� 1 �������
            //UpdateTimerText(); // ��������� ����� �� ������
            print(_timeEvent);
        }
    }

    public void PlusTime()
    {
        if (Time.timeScale < 5)
        {
            Time.timeScale += 1;
        }
    }

    public void MinusTime()
    {
        if (Time.timeScale > 1)
        {
            Time.timeScale -= 1;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Debug.Log('0');
        }
        else if (Input.GetKeyDown(KeyCode.Space) & Time.timeScale == 0)
        {
            Time.timeScale = 1;
            Debug.Log('1');
        }

        if (_timeEvent == TimeBetweenEvents)
        {
            _timeEvent = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Office : MonoBehaviour
{
    [SerializeField] int MaxMoneyAmount = 1000;

    int currentMoney = 0;
    float deseaseLevel = 0f;

    public Text moneyAmountLabel;
    public Text deseaseLevelLabel;

    public int CurrentMoney
    {
        get { return currentMoney; }
        set
        {
            if (value > MaxMoneyAmount)
                currentMoney = MaxMoneyAmount;
            else
                currentMoney = value;
        }
    }

    public float DeseaseLevel
    {
        get { return deseaseLevel; }
        set
        {
            if (deseaseLevel > 100f)
                deseaseLevel = 100f;
            else if (deseaseLevel < 0f)
                deseaseLevel = 0f;
        }
    }

    private void Awake()
    {
        ChangeMoneyAmount(MaxMoneyAmount);
        ChangeDeseaseLevel(0f);
    }
    
    void ChangeMoneyAmount(int value)
    {
        CurrentMoney += value;
        moneyAmountLabel.text = CurrentMoney.ToString() + "$";
    }

    void ChangeDeseaseLevel(float value)
    {
        DeseaseLevel += value;
        deseaseLevelLabel.text = DeseaseLevel.ToString() + "%";
    }
}

using System;
using DefaultNamespace;
using Game;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    public TMP_Text timer;
    public TMP_Text timeLeft;
    
    private Player player;
    private void Awake()
    {
        DI.sceneScope.register(this);
    }

    private void Start()
    {
        player = DI.sceneScope.getInstance<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = player.Budget.ToString();
        timeLeft.text = player.currentBudget.ToString();
    }
}

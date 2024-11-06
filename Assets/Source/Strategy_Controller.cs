using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strategy_Controller : Strategy
{
    public void Run()
    {
        StartGame();
    }

    public void StartGame()
    {
        Character adchar = Character.Create<ADCharacter>(Program.FindObject("/Field").transform);
        KeyboardController control = new KeyboardController();
        adchar.SetController(control);
    }
    public void Update()
    {

    }
}

namespace PingPong;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Numerics;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Timers;
using PingPong.States;

public class Game : DIKUGame
{
  private StateMachine stateMachine;
  public Game(WindowArgs windowArgs) : base(windowArgs)
  {
    stateMachine = new StateMachine();
  }

  public override void KeyHandler(KeyboardAction action, KeyboardKey key)
  {
    stateMachine.ActiveState.HandleKeyEvent(action, key);
  }

  public override void Render(WindowContext context)
  {
    stateMachine.ActiveState.Render(context);
  }

  public override void Update()
  {
    stateMachine.ActiveState.Update();
  }
}
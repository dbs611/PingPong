namespace PingPong.States;

using System;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Timers;
using PingPong;
using PingPong.Entities;
using PingPong.GameLogic;
using Microsoft.VisualBasic;
using PingPong.States;

public class GameRunning : IGameState
{
    private Player player;
    private ScoreManager scoreManager;
    private int ballCount = 0;
    private EntityContainer<Ball> balls = new();

    private StateMachine stateMachine;
    private Player CreatePlayer()
    {
        return player = new Player(
    new DynamicShape(new Vector2(0.43f, 0.15f), new Vector2(0.15f, 0.025f)),
    new Image("Breakout.Assets.Images.player.png"));
    }
    private Ball CreateBall()
    {
        //
        ballCount++;
        return new Ball(
        new DynamicShape(new Vector2(0.43f, 0.20f), new Vector2(0.045f, 0.045f)),
        new Image("Breakout.Assets.Images.ball.png"));
    }
    public GameRunning(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        player = CreatePlayer();
        balls.AddEntity(CreateBall());

        scoreManager = new ScoreManager(stateMachine);
    }

    public void Update()
    {
        player.Move();
        BallsUpdate();
        EffectCollision.EffectPaddle(player, powerUps, hazards);
    }
    public void Render(WindowContext context)
    {
        player.RenderEntity(context);
        balls.Iterate(ball =>
        {
            ball.RenderEntity(context);
        });
        scoreManager.GetScoreBoard().Render(context);
    }
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
    {
        player.KeyHandler(action, key);
        if (action == KeyboardAction.KeyPress && key == KeyboardKey.Escape)
        {
            KeyEscape();
        }
    }
    private void KeyEscape()
    {
        DIKUArcade.Timers.StaticTimer.PauseTimer();
        stateMachine.ActiveState = new GamePaused(stateMachine);
    }

    private void BallsUpdate()
    {
        balls.Iterate(ball =>
        {
            ball.Move();
            BallCollision.BallWall(ball);
            BallCollision.BallBlock(ball, blocks, scoreManager.AddPoints, scoreManager.GetScore, scoreManager.TriggerWin);
            BallCollision.BallPaddle(ball, player);
            DestroyBall(ball);
        });
    }

    private void DestroyBall(Ball ball)
    {
        var dyn = ball.Shape.AsDynamicShape();
        if (dyn.Position.Y + dyn.Extent.Y < 0.0f)
        {
            ball.DeleteEntity();
            ballCount--;
            if (ballCount <= 0)
            {
                healthManager.LoseHealth();
                if (healthManager.Health <= 0)
                {
                    Cleanup();
                    stateMachine.ActiveState = new GameOver(stateMachine);
                }
                else
                {
                    balls.AddEntity(CreateBall());
                }
            }
        }
    }
}
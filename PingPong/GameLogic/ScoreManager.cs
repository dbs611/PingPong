namespace PingPong.GameLogic;
// Create a scoreboard, and implement the method to destroy the ball, while tracking which side the ball went through (Right or Left)
/*
Original DestroyBall():

        public void DestroyBall() {
            var ballPosition = this.Shape.AsDynamicShape();
            if (ballPosition.Position.Y + ballPosition.Extent.Y < 0.0f) {
                this.DeleteEntity();
            }
        }

        public void DeleteEntity() {
        base.DeleteEntity();
        ActiveBalls.Remove(this);
        }
*/
using System.Numerics;
using DIKUArcade.Graphics;
using PingPong.States;
public class ScoreManager
{
    
} 
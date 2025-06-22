namespace PingPong.Entities;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using System.Numerics;

public class Player : Entity {
    private float moveUp = 0.0f;
    private float moveDown = 0.0f;

    private const float BASE_SPEED = 0.04f;

    private float currentSpeed = BASE_SPEED;

    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) { }

    public void Move() {
        DynamicShape dynamicShape = Shape.AsDynamicShape();
        dynamicShape.Move();

        float newy = Math.Clamp(Shape.Position.Y, 0.0f, 1.0f - Shape.Extent.Y);

        if (newY != Shape.Position.Y) {
            dynamicShape.Velocity.Y = 0;
        }

        Shape.Position = new Vector2(Shape.Position.X, newY);
    }

    private void UpdateVelocity() {
        DynamicShape dynamicShape = Shape.AsDynamicShape();
        currentSpeed = BASE_SPEED;

        float velocityX = moveRight + moveLeft;
        dynamicShape.Velocity = new Vector2(velocityX * currentSpeed, 0f);
    }

    public void SetMoveUp(bool val) {
        moveLeft = val ? 0 : -1.0f;
        UpdateVelocity();
    }

    public void SetMoveDown(bool val) {
        moveRight = val ? 0 : 1.0;
        UpdateVelocity();
    }


    public void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch ((key, action)) {
            case (KeyboardKey.W, KeyboardAction.KeyPress):
                SetMoveUp(true);
                break;
            case (KeyboardKey.S, KeyboardAction.KeyPress):
                SetMoveDown(true);
                break;
            case (KeyboardKey.W, KeyboardAction.KeyRelease):
                SetMoveUp(false);
                break;
            case (KeyboardKey.S, KeyboardAction.KeyRelease):
                SetMoveDown(false);
                break;
        }
    }
}

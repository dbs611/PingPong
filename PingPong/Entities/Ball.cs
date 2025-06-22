namespace PingPong.Entities;

using System;
using System.Collections.Generic;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class Ball : Entity {
    private Vector2 direction;
    public static List<Ball> ActiveBalls { get; } = new();

    public Vector2 Direction {
        get => direction;
        set => direction = FixedSpeed(value);
    }

    public bool IsLaunched { get; private set; } = false;

    public float Speed { get; set; } = 0.02f;

    public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.Shape = shape;
        this.Direction = new Vector2(0.0f, 0.02f);
        Shape.AsDynamicShape().Velocity = Direction;
        ActiveBalls.Add(this);
    }

    public void Launch() {
        if (!IsLaunched) {
            Direction = new Vector2(0.00f, 0.02f);
            Shape.AsDynamicShape().Velocity = Direction;
            IsLaunched = true;
        }
    }

    public void Move() {
        var dyn = Shape.AsDynamicShape();
        dyn.Position += Speed * Direction;
        dyn.Velocity = Speed * Direction;
    }

    public void ReflectX() {
        Direction = new Vector2(-Direction.X, Direction.Y);
        Shape.AsDynamicShape().Velocity = Direction;
    }

    public void ReflectY() {
        Direction = new Vector2(Direction.X, -Direction.Y);
        Shape.AsDynamicShape().Velocity = Direction;
    }

    public void NormalizeDirection() {
        Direction = Vector2.Normalize(Direction);
    }

    public Vector2 FixedSpeed(Vector2 dir) {
        float scalar = Speed / dir.Length();
        return dir * scalar;
    }
}

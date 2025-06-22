namespace PingPong.Entities;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using System.Numerics;

public class player : Entity {
    private float moveUp = 0.0f;
    private float moveDown = 0.0f;
    private const float BaseSpeed = 0.04f;

    public player(DynamicShape shape, IBaseImage image) : base(shape, image) { }

    public move() {
        DynamicShape dynamicshape = Shape.AsDynamicShape();
        DynamicShape.move();

        float newY = Math.Clamp(ShapePosition.Y);
    }
}
namespace PingPong;

using System;
using DIKUArcade.GUI;

class Program {
    static void Main(string[] args) {
        var windowArgs = new WindowArgs() { Title = "PingPong v1.0" };
        var game = new Game(windowArgs);
        game.Run();
    }
}
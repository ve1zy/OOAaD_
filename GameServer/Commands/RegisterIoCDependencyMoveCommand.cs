#nullable disable

using GameServer.Interfaces;
using GameServer.IoC;
using GameServer.Models;
using System;

namespace GameServer.Commands;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        Ioc.Instance.RegisterSingleton<IMovingObject, MockMovingObject>();
        Ioc.Instance.RegisterSingleton<Vector>(new Vector(1, 2, 3));
        Ioc.Instance.RegisterTransient<ICommand, MoveCommand>();
    }

    private class MockMovingObject : IMovingObject
    {
        public Vector LastPosition { get; private set; }
        
        public void Move(Vector position)
        {
            LastPosition = position;
        }
    }
}

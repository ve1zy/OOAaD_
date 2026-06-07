using System;
using System.Collections.Generic;
using GameServer.Interfaces;
using GameServer.Commands;
using GameServer.Models;

namespace GameServer.IoC
{

    public class RegisterIoCDependencyActionsStart : ICommand
    {
        public void Execute()
        {
            Ioc.Register("Actions.Start", (Func<object[], object>)(args =>
            {
                var order = (IDictionary<string, object>)args[0];
                return new StartActionCommand(order);
            }));

            Ioc.Register("Operations.move.Start", (Func<object[], object>)(args =>
            {
                var order = (IDictionary<string, object>)args[0];
                var movableObject = (IMovingObject)order["movableObject"];
                var position = (Vector)order["position"];
                return new MoveCommand(movableObject, position);
            }));

            Ioc.Register("Operations.rotate.Start", (Func<object[], object>)(args =>
            {
                var order = (IDictionary<string, object>)args[0];
                var rotatingObject = (IRotatingObject)order["rotatingObject"];
                return new RotateCommand(rotatingObject);
            }));
        }
    }
}
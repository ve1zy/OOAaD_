using System.Collections.Generic;
using GameServer.Interfaces;
using GameServer.Commands;

namespace GameServer.IoC
{
    public class RegisterIoCDependencyOperationsRotateStart : ICommand
    {
        public void Execute()
        {
            Ioc.Register("Operations.rotate.Start", (Func<object[], object>)(args =>
            {
                var order = (IDictionary<string, object>)args[0];
                var rotatingObject = (IRotatingObject)order["rotatingObject"];
                return new RotateCommand(rotatingObject);
            }));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;

namespace ParserWebApp.App_Start
{
    public class DependencyInjector : NinjectModule
    {
        public override void Load()
        {
            Bind<Repositories.Interfaces.IUnitOfWork>().To<Repositories.UnitOfWork.UnitOfWork>();
        }
    }
}
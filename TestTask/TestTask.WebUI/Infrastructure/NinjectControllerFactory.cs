using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using TestTask.Domain.Entities;
using TestTask.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TestTask.Domain.Concrete;
using System.Configuration;

namespace TestTask.WebUI.Infrastructure {
    public class NinjectControllerFactory : DefaultControllerFactory {
        private IKernel ninjectKernel;
        public NinjectControllerFactory() {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            return controllerType == null
              ? null
              : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings() {

            EmailSettings emailSettings = new EmailSettings {
                WriteAsFile = bool.Parse(ConfigurationManager
                  .AppSettings["Email.WriteAsFile"] ?? "false")
            };
            ninjectKernel.Bind<IUserRepository>().To<EFUserRepository>();
        }
    }
}
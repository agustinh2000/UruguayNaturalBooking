using BusinessLogicInterface;
using Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace WebApiTest
{
    [TestClass]
    public class AuthorizationFilterTest
    {
        [TestMethod]
        public void TestAuthFilterWithoutHeader()
        {
            var logicMock = new Mock<IUserManagement>(MockBehavior.Strict);
            AuthorizationFilter authFilter = new AuthorizationFilter(logicMock.Object);

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());

            authFilter.OnAuthorization(context);

            ContentResult response = context.Result as ContentResult;

            Assert.AreEqual(401, response.StatusCode);
        }

        [TestMethod]
        public void TestAuthFilterWithValidHeader()
        {
            var logicMock = new Mock<IUserManagement>(MockBehavior.Strict);
            AuthorizationFilter authFilter = new AuthorizationFilter(logicMock.Object);

            string token = Guid.NewGuid().ToString();

            logicMock.Setup(x => x.IsLogued(token)).Returns(true);

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["token"] = token;
            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());

            authFilter.OnAuthorization(context);

            ContentResult response = context.Result as ContentResult;

            Assert.IsNull(response);
        }

        [TestMethod]
        public void TestAuthFilterWithInvalidHeader()
        {
            var logicMock = new Mock<IUserManagement>(MockBehavior.Strict);
            AuthorizationFilter authFilter = new AuthorizationFilter(logicMock.Object);

            string token = Guid.NewGuid().ToString();

            logicMock.Setup(x => x.IsLogued(token)).Returns(false);

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["token"] = token;

            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());

            authFilter.OnAuthorization(context);

            ContentResult response = context.Result as ContentResult;

            Assert.AreEqual(403, response.StatusCode);
        }

    }
}

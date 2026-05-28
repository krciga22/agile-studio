
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Resources.Resource;
using AgileStudioServer.Features.Users.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace AgileStudioServerTest.IntegrationTests
{
    public abstract class ResourceControllerTest : DBTest
    {
        protected HttpContext _HttpContext = new DefaultHttpContext();

        protected ResourceController _ResourceController;

        protected ServiceContext _ServiceContext;

        protected IUrlHelperFactory _IUrlHelperFactory;

        public ResourceControllerTest(
            DBContext dbContext,
            ResourceController resourceController,
            ServiceContext serviceContext,
            IUrlHelperFactory? iUrlHelperFactory = null) : 
            base(dbContext)
        {
            _ResourceController = resourceController;
            _ServiceContext = serviceContext;
            _IUrlHelperFactory = iUrlHelperFactory ?? new UrlHelperFactory();
        }

        protected void InitHttpAndServiceContextWithUser(UserModel user)
        {
            _HttpContext = new DefaultHttpContext
            {
                User = IntegrationTestsUtil.GenerateCurrentUserClaimsPrincipal(user.ID)
            };

            _ServiceContext.currentUser = _HttpContext.User;
        }

        protected IUrlHelper GetUrlHelper()
        {
            return _IUrlHelperFactory.GetUrlHelper(new ActionContext
            {
                HttpContext = _HttpContext,
                RouteData = _HttpContext.GetRouteData(),
                ActionDescriptor = new ActionDescriptor(),
            });
        }
    }
}

 using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;
 using Microsoft.AspNetCore.Http;
 using Microsoft.AspNetCore.Mvc.Filters;
 using FirstAppMVC.Models;
 using FirstAppMVC.Services;
 using Microsoft.AspNetCore.Mvc;
 namespace FirstAppMVC.Filters
 {
     public class AuthorizeActionFilter : IAuthorizationFilter
     {
         readonly string _permission;
         readonly TestService _service;
         private List<Role> _roles;

        public AuthorizeActionFilter(string permission,TestService service)
        {
            _permission = permission ;
            _service = service;
            _roles =  _service.GetRoles();
        }
        public void OnAuthorization(AuthorizationFilterContext context)
         {
             var GetRoleID = context.HttpContext.Session.GetString("RoleID");
             if (String.IsNullOrEmpty(GetRoleID))
             {
                GetRoleID="-1";
             }
             Role  getRole = _roles.SingleOrDefault(p => p.ID == Int32.Parse(GetRoleID));
             if (getRole==null)
             {
                 getRole = new Role(){ID=2,RoleName="User"};
             }
             if (_permission!=getRole.RoleName || getRole==null)
             {
                 context.Result = new ForbidResult();
             }
         }
     }
 }
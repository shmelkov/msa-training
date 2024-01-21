using MediatR;
using Portal.Common.Constants;
using Portal.Common.DTOs;
using Portal.Common.Entities.Base;
using Portal.Common.Services.Interfaces;
using System.Reflection;

namespace Portal.Common.Helpers
{
    public class AccessHelper
    {
        public static bool IsInAuthorizedRole(string[] userRoles, string[] authorizedRoles)
        {
            return userRoles.Intersect(authorizedRoles).Any();
        }

        public static bool IsInAuthorizedRole(string[] userRoles, string authorizedRoles)
        {
            return IsInAuthorizedRole(userRoles, authorizedRoles.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }

        public static bool IsInAuthorizedRole(IUserAccessor userAccessor, string[] authorizedRoles)
        {
            return IsInAuthorizedRole(userAccessor.Roles, authorizedRoles);
        }

        public static bool IsInAuthorizedRole(IUserAccessor userAccessor, string authorizedRoles)
        {
            return IsInAuthorizedRole(userAccessor.Roles, authorizedRoles);
        }

        public static bool IsAuthorized(Attributes.AccessAttribute authorizeAttributeValue, IUserAccessor userAccessor)
        {
            return authorizeAttributeValue.Roles != null && IsInAuthorizedRole(userAccessor, authorizeAttributeValue.Roles);
        }

        public static bool IsEntityOwner(IAuditableEntity entity, IUserAccessor userAccessor)
        {
            return entity?.CreatedById == userAccessor.UserId;
        }

        public static bool IsAuthorized(IBaseRequest request, IUserAccessor userAccessor)
        {
            var authorizeAttribute = request.GetType().GetCustomAttribute<Attributes.AccessAttribute>();

            return authorizeAttribute == null || IsAuthorized(authorizeAttribute, userAccessor);
        }

        public static bool IsAuthorized(IBaseRequest request, IEntity<Guid> entity, IUserAccessor userAccessor)
        {
            var authorizeAttribute = request.GetType().GetCustomAttribute<Attributes.AccessAttribute>();

            return
                authorizeAttribute == null ||
                IsAuthorized(authorizeAttribute, userAccessor) ||
                (authorizeAttribute.CanAccessOwnEntity && entity is IAuditableEntity auditable && IsEntityOwner(auditable, userAccessor));
        }

        public static string[] GetPriviligedRolesForModule(string module)
        {
            switch(module)
            {
                case Modules.News:
                    return new[] { Roles.NewsEditor, Roles.NewsAdmin, Roles.PortalAdmin };
                case Modules.Thanks:
                    return new[] { Roles.ThanksEditor, Roles.ThanksAdmin, Roles.PortalAdmin };
                case Modules.Notifications:
                    return new[] { Roles.NotificationsEditor, Roles.NotificationsAdmin, Roles.PortalAdmin };
                case Modules.CorpBook:
                    return new[] { Roles.CorpBookEditor, Roles.CorpBookAdmin, Roles.PortalAdmin };
                case Modules.Profile:
                    return new[] { Roles.ProfileEditor, Roles.ProfileAdmin, Roles.PortalAdmin };
                case Modules.Communities:
                    return new[] { Roles.CommunitiesEditor, Roles.CommunitiesAdmin, Roles.PortalAdmin };

                default:
                    return new string[] { };
            }
        }

        public static string[] GetAdminRolesForModule(string module)
        {
            switch (module)
            {
                case Modules.News:
                    return new[] { Roles.NewsAdmin, Roles.PortalAdmin };
                case Modules.Thanks:
                    return new[] { Roles.ThanksAdmin, Roles.PortalAdmin };
                case Modules.Notifications:
                    return new[] { Roles.NotificationsAdmin, Roles.PortalAdmin };
                case Modules.CorpBook:
                    return new[] { Roles.CorpBookAdmin, Roles.PortalAdmin };
                case Modules.Profile:
                    return new[] { Roles.ProfileAdmin, Roles.PortalAdmin };

                default:
                    return new string[] { };
            }
        }
    }
}
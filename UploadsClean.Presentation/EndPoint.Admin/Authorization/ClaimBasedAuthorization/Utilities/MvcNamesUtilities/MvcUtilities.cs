﻿using System.Collections.Immutable;
using System.Reflection;
using EndPoint.Admin.Authorization.ClaimBasedAuthorization.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EndPoint.Admin.Authorization.ClaimBasedAuthorization.Utilities.MvcNamesUtilities
{
    public class MvcUtilities : IMvcUtilities
    {
        public MvcUtilities(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            var mvcInfo = new List<MvcNamesModel>();
            var mvcInfoForActionsThatRequireClaimBasedAuthorization = new List<MvcNamesModel>();

            var actionDescriptors = actionDescriptorCollectionProvider.ActionDescriptors.Items;
            foreach (var actionDescriptor in actionDescriptors)
            {
                if (!(actionDescriptor is ControllerActionDescriptor descriptor)) continue;

                var controllerTypeInfo = descriptor.ControllerTypeInfo;

                var claimToAuthorize = descriptor.MethodInfo
                    .GetCustomAttribute<ClaimBasedAuthorizationAttribute>()?.ClaimToAuthorize;

                mvcInfo.Add(new MvcNamesModel(
                    controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                    descriptor.ControllerName,
                    descriptor.ActionName,
                    claimToAuthorize));

                if (!string.IsNullOrWhiteSpace(claimToAuthorize))
                    mvcInfoForActionsThatRequireClaimBasedAuthorization.Add(new MvcNamesModel(
                        controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                        descriptor.ControllerName,
                        descriptor.ActionName,
                        claimToAuthorize));
            }

            MvcInfo = ImmutableHashSet.CreateRange(mvcInfo);
            MvcInfoForActionsThatRequireClaimBasedAuthorization = 
                ImmutableHashSet.CreateRange(mvcInfoForActionsThatRequireClaimBasedAuthorization);
        }

        public ImmutableHashSet<MvcNamesModel> MvcInfo { get; }
        public ImmutableHashSet<MvcNamesModel> MvcInfoForActionsThatRequireClaimBasedAuthorization { get; }
    }
}

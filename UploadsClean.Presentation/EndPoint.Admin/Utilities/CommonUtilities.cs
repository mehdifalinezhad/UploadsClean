﻿using System.Reflection;
using EndPoint.Admin.Models.Role;
using Microsoft.AspNetCore.Mvc;
using UploadClean.Application.Service;
using UploadsClean.Persistence.DataBaceContext;

namespace EndPoint.Admin.Utilities
{
    public class CommonUtilities : ICommonUtilities
    {
        private readonly AppDbContext _dbContext;

        public CommonUtilities(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<ActionAndControllerName> AreaAndActionAndControllerNamesList()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var contradistinction = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type =>
                    type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Select(x => new
                {
                    Controller = x.DeclaringType?.Name,
                    Action = x.Name,
                    Area = x.DeclaringType?.CustomAttributes.Where(c => c.AttributeType == typeof(AreaAttribute))
                });

            var list = new List<ActionAndControllerName>();

            foreach (var item in contradistinction)
            {
                if (item.Area.Count() != 0)
                {
                    list.Add(new ActionAndControllerName()
                    {
                        ControllerName = item.Controller,
                        ActionName = item.Action,
                        AreaName = item.Area.Select(v => v.ConstructorArguments[0].Value.ToString()).FirstOrDefault()
                    });
                }
                else
                {
                    list.Add(new ActionAndControllerName()
                    {
                        ControllerName = item.Controller,
                        ActionName = item.Action,
                        AreaName = null,
                    });
                }

            }

            return list.Distinct().ToList();
        }

        public IList<string> GetAllAreasNames()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var contradistinction = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type =>
                    type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Select(x => new
                {
                    Area = x.DeclaringType?.CustomAttributes.Where(c => c.AttributeType == typeof(AreaAttribute))

                });

            var list = new List<string>();

            foreach (var item in contradistinction)
            {
                list.Add(item.Area.Select(v => v.ConstructorArguments[0].Value.ToString()).FirstOrDefault());
            }

            if (list.All(string.IsNullOrEmpty))
            {
                return new List<string>();
            }

            list.RemoveAll(x => x == null);

            return list.Distinct().ToList();
        }

        public string DataBaseRoleValidationGuid()
        {
            //var roleValidationGuid =
            //    _dbContext.SiteSettings.SingleOrDefault(s => s.Key == "RoleValidationGuid")?.Value;

            //while (roleValidationGuid == null)
            //{
            //    _dbContext.SiteSettings.Add(new SiteSetting()
            //    {
            //        Key = "RoleValidationGuid",
            //        Value = Guid.NewGuid().ToString(),
            //        LastTimeChanged = DateTime.Now
            //    });

            //    _dbContext.SaveChanges();

            //    roleValidationGuid =
            //        _dbContext.SiteSettings.SingleOrDefault(s => s.Key == "RoleValidationGuid")?.Value;
            //}

            return "";
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using WebApplicationNew.Models;
using AutoMapper;

namespace WebApplicationNew.Controllers
{
    [RoutePrefix("api/me")]
    [LogActionFilter]
    public class MeController : ApiController
    {
        private ApplicationUserManager _userManager;

        public MeController()
        {
        }

        public MeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET api/Me
        public GetViewModel Get()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            return new GetViewModel() { Hometown = user.Hometown };
        }

        [HttpGet]
        [Route("users/all")]
        public IEnumerable<UserViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<UserViewModel>>(UserManager.Users.ToList());
        }

        [HttpGet]
        [Route("users/delete/{username}")]
        //[Authorize(Roles = "admin")]
        public void DeleteUser(String username)
        {
            UserManager.Delete(UserManager.FindByName(username));
        }
    }
}
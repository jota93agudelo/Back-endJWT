using DesparcheBackend.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesparcheBackend.Autorizacion
{
    public class IdentityConfig
    {
        //public class ApplicationUserManager : UserManager<Usuario>
        //{
        //    //public ApplicationUserManager(IUserStore<Usuario> options)
        //    //    : base()
        //    //{
        //    //}
        //    //public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        //    //{
        //    //    var manager = new ApplicationUserManager(new UserStore<Usuario>(new PruebaAngularContext>()));
        //    //    // Configure validation logic for usernames
               
        //    //    return manager;
        //    //}
        //}
    }
}

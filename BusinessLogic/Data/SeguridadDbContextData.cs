using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class SeguridadDbContextData
    {
        public static async Task SeedUserAsync(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    Nombre = "Alex",
                    Apellido = "CG",
                    UserName = "Alekey",
                    Email = "cesar.alejandro.cg@hotmail.com",
                    Direccion = new Direccion { Calle = "17 de mayo", Ciudad = "Durango", Departamento = "Mexico", CP = "34224" }
                };

                await userManager.CreateAsync(usuario, "Kamisamano21099@");
            }

            if (!roleManager.Roles.Any())
            {
                var role = new IdentityRole
                {
                    Name = "ADMIN"
                };
                await roleManager.CreateAsync(role);
            }
        }
    }
}

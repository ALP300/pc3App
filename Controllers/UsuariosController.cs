using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pc3App.Integration;
using pc3App.Integration.jsonplaceholder.dto;
using pc3App.Integration.jsonplaceholder;

namespace pc3App.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly ListarUsuariosApiIntegration _listUsers;
        private readonly ListarUsuarioApiIntegration _unUser;
        private readonly CrearUsuarioApiIntegration _createUser;

        public UsuariosController(ILogger<UsuariosController> logger,
        ListarUsuariosApiIntegration listUsers,
        ListarUsuarioApiIntegration unUser,
        CrearUsuarioApiIntegration createUser)
        
        {
            _logger = logger;
            _listUsers = listUsers;
            _unUser = unUser;
            _createUser = createUser;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Usuarios> users = await _listUsers.GetAllUser();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Perfil(int Id)
        {
            Usuarios user = await _unUser.GetUser(Id);
            return View(user);
        }

         public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name, string job)
        {
            try
            {
                
                var response = await _createUser.CreateUser(name, job);
                
               
                if (response != null)
                {
                    
                    TempData["SuccessMessage"] = "Usuario creado correctamente.";
                }
                else
                {
                   
                    ModelState.AddModelError("", "Error al crear el usuario");
                }
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Error al crear el usuario: {ex.Message}");
                ModelState.AddModelError("", "Error al crear el usuario");
            }
            
           
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
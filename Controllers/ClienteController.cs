﻿using DemoJWTNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DemoJWTNetCore.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("listar")]
        public dynamic listarCliente()
        {
            //Todo el codigo

            List<Cliente> clientes = new List<Cliente>
            {
                new Cliente
                {
                    id = "1",
                    correo = "google@gmail.com",
                    edad = "19",
                    nombre = "Bernardo Peña"
                },
                new Cliente
                {
                    id = "2",
                    correo = "miguelgoogle@gmail.com",
                    edad = "23",
                    nombre = "Miguel Mantilla"
                }
            };

            return clientes;
        }

        [HttpGet]
        [Route("listarxid")]
        public dynamic listarClientexid(int codigo)
        {
            //obtienes el cliente de la db

            return new Cliente
            {
                id = codigo.ToString(),
                correo = "google@gmail.com",
                edad = "19",
                nombre = "Bernardo Peña"
            };
        }

        [HttpPost]
        [Route("guardar")]
        public dynamic guardarCliente(Cliente cliente)
        {
            //Guardar en la db y le asignas un id
            cliente.id = "3";

            return new
            {
                success = true,
                message = "cliente registrado",
                result = cliente
            };
        }

        [HttpPost]
        [Route("eliminar")]
        //[Authorize]
        public dynamic eliminarCliente(Cliente cliente)
        {
           var identity = HttpContext.User.Identity as ClaimsIdentity;

            var respuestaToken = Jwt.validarToken(identity);
            if (!respuestaToken.success) return respuestaToken;

            Usuario usuario = respuestaToken.result;
            
            if(usuario.rol != "administrador")
            {
                return new
                {
                    success = false,
                    message = "No tienes permisos de administrador, no puedes eliminar usuarios",
                    result = ""
                };
            }

            return new
            {
                success = true, 
                message = "cliente eliminado",
                result = cliente
            };
        }
    }
}

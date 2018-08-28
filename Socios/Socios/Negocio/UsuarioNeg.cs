﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Socios.Entidades;
using System.Windows.Forms;
using Socios.Dao;

namespace Socios.Negocio
{
    public class UsuarioNeg
    {
        public static List<Usuario> BuscarUsuarioPorDni(string dni)
        {
            List<Usuario> _listaUsuarios = new List<Usuario>();
            try
            {
                _listaUsuarios = UsuarioDao.BuscarUsuarioPorDNI(dni);
            }
            catch (Exception ex)
            {
                const string message = "Error en el sistema. Intente nuevamente o comuniquese con el administrador.";
                const string caption = "Atención";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Warning);
                throw new Exception();
            }
            return _listaUsuarios;
        }
        public static bool EliminarUsuario(string dni)
        {
            throw new NotImplementedException();
        }
        public static bool GurdarUsuario(Usuario _usuario)
        {
            bool exito = false;
            try
            {
                ValidarDatos(_usuario);
                bool UsuarioExistente = ValidarUsuarioExistente(_usuario.Dni);
                if (UsuarioExistente == true)
                {
                    const string message = "Ya existe un usuario registrado con el dni ingresado.";
                    const string caption = "Error";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                               MessageBoxIcon.Exclamation);
                    throw new Exception();
                }
                else
                {
                    exito = UsuarioDao.InsertUsuario(_usuario);
                }
            }
            catch (Exception ex)
            {

            }
            return exito;
        }
        private static bool ValidarUsuarioExistente(string dni)
        {
            bool existe = UsuarioDao.ValidarUsuarioExistente(dni);
            return existe;
        }
        private static void ValidarDatos(Usuario _usuario)
        {
            if (String.IsNullOrEmpty(_usuario.Dni))
            {
                const string message = "El campo dni es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
            if (String.IsNullOrEmpty(_usuario.Apellido))
            {
                const string message = "El campo apellido es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
            if (String.IsNullOrEmpty(_usuario.Nombre))
            {
                const string message = "El campo nombre es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
            if (String.IsNullOrEmpty(_usuario.Contraseña))
            {
                const string message = "El campo contraseña es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
            if (_usuario.Contraseña != _usuario.Contraseña2)
            {
                const string message = "Las contraseñas ingresadas no coinciden.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
            if (String.IsNullOrEmpty(_usuario.Perfil))
            {
                const string message = "El perfil es un campo obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
            if (_usuario.Perfil != "ADMINISTRADOR" & _usuario.Perfil != "OPERADOR")
            {
                const string message = "El perfil ingresado es inexistente.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
        }
        public static bool EditarUsuario(Usuario _usuario)
        {
            bool exito = false;
            try
            {
                ValidarDatos(_usuario);
                exito = UsuarioDao.EditarUsuario(_usuario);
            }
            catch (Exception ex)
            {

            }
            return exito;
        }
    }
}

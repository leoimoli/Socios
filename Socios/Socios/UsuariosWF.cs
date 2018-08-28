﻿using Socios.Entidades;
using Socios.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Socios
{
    public partial class UsuariosWF : MasterWF
    {
        public UsuariosWF()
        {
            InitializeComponent();
        }
        private void UsuariosWF_Load(object sender, EventArgs e)
        {
            try { CargarCombo(); }
            catch (Exception ex)
            {
                const string message = "Error en el sistema. Intente nuevamente o comuniquese con el administrador.";
                const string caption = "Atención";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Warning);
                throw new Exception();
            }
        }
        #region Botones
        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                FuncionesBotonNuevoUsuario();

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
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                FuncionesBotonCancelar();
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
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Usuario> _usuario = new List<Usuario>();
                var dni = txtDniBuscar.Text;
                _usuario = UsuarioNeg.BuscarUsuarioPorDni(dni);
                if (_usuario.Count > 0)
                {
                    var usuario = _usuario.First();
                    txtDni.Text = usuario.Dni;
                    txtApellido.Text = usuario.Apellido;
                    txtNombre.Text = usuario.Nombre;
                    dtFechaNac.Value = usuario.FechaDeNacimiento;
                    txtContraseña.Text = usuario.Contraseña;
                    txtRepiteContraseña.Text = usuario.Contraseña;
                    cmbPerfil.Text = usuario.Perfil;
                    txtDni.Enabled = false;
                    btnEditar.Visible = true;
                    btnEliminar.Visible = true;
                    btnHistorial.Visible = true;
                }
                else
                {
                    txtDniBuscar.Focus();
                    const string message = "No existe ningun usuario con el dni ingresado.";
                    const string caption = "Atención";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                               MessageBoxIcon.Exclamation);
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                FuncionesBotonEditar();

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
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var dni = txtDni.Text;
                if (dni != "")
                {
                    const string message = "Desea eliminar el usuario seleccionado ?";
                    const string caption = "Eliminar Usuario";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
                    {
                        if (result == DialogResult.Yes)
                        {
                            bool exito;
                            exito = UsuarioNeg.EliminarUsuario(dni);
                            if (exito == true)
                            {
                                ProgressBar();
                                const string message2 = "Se elimino el usuario exitosamente.";
                                const string caption2 = "Éxito";
                                var result2 = MessageBox.Show(message2, caption2,
                                                             MessageBoxButtons.OK,
                                                             MessageBoxIcon.Asterisk);
                            }
                            else { }
                        }
                        else
                        { }

                    }
                }
            }
            catch { }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Entidades.Usuario _usuario = CargarEntidad();
                if (txtDni.Enabled == false)
                {
                    bool Exito = UsuarioNeg.EditarUsuario(_usuario);
                    if (Exito == true)
                    {
                        ProgressBar();
                        const string message2 = "La edición del usuario se registro exitosamente.";
                        const string caption2 = "Éxito";
                        var result2 = MessageBox.Show(message2, caption2,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Asterisk);
                        LimpiarCampos();
                    }
                }
                else
                {

                    bool Exito = UsuarioNeg.GurdarUsuario(_usuario);
                    if (Exito == true)
                    {
                        ProgressBar();
                        const string message2 = "Se registro el usuario exitosamente.";
                        const string caption2 = "Éxito";
                        var result2 = MessageBox.Show(message2, caption2,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Asterisk);
                        LimpiarCampos();
                    }
                    else
                    {
                        txtDni.Focus();
                    }
                }
            }
            catch { }
        }
        #endregion
        #region Funciones
        private void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back);
        }
        private void FuncionesBotonCancelar()
        {
            txtDni.Clear();
            txtApellido.Clear();
            txtNombre.Clear();
            txtContraseña.Clear();
            txtRepiteContraseña.Clear();
            dtFechaNac.Value = DateTime.Now;
            CargarCombo();
            txtDni.Focus();
        }
        private void FuncionesBotonNuevoUsuario()
        {
            groupBox1.Enabled = true;
            txtDni.Focus();
            groupBox1.Text = "Nuevo Usuario";
        }
        private void CargarCombo()
        {
            string[] Perfiles = Clases_Maestras.ValoresConstantes.Perfiles;
            cmbPerfil.Items.Add("Seleccione");
            cmbPerfil.Items.Clear();
            foreach (string item in Perfiles)
            {
                cmbPerfil.Text = "Seleccione";
                cmbPerfil.Items.Add(item);
            }
        }
        private void FuncionesBotonEditar()
        {
            groupBox1.Enabled = true;
            groupBox1.Text = "Editar Usuario";
            txtApellido.Focus();
        }
        private void ProgressBar()
        {
            progressBar1.Visible = true;
            progressBar1.Maximum = 100000;
            progressBar1.Step = 1;

            for (int j = 0; j < 100000; j++)
            {
                Caluculate(j);
                progressBar1.PerformStep();
            }
        }
        private void Caluculate(int i)
        {
            double pow = Math.Pow(i, i);
        }
        private Usuario CargarEntidad()
        {
            Usuario _usuario = new Usuario();
            _usuario.Dni = txtDni.Text;
            _usuario.Apellido = txtApellido.Text;
            _usuario.Nombre = txtNombre.Text;
            DateTime fecha = dtFechaNac.Value;
            _usuario.FechaDeNacimiento = fecha;
            DateTime fechaActual = DateTime.Now;
            _usuario.FechaDeAlta = fechaActual;
            _usuario.FechaUltimaConexion = fechaActual;
            _usuario.Perfil = cmbPerfil.Text;
            _usuario.Estado = "ACTIVO";
            _usuario.Contraseña = txtContraseña.Text;
            _usuario.Contraseña2 = txtRepiteContraseña.Text;
            return _usuario;
        }
        private void LimpiarCampos()
        {
            txtDni.Clear();
            txtApellido.Clear();
            txtNombre.Clear();
            txtContraseña.Clear();
            txtRepiteContraseña.Clear();
            DateTime fecha = DateTime.Now;
            dtFechaNac.Value = fecha;
            string[] Perfiles = Clases_Maestras.ValoresConstantes.Perfiles;
            CargarCombo();
            progressBar1.Value = Convert.ToInt32(null);
            progressBar1.Visible = false;
            groupBox1.Enabled = false;
            txtDniBuscar.Focus();
        }
        #endregion
    }
}

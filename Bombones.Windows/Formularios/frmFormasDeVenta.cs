using Bombones.Entidades.Entidades;
using Bombones.Servicios.Intefaces;
using Bombones.Windows.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bombones.Windows.Formularios
{
    public partial class frmFormasDeVenta : Form
    {


        private readonly IServiciosFormasDeVenta? _servicio;
        private List<FormasDeVenta>? lista;
        public frmFormasDeVenta(IServiceProvider? serviceProvider)
        {
            InitializeComponent();
            _servicio = serviceProvider?.GetService<IServiciosFormasDeVenta>()
    ?? throw new ApplicationException("Dependencias no cargadas!!!");
        }

        private void frmFormasDeVenta_Load(object sender, EventArgs e)
        {
            try
            {
                lista = _servicio!.GetLista();
                MostrarDatosEnGrilla();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MostrarDatosEnGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            if (lista is not null)
            {
                foreach (var item in lista)
                {
                    var r = GridHelper.ConstruirFila(dgvDatos);
                    GridHelper.SetearFila(r, item);
                    GridHelper.AgregarFila(r, dgvDatos);
                }

            }
        }



        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmFormaDeVentaAE frm = new frmFormaDeVentaAE() { Text = "Agregar Forma de pago" };
            DialogResult dr = frm.ShowDialog(this);
            try
            {
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                FormasDeVenta? formasDeVenta = frm.GetFormasDeVenta();
                if (formasDeVenta is null)
                {
                    return;
                }

                if (!_servicio!.Existe(formasDeVenta))
                {
                    _servicio!.Guardar(formasDeVenta);

                    int row = GridHelper.ObtenerRowIndex(dgvDatos, formasDeVenta.FormasDeVentaid);
                    GridHelper.MarcarRow(dgvDatos, row);

                    MessageBox.Show("Registro agregado",
                        "Mensaje",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Forma de Venta Duplicado!!!",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

            }
        }
    }
}

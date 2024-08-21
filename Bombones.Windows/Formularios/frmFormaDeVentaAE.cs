using Bombones.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bombones.Windows.Formularios
{
    public partial class frmFormaDeVentaAE : Form
    {
        private FormasDeVenta? formasDeVenta;
        public frmFormaDeVentaAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (formasDeVenta != null)
            {
                txtForma.Text = formasDeVenta.formasDeVenta;
            }
        }

        public FormasDeVenta? GetFormasDeVenta()
        {
            return formasDeVenta;
        }
        public void SetFormasDeVenta(FormasDeVenta? formasDeVenta)
        {
            this.formasDeVenta = formasDeVenta;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (formasDeVenta == null)
                {
                    formasDeVenta = new FormasDeVenta();
                }
                formasDeVenta.formasDeVenta = txtForma.Text;

                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtForma.Text))
            {
                valido = false;
                errorProvider1.SetError(txtForma, "El país es requerido");
            }
            return valido;
        }
    }
}

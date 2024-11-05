using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL;
using BE;

namespace SistemaEstacionamiento
{
    public partial class MenuInicial : Form
    {
        public MenuInicial()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            //this.Width = 800;
            //this.Height = 600;
        }


        /// <summary>
        /// Administrativo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listadoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //PlayasForm playasForm = new PlayasForm();
            //playasForm.MdiParent = this;
            //playasForm.WindowState = FormWindowState.Maximized;
            ////playasForm.MinimizeBox = false;
            ////playasForm.MaximizeBox = false;
            //playasForm.Show();
            PlayasForm formHijo = new PlayasForm();
            formHijo.MdiParent = this; // Establece el formulario padre
            formHijo.WindowState = FormWindowState.Normal; // Asegura que no esté maximizado
            formHijo.StartPosition = FormStartPosition.CenterScreen; // Opcional: Centrar el formulario en la pantalla
            formHijo.Size = new Size(800, 600); // Establece el tamaño deseado (ancho x alto)
            formHijo.Show(); // Muestra el formulario

        }

        private void listadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //VehiculosForm vehiculosForm = new VehiculosForm();
            //vehiculosForm.MdiParent = this;
            //vehiculosForm.WindowState = FormWindowState.Maximized;
            //vehiculosForm.MinimizeBox = false;
            //vehiculosForm.MaximizeBox = false;
            //vehiculosForm.Show();

            PlayasForm playasForm = new PlayasForm();
            playasForm.MdiParent = this;
            playasForm.WindowState = FormWindowState.Maximized;
            playasForm.MinimizeBox = false;
            playasForm.MaximizeBox = false;
            playasForm.sinEdicion=true;
            playasForm.Show();
        }
    }
}

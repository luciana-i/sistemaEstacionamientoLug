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
            this.Width = 800;
            this.Height = 600;
        }

        private void listadoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PlayasForm playasForm = new PlayasForm();
            playasForm.MdiParent = this;
            playasForm.WindowState = FormWindowState.Maximized;
            playasForm.MinimizeBox = false;
            playasForm.MaximizeBox = false;
            playasForm.Show();

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

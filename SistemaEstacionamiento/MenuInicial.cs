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
            PlayasForm playasForm = new PlayasForm();
            playasForm.MdiParent = this; 
            playasForm.WindowState = FormWindowState.Normal; 
            playasForm.StartPosition = FormStartPosition.CenterScreen; 
            playasForm.Size = new Size(800, 600); 
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
            playasForm.WindowState = FormWindowState.Normal;
            playasForm.StartPosition = FormStartPosition.CenterScreen;
            playasForm.Size = new Size(800, 600);
            playasForm.sinEdicion = true;
            playasForm.Show();

            //PlayasForm playasForm = new PlayasForm();
            //playasForm.MdiParent = this;
            //playasForm.WindowState = FormWindowState.Maximized;
            //playasForm.MinimizeBox = false;
            //playasForm.MaximizeBox = false;
            //playasForm.Show();
        }

        private void vehiculosToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ABMVehiculoForm vehiculosAbmForm = new ABMVehiculoForm();
            vehiculosAbmForm.MdiParent = this;
            vehiculosAbmForm.WindowState = FormWindowState.Normal;
            vehiculosAbmForm.StartPosition = FormStartPosition.CenterScreen;
            vehiculosAbmForm.Size = new Size(800, 600);
            vehiculosAbmForm.Show();

           
        }
    }
}

using BE;
using BL.Excepciones;
using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SistemaEstacionamiento
{
    public partial class PlayaCocherasForm : Form
    {
        public PlayaCocherasForm()
        {
            InitializeComponent();
        }
        public PlayaCocherasForm(int idPlaya)
        {
            InitializeComponent();
        }

        private void PlayaCocherasForm_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("IdEspacio", "IdEspacio");
            dataGridView1.Columns["IdEspacio"].Visible = false;
            dataGridView1.Columns.Add("Piso", "Piso");
            dataGridView1.Columns["Piso"].Width = 100;
            dataGridView1.Columns.Add("Tamano", "Tamaño");
            dataGridView1.Columns["Tamano"].Width = 150;
            dataGridView1.Columns.Add("PorcentajeValor", "Porcentaje Valor");
            dataGridView1.Columns["PorcentajeValor"].Width = dataGridView1.Width - 500;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            


            //Actualizar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CocherasForm cForm = new CocherasForm();
            cForm.MinimizeBox = false;
            cForm.MaximizeBox = false;
            cForm.Show();


        }
    }
}

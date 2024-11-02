using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BL;
using BL.Excepciones;

namespace SistemaEstacionamiento
{
    public partial class PlayasForm : Form
    {
        PlayaBL playaBL = new PlayaBL();

        public bool sinEdicion= false;
        public PlayasForm()
        {
            InitializeComponent();
        }

        private void PlayasForm_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("IdPlaya", "IdPlaya");
            dataGridView1.Columns["IdPlaya"].Visible = false;
            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns["Nombre"].Width = 150;
            dataGridView1.Columns.Add("Direccion", "Direccion");
            dataGridView1.Columns["Direccion"].Width = 150;
            dataGridView1.Columns.Add("HoraApertura", "HoraApertura");
            dataGridView1.Columns["HoraApertura"].Width = dataGridView1.Width - 500;
            dataGridView1.Columns.Add("HoraCierre", "HoraCierre");
            dataGridView1.Columns["HoraCierre"].Width = dataGridView1.Width - 500;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (sinEdicion)
            {
                button1.Hide();
                button4.Hide();
                button3.Hide();
            }


            Actualizar();
        }

        private void Actualizar()
        {
            dataGridView1.Rows.Clear();
            foreach (Playa playa in playaBL.Listar())
            {
                dataGridView1.Rows.Add(playa.IdPlaya, playa.Nombre, playa.Direccion, playa.HoraApertura, playa.HoraCierre);

            }
        }

        /*
         boton de edicion
         */
        private void button3_Click(object sender, EventArgs e)
        {
           
            PlayaCocherasForm pcForm = new PlayaCocherasForm();
            pcForm.playaEditada = playaBL.Obtener(int.Parse(dataGridView1.SelectedRows[0].Cells["IdPlaya"].Value.ToString()));
            pcForm.MinimizeBox = false;
            pcForm.MaximizeBox = false;
            pcForm.ShowDialog();
            Actualizar();
            
        }

        /*
        boton de eliminar 
        */
        private void button4_Click(object sender, EventArgs e)
        {
                int id = int.Parse(dataGridView1.SelectedRows[0].Cells["IdPlaya"].Value.ToString());
                if (id > 0)
                {
                    if (Preguntar())
                    {
                        playaBL.Eliminar(id);
                        Actualizar();
                        MessageBox.Show("Se eliminaron exitosamente los datos");
                
                    }
                }else
                {
                    MessageBox.Show("No seleccionaste ninguna playa para eliminar");
                }
                
          
        }

        private bool Preguntar()
        {
            DialogResult result = MessageBox.Show("Si elegis esta opcion vas a borrar todos los datos asociados, ¿Deseas continuar de todos modos?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    

        private void button1_Click(object sender, EventArgs e)
        {
            PlayaCocherasForm pcForm = new PlayaCocherasForm();
            pcForm.MinimizeBox = false;
            pcForm.MaximizeBox = false;
            Playa playa = new Playa();
            pcForm.playaEditada = playa;
            pcForm.Show();
            Actualizar();
        }
    }
}

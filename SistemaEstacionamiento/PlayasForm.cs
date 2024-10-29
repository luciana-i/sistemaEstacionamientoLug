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
        bool editando = false;
        public PlayasForm()
        {
            InitializeComponent();
        }

        private void PlayasForm_Load(object sender, EventArgs e)
        {
            CocherasButton.Enabled = editando;
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

        /***
         * guardar - actualizar
         */
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Playa playa = new Playa();
                if (!editando)
                {
                    LlenarObjetoPlaya(playa);
                    VaciarTextbox();
                    playaBL.Guardar(playa);
                }
                else
                {
                    playa.IdPlaya = int.Parse(dataGridView1.SelectedRows[0].Cells["IdPlaya"].Value.ToString());
                    LlenarObjetoPlaya(playa);
                    VaciarTextbox();
                    playaBL.Guardar(playa);
                    editando = false;
                    CocherasButton.Enabled = editando;
                }
                Actualizar();
            }
            catch (IcompleteException  ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            
        
        }

        private void VaciarTextbox()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        void LlenarObjetoPlaya(Playa playa)
        {
            if(textBox1.Text != "" && textBox2.Text!="" && textBox3.Text!="" && textBox4.Text != "")
            {
                playa.Nombre = textBox1.Text;
                playa.Direccion = textBox2.Text;
                playa.HoraApertura = TimeSpan.Parse(textBox3.Text);
                playa.HoraCierre = TimeSpan.Parse(textBox4.Text);
            }else
            {
                throw new BL.Excepciones.IcompleteException("Debe completar todos los campos para poder guardar los datos");
            }
           
        }
        /*
         boton de edicion
         */
        private void button3_Click(object sender, EventArgs e)
        {
            editando = true;
            textBox1.Text = dataGridView1.SelectedRows[0].Cells["Nombre"].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells["Direccion"].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells["HoraApertura"].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells["HoraCierre"].Value.ToString();
            CocherasButton.Enabled= editando;
        }

        /*
        boton de eliminar TODO: hay que eliminar los objetos Contenidos
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

        private void CocherasButton_Click(object sender, EventArgs e)
        {
            EspaciosForm espaciosForm = new EspaciosForm();
            espaciosForm.MinimizeBox = false;
            espaciosForm.MaximizeBox = false;
            espaciosForm.Show();
        }
    }
}

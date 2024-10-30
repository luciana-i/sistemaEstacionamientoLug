using BE;
using BL;
using BL.Excepciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SistemaEstacionamiento
{
    public partial class EspaciosForm : Form
    {
        EspacioBL espacioBL = new EspacioBL();
        bool editando = false;
        public EspaciosForm()
        {
            InitializeComponent();
        }

        private void EspaciosForm_Load(object sender, EventArgs e)
        {
          /*  dataGridView1.Columns.Add("IdEspacio", "IdEspacio");
            dataGridView1.Columns["IdEspacio"].Visible = false;
            dataGridView1.Columns.Add("Piso", "Piso");
            dataGridView1.Columns["Piso"].Width = 100;
            dataGridView1.Columns.Add("Tamano", "Tamaño");
            dataGridView1.Columns["Tamano"].Width = 150;
            dataGridView1.Columns.Add("PorcentajeValor", "PorcentajeValor");
            dataGridView1.Columns["PorcentajeValor"].Width = dataGridView1.Width - 500;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //valor mes
            textBox2.Visible = false;
            label5.Visible = false;

            //hora entrada
            textBox4.Visible = false;
            label6.Visible = false;

            //hora salida
            textBox5.Visible = false;
            label7.Visible = false;
          */

            Actualizar();
        }
        private void Actualizar()
        {
            /*dataGridView1.Rows.Clear();
            foreach (Espacio espacio in espacioBL.Listar())
            {
                dataGridView1.Rows.Add(espacio.IdEspacio, espacio.Piso, espacio.Tamano, espacio.PorcentajeValor);

            }
            */
        }
        /*
         Guardar -editar
         */
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Espacio espacio = new Espacio();
                if (!editando)
                {
                    LlenarObjetoEspacio(espacio);
                    VaciarTextbox();
                    espacioBL.Guardar(espacio);
                }
                else
                {
                    espacio.IdEspacio = 1;//int.Parse(dataGridView1.SelectedRows[0].Cells["IdEspacio"].Value.ToString());
                    LlenarObjetoEspacio(espacio);
                    VaciarTextbox();
                    espacioBL.Guardar(espacio);
                    editando = false;
                }
                Actualizar();
            }
            catch (IcompleteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            void LlenarObjetoEspacio(Espacio espacio)
            {
                
                if (textBox1.Text != "" && comboBox1.SelectedItem != null && textBox3.Text != "")
                {
                    espacio.Piso = int.Parse(textBox1.Text);
                    espacio.PorcentajeValor = int.Parse(textBox3.Text);
                    espacio.Tamano = comboBox1.SelectedItem.ToString();
                }
                else
                {
                    throw new BL.Excepciones.IcompleteException("Debe completar todos los campos para poder guardar los datos");
                }

            }

            void VaciarTextbox()
            {
                textBox1.Text = "";
                textBox3.Text = "";
                comboBox1.Text = "";
            }
        }
        /*
         editar
         */
        private void button3_Click(object sender, EventArgs e)
        {
            editando=true;
            textBox1.Text = "";// dataGridView1.SelectedRows[0].Cells["Piso"].Value.ToString();
            comboBox1.SelectedIndex = 1;// comboBox1.Items.IndexOf(dataGridView1.SelectedRows[0].Cells["Tamano"].Value.ToString());
            textBox3.Text = "";//dataGridView1.SelectedRows[0].Cells["PorcentajeValor"].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            int id = 0;//int.Parse(dataGridView1.SelectedRows[0].Cells["IdEspacio"].Value.ToString());
            if (id > 0)
            {
                if (Preguntar())
                {
                    espacioBL.Eliminar(id);
                    Actualizar();
                }
            }
            else
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                if (comboBox2.SelectedItem == "Fija")
                {
                    /*
                    textBox2.Visible = true;
                    label5.Visible = true;
                    
                    //hora entrada
                    textBox4.Visible = false;
                    label6.Visible = false;
                    textBox4.Text = "";

                    //hora salida
                    textBox5.Visible = false;
                    label7.Visible = false;
                    textBox5.Text = "";
                    */
                }
                else if(comboBox2.SelectedItem == "Movil")
                {
                    /*textBox4.Visible = true;
                    textBox5.Visible = true;
                    label7.Visible = true;
                    label6.Visible = true;

                    //valor mes
                    textBox2.Visible = false;
                    label5.Visible = false;
                    textBox2.Text = "";
                    */
                }
                

                

            }
        }
    }
}

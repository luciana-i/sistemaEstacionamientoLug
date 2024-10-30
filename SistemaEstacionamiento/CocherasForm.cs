using BE;
using BL.Excepciones;
using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SistemaEstacionamiento
{
    public partial class CocherasForm : Form
    {
        bool editando = false;
        CocheraFijaBL cocheraFijaBL = new CocheraFijaBL();
        CocheraMovilBL cocheraMovilBL = new CocheraMovilBL();
        public CocherasForm()
        {
            InitializeComponent();
        }

        private void CocherasForm_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// editar y guardar cochera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Espacio cochera;
                if (comboBox2.SelectedItem == "Movil")
                {
                    cochera = new CocheraMovil();

                }
                else
                {
                    cochera = new CocheraFija();
                }
                if (!editando)
                {
                    LlenarObjetoCochera(cochera);
                    VaciarTextbox();
                    guardarCochera(cochera);
                }
                else
                {
                    cochera.IdEspacio = 1;//int.Parse(dataGridView1.SelectedRows[0].Cells["IdEspacio"].Value.ToString());
                    LlenarObjetoCochera(cochera);
                    VaciarTextbox();
                    guardarCochera(cochera);
                    editando = false;
                }
            }
            catch (IcompleteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Log(1,"error",ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void guardarCochera(Espacio cochera)
        {
            if (cochera is CocheraFija)
            {
                cocheraFijaBL.Guardar((CocheraFija)cochera);
            }
            else
            {
                cocheraMovilBL.Guardar((CocheraMovil)cochera);
            }
        }

        void LlenarObjetoCochera(Espacio cochera)
        {

            if (textBox1.Text != "" && comboBox1.SelectedItem != null && textBox3.Text != "")
            {
                cochera.Piso = int.Parse(textBox1.Text);
                cochera.PorcentajeValor = int.Parse(textBox3.Text);
                cochera.Tamano = comboBox1.SelectedItem.ToString();
              
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
}

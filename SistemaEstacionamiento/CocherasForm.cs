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
        CocheraFijaBL cocheraFijaBL = new CocheraFijaBL();
        CocheraMovilBL cocheraMovilBL = new CocheraMovilBL();
        int idEspacio = 0;
        int idCocheraEditada = 0;
        public CocherasForm()
        {
            InitializeComponent();
        }

        public CocherasForm( int id)
        {
            idEspacio = id;
            InitializeComponent();
        }

        private void CocherasForm_Load(object sender, EventArgs e)
        {
            if (idEspacio != 0)
            {
                Espacio cocheraEditada = cocheraFijaBL.Listar().FirstOrDefault(x=> x.IdEspacio == idEspacio);
                if (cocheraEditada == null)
                {
                    
                    cocheraEditada = cocheraMovilBL.Listar().FirstOrDefault(x => x.IdEspacio == idEspacio);
                    idCocheraEditada = ((CocheraMovil)cocheraEditada).IdCocheraMovil;

                }
                else
                {
                    idCocheraEditada = ((CocheraFija)cocheraEditada).IdCocheraFija;
                }

                textBox1.Text = cocheraEditada.Piso.ToString();
                textBox3.Text = cocheraEditada.PorcentajeValor.ToString();
                comboBox1.Text = cocheraEditada.Tamano.ToString();
                comboBox2.Text = (cocheraEditada is CocheraFija )?  "Fija":"Movil";
                comboBox2.Enabled = false;
            }
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
                if (idEspacio==0)
                {
                    LlenarObjetoCochera(cochera);
                    VaciarTextbox();
                    guardarCochera(cochera);
                    this.Close();
                }
                else
                {
                    LlenarObjetoCochera(cochera);
                    cochera.IdEspacio = idEspacio;
                    VaciarTextbox();
                    if (cochera is CocheraMovil)
                    {
                        ((CocheraMovil)cochera).IdEspacio = idEspacio;
                        ((CocheraMovil)cochera).IdCocheraMovil = idCocheraEditada;
                        cocheraMovilBL.Guardar((CocheraMovil)cochera);
                    }
                    else
                    {
                        ((CocheraFija)cochera).IdEspacio = idEspacio;
                        ((CocheraFija)cochera).IdCocheraFija = idCocheraEditada;
                        cocheraFijaBL.Guardar((CocheraFija)cochera);
                    }
                }
                this.Close();
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
            comboBox2.Text = "";
        }
    }
}

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
using System.Reflection;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SistemaEstacionamiento
{
    public partial class CocherasForm : Form
    {

        public CocheraDto cocheraDtoEditada;
        EspacioBL espacioBL = new EspacioBL();
        public List<CocheraDto> cocherasEditadasDto { get; set; }
       
        public CocherasForm()
        {
            InitializeComponent();
        }


        private void CocherasForm_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#1ac1fd");
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;

            if (cocheraDtoEditada !=null)
            {

                textBox1.Text = cocheraDtoEditada.Piso.ToString();
                textBox3.Text = cocheraDtoEditada.PorcentajeValor.ToString();
                comboBox1.Text = cocheraDtoEditada.Tamano.ToString();
                comboBox2.Text = (cocheraDtoEditada.TipoCocheraEnum == Constantes.TipoCochera.Fija )?  "Fija":"Movil";
                comboBox2.Enabled = false;
                
            }
            textBox3.ReadOnly = true;

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

                if (cocheraDtoEditada==null)
                {
                    CocheraDto cocheraDto = new CocheraDto();
                    if (comboBox2.SelectedItem == "Movil")
                    {
                        cocheraDto.TipoCocheraEnum = Constantes.TipoCochera.Movil;

                    }
                    else
                    {
                        cocheraDto.TipoCocheraEnum = Constantes.TipoCochera.Fija;
                    }
                    cocheraDto.EstadoColeccion = Constantes.EstadosColeccion.Agregado;
                    LlenarObjetoCochera(cocheraDto);
                    VaciarTextbox();
                    guardarCochera(cocheraDto);
                    
                }
                else
                {
                    LlenarObjetoCochera(cocheraDtoEditada);
                    if (cocheraDtoEditada.EstadoColeccion != Constantes.EstadosColeccion.Agregado) // significa que si agrego algo y despues lo modifico, entonces no es un update, sigue siendo un insert
                             cocheraDtoEditada.EstadoColeccion = Constantes.EstadosColeccion.Modificado;
                    VaciarTextbox();
                    cocherasEditadasDto[cocheraDtoEditada.IndiceColeccion]= cocheraDtoEditada;

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
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);

            }
        }

        private void guardarCochera(CocheraDto cocheraDto)
        {

            cocherasEditadasDto.Add(cocheraDto);
            int index = cocherasEditadasDto.IndexOf(cocheraDto);
            cocherasEditadasDto[index].IndiceColeccion = index;
  
        }

        void LlenarObjetoCochera(CocheraDto cochera)
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int valor))
            {
                textBox3.Text = espacioBL.PorcentajeValorChanged(valor).ToString();
            }
           
        }
    }
}

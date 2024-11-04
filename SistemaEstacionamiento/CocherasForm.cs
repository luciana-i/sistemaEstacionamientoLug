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
        //CocheraFijaBL cocheraFijaBL = new CocheraFijaBL();
        //CocheraMovilBL cocheraMovilBL = new CocheraMovilBL();
        //PlayaBL playaBL = new PlayaBL();
        //int idEspacio = 0;
        //int idCocheraEditada = 0;
        public CocheraDto cocheraDtoEditada;
        EspacioBL espacioBL = new EspacioBL();
        public List<CocheraDto> cocherasEditadasDto { get; set; }
        //public Playa playaEditada { get; set; }
       
        public CocherasForm()
        {
            InitializeComponent();
        }


        private void CocherasForm_Load(object sender, EventArgs e)
        {
            if (cocheraDtoEditada !=null)
            {
                //Espacio cocheraEditada = cocheraFijaBL.Listar().FirstOrDefault(x=> x.IdEspacio == idEspacio);



                //if (cocheraDtoEditada == null)
                //{
                    
                //    cocheraEditada = cocheraMovilBL.Listar().FirstOrDefault(x => x.IdEspacio == idEspacio);
                //    idCocheraEditada = ((CocheraMovil)cocheraEditada).IdCocheraMovil;

                //}
                //else
                //{
                //    idCocheraEditada = ((CocheraFija)cocheraEditada).IdCocheraFija;
                //}

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
                //Espacio cochera;
                //if (comboBox2.SelectedItem == "Movil")
                //{
                //    cochera = new CocheraMovil();
                   
                //}
                //else
                //{
                //    cochera = new CocheraFija();
                //}
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
                    //cocheraDtoEditada.IdEspacio = idEspacio;
                    if (cocheraDtoEditada.EstadoColeccion != Constantes.EstadosColeccion.Agregado) // significa que si agrego algo y despues lo modifico, entonces no es un update, sigue siendo un insert
                             cocheraDtoEditada.EstadoColeccion = Constantes.EstadosColeccion.Modificado;
                    //cochera.IdEspacio = idEspacio;
                    VaciarTextbox();
                    cocherasEditadasDto[cocheraDtoEditada.IndiceColeccion]= cocheraDtoEditada;
                    //guardarCochera(cocheraDtoEditada);
                    //if (cochera is CocheraMovil)
                    //{
                    //    ((CocheraMovil)cochera).IdEspacio = idEspacio;
                    //    ((CocheraMovil)cochera).IdCocheraMovil = idCocheraEditada;
                    //    cocheraMovilBL.Guardar((CocheraMovil)cochera);

                    //}
                    //else
                    //{
                    //    ((CocheraFija)cochera).IdEspacio = idEspacio;
                    //    ((CocheraFija)cochera).IdCocheraFija = idCocheraEditada;
                    //    cocheraFijaBL.Guardar((CocheraFija)cochera);
                    //}
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
            //if (cocheraDto.TipoCocheraEnum == Constantes.TipoCochera.Fija)
            //{

            //    // playaEditada.AgregarEspacio(cochera);
            //    //CocheraDto cocheraDto = new CocheraDto();
            //    //cocheraDto.Tamano = cochera.Tamano;
            //    //cocheraDto.Piso = cochera.Piso;
            //    //cocheraDto.PorcentajeValor = cochera.PorcentajeValor;
            //    //cocheraDto.TipoCocheraEnum = Constantes.TipoCochera.Fija;
            //    //cocheraDto.EstadoColeccion = Constantes.EstadosColeccion.Agregado;
            //    cocherasEditadasDto.Add(cocheraDto);
            //    int index = cocherasEditadasDto.IndexOf(cocheraDto);
            //    cocherasEditadasDto[index].IndiceColeccion = index;
            //   // cocheraFijaBL.Guardar((CocheraFija)cochera);
            //    //
            //}
            //else
            //{
            //    //CocheraDto cocheraDto = new CocheraDto();
            //    //cocheraDto.Tamano = cochera.Tamano;
            //    //cocheraDto.Piso = cochera.Piso;
            //    //cocheraDto.PorcentajeValor = cochera.PorcentajeValor;
            //    //cocheraDto.TipoCocheraEnum = Constantes.TipoCochera.Movil;
            //    //cocheraDto.EstadoColeccion = Constantes.EstadosColeccion.Agregado;
            //    cocherasEditadasDto.Add(cocheraDto);
            //    int index = cocherasEditadasDto.IndexOf(cocheraDto);
            //    cocherasEditadasDto[index].IndiceColeccion = index;
            //    //playaEditada.AgregarEspacio(cochera);
            //    // cocheraMovilBL.Guardar((CocheraMovil)cochera);

            //}
        }

        void LlenarObjetoCochera(CocheraDto cochera)
        {

            if (textBox1.Text != "" && comboBox1.SelectedItem != null && textBox3.Text != "")
            {
                cochera.Piso = int.Parse(textBox1.Text);
                cochera.PorcentajeValor = int.Parse(textBox3.Text);
                cochera.Tamano = comboBox1.SelectedItem.ToString();
                //cochera.IdPlaya = playaEditada.IdPlaya;


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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Presionaste un número: " + e.KeyChar);
            }
            // Verifica si la tecla presionada es la tecla de retroceso
            else if (e.KeyChar == (char)Keys.Back)
            {
                MessageBox.Show("Presionaste la tecla de retroceso.");
            }
            else
            {
                // Cancela el evento si no es un número o la tecla de retroceso
                e.Handled = true;
            }
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

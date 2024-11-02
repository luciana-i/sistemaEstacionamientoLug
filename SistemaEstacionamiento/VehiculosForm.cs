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
    public partial class VehiculosForm : Form
    {
        VehiculoBL vehiculoBL = new VehiculoBL();
        TipoVehiculoBL tipoVehiculoBL = new TipoVehiculoBL();
        bool editando = false;
        Espacio espacio;
        EspacioBL espacioBL = new EspacioBL();
        CocheraFijaBL fijaBL = new CocheraFijaBL();
        CocheraMovilBL movilBL = new CocheraMovilBL();
        Vehiculo vehiculo;
        List<Espacio> ListaEspacios = new List<Espacio>();
        public Playa playaEditada { get; set; }
        public VehiculosForm()
        {
            InitializeComponent();
        }

        private void VehiculosForm_Load(object sender, EventArgs e)
        {

            
            comboBox4.DataSource = vehiculoBL.ListarVehiculosSinEstacionar();

            dataGridView1.Hide();
            llenarCombo();
            //comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            //Actualizar();
        }

        private void llenarCombo()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(tipoVehiculoBL.Listar().ToArray());
        }

        //private void Actualizar(Espacio espacios)
        //{
        //    dataGridView1.Rows.Clear();
        //    foreach (Espacio espacio in espacios)
        //    {
        //        dataGridView1.Rows.Add(vehiculo.IdVehiculo, vehiculo.Patente, vehiculo.Abono, vehiculo.TipoVehiculo);

        //    }
        //}

        /***
         * guardar - actualizar
         */
        private void button2_Click_1(object sender, EventArgs e)
        {
           
            try
            {
                vehiculo = new Vehiculo();
                if (!editando)
                {
                    LlenarObjetoVehiculo(vehiculo);
                    VaciarTextbox();
                    vehiculoBL.Guardar(vehiculo);
                }
                else
                {
                    vehiculo.IdVehiculo = int.Parse(dataGridView1.SelectedRows[0].Cells["IdVehiculo"].Value.ToString());
                    LlenarObjetoVehiculo(vehiculo);
                    VaciarTextbox();
                    vehiculoBL.Guardar(vehiculo);
                    editando = false;

                }
                MessageBox.Show("Agregaste con exito el vehiculo con patente: " + vehiculo.Patente + " ahora elegi la cochera " + comboBox3.SelectedText);
                if(comboBox3.SelectedItem == "Fija")
                {
                    espacio = new CocheraFija();
                }else
                    espacio = new CocheraMovil();
                dataGridView1.Show();
                ActualizarCocheras();
            }
            catch (IcompleteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
           
        }

        private void ActualizarCocheras()
        {
            columnasDGV();
            if (espacio is CocheraFija)
            {
                List<CocheraFija> lista = fijaBL.ListarPorPlaya(playaEditada.IdPlaya);

                dataGridView1.Rows.Clear();
                ListaEspacios.AddRange(lista);
                
                foreach (CocheraFija cochera in lista.Where(x => x.Vehiculo == null).Cast<Espacio>().ToList())
                {
                    dataGridView1.Columns.Add("IdCocheraFija", "IdCocheraFija");
                    dataGridView1.Columns["IdCocheraFija"].Visible = false;
                    dataGridView1.Rows.Add(cochera.IdEspacio, cochera.Piso, cochera.Piso, cochera.Tamano, cochera.PorcentajeValor, cochera.IdCocheraFija);
                }
               
            }
            if (espacio is CocheraMovil)
            {
                List<CocheraMovil> lista = movilBL.ListarPorPlaya(playaEditada.IdPlaya);

                dataGridView1.Rows.Clear();
                ListaEspacios.AddRange(lista);
                foreach (CocheraMovil cochera in lista.Where(x => x.Vehiculo == null).Cast<Espacio>().ToList())
                {
                    dataGridView1.Columns.Add("IdCocheraMovil", "IdCocheraMovil");
                    dataGridView1.Columns["IdCocheraMovil"].Visible = false;
                    dataGridView1.Rows.Add(cochera.IdEspacio, cochera.Piso, cochera.Piso, cochera.Tamano, cochera.PorcentajeValor, cochera.IdCocheraMovil);
                }
            }

            
        }


        private void columnasDGV()
        {
            dataGridView1.Columns.Add("IdEspacio", "IdEspacio");
            dataGridView1.Columns["IdEspacio"].Visible = false;
            dataGridView1.Columns.Add("Piso", "Piso");
            dataGridView1.Columns["Piso"].Width = 150;
            dataGridView1.Columns.Add("Tamano", "Tamaño");
            dataGridView1.Columns.Add("PorcentajeValor", "PorcentajeValor"); 
            dataGridView1.Columns.Add("IdCocheraMovil", "IdCocheraMovil");
            dataGridView1.Columns["IdCocheraMovil"].Visible = false;
            dataGridView1.Columns.Add("IdCocheraFija", "IdCocheraFija");
            dataGridView1.Columns["IdCocheraFija"].Visible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //switch (vehiculo.TipoVehiculo.Nombre)
            //{
            //    case "Moto":
            //        Código para el caso "Moto"
            //        break;

            //        Puedes agregar otros casos aquí
            //    case "Auto":
            //        Código para el caso "Auto"
            //        break;

            //    default:
            //        Código para el caso por defecto
            //        break;
            //}
        }

        private void VaciarTextbox()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private void LlenarObjetoVehiculo(Vehiculo vehiculo)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.SelectedIndex>-1 && comboBox3.SelectedIndex > -1)
            {
                vehiculo.Patente = textBox1.Text;
                vehiculo.Abono = textBox2.Text;
                TipoVehiculo selectedTipoVehiculo = (TipoVehiculo)comboBox1.SelectedItem;
                vehiculo.TipoVehiculo = tipoVehiculoBL.Obtener(selectedTipoVehiculo.IdTipoVehiculo);
            }
            else
            {
                throw new BL.Excepciones.IcompleteException("Debe completar todos los campos para poder guardar los datos");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool Preguntar()
        {
            DialogResult result = MessageBox.Show("Si elegis esta opcion vas a borrar el vehiculo, ¿Deseas continuar de todos modos?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        //Ingreso vehiculo Seleccionar cochera TODO: CALCULO PLATA Y HORA
        private void button3_Click(object sender, EventArgs e)
        {
            Espacio espacioAEditar = ListaEspacios.FirstOrDefault(x => x.IdEspacio == int.Parse(dataGridView1.SelectedRows[0].Cells["IdEspacio"].Value.ToString()));
         
            if (espacioAEditar is CocheraFija)
            {
                if(Preguntar("El valor que debe abonar es: " + vehiculo.TipoVehiculo.ValorEstadia + "¿Desea abonar por adelantado?"))
                {
                    vehiculo.Abono = "Si";
                }else
                {
                    vehiculo.Abono = "No";
                }
                (espacioAEditar as CocheraFija).ValorMes = vehiculo.TipoVehiculo.ValorEstadia;
                fijaBL.Guardar(espacioAEditar as CocheraFija);


            }
            if (espacioAEditar is CocheraMovil)
            {
                MessageBox.Show("El valor por hora es: " + vehiculo.TipoVehiculo.ValorHora + "Si se pasa de las 5 horas, cobrara estadia");
                (espacioAEditar as CocheraMovil).HoraEntrada = DateTime.Now.TimeOfDay;
                movilBL.Guardar(espacioAEditar as CocheraMovil);
            }
            espacioAEditar.Vehiculo = vehiculo;
            espacioBL.Guardar(espacioAEditar);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == "Fija")
            {
                espacio = new CocheraFija();
            }
            else
                espacio = new CocheraMovil();
            vehiculo = comboBox4.SelectedItem as Vehiculo;
            dataGridView1.Show();
            ActualizarCocheras();
        }

        private bool Preguntar(String mensaje)
        {
            DialogResult result = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}

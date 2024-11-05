using BE;
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
using static BE.Constantes;

namespace SistemaEstacionamiento
{
    public partial class CocheraVehiculoForm : Form
    {
        public Playa playaEditada { get; set; }
        CocheraFijaBL fijaBL = new CocheraFijaBL();
        CocheraMovilBL movilBL = new CocheraMovilBL();
        EspacioBL espacioBL = new CocheraMovilBL();
        VehiculoBL vehiculoBL = new VehiculoBL();
        List<CocheraDto> cocheraDtos = new List<CocheraDto>();

        public CocheraVehiculoForm()
        {
            InitializeComponent();
        }

        private void CocheraVehiculo_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#1ac1fd");
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            

            textBox1.Text = playaEditada.Nombre;
            textBox2.Text = playaEditada.HoraApertura.ToString(@"hh\:mm");
            textBox3.Text = playaEditada.HoraCierre.ToString(@"hh\:mm");
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.Text = DateTime.Now.TimeOfDay.ToString(@"hh\:mm");
            textBox4.ReadOnly = true;

            cocherasDGV.Columns.Add("IdEspacio", "IdEspacio");
            cocherasDGV.Columns["IdEspacio"].Visible = false;
            cocherasDGV.Columns.Add("Ocupada", "Ocupada");
            cocherasDGV.Columns.Add("Piso", "Piso");
            cocherasDGV.Columns["Piso"].Width = 100;
            cocherasDGV.Columns.Add("Tamano", "Tamaño");
            cocherasDGV.Columns["Tamano"].Width = 150;
            cocherasDGV.Columns.Add("TipoCochera", "Tipo Cochera");
            cocherasDGV.Columns["TipoCochera"].Width = 100;
            cocherasDGV.Columns.Add("Patente", "Patente");
            cocherasDGV.Columns.Add("IdCocheraMovil", "IdCocheraMovil");
            cocherasDGV.Columns["IdCocheraMovil"].Visible = false;
            cocherasDGV.Columns.Add("HoraEntrada", "Hora Entrada");
            cocherasDGV.Columns.Add("IdCocheraFija", "IdCocheraFija");
            cocherasDGV.Columns["IdCocheraFija"].Visible = false;
            cocherasDGV.Columns.Add("Abono", "Abono por Adelantado");
            cocherasDGV.RowHeadersVisible = false;
            cocherasDGV.AllowUserToAddRows = false;
            cocherasDGV.AllowUserToDeleteRows = false;
            cocherasDGV.EditMode = DataGridViewEditMode.EditProgrammatically;
            cocherasDGV.MultiSelect = false;
            cocherasDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            InicializarBusquedaCocheras();

            if ((DateTime.Now.TimeOfDay - playaEditada.HoraCierre).TotalHours >= 0)
            {
                button1.Enabled = false;
                button3.Enabled = false;
                label5.Show();
            }
            else
            {
                label5.Hide();
            }

            

        }


        #region ActualizarGrid
        public void InicializarBusquedaCocheras()
        {
            cocherasDGV.Rows.Clear();
            cocheraDtos.Clear();
            foreach (var item in movilBL.ListarPorPlaya(playaEditada.IdPlaya))
            {
                CocheraDto cocheraDto = new CocheraDto();
                cocheraDto.IdEspacio = item.IdEspacio;
                cocheraDto.PorcentajeValor = item.PorcentajeValor;
                cocheraDto.Tamano = item.Tamano;
                cocheraDto.Piso = item.Piso;
                cocheraDto.IdCocheraMovil = item.IdCocheraMovil;
                cocheraDto.EstadoColeccion = Constantes.EstadosColeccion.SinCambio;
                cocheraDto.TipoCocheraEnum = Constantes.TipoCochera.Movil;
                cocheraDto.Vehiculo = item.Vehiculo;
                cocheraDto.HoraEntrada = item.HoraEntrada;
                cocheraDtos.Add(cocheraDto);
                int index = cocheraDtos.IndexOf(cocheraDto);
                cocheraDtos[index].IndiceColeccion = index;
            }
            foreach (var item in fijaBL.ListarPorPlaya(playaEditada.IdPlaya))
            {
                CocheraDto cocheraDto = new CocheraDto();
                cocheraDto.IdEspacio = item.IdEspacio;
                cocheraDto.PorcentajeValor = item.PorcentajeValor;
                cocheraDto.Tamano = item.Tamano;
                cocheraDto.Piso = item.Piso;
                cocheraDto.IdCocheraFija = item.IdCocheraFija;
                cocheraDto.EstadoColeccion = Constantes.EstadosColeccion.SinCambio;
                cocheraDto.TipoCocheraEnum = Constantes.TipoCochera.Fija;
                cocheraDto.Vehiculo = item.Vehiculo;
                cocheraDto.Abono = (item.Vehiculo != null) ? item.Vehiculo.Abono : "";
                cocheraDtos.Add(cocheraDto);
                int index = cocheraDtos.IndexOf(cocheraDto);
                cocheraDtos[index].IndiceColeccion = index;
            }

            foreach (CocheraDto cochera in cocheraDtos)
            {
                cocherasDGV.Rows.Add(cochera.IdEspacio, cochera.CocheraOcupada(), cochera.Piso, cochera.Tamano,/*cochera.PorcentajeValor ,*/ cochera.TipoCochera, cochera.obtenerPatente(), cochera.IdCocheraMovil, cochera.HoraEntrada, cochera.IdCocheraFija, cochera.ObtenerAbono()/* cochera.TipoCochera, cochera.IndiceColeccion*/);
            }
        } 
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            VehiculosForm vehiculosForm = new VehiculosForm();
            vehiculosForm.MinimizeBox = false;
            vehiculosForm.MaximizeBox = false;
            vehiculosForm.playaEditada = playaEditada;
            vehiculosForm.ShowDialog();
            InicializarBusquedaCocheras();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        #region Egreso Vehiculo
        private void button3_Click(object sender, EventArgs e)
        {
            if (cocherasDGV.SelectedRows[0].Cells["IdEspacio"].Value.ToString() != "")
            {
                if ((DateTime.Now.TimeOfDay - playaEditada.HoraCierre).TotalHours <= 0)
                {

                    CocheraMovil cocheraMovil = movilBL.ListarPorPlaya(playaEditada.IdPlaya).FirstOrDefault(x => x.IdEspacio.ToString() == cocherasDGV.SelectedRows[0].Cells["IdEspacio"].Value.ToString());

                    if (cocheraMovil != null)
                    {
                        string mensaje = CocheraMovilBL.SuperoCincoHoras(cocheraMovil);

                        if (mensaje.Length > 0)
                        {
                            MessageBox.Show(mensaje);
                        }
                        movilBL.EgresarAuto(cocheraMovil);
                    }
                    else
                    {
                        Espacio espacio = espacioBL.Obtener(int.Parse(cocherasDGV.SelectedRows[0].Cells["IdEspacio"].Value.ToString()));
                        espacioBL.BorrarVehiculo(espacio);
                    }

                    MessageBox.Show("El vehiculo egresó del estacionamiento");
                    InicializarBusquedaCocheras();
                }
                else
                {
                    MessageBox.Show("No es posible egresar el vehiculo debido a que el estacionamiento ha cerrado");
                }
            }
            else
            {
                MessageBox.Show("No seleccionaste ningun estacionamiento");
            }
        } 
        #endregion
    }
}

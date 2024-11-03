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
            cocherasDGV.Columns.Add("Piso", "Piso");
            cocherasDGV.Columns["Piso"].Width = 100;
            cocherasDGV.Columns.Add("Tamano", "Tamaño");
            cocherasDGV.Columns["Tamano"].Width = 150;
            cocherasDGV.Columns.Add("PorcentajeValor", "Porcentaje Valor");
            cocherasDGV.Columns["PorcentajeValor"].Width = cocherasDGV.Width - 580;
            cocherasDGV.Columns.Add("IdCocheraMovil", "IdCocheraMovil");
            cocherasDGV.Columns["IdCocheraMovil"].Visible = false;
            cocherasDGV.Columns.Add("IdCocheraFija", "IdCocheraFija");
            cocherasDGV.Columns["IdCocheraFija"].Visible = false;
            cocherasDGV.Columns.Add("TipoCochera", "Tipo Cochera");
            cocherasDGV.Columns["TipoCochera"].Width = 100;
            cocherasDGV.Columns.Add("IndiceColeccion", "IndiceColeccion");
            cocherasDGV.Columns["IndiceColeccion"].Visible = false;
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


        public void InicializarBusquedaCocheras()
        {
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
                cocheraDtos.Add(cocheraDto);
                int index = cocheraDtos.IndexOf(cocheraDto);
                cocheraDtos[index].IndiceColeccion = index;
            }

            foreach (CocheraDto cochera in cocheraDtos)
            {
                cocherasDGV.Rows.Add(cochera.IdEspacio, cochera.Piso, cochera.Tamano, cochera.PorcentajeValor, cochera.IdCocheraMovil, cochera.IdCocheraFija, cochera.TipoCochera, cochera.IndiceColeccion);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VehiculosForm vehiculosForm = new VehiculosForm();
            vehiculosForm.MinimizeBox = false;
            vehiculosForm.MaximizeBox = false;
            vehiculosForm.playaEditada = playaEditada;
            vehiculosForm.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Egreso vehiculo
        private void button3_Click(object sender, EventArgs e)
        {
            if (cocherasDGV.SelectedRows[0].Cells["IdEspacio"].Value.ToString() != "")
            {
                if ((DateTime.Now.TimeOfDay - playaEditada.HoraCierre).TotalHours <=0)
                {
                    CocheraMovil cocheraMovil = movilBL.ListarPorPlaya(playaEditada.IdPlaya).FirstOrDefault(x => x.IdEspacio.ToString() == cocherasDGV.SelectedRows[0].Cells["IdEspacio"].Value.ToString());
                    if (cocheraMovil != null)
                    {
                        cocheraMovil.HoraSalida = DateTime.Now.TimeOfDay;

                        if((cocheraMovil.HoraSalida - cocheraMovil.HoraEntrada).TotalHours >= 5)
                        {
                            MessageBox.Show("Ha sobrepasado las 5 horas, deberá abonar estadía");
                        }
                    }
                    Espacio espacio = espacioBL.Obtener(int.Parse(cocherasDGV.SelectedRows[0].Cells["IdEspacio"].Value.ToString()));
                    espacioBL.Guardar(espacio);
                    MessageBox.Show("El vehiculo egreso del estacionamiento");
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
    }
}

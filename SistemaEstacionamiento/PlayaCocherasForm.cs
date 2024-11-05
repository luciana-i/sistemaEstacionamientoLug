using BE;
using BL.Excepciones;
using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static BE.Constantes;

namespace SistemaEstacionamiento
{
    public partial class PlayaCocherasForm : Form
    {
        EspacioBL espacioBL = new EspacioBL();
        CocheraMovilBL  movilBL = new CocheraMovilBL();
        CocheraFijaBL fijaBL = new CocheraFijaBL();

        PlayaBL playaBL = new PlayaBL();
        List<CocheraDto> cocheraDtos = new List<CocheraDto>();
        public Playa playaEditada { get; set; }
        public PlayaCocherasForm()
        {
            InitializeComponent();
        }

        private void PlayaCocherasForm_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#1ac1fd");
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns.Add("IdEspacio", "IdEspacio");
            dataGridView1.Columns["IdEspacio"].Visible = false;
            dataGridView1.Columns.Add("Piso", "Piso");
            dataGridView1.Columns["Piso"].Width = 100;
            dataGridView1.Columns.Add("Tamano", "Tamaño");
            dataGridView1.Columns["Tamano"].Width = 150;
            dataGridView1.Columns.Add("PorcentajeValor", "Porcentaje Valor");
            dataGridView1.Columns["PorcentajeValor"].Width = dataGridView1.Width - 580;
            dataGridView1.Columns.Add("IdCocheraMovil", "IdCocheraMovil");
            dataGridView1.Columns["IdCocheraMovil"].Visible = false;
            dataGridView1.Columns.Add("IdCocheraFija", "IdCocheraFija");
            dataGridView1.Columns["IdCocheraFija"].Visible = false;
            dataGridView1.Columns.Add("TipoCochera", "Tipo Cochera");
            dataGridView1.Columns["TipoCochera"].Width = 100;
            dataGridView1.Columns.Add("IndiceColeccion", "IndiceColeccion");
            dataGridView1.Columns["IndiceColeccion"].Visible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                     
            cargarDatos(playaEditada);
            
            if (playaEditada.IdPlaya != 0)
                InicializarBusquedaCocheras();
        }

   

        private void cargarDatos(Playa playaEditada)
        {
            textBox1.Text = playaEditada.Nombre;
            textBox2.Text = playaEditada.Direccion;
            textBox3.Text = playaEditada.HoraApertura.ToString();
            textBox4.Text = playaEditada.HoraCierre.ToString();
        }

        // agregarCochera
        private void button1_Click(object sender, EventArgs e)
        {
            CocherasForm cocherasForm = new CocherasForm();
            cocherasForm.MinimizeBox = false;
            cocherasForm.MaximizeBox = false;
            cocherasForm.cocherasEditadasDto = cocheraDtos;
            cocherasForm.ShowDialog(this);
            Actualizar();

        }

        private void Actualizar()
        {
            dataGridView1.Rows.Clear();
            foreach (CocheraDto cochera in cocheraDtos.Where( x=> x.EstadoColeccion  != Constantes.EstadosColeccion.Eliminado))
            {
                    dataGridView1.Rows.Add(cochera.IdEspacio, cochera.Piso, cochera.Tamano, cochera.PorcentajeValor, cochera.IdCocheraMovil, cochera.IdCocheraFija, cochera.TipoCochera, cochera.IndiceColeccion);
            }
        }

        #region ActualizarGrid
        public void InicializarBusquedaCocheras()
        {
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
                dataGridView1.Rows.Add(cochera.IdEspacio, cochera.Piso, cochera.Tamano, cochera.PorcentajeValor, cochera.IdCocheraMovil, cochera.IdCocheraFija, cochera.TipoCochera, cochera.IndiceColeccion);
            }
        } 
        #endregion

        // editar cochera
        private void button2_Click(object sender, EventArgs e)
        {
            CocherasForm cocherasForm = new CocherasForm();
            cocherasForm.MinimizeBox = false;
            cocherasForm.MaximizeBox = false;
            cocherasForm.cocheraDtoEditada = cocheraDtos[int.Parse(dataGridView1.SelectedRows[0].Cells["IndiceColeccion"].Value.ToString())];
            cocherasForm.cocherasEditadasDto =cocheraDtos;
            cocherasForm.ShowDialog(this);
            Actualizar();
        }

        /// <summary>
        /// eleminar Cochera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            cocheraDtos[int.Parse(dataGridView1.SelectedRows[0].Cells["IndiceColeccion"].Value.ToString())].EstadoColeccion=Constantes.EstadosColeccion.Eliminado;
            Actualizar();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// /GUARDA LA PLAYA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            LlenarObjetoPlaya();
           
            foreach (var item in cocheraDtos)
            {
                Espacio espacio = item.TransformarCocheraDtoEnEspacio(item);
                playaEditada.AgregarEspacio(espacio, espacio.EstadoColeccion);
            }

            playaBL.Guardar(playaEditada);

            MessageBox.Show("Se modificaron los datos satisfactoriamente");
            this.Close();

        }

        private void LlenarObjetoPlaya()
        {
            playaEditada.Nombre = textBox1.Text;
            playaEditada.Direccion = textBox2.Text;
            playaEditada.HoraApertura = TimeSpan.Parse(textBox3.Text.ToString());
            playaEditada.HoraCierre = TimeSpan.Parse(textBox4.Text.ToString());
        }
    }
}

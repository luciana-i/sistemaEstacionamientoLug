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

namespace SistemaEstacionamiento
{
    public partial class PlayaCocherasForm : Form
    {
        EspacioBL espacioBL = new EspacioBL();
        CocheraMovilBL  movilBL = new CocheraMovilBL();
        CocheraFijaBL fijaBL = new CocheraFijaBL();
        List<CocheraDto> cocheraDtos = new List<CocheraDto>();
        public Playa playaEditada { get; set; }
        public PlayaCocherasForm()
        {
            InitializeComponent();
        }

        private void PlayaCocherasForm_Load(object sender, EventArgs e)
        {
           

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
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (playaEditada != null)
            {
                cargarDatos(playaEditada);
                Actualizar();
            }
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
            CocherasForm cForm = new CocherasForm();
            cForm.MinimizeBox = false;
            cForm.MaximizeBox = false;
            cForm.ShowDialog(this);
            Actualizar();

        }

        private void Actualizar()
        {
            
            dataGridView1.Rows.Clear();
            foreach (CocheraDto cochera in obtenerCocheras())
            {
                 dataGridView1.Rows.Add(cochera.IdEspacio, cochera.Piso, cochera.Tamano, cochera.PorcentajeValor, cochera.IdCocheraMovil, cochera.IdCocheraFija,cochera.TipoCochera);
            }
        }

        private List<CocheraDto> obtenerCocheras()
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
                cocheraDtos.Add(cocheraDto);
            }
            foreach (var item in fijaBL.ListarPorPlaya(playaEditada.IdPlaya))
            {
                CocheraDto cocheraDto = new CocheraDto();
                cocheraDto.IdEspacio = item.IdEspacio;
                cocheraDto.PorcentajeValor = item.PorcentajeValor;
                cocheraDto.Tamano = item.Tamano;
                cocheraDto.Piso = item.Piso;
                cocheraDto.IdCocheraFija = item.IdCocheraFija;
                cocheraDtos.Add(cocheraDto);
            }
            return cocheraDtos;
        }

        // editar cochera
        private void button2_Click(object sender, EventArgs e)
        {
            CocherasForm cForm = new CocherasForm(int.Parse(dataGridView1.SelectedRows[0].Cells["IdEspacio"].Value.ToString()));
            cForm.MinimizeBox = false;
            cForm.MaximizeBox = false;
            cForm.ShowDialog(this);
            Actualizar();
        }
        /// <summary>
        /// eleminar Cochera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            CocheraDto cochera = cocheraDtos.FirstOrDefault(x => x.IdEspacio == int.Parse(dataGridView1.SelectedRows[0].Cells["IdEspacio"].Value.ToString()));
            if (cochera.IdCocheraMovil != 0)
            {
               CocheraMovil cm = movilBL.Obtener(cochera.IdCocheraMovil);
               movilBL.Eliminar(cm);
            }
            else
            {
                CocheraFija cf = fijaBL.Obtener(cochera.IdCocheraFija);
                fijaBL.Eliminar(cf);
            }

            Actualizar();
        }
    }
}

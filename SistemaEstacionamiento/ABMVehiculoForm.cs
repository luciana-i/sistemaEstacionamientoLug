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
    public partial class ABMVehiculoForm : Form
    {
        VehiculoBL vehiculoBL = new VehiculoBL();
        TipoVehiculoBL tipoVehiculoBL = new TipoVehiculoBL();
        bool editando = false;
        public ABMVehiculoForm()
        {
            InitializeComponent();
        }

        private void ABMVehiculoForm_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#1ac1fd");
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns.Add("IdVehiculo", "IdVehiculo");
            dataGridView1.Columns["IdVehiculo"].Visible = false;
            dataGridView1.Columns.Add("Patente", "Patente");
            dataGridView1.Columns["Patente"].Width = 500;
            dataGridView1.Columns.Add("Abono", "Abono");
            dataGridView1.Columns["Abono"].Width = 150;
            dataGridView1.Columns.Add("IdTipoVehiculo", "Tipo Vehiculo");
            dataGridView1.Columns["IdTipoVehiculo"].Width = dataGridView1.Width - 300;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            llenarCombo();

            Actualizar();
        }
        private void llenarCombo()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(tipoVehiculoBL.Listar().ToArray());
        }

        private void Actualizar()
        {
            dataGridView1.Rows.Clear();
            foreach (Vehiculo vehiculo in vehiculoBL.Listar())
            {
                dataGridView1.Rows.Add(vehiculo.IdVehiculo, vehiculo.Patente, vehiculo.Abono, vehiculo.TipoVehiculo);

            }
        }


        #region Guardar Vehiculo
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Vehiculo vehiculo = new Vehiculo();
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
        }
        #endregion

        private void VaciarTextbox()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private void LlenarObjetoVehiculo(Vehiculo vehiculo)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.SelectedIndex > -1)
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

        /// <summary>
        /// Eliminar vehiculo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click_1(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView1.SelectedRows[0].Cells["IdVehiculo"].Value.ToString());
            if (id > 0)
            {
                if (!vehiculoBL.ExisteUnVehiculoEnUso(id))
                {
                    if (Preguntar())
                    {
                        vehiculoBL.Eliminar(id);
                        Actualizar();
                    }
                }else
                {
                    MessageBox.Show("Ese vehiculo no se puede eliminar porque esta estacionado en otro garage");
                }
                
            }
            else
            {
                MessageBox.Show("No seleccionaste ningun vehiculo para eliminar");
            }
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
        //edicion
        private void button3_Click_1(object sender, EventArgs e)
        {
            editando = true;
            textBox1.Text = dataGridView1.SelectedRows[0].Cells["Patente"].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells["Abono"].Value.ToString();
            int idTipoVehiculo = ((TipoVehiculo)dataGridView1.SelectedRows[0].Cells["IdTipoVehiculo"].Value).IdTipoVehiculo;
            comboBox1.SelectedItem = comboBox1.Items
                .OfType<TipoVehiculo>()
                .FirstOrDefault(item => item.IdTipoVehiculo == idTipoVehiculo);
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsultorioDental.Operaciones
{
    public partial class añadirdatos : Form
    {
        private string buscar = "Busqueda por ID.....";
        private DataTable tabla;
        private Operaciones.registrocitas _re;
        private Operaciones.actualizarcitas _r;
        public añadirdatos(Operaciones.registrocitas re, Operaciones.actualizarcitas r)
        {
            InitializeComponent();
            creaciondatatable();
            _re = re;
            _r=r;
            textboxbuscar();
            textBox1.GotFocus += textbox1_gotfocus;
            textBox1.LostFocus += textbox1_lostfocus;
        }
        private void creaciondatatable()
        {
            tabla = new DataTable();
            tabla.Columns.Add("ID", typeof(int));
            tabla.Columns.Add("Informacion", typeof(string));

        }

        public void cargardatagridview()
        {

            try
            {
                var db = new Db.dbconsultorioEntities1();
                tabla.Clear();
                var motivo = db.motivoes.ToList();
                var paciente = db.pacientes.ToList();
                var dentista = db.dentistas.ToList();
                if (comboBox1.SelectedIndex == 2)
                {
                    if (motivo != null && motivo.Any())
                    {
                        foreach (var datos in motivo)
                        {
                            tabla.Rows.Add(
                                 datos.motivoid,
                                 datos.motivo1
                                );
                        }
                        cargardatos();
                    }
                }
                else if (comboBox1.SelectedIndex == 0)
                {
                    if (paciente != null && paciente.Any())
                    {
                        foreach (var datos in paciente)
                        {
                            tabla.Rows.Add(
                                 datos.pacienteid,
                                 datos.nombre + "  " + datos.apellido
                                );
                        }
                        cargardatos();
                    }
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    if (dentista != null && dentista.Any())
                    {
                        foreach (var datos in dentista)
                        {
                            tabla.Rows.Add(
                                 datos.dentistaid,
                                 datos.nombre + "  " + datos.apellido
                                );
                        }
                        cargardatos();
                    }
                }
                else { MessageBox.Show("Debe selecionar un dato del combobox para ejecutar la busqueda"); }

            }
            catch (Exception t)
            {
                MessageBox.Show("Error:" + t.Message);
            }
        }
        private void cargardatos()
        {
            dataGridView1.Rows.Clear();

            foreach (DataRow row in tabla.Rows)
            {
                int fila = dataGridView1.Rows.Add();
                dataGridView1.Rows[fila].Cells["ID"].Value = row["ID"];
                dataGridView1.Rows[fila].Cells["Informacion"].Value = row["informacion"];
            }

        }
        private void textbox1_gotfocus(object sender, EventArgs e)
        {
            if (textBox1.Text == buscar)
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textbox1_lostfocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = buscar;
                textBox1.ForeColor = Color.Gray;
            }
        }
        private void textboxbuscar()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = buscar;
                textBox1.ForeColor = Color.Gray;
            }
            else if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = buscar;
                textBox1.ForeColor = Color.Gray;
            }
        }
        private void añadirdatos_Load(object sender, EventArgs e)
        {
            button1.Select();
          
            if (dato== "Añadir Paciente")
            {
                comboBox1.SelectedIndex = 0;
            }else if (dato== "Añadir Motivo")
            {
                comboBox1.SelectedIndex = 2;
            }else if (dato== "Añadir Dentista")
            {
                comboBox1.SelectedIndex = 1;
            }
            
        }
        string dato;
        public void combo(string opcion)
        {
            dato = opcion;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ts;
            if (int.TryParse(textBox1.Text, out ts))
            {

                cargardatagridview();
                dataGridView1.Rows.Clear();
                DataRow[] fila = tabla.Select($"ID={ts}");

                try
                {
                    if (fila.Length > 0)
                    {
                        foreach (DataRow filas in fila)
                        {
                            int num = dataGridView1.Rows.Add();
                            dataGridView1.Rows[num].Cells["ID"].Value = filas["ID"];
                            dataGridView1.Rows[num].Cells["Informacion"].Value = filas["informacion"];

                        }

                    }
                    else
                    {
                       
                    }
                    textBox1.Clear();
                    textBox1.Select();
                }
                catch (Exception p)
                {
                    MessageBox.Show($"Error:{p}");
                }

            }
            else
            {
                MessageBox.Show("Digite un ID valido");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargardatagridview();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Dispose();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && e.RowIndex >= 0)
            {
                if (_re!=null)
                {
                    if (comboBox1.SelectedIndex == 0)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        _re.paciente(id, nombre);
                        _re.Refresh();
                        this.Dispose();
                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        _re.dentista(id, nombre);
                        _re.Refresh();
                        this.Dispose();
                    } else if (comboBox1.SelectedIndex == 2)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        _re.motivo(id, nombre);
                        _re.Refresh();
                        this.Dispose();
                    }
                }else if (_r!=null)
                {
                    if (comboBox1.SelectedIndex == 0)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        _r.paciente(id, nombre);
                        _r.Refresh();
                        this.Dispose();
                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        _r.dentista(id, nombre);
                        _r.Refresh();
                        this.Dispose();
                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        _r.motivo(id, nombre);
                        _r.Refresh();
                        this.Dispose();
                    }

                }



            }
        }
    }
}

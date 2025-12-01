using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsultorioDental.formularios
{
    public partial class menupacientes : Form
    {
        private DataTable tabla;
        private string buscar = "Busqueda por ID.....";
        public menupacientes()
        {
            InitializeComponent();
            creaciondatatable();
            cargardatagridview();
            textboxbuscar();
            textBox1.GotFocus += textbox1_gotfocus;
            textBox1.LostFocus += textbox1_lostfocus;
        }
        private void creaciondatatable()
        {
            tabla = new DataTable();
            tabla.Columns.Add("ID", typeof(int));
            tabla.Columns.Add("Nombre", typeof(string));
            tabla.Columns.Add("Apellido", typeof(string));
            tabla.Columns.Add("Telefono", typeof(string));
            tabla.Columns.Add("Email", typeof(string));
            tabla.Columns.Add("Direccion", typeof(string));;
            tabla.Columns.Add("fecharegistro", typeof(DateTime));
            tabla.Columns.Add("Alergias", typeof(string)); 
            tabla.Columns.Add("historial", typeof(string));
            tabla.Columns.Add("Medicamentos", typeof(string)); 
        }

        public void cargardatagridview()
        {

            try
            {
                var db = new Db.dbconsultorioEntities1();
                tabla.Clear();
                var pacientes = db.pacientes.ToList();
                if (pacientes!=null)
                {
                    foreach (var datos in pacientes)
                    {
                        tabla.Rows.Add(
                             datos.pacienteid,
                             datos.nombre,
                             datos.apellido,
                             datos.telefono,
                             datos.email,
                             datos.direccion,
                             datos.fecharegistro,
                             datos.alergias,
                             datos.historialdental,
                             datos.medicamentos);
                        
                    }
                    cargardatos();
                }
                else
                {

                }
                
                
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
                dataGridView1.Rows[fila].Cells["Nombre"].Value = row["Nombre"];
                dataGridView1.Rows[fila].Cells["Apellido"].Value = row["Apellido"];
                dataGridView1.Rows[fila].Cells["Telefono"].Value = row["Telefono"];
                dataGridView1.Rows[fila].Cells["Email"].Value = row["Email"];
                dataGridView1.Rows[fila].Cells["Direccion"].Value = row["Direccion"];
                dataGridView1.Rows[fila].Cells["fecharegistro"].Value = row["fecharegistro"];
                dataGridView1.Rows[fila].Cells["alergias"].Value = row["alergias"];
                dataGridView1.Rows[fila].Cells["Historial"].Value = row["historial"];
                dataGridView1.Rows[fila].Cells["Medicamentos"].Value = row["medicamentos"];
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
        private void button2_Click(object sender, EventArgs e)
        {
            cargardatagridview();
        }

        private void menupacientes_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form openform = Application.OpenForms["registropacientes"];
            if (openform == null)
            {
                Operaciones.registropacientes registro = new Operaciones.registropacientes(this);
                registro.Show();
            }
            else
            {
                openform.BringToFront();
            }
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                int ts;
                if (int.TryParse(textBox1.Text, out ts))
                {

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
                                dataGridView1.Rows[num].Cells["Nombre"].Value = filas["Nombre"];
                                dataGridView1.Rows[num].Cells["Apellido"].Value = filas["Apellido"];
                                dataGridView1.Rows[num].Cells["Telefono"].Value = filas["Telefono"];
                                dataGridView1.Rows[num].Cells["Email"].Value = filas["Email"];
                                dataGridView1.Rows[num].Cells["Direccion"].Value = filas["Direccion"];
                                dataGridView1.Rows[num].Cells["fecharegistro"].Value = filas["fecharegistro"];
                                dataGridView1.Rows[num].Cells["alergias"].Value = filas["alergias"];
                                dataGridView1.Rows[num].Cells["Historial"].Value = filas["historial"];
                                dataGridView1.Rows[num].Cells["Medicamentos"].Value = filas["medicamentos"];
                        }
                            
                        }
                        else
                        {
                            MessageBox.Show("Dato no encontrado");
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.PerformClick();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {
                if (tabla == null)
                {
                    MessageBox.Show("el datatable no esta iniciado");
                }
                else
                {
                    if (comboBox1.SelectedItem?.ToString() == "Orden ascendente")
                    {
                        tabla.DefaultView.Sort = "ID ASC";
                    }
                    else if (comboBox1.SelectedItem?.ToString() == "Orden descendente")
                    {
                        tabla.DefaultView.Sort = "ID DESC";
                    }
                    dataGridView1.Rows.Clear();
                    foreach (DataRowView vista in tabla.DefaultView)
                    {
                        int fila = dataGridView1.Rows.Add();
                        dataGridView1.Rows[fila].Cells["ID"].Value = vista["ID"];
                        dataGridView1.Rows[fila].Cells["Nombre"].Value = vista["Nombre"];
                        dataGridView1.Rows[fila].Cells["Apellido"].Value = vista["Apellido"];
                        dataGridView1.Rows[fila].Cells["Telefono"].Value = vista["Telefono"];
                        dataGridView1.Rows[fila].Cells["Email"].Value = vista["Email"];
                        dataGridView1.Rows[fila].Cells["Direccion"].Value = vista["Direccion"];
                        dataGridView1.Rows[fila].Cells["fecharegistro"].Value = vista["fecharegistro"];
                        dataGridView1.Rows[fila].Cells["alergias"].Value = vista["alergias"];
                        dataGridView1.Rows[fila].Cells["Historial"].Value = vista["historial"];
                        dataGridView1.Rows[fila].Cells["Medicamentos"].Value = vista["medicamentos"];
                    }

                    
                }
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 10 || e.ColumnIndex == 11)
            {
                this.Cursor = Cursors.Hand;

            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && e.RowIndex >= 0)
            {
              
                if (e.ColumnIndex == 10)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    string nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string apellido = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    string telefono = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    string email = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    string direccion = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    string alergia = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    string historial = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    string medicamento = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    DateTime fecha = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());

                    Form openform = Application.OpenForms["actualizarpacientes"];
                    if (openform == null)
                    {
                        Operaciones.actualizarpacientes actualizar = new Operaciones.actualizarpacientes(this);
                        actualizar.datos(id, nombre, apellido, telefono, email, direccion, alergia, historial, medicamento, fecha);
                        actualizar.Show();
                    }
                    else
                    {
                        openform.BringToFront();
                    }
                   
                }
                else if (e.ColumnIndex == 11)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    try
                    {
                        var db = new Db.dbconsultorioEntities1(); ;
                        var delete = db.pacientes.Find(id);
                        DialogResult rs = MessageBox.Show("Desea eliminar este Paciente?", "Aviso", MessageBoxButtons.OKCancel);
                        switch (rs)
                        {
                            case DialogResult.OK:
                                if (delete != null)
                                {
                                    db.pacientes.Remove(delete);
                                    db.SaveChanges();
                                    button2.PerformClick();

                                    MessageBox.Show("Paciente correctamente eliminado");
                                }
                                else
                                {
                                    MessageBox.Show("Paciente no encontrado");
                                }
                                break;
                            case DialogResult.Cancel:
                                break;
                        }

                    }
                    catch (Exception p)
                    {
                        MessageBox.Show($"Error:{p}");
                    }

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog exportar = new SaveFileDialog();
                exportar.Filter = "CSV files (*.csv) |*.csv|All files (*.*) | *.*";
                exportar.Title = "Guardar el archivo csv";
                if (exportar.ShowDialog() == DialogResult.OK)
                {
                    string rutaarchivo = exportar.FileName;
                    StringBuilder contenido = new StringBuilder();

                    foreach (DataGridViewColumn columna in dataGridView1.Columns)
                    {
                        contenido.Append($"\"{columna.HeaderText.Trim()}\"");
                        contenido.Append(",");
                    }
                    contenido.Length--;
                    contenido.AppendLine();

                    foreach (DataGridViewRow fila in dataGridView1.Rows)
                    {
                        for (int i = 0; i < fila.Cells.Count; i++)
                        {
                            contenido.Append($"\"{fila.Cells[i].Value?.ToString().Replace("\"", "\"\"").Trim()}\"");
                            contenido.Append(",");
                        }
                        contenido.Length--;
                        contenido.AppendLine();
                    }
                    File.WriteAllText(rutaarchivo, contenido.ToString(), Encoding.UTF8);
                    MessageBox.Show("Datos exportados correctamente en la siguiente direccion:" + rutaarchivo);
                }
            }
            else
            {
                MessageBox.Show("No hay datos que exportar");
            }
        }
    }
}

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
    public partial class menudentista : Form
    {
        private string buscar = "Busqueda por ID.....";
        private DataTable tabla;
        public menudentista()
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
            tabla.Columns.Add("Especialidad", typeof(string));
            tabla.Columns.Add("Telefono", typeof(string));
            tabla.Columns.Add("Email", typeof(string));
            tabla.Columns.Add("Direccion", typeof(string));
            tabla.Columns.Add("Estado", typeof(string));
            tabla.Columns.Add("fecharegistro", typeof(DateTime));
        }

        public void cargardatagridview()
        {

            try
            {
                var db = new Db.dbconsultorioEntities1();
                tabla.Clear();
                var dentistas = db.dentistas.ToList();
                foreach (var datos in dentistas)
                {
                    tabla.Rows.Add(
                         datos.dentistaid,
                         datos.nombre,
                         datos.apellido,
                         datos.especialidad,
                         datos.telefono,
                         datos.email,
                         datos.direccion,
                         datos.estado,
                         datos.fecharegistro);
                }
                cargardatos();
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
                dataGridView1.Rows[fila].Cells["Especialidad"].Value = row["Especialidad"];
                dataGridView1.Rows[fila].Cells["Telefono"].Value = row["Telefono"];
                dataGridView1.Rows[fila].Cells["Email"].Value = row["Email"];
                dataGridView1.Rows[fila].Cells["Direccion"].Value = row["Direccion"];
                dataGridView1.Rows[fila].Cells["Estado"].Value = row["Estado"];
                dataGridView1.Rows[fila].Cells["fecharegistro"].Value = row["fecharegistro"];
            }
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                string estado = fila.Cells["Estado"].Value as string;
                if (!string.IsNullOrEmpty(estado))
                {
                    if (estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                    {
                        fila.Cells["Estado"].Style.ForeColor = Color.Green;
                    }
                    else if (estado.Equals("Inactivo", StringComparison.OrdinalIgnoreCase))
                    {
                        fila.Cells["Estado"].Style.ForeColor = Color.Red;
                    }
                }
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
        private void textboxbuscar(){
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = buscar;
                textBox1.ForeColor = Color.Gray;
            }else if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = buscar;
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form openform = Application.OpenForms["registrodentista"];
            if (openform==null)
            {
                Operaciones.registrodentista registro = new Operaciones.registrodentista(this);
                registro.Show();

            }
            else
            {
                openform.BringToFront();
            }


        }

        private void menudentista_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            
        }
   

        private void button2_Click(object sender, EventArgs e)
        {
            cargardatagridview();
            
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
                    if (fila.Length>0)
                    {
                        foreach (DataRow filas in fila)
                        {
                            int num = dataGridView1.Rows.Add();
                            dataGridView1.Rows[num].Cells["ID"].Value = filas["ID"];
                            dataGridView1.Rows[num].Cells["Nombre"].Value = filas["Nombre"];
                            dataGridView1.Rows[num].Cells["Apellido"].Value = filas["Apellido"];
                            dataGridView1.Rows[num].Cells["Especialidad"].Value = filas["Especialidad"];
                            dataGridView1.Rows[num].Cells["Telefono"].Value = filas["Telefono"];
                            dataGridView1.Rows[num].Cells["Email"].Value = filas["Email"];
                            dataGridView1.Rows[num].Cells["Direccion"].Value = filas["Direccion"];
                            dataGridView1.Rows[num].Cells["Estado"].Value = filas["Estado"];
                            dataGridView1.Rows[num].Cells["fecharegistro"].Value = filas["fecharegistro"];
                        }

                        foreach (DataGridViewRow estatus in dataGridView1.Rows)
                        {
                            string estado = estatus.Cells["Estado"].Value as string;
                            if (!string.IsNullOrEmpty(estado))
                            {
                                if (estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                                {
                                    estatus.Cells["Estado"].Style.ForeColor = Color.Green;
                                }
                                else if (estado.Equals("Inactivo", StringComparison.OrdinalIgnoreCase))
                                {
                                    estatus.Cells["Estado"].Style.ForeColor = Color.Red;
                                }
                            }
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
            if (e.KeyChar==(char)13)
            {
                button1.PerformClick();
            }
        }
     

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count>1)
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
                        dataGridView1.Rows[fila].Cells["Especialidad"].Value = vista["Especialidad"];
                        dataGridView1.Rows[fila].Cells["Telefono"].Value = vista["Telefono"];
                        dataGridView1.Rows[fila].Cells["Email"].Value = vista["Email"];
                        dataGridView1.Rows[fila].Cells["Direccion"].Value = vista["Direccion"];
                        dataGridView1.Rows[fila].Cells["Estado"].Value = vista["Estado"];
                        dataGridView1.Rows[fila].Cells["fecharegistro"].Value = vista["fecharegistro"];
                    }

                    foreach (DataGridViewRow estatus in dataGridView1.Rows)
                    {
                        string estado = estatus.Cells["Estado"].Value as string;
                        if (!string.IsNullOrEmpty(estado))
                        {
                            if (estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                            {
                                estatus.Cells["Estado"].Style.ForeColor = Color.Green;
                            }
                            else if (estado.Equals("Inactivo", StringComparison.OrdinalIgnoreCase))
                            {
                                estatus.Cells["Estado"].Style.ForeColor = Color.Red;
                            }
                        }
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
            if (e.ColumnIndex==9 || e.ColumnIndex==10)
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
            if (dataGridView1.Rows.Count>0 && e.RowIndex>=0)
            {
                if (e.ColumnIndex==9)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    string nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                  string apellido=dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                   string especialidad = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    string telefono = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    string email = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    string direccion = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    string estado = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

                    Form openform = Application.OpenForms["actualizardentista"];
                    if (openform == null)
                    {
                        Operaciones.actualizardentista actualizar = new Operaciones.actualizardentista(this);
                        actualizar.datos(id, nombre, apellido, especialidad, telefono, email, direccion, estado);
                        actualizar.Show();

                    }
                    else
                    {
                        openform.BringToFront();
                    }
                   
                }
                else if(e.ColumnIndex==10)
                {
                     int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        try
                        {
                            var db = new Db.dbconsultorioEntities1(); ;
                            var delete = db.dentistas.Find(id);
                            DialogResult rs = MessageBox.Show("Desea eliminar este Dentista?", "Aviso", MessageBoxButtons.OKCancel);
                            switch (rs)
                            {
                                case DialogResult.OK:
                                    if (delete != null)
                                    {
                                        db.dentistas.Remove(delete);
                                        db.SaveChanges();
                                        button2.PerformClick();

                                        MessageBox.Show("Dentista correctamente eliminado");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Dentista no encontrado");
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

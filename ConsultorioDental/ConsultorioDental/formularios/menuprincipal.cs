using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsultorioDental.formularios
{
    public partial class menuprincipal : Form
    {
        private DataTable tabla;
        public menuprincipal()
        {
            InitializeComponent();
            creaciondatatable();
            cargardatagridview();
        }
        public class CitaDto
        {
            public int CitaID { get; set; }
            public int? PacienteID { get; set; }
            public string PacienteNombre { get; set; }
            public string PacienteApellido { get; set; }
            public int? DentistaID { get; set; }
            public string DentistaNombre { get; set; }
            public string DentistaApellido { get; set; }
            public DateTime Fecha { get; set; }
            public TimeSpan Hora { get; set; }
            public int? MotivoID { get; set; }
            public string Motivo { get; set; }
            public int DiasRestantes { get; set; }
            public int HorasRestantes { get; set; }
            public string Estado { get; set; }
        }
        private void creaciondatatable()
        {
            tabla = new DataTable();
            tabla.Columns.Add("ID", typeof(int));
            tabla.Columns.Add("Paciente", typeof(string));
            tabla.Columns.Add("Dentista", typeof(string));
            tabla.Columns.Add("Motivo", typeof(string));
            tabla.Columns.Add("fecha", typeof(DateTime));
            tabla.Columns.Add("hora", typeof(string));
            tabla.Columns.Add("tiemporestante", typeof(string));
            tabla.Columns.Add("Estado", typeof(string));
        }
        public void cargardatagridview()
        {
            try
            {
                using (var db = new Db.dbconsultorioEntities1())
                {

                    tabla.Clear();

                    var citas = db.Database.SqlQuery<CitaDto>("EXEC procesarcitas").ToList();
                    int totalestadovigente = 0;
                    int totalestadoproceso = 0;

                    //calculo cantidad de citas en el mes
                    DateTime fechaactual = DateTime.Now;
                    int mesactual = fechaactual.Month;
                    int añactual = fechaactual.Year;
                    int totalcitasdelmes = 0;
                    int totalcitasgenerales = 0;

                    var ultimoscincocitas = citas.OrderByDescending(c=>c.Fecha).Take(5).ToList();
                    listBox1.Items.Clear();
                    if (citas != null && citas.Any())
                    {
                        foreach (var datos in citas)
                        {
                           
                            int diarestante = datos.DiasRestantes;
                            int horarestante = datos.HorasRestantes;
                            //el siguiente codigo le daremos formato de 12horas,AM,PM a nuestro dato hora de la base de datos 
                            TimeSpan valorhora = datos.Hora;
                            int horas = valorhora.Hours;
                            int minutos = valorhora.Minutes;
                            string periodo = (horas >= 12) ? "PM" : "AM";
                            int mostrarhoras = horas % 12;
                            mostrarhoras = (mostrarhoras == 0) ? 12 : mostrarhoras;
                            string formatohora = $"{mostrarhoras:D2}:{minutos:D2} {periodo}";
                            tabla.Rows.Add(
                                datos.CitaID,
                                $"{datos.PacienteNombre}  {datos.PacienteApellido}",
                                $"{datos.DentistaNombre}  {datos.DentistaApellido}",
                                datos.Motivo,
                                datos.Fecha,
                                formatohora,
                                $"{diarestante} Días {horarestante}  Horas restantes",
                                datos.Estado
                            );

                            if (datos.Estado.Equals("vigente",StringComparison.OrdinalIgnoreCase))
                            {
                                totalestadovigente++;
                            }else if (datos.Estado.Equals("En proceso",StringComparison.OrdinalIgnoreCase))
                            {
                                totalestadoproceso++;
                            }

                            if (datos.Fecha.Month==mesactual && datos.Fecha.Year==añactual)
                            {
                                totalcitasdelmes++;
                            }
                            totalcitasgenerales++;
                       
                        }
                        foreach (var cita in ultimoscincocitas)
                        {
                            listBox1.Items.Add($"Cita ID: {cita.CitaID} | Paciente: {cita.PacienteNombre} {cita.PacienteApellido}");
                            listBox1.Items.Add("");
                        }
                   
                    }
                    else
                    {
                        MessageBox.Show("No citas encontradas.");
                    }
                    label5.Text = totalestadovigente.ToString();
                    label6.Text = totalestadoproceso.ToString();
                    label9.Text = totalcitasdelmes.ToString();
                    label11.Text = totalcitasgenerales.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
            }
        }
    
        private void menuprincipal_Load(object sender, EventArgs e)
        {

        }
    }
}

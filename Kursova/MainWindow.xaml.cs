using Kursova.Const;
using Kursova.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kursova
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Services.StudentsService s;
        Services.ProfessorsService p;
        public MainWindow()
        {
            InitializeComponent();
            //s = new Services.StudentsService("d123aadwwa1212ed", 1, 1, Const.Enums.Subject.Math);
            //p = new Services.ProfessorsService("1121312", 0, 0, new Entity_Framework.LessonsDate() { DateTime = new DateTime(1, 11, 1)}, 1);
            //s.Clear();
            s = new StudentsService();
            p = new ProfessorsService();
            s.ClearStudents();
            p.ClearProfessors();
            p.ClearSchedules();
            List<Subject> a = new List<Subject>() { Subject.English, Subject.Math, Subject.Physics };
            var list = new List<int>() { 1, 2, 3 };
            s.AddStudent("13223", 1, 1, a);
            p.AddProfessor("12312", a, Position.Rector, new Entity_Framework.LessonsDate() { DateTime = new DateTime(11, 1, 10) }, 1, list);
            p.AddProfessor("adw123", a, Position.Rector, new Entity_Framework.LessonsDate() { DateTime = new DateTime(21, 1, 10) }, 1, list);
            p.AddProfessor("1213", a, Position.Rector, new Entity_Framework.LessonsDate() { DateTime = new DateTime(13, 1, 10) }, 1, list);
            var qwe = p.GetProfessorsBySubject(Subject.English);

            foreach(var el in qwe)
            {
                this.textBox.Text += el.Name.ToString();
                this.textBox.Text += "\n";
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

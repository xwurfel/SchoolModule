using Kursova.Const;
using Kursova.Entity_Framework;
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
        Services.ScheduleService schedule;
        public MainWindow()
        {
            InitializeComponent();
            //s = new Services.StudentsService("d123aadwwa1212ed", 1, 1, Const.Enums.Subject.Math);
            //p = new Services.ProfessorsService("1121312", 0, 0, new Entity_Framework.LessonsDate() { DateTime = new DateTime(1, 11, 1)}, 1);
            //s.Clear();
            s = new StudentsService();
            p = new ProfessorsService();
            schedule = new ScheduleService();
            s.ClearStudents();
            p.ClearProfessors();
            schedule.ClearSchedules();
            List<Subject> a = new List<Subject>() { Subject.English, Subject.Math, Subject.Physics };
            var list = new List<int>() { 1, 2, 3 };
            var list2 = new List<int>() { 1, 2, 3, 4 };
            var list3 = new List<int>() { 1 };
            s.AddStudent("1", 1, 1, a);
            s.AddStudent("2", 1, 1, a);
            s.AddStudent("3", 1, 1, a);
            s.AddStudent("4", 2, 1, a);
            s.AddStudent("5", 2, 1, a);
            s.AddStudent("6", 2, 1, a);

            p.AddProfessor("1", a, Position.Rector,/* new List<Entity_Framework.LessonsDate>() {  new LessonsDate { DateTime = new DateTime(1, 1, 10) }, new LessonsDate { DateTime = new DateTime(2, 1, 10) } },*/ 1, list, new List<int>() { 1, 2});
            p.AddProfessor("2", a, Position.Dean,/* new List<Entity_Framework.LessonsDate>() { { new LessonsDate { DateTime = new DateTime(3, 1, 10) } } },*/ 1, list2, new List<int>() { 1, 2 });
            p.AddProfessor("3", a, Position.Lecturer, /*new List<Entity_Framework.LessonsDate>() { { new LessonsDate { DateTime = new DateTime(4, 1, 10) } } },*/ 1, list3, new List<int>() { 1 });
            p.AddProfessor("4", a, Position.Lecturer, /*new List<Entity_Framework.LessonsDate>() { { new LessonsDate { DateTime = new DateTime(5, 1, 10) } } },*/ 1, list3, new List<int>() { 1 });

            Professor professor = new Professor() { Name = "5",
                                                    Subjects = a,
                                                    Position = Position.Lecturer,
                                                    //Schedule = new List<Entity_Framework.LessonsDate>() { { new LessonsDate { DateTime = new DateTime(213, 1, 10) } } },
                                                    Experience = 1, 
                                                    Groups = list3, 
                                                    Courses = new List<int>() { 1, 2 } };

            p.AddProfessor(professor);

            schedule.AddDate(new DateTime(1, 1, 1), Subject.Math, 1, 1, "4" );
            schedule.AddDate(new DateTime(1, 1, 1), Subject.Math, 1, 1, "2");

            /*var qwe = p.GetProfessorsBySubject(Subject.English);
            
            foreach(var el in qwe)
            {
                this.textBox.Text += el.Name.ToString();
                this.textBox.Text += "\n";
            }*/


            //this.textBox.Text = p.GetMostPopularBySubject(Subject.Math).Name ?? "123";

            /*var qwe = p.GetFreeByDate(new DateTime(11, 1, 10));
            foreach (var el in qwe)
            {
                this.textBox.Text += el.Name.ToString() + " " + el.Position.ToString() + " " + el.Groups.Count.ToString() + " " + el.Schedule.DateTime.ToString();
                this.textBox.Text += "\n";
            }*/

            /*var qw = p.GetFreeByDate(new DateTime(5, 1, 10));
            foreach (var el in qw)
            {
                this.textBox.Text += el.Name.ToString();
                this.textBox.Text += "\n";
            }*/
            foreach (var el in p.GetFreeByDate(new DateTime(1, 1, 1)))
                this.textBox.Text += el.Name;

        }
    }
}

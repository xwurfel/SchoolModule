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
        Services.DatesService dates;
        Services.GroupCourseService groups;
        Dates date;
        public MainWindow()
        {
            InitializeComponent();
            
            using (EntityLogicContext db = new EntityLogicContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                groups = new Services.GroupCourseService();
            }

            groups.AddCourse("course 1", new List<Subject>() { Subject.Math, Subject.English, Subject.CDM });
            p = new Services.ProfessorsService("prof 1", new List<Subject>() { Subject.Math, Subject.English, Subject.CDM }, Position.Lecturer, 2);
            //date = new Dates() { Group = new Group { Id = 1 }, Subject = Subject.CDM, DateTime = new DateTime(1, 1, 1), Professor = new Professor() { } };
            using(EntityLogicContext db = new EntityLogicContext())
            {
                
            groups.AddGroup("Group 1", db.courses.OrderBy(x => x.Id).Last().Id);
            }
            dates = new DatesService(new DateTime(1, 1, 1), Subject.CDM, 1, 1);
            s = new Services.StudentsService("aaa", 1);

            

        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            MessageBox.Show(menuItem.Header.ToString());
        }


        private void selectProfBySubjectBttn_Click(object sender, RoutedEventArgs e)
        {
            var profService = new ProfessorsService();
            var list = profService.GetBySubject((Subject)Enum.Parse(typeof(Subject), subjectcb.Text));


            if(list is null || list.Count == 0)
            {
                infotb.Text = "There are no professors, who are teaching this subject";
                return;
            }

            infotb.Text = "";
            

            foreach(var el in list)
            {
                infotb.Text += el.Name;
                infotb.Text += " ";
            }
        }


        private void getMostPopularProfBySubject_Click(object sender, RoutedEventArgs e)
        {
            var profService = new ProfessorsService();
            var prof = profService.GetMostPopularBySubject((Subject)Enum.Parse(typeof(Subject), subjectcb.Text));

            if (prof is null)
            {
                infotb.Text = "There is none most popular";
                return;
            }

            infotb.Text = prof.Name;
        }

        private void getProfessorsWithOnlyOneCourseBttn_Click(object sender, RoutedEventArgs e)
        {
            var profService = new ProfessorsService();

            var profs = profService.GetWithOnlyOneCourse();

            infotb.Text = "";


            if (profs is null || profs.Count == 0)
            {
                infotb.Text = "There are no professors with only one course";
                return;
            }



            foreach (var el in profs)
            {
                infotb.Text += el.Name;
                infotb.Text += " ";
            }
        }

        private void OrderByPositionAndNameBttn_Click(object sender, RoutedEventArgs e)
        {
            var profService = new ProfessorsService();

            var profs = profService.SortByPositionThenByName();

            infotb.Text = "";


            if (profs is null || profs.Count == 0)
            {
                infotb.Text = "List is empty!";
                return;
            }



            foreach (var el in profs)
            {
                infotb.Text += el.Position;
                infotb.Text += " ";
                infotb.Text += el.Name;
                infotb.Text += "\n";
            }
        }

        private void searchFreeByDateBttn_Click(object sender, RoutedEventArgs e)
        {
            var dtInput = Array.ConvertAll(dateInputTb.Text.Split("-"), s => int.Parse(s));

            if(!dtInput.Any())
            {
                infotb.Text = "Correct input needed!";
                return;
            }

            DateTime dateTime = new DateTime(dtInput[0], dtInput[1], dtInput[2], dtInput[3], dtInput[4], 0);

            var profService = new ProfessorsService();

            var profs = profService.GetFreeByDate(dateTime);

            infotb.Text = "";


            if (profs is null || profs.Count == 0)
            {
                infotb.Text = "There are no free professors on that date!";
                return;
            }



            foreach (var el in profs)
            {
                /*infotb.Text += el.Position;
                infotb.Text += " ";*/
                infotb.Text += el.Name;
                infotb.Text += " ";
            }

        }

        private void getStudentsByProfBttn_Click(object sender, RoutedEventArgs e)
        {
            var studentService = new StudentsService();

            var profService = new ProfessorsService();

            if (profService is null || studentService is null)
                throw new Exception();

            var list = studentService.GetStudentsByProfessor(profService.GetByName(profNameTb.Text));


            if (list is null || list.Count == 0)
            {
                infotb.Text = "There are no students, who are teached by that professor";
                return;
            }

            infotb.Text = "";


            foreach (var el in list)
            {
                infotb.Text += el.Name;
                infotb.Text += " ";
                //  infotb.Tag += ;
            }

        }

        private void getStudentsByCourseBttn_Click(object sender, RoutedEventArgs e)
        {
            var studentService = new StudentsService();

            var gcService = new GroupCourseService();

            if (gcService is null || studentService is null)
                throw new Exception();

            var list = studentService.GetStudentsByCourse(gcService.GetCourseByName(courseNameTb.Text));


            if (list is null || list.Count == 0)
            {
                infotb.Text = "There are no students, who are in that course";
                return;
            }

            infotb.Text = "";


            foreach (var el in list)
            {
                infotb.Text += el.Name;
                infotb.Text += " ";
                //  infotb.Tag += ;
            }
        }

        private void AppendData_Click(object sender, RoutedEventArgs e)
        {
            AddInfoWindow win = new AddInfoWindow();
            win.Show();
        }
    }
}

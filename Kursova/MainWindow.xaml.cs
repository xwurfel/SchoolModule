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
using System.Windows.Media.Animation;
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
        //Services.StudentsService s;
        //Services.ProfessorsService p;
        Services.DatesService dates;
        //Services.GroupCourseService groups;
        //Dates date;
        public MainWindow()
        {
            InitializeComponent();
            
            using (EntityLogicContext db = new EntityLogicContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                dates = new DatesService();
                //groups = new GroupCourseService();
            }
/*
            groups.AddCourse("course 1", new List<Subject>() { Subject.Math, Subject.English, Subject.CDM });
            groups.AddCourse("course 2", new List<Subject>() { Subject.Art, Subject.Physics, Subject.Programming });
            groups.AddCourse("course 3", new List<Subject>() { Subject.Math, Subject.English, Subject.Programming, Subject.History });
            p = new ProfessorsService("prof 1", new List<Subject>() { Subject.Math, Subject.English, Subject.CDM }, Position.Lecturer, 12);

            p.AddProfessor("prof 2", new List<Subject>() { Subject.Math, Subject.Programming, Subject.Physics }, Position.Assistant, 1);

            p.AddProfessor("prof 3", new List<Subject>() { Subject.Art, Subject.Programming, Subject.History }, Position.Assistant, 1);
            //date = new Dates() { Group = new Group { Id = 1 }, Subject = Subject.CDM, DateTime = new DateTime(1, 1, 1), Professor = new Professor() { } };
            using (EntityLogicContext db = new EntityLogicContext())
            {
            groups.AddGroup("group 1", db.courses.OrderBy(x => x.Id).First().Id);

            groups.AddGroup("group 2", db.courses.OrderBy(x => x.Id).Skip(1).First().Id);

            groups.AddGroup("group 3", db.courses.OrderBy(x => x.Id).Skip(2).First().Id);
            }


            dates = new DatesService(new DateTime(2022, 12, 3, 12, 15, 0), Subject.CDM, 1, 1);

            dates = new DatesService(new DateTime(2022, 12, 3, 12, 15, 0), Subject.Programming, 2, 2);

            dates = new DatesService(new DateTime(2022, 12, 3, 12, 15, 0), Subject.History, 3, 3);


            dates = new DatesService(new DateTime(2022, 12, 4, 12, 15, 0), Subject.History, 3, 2);


            dates = new DatesService(new DateTime(2021, 12, 3, 12, 15, 0), Subject.History, 3, 2);
            s = new StudentsService("stud 11", 1);
            s.AddStudent("stud 12", 1);
            s.AddStudent("stud 13", 1);
            s.AddStudent("stud 21", 2);
            s.AddStudent("stud 22", 2);
            s.AddStudent("stud 23", 2);
            s.AddStudent("stud 31", 3);
            s.AddStudent("stud 32", 3);
            s.AddStudent("stud 33", 3);*/

            dates.ClearOldDates();
        }


        private void selectProfBySubjectBttn_Click(object sender, RoutedEventArgs e)
        {
            var profService = new ProfessorsService();

            if (subjectcb.Text is null || subjectcb.Text.Length == 0)
            {
                infotb.Text = "Subjects should be selected!";
                return;
            }

            List<Professor>? list = null;

            try
            {
                list = profService.GetBySubject((Subject)Enum.Parse(typeof(Subject), subjectcb.Text));
            }
            catch(Exception ex)
            {
                infotb.Text = "There are no professors, who are teaching this subject";
                return;
            }



            if(list is null || list.Count == 0)
            {
                infotb.Text = "There are no professors, who are teaching this subject";
                return;
            }

            infotb.Text = "";
            

            foreach(var el in list)
            {
                infotb.Text += el.Id + " | " + el.Name + " | " + el.Experience + " | " + el.Position + " | ";
                foreach (var sub in el.Subjects)
                    infotb.Text += sub.ToString() + " | ";

                infotb.Text += "\n";
            }
        }


        private void getMostPopularProfBySubject_Click(object sender, RoutedEventArgs e)
        {
            infotb.Text = "";
            if (subjectcb.Text is null || subjectcb.Text.Length == 0)
            {
                infotb.Text = "Subjects should be selected!";
                return;
            }

            var profService = new ProfessorsService();
            Professor? prof = null;

            try
            {
                prof = profService.GetMostPopularBySubject((Subject)Enum.Parse(typeof(Subject), subjectcb.Text));
            }
            catch (Exception ex)
            {
                infotb.Text = "There are no professors, who are teaching this subject";
                return;
            }


            if (prof is null)   
            {
                infotb.Text = "There is none most popular";
                return;
            }

                infotb.Text += prof.Id + " | " + prof.Name + " | " + prof.Experience + " | " + prof.Position + " | ";
            if(prof.Subjects != null && prof.Subjects.Any())
                foreach (var sub in prof.Subjects)
                    infotb.Text += sub.ToString() + " | ";

                infotb.Text += "\n";
           
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
                infotb.Text += el.Id + " | " + el.Name + " | " + el.Experience + " | " + el.Position + " | ";
                foreach (var sub in el.Subjects)
                    infotb.Text += sub.ToString() + " | ";

                infotb.Text += "\n";
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
                infotb.Text += el.Id + " | " + el.Name + " | " + el.Experience + " | " + el.Position + " | ";
                foreach (var sub in el.Subjects)
                    infotb.Text += sub.ToString() + " | ";

                infotb.Text += "\n";
            }
        }

        private void searchFreeByDateBttn_Click(object sender, RoutedEventArgs e)
        {
            int[] dtInput;
            try
            {
                dtInput = Array.ConvertAll(dateInputTb.Text.Split("-"), s => int.Parse(s));

            }
            catch(Exception ex)
            {
                infotb.Text=ex.Message;
                return;
            }

            if(dtInput.Length != 5)
            {
                infotb.Text = "Correct input needed!";
                return ;
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
                infotb.Text += el.Id + " | " + el.Name + " | " + el.Experience + " | " + el.Position + " | ";
                foreach (var sub in el.Subjects)
                    infotb.Text += sub.ToString() + " | ";

                infotb.Text += "\n";
            }

        }

        private void getStudentsByProfBttn_Click(object sender, RoutedEventArgs e)
        {
            var studentService = new StudentsService();

            var profService = new ProfessorsService();

            if (profService is null || studentService is null)
            {
                infotb.Text = "Error!";
                return ;
            }


            List<Student>? list = null;

            try
            {

                list = studentService.GetStudentsByProfessor(profService.GetByName(profNameTb.Text));

            }
            catch(Exception exc)
            {
                infotb.Text = exc.Message;
                return;
            }

            

            infotb.Text = "";


            foreach (var el in list)
            {
                infotb.Text += el.Name;
                infotb.Text += ", ";
                
            }

        }

        private void getStudentsByCourseBttn_Click(object sender, RoutedEventArgs e)
        {
            var studentService = new StudentsService();

            var gcService = new GroupCourseService();

            if (gcService is null || studentService is null)
            {
                infotb.Text = "Error!";
                return;
            }



            List<Student>? list = null;

            try
            {

                list = studentService.GetStudentsByCourse(gcService.GetCourseByName(courseNameTb.Text));

            }
            catch (Exception exc)
            {
                infotb.Text = exc.Message;
                return;
            }

            

            infotb.Text = "";


            foreach (var el in list)
            {
                infotb.Text += el.Name;
                infotb.Text += ", ";
                //  infotb.Tag += ;
            }
        }

        private void AppendData_Click(object sender, RoutedEventArgs e)
        {
            AddInfoWindow win = new AddInfoWindow();
            win.Show();
        }

        private void DeleteData_Click(object sender, RoutedEventArgs e)
        {
            DeleteInfoPage win = new DeleteInfoPage();

            win.Show();
        }
    }
}

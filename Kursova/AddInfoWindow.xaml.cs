using Kursova.Const;
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
using System.Windows.Shapes;

namespace Kursova
{
    /// <summary>
    /// Interaction logic for AddInfoWindow.xaml
    /// </summary>
    public partial class AddInfoWindow : Window
    {
        Services.StudentsService s;
        Services.ProfessorsService p;
        Services.DatesService dates;
        Services.GroupCourseService gc;

        List<Const.Subject> professorSubjects;
        List<Const.Subject> courseSubjects;

        public AddInfoWindow()
        {
            InitializeComponent();
            p = new Services.ProfessorsService();
            s = new Services.StudentsService();
            gc = new Services.GroupCourseService();
            dates = new Services.DatesService();

            professorSubjects = new List<Const.Subject>();
            courseSubjects = new List<Const.Subject>(); 
        }


        private void profsubmit_Click(object sender, RoutedEventArgs e)
        {
            p.AddProfessor(profNametb.Text, professorSubjects.Distinct().ToList(), (Position)Enum.Parse(typeof(Position), profPositionCb.Text), int.Parse(profXpTb.Text));
            logTb.Text += "Professor added.\n";
        }

        private void studsubmit_Click(object sender, RoutedEventArgs e)
        {
            var group = gc.GetGroupByName(StudentGroupIdTb.Text);
            if (group is null)
            {
                logTb.Text += "Error! Name not found.\n";
                return;
            }
            s.AddStudent(StudentNameTb.Text, group.Id);
            logTb.Text += "Student added.\n";
        }

        private void groupsubmit_Click(object sender, RoutedEventArgs e)
        {
            var course = gc.GetCourseByName(GroupCourseNameTb.Text);

            if (course is null)
            {
                logTb.Text += "Error! Course not found.\n";
                return ;
            }


            gc.AddGroup(GroupNameTb.Text, course.Id);

            logTb.Text += "Group added.\n";
        }

        private void coursesubmit_Click(object sender, RoutedEventArgs e)
        {
            gc.AddCourse(courseNameTb.Text, courseSubjects.Distinct().ToList());

            logTb.Text += "Course added.\n";
        }
        private void datesubmit_Click(object sender, RoutedEventArgs e)
        {
            var dtInput = Array.ConvertAll(dateInputTb.Text.Split("-"), s => int.Parse(s));

            if (!dtInput.Any() || dtInput is null || dtInput.Length < 5)
                logTb.Text += "Error! Datetime correct input needed.\n";
            

            DateTime dateTime = new DateTime(dtInput[0], dtInput[1], dtInput[2], dtInput[3], dtInput[4], 0);

            dates.AddDate(dateTime, (Subject)Enum.Parse(typeof(Subject), dateSubjectCb.Text), gc.GetGroupByName(dateGroupNameTb.Text).Id, p.GetByName(dateProfNametb.Text).Id);

            logTb.Text += "Date added.\n";
        }

        private void profAddSubjectBttn_Click(object sender, RoutedEventArgs e)
        {
            professorSubjects.Add((Subject)Enum.Parse(typeof(Subject), profSubjectCb.Text));

            logTb.Text += "Professor`s subject added.\n";
        }

        private void courseAddSubjectBttn_Click(object sender, RoutedEventArgs e)
        {
            courseSubjects.Add((Subject)Enum.Parse(typeof(Subject), courseSubjectCb.Text));

            logTb.Text += "Course`s subject added.\n";
        }
    }
}

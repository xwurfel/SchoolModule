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
using System.Windows.Shapes;

namespace Kursova
{
    /// <summary>
    /// Interaction logic for DeleteInfoPage.xaml
    /// </summary>
    public partial class DeleteInfoPage : Window
    {
        public DeleteInfoPage()
        {
            InitializeComponent();
        }

        private void profsubmit_Click(object sender, RoutedEventArgs e)
        {
            var ps = new ProfessorsService();
            try
            {
                ps.RemoveProfessor(profNametb.Text);

            }
            catch(Exception ex)
            {
                logTb.Text += ex.Message + "\n";
            }
        }


        private void groupsubmit_Click(object sender, RoutedEventArgs e)
        {
            var gcs = new GroupCourseService();

            try
            {
                gcs.RemoveGroup(GroupNameTb.Text);
            }
            catch(Exception ex)
            {
                logTb.Text += ex.Message + "\n";
            }
        }

        private void studentSubmitBttn_Click(object sender, RoutedEventArgs e)
        {
            var ss = new StudentsService();
            try
            {
                ss.RemoveStudent(StudentNameTb.Text);

            }
            catch (Exception ex)
            {
                logTb.Text += ex.Message + "\n";
            }
        }

        private void courseSubmitBttn_Click(object sender, RoutedEventArgs e)
        {
            var gcs = new GroupCourseService();

            try
            {
                gcs.RemoveCourse(courseNameTb.Text);
            }
            catch (Exception ex)
            {
                logTb.Text += ex.Message + "\n";
            }
        }

        private void dateSubmitBttn_Click(object sender, RoutedEventArgs e)
        {
            int[] dtInput;
            try
            {
                dtInput = Array.ConvertAll(dateInputTb.Text.Split("-"), s => int.Parse(s));

            }
            catch (Exception ex)
            {
                logTb.Text = ex.Message;
                return;
            }

            if (dtInput.Length != 5)
            {
                logTb.Text = "Correct input needed!";
                return;
            }


            DateTime dateTime = new DateTime(dtInput[0], dtInput[1], dtInput[2], dtInput[3], dtInput[4], 0);
            var ds = new DatesService();
            try
            {
                ds.RemoveDate(dateGroupNameTb.Text, dateTime);
            }
            catch (Exception ex)
            {
                logTb.Text += ex.Message + "\n";
            }
        }
    }
}

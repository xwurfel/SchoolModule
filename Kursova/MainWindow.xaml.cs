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
            
        }
    }
}

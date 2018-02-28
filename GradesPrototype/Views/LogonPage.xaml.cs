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
using GradesPrototype.Data;
using GradesPrototype.Services;

namespace GradesPrototype.Views
{
    /// <summary>
    /// Interaction logic for LogonPage.xaml
    /// </summary>
    public partial class LogonPage : UserControl
    {
        public LogonPage()
        {
            InitializeComponent();
        }

        #region Event Members
        public event EventHandler LogonSuccess;

        // TODO: Exercise 3: Task 1a: Define LogonFailed event
        public event EventHandler LogonFailed;
        #endregion

        #region Logon Validation

        // TODO: Exercise 3: Task 1b: Validate the username and password against the Users collection in the MainWindow window
        private void Logon_Click(object sender, RoutedEventArgs e)
        {
            var teacher = (from Teacher t in DataSource.Teachers // Selects the DataSource.cs, accesses the Teachers array.
                           where string.Compare(t.UserName, username.Text) == 0 // Compares the username inputted to any match in the array == 0 means if they are the same.
                           && string.Compare(t.Password, password.Password) == 0 // Compares the password to the password in the datasource.
                           select t).FirstOrDefault(); // unsure what this does yet. <---------------------------

            if (teacher.UserName != null)
            {
                SessionContext.UserID = teacher.TeacherID;
                SessionContext.UserName = teacher.UserName;
                SessionContext.UserRole = Role.Teacher;
                SessionContext.CurrentTeacher = teacher;

                    LogonSuccess(this, null);
                    return;

            }
            else
            {
                var student = (from Student s in DataSource.Students where string.Compare (username.Text, s.UserName) == 0 && //Does it matter what comes first using the string.Compare method?? <----------
                               string.Compare (s.Password, password.Password) == 0 select s).FirstOrDefault();
                if (student.UserName != null)
                {
                    SessionContext.UserID = student.StudentID;
                    SessionContext.UserName = student.UserName;
                    SessionContext.UserRole = Role.Student;
                    SessionContext.CurrentStudent = student;

                    LogonSuccess(this, null);
                    return;
                    
                }
            }
            LogonFailed(this, null);


        }
        #endregion
    }
}

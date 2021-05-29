using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainProject.ViewModel
{
    class EmployeeViewModel : BaseViewModel
    {
        #region Field
        ObservableCollection<EMPLOYEE> _ListEmployee;
        int _Index_CurrentEmployee; //Lưu trữ thông tin của view addemployee 
        EMPLOYEE _New_Infor_Employee;// Lưu trữ thông tin của view updateeployee 
        EMPLOYEE _Login_Employee;
        bool _Is_Account;

        ICommand _Check_Account;

        ICommand _Load_View_Update_Employee;
        ICommand _Update_EMployee;
        ICommand _Exit_View_Update_Employee;

        ICommand _Load_View_Add_Employee;
        ICommand _Add_EMployee;
        ICommand _Exit_View_Add_Employee;
    
        ICommand _Delete_EMployee;

        ICommand _Load_View_Detail_Employee;
        ICommand _Exit_View_Detail_Employee;

        ICommand _Load_View_Change_Pass_Employee;
        ICommand _Exit_View_Change_Pass_Employee;

        #endregion

        #region Init

        public EmployeeViewModel()
        {
            using (var db = new mainEntities())
            {

                ListEmployee = new ObservableCollection<EMPLOYEE>(db.EMPLOYEEs.Where(e => ((e.DELETED == 0))).ToList());
                New_Infor_Employee = new EMPLOYEE() { DELETED = 0};
                Is_Account = false;

            }
        }
        #endregion

        #region Propertity

        public ObservableCollection<EMPLOYEE> ListEmployee { get => _ListEmployee; set { if (value != _ListEmployee) { _ListEmployee = value; OnPropertyChanged(); } } }
        public int Index_CurrentEmployee { get => _Index_CurrentEmployee; set { if (_Index_CurrentEmployee != value) { _Index_CurrentEmployee = value; OnPropertyChanged(); } } }
        public EMPLOYEE New_Infor_Employee { get => _New_Infor_Employee; set { if (_New_Infor_Employee != value) { _New_Infor_Employee = value; OnPropertyChanged(); } } }
        public EMPLOYEE Login_Employee { get => _Login_Employee; set { if (_Login_Employee != value) { _Login_Employee = value; OnPropertyChanged(); } } }
        public bool Is_Account { get => _Is_Account; set { if (_Is_Account != value) { _Is_Account = value; OnPropertyChanged(); } } }

        #endregion

        #region Command

        public ICommand Check_Account_Command
        {
            get
            {
                if (_Check_Account == null)
                {
                    _Check_Account = new RelayingCommand<LoginWorkSpace.LoginView>(view => Check_Account(view));
                }
                return _Check_Account;
            }
        }


        public void Check_Account(LoginWorkSpace.LoginView view)
        {
            using (var db = new mainEntities())
            {
                EMPLOYEE employee = (EMPLOYEE)db.EMPLOYEEs.Where(e => (e.Name == Login_Employee.Name && e.Password == Login_Employee.Password && e.DELETED == 0));

                if (employee != null)
                {
                    Is_Account = true;
                    view.Close();
                    MainWindow main = new MainWindow();
                    main.Show();
                }
                else
                {
                    Is_Account = false;
                }
            }
        }

        public ICommand Load_View_Update_Employee_Command
        {
            get
            {
                if (_Load_View_Update_Employee == null)
                {
                    _Load_View_Update_Employee = new RelayingCommand<Object>(a => Load_View_Update_Employee());
                }
                return _Load_View_Update_Employee;
            }
        }


        public void Load_View_Update_Employee()
        {
            //open view Update employee
        }

        public ICommand Update_EMployee_Command
        {
            get
            {
                if (_Update_EMployee == null)
                {
                    _Update_EMployee = new RelayingCommand<Object>(a => Update_EMployee());
                }
                return _Update_EMployee;
            }
        }


        public void Update_EMployee()
        {

            using (var db = new mainEntities())
            {
                EMPLOYEE employee = db.EMPLOYEEs.Where(e => ( e.DELETED == 0 && e.ID == ListEmployee.ElementAt(Index_CurrentEmployee).ID )).FirstOrDefault();
                employee.DELETED = 1;
                db.EMPLOYEEs.Add(ListEmployee.ElementAt(Index_CurrentEmployee));
                db.SaveChanges();
            }
        }

        public ICommand Exit_View_Update_Employee_Command
        {
            get
            {
                if (_Exit_View_Update_Employee == null)
                {
                    _Exit_View_Update_Employee = new RelayingCommand<Object>(a => Exit_View_Update_Employee());
                }
                return _Exit_View_Update_Employee;
            }
        }


        public void Exit_View_Update_Employee()
        {
            //close Updateview Employee
        }


        public ICommand Load_View_Add_Employee_Command
        {
            get
            {
                if (_Load_View_Add_Employee == null)
                {
                    _Load_View_Add_Employee = new RelayingCommand<Object>(a => Load_View_Add_Employee());
                }
                return _Load_View_Update_Employee;
            }
        }


        public void Load_View_Add_Employee()
        {
            //open view Load_View_Add_Employee
        }

        public ICommand Add_EMployee_Command
        {
            get
            {
                if (_Add_EMployee == null)
                {
                    _Add_EMployee = new RelayingCommand<Object>(a => Add_EMployee());
                }
                return _Load_View_Update_Employee;
            }
        }


        public void Add_EMployee()
        {
            using (var db = new mainEntities())
            {
                {
                    db.EMPLOYEEs.Add(New_Infor_Employee);
                    db.SaveChanges();
                }
            }

            ListEmployee.Add(New_Infor_Employee);
        }

        public ICommand Exit_View_Add_Employee_Command
        {
            get
            {
                if (_Exit_View_Add_Employee == null)
                {
                    _Exit_View_Add_Employee = new RelayingCommand<Object>(a => Exit_View_Add_Employee());
                }
                return _Exit_View_Add_Employee;
            }
        }


        public void Exit_View_Add_Employee()
        {
            //close Exit_View_Add_Employee
        }

        public ICommand Delete_EMployee_Command
        {
            get
            {
                if (_Delete_EMployee == null)
                {
                    _Delete_EMployee = new RelayingCommand<Object>(a => Delete_EMployee());
                }
                return _Delete_EMployee;
            }
        }


        public void Delete_EMployee()
        {
            using (var db = new mainEntities())
            {
                EMPLOYEE employee = db.EMPLOYEEs.Where(e => (e.DELETED == 0 && e.ID == ListEmployee.ElementAt(Index_CurrentEmployee).ID)).FirstOrDefault();
                employee.DELETED = 1;
                db.SaveChanges();
            }
            ListEmployee.RemoveAt(Index_CurrentEmployee);
        }

        public ICommand Load_View_Detail_Employee_Command
        {
            get
            {
                if (_Load_View_Detail_Employee == null)
                {
                    _Load_View_Detail_Employee = new RelayingCommand<Object>(a => Load_View_Detail_Employee());
                }
                return _Load_View_Detail_Employee;
            }
        }


        public void Load_View_Detail_Employee()
        {
            //openView_Detail
        }

        public ICommand Exit_View_Detail_Employee_Command
        {
            get
            {
                if (_Exit_View_Detail_Employee == null)
                {
                    _Exit_View_Detail_Employee = new RelayingCommand<Object>(a => Exit_View_Detail_Employee());
                }
                return _Exit_View_Detail_Employee;
            }
        }


        public void Exit_View_Detail_Employee()
        {
            //close Exit_View_Detail_Employee
        }


        public ICommand Load_ViewChange_Pass_Command
        {
            get
            {
                if (_Load_View_Change_Pass_Employee == null)
                {
                    _Load_View_Change_Pass_Employee = new RelayingCommand<Object>(a => Load_View_Change_Pass_Employee());
                }
                return _Load_View_Change_Pass_Employee;
            }
        }


        public void Load_View_Change_Pass_Employee()
        {
            //open View_Change_Passl
        }

        public ICommand Exit_View_Change_Pass_Employee_Command
        {
            get
            {
                if (_Exit_View_Change_Pass_Employee == null)
                {
                    _Exit_View_Change_Pass_Employee = new RelayingCommand<Object>(a => Exit_View_Change_Pass_Employee());
                }
                return _Exit_View_Change_Pass_Employee;
            }
        }


        public void Exit_View_Change_Pass_Employee()
        {
            //close Exit_View_Change_Pass_Employee
        }


        #endregion




    }
}

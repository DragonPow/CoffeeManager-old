using MainProject.AccountWorkSpace;
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
        bool _IsPassword;
        bool _IsConfirmPassword;
        bool _Is_Add_New_Employee;

        string _PassWord;
        string _Confirm_Password;

        ICommand _Click_Add_New_Employee;

        ICommand _Cancel;

        ICommand _UpDate_Add_EMployee;
    
        ICommand _Delete_EMployee;

        ICommand _Load_View_Change_Pass_Employee;
        ICommand _Change_Pass_Employee;
       

        #endregion

        #region Init

        public EmployeeViewModel()
        {
            using (var db = new mainEntities())
            {

                ListEmployee = new ObservableCollection<EMPLOYEE>(db.EMPLOYEEs.Where(e => ((e.DELETED == 0))).ToList());
                New_Infor_Employee = new EMPLOYEE() { DELETED = 0};
                Index_CurrentEmployee = 0;
                IsPassword = IsConfirmPassword = true;
                Is_Add_New_Employee = false;
            }
        }
        #endregion

        #region Propertity

        public ObservableCollection<EMPLOYEE> ListEmployee { get => _ListEmployee; set { if (value != _ListEmployee) { _ListEmployee = value; OnPropertyChanged(); } } }
        public int Index_CurrentEmployee { get => _Index_CurrentEmployee; set { if (_Index_CurrentEmployee != value) { _Index_CurrentEmployee = value; OnPropertyChanged(); } } }
        public EMPLOYEE New_Infor_Employee { get => _New_Infor_Employee; set { if (_New_Infor_Employee != value) { _New_Infor_Employee = value; OnPropertyChanged(); } } }
        public string PassWord { get => _PassWord; set { if (_PassWord != value) { _PassWord = value; OnPropertyChanged(); } } }
        public string Confirm_Password { get => _Confirm_Password; set { if (_Confirm_Password != value) { _Confirm_Password = value; OnPropertyChanged(); } } }
        public bool IsPassword { get => _IsPassword; set { if (_IsPassword != value) { _IsPassword = value; OnPropertyChanged(); } } }
        public bool IsConfirmPassword { get => _IsConfirmPassword; set { if (_IsConfirmPassword != value) { _IsConfirmPassword = value; OnPropertyChanged(); } } }
        public bool Is_Add_New_Employee { get => _Is_Add_New_Employee; set { if (_Is_Add_New_Employee != value) { _Is_Add_New_Employee = value; OnPropertyChanged(); } } }

        #endregion

        #region Command

        /* public ICommand Check_Account_Command
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
        }*/



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

        public ICommand UpDate_Add_EMployee_Command
        {
            get
            {
                if (_UpDate_Add_EMployee == null)
                {
                    _UpDate_Add_EMployee = new RelayingCommand<Object>(a => UpDate_Add_EMployee());
                }
                return _UpDate_Add_EMployee;
            }
        }


        public void UpDate_Add_EMployee()
        {
            using (var db = new mainEntities())
            {
                    if (Is_Add_New_Employee)
                    {
                        db.EMPLOYEEs.Add(New_Infor_Employee);
                        db.SaveChanges();
                        ListEmployee.Add(New_Infor_Employee);
                }
                    else
                    {
                        EMPLOYEE employee = db.EMPLOYEEs.Where(e => (e.DELETED == 0 && e.ID == ListEmployee.ElementAt(Index_CurrentEmployee).ID)).FirstOrDefault();
                        employee.DELETED = 1;
                        db.EMPLOYEEs.Add(ListEmployee.ElementAt(Index_CurrentEmployee));
                        db.SaveChanges();
                        ListEmployee[ListEmployee.IndexOf(ListEmployee.ElementAt(Index_CurrentEmployee))] = employee;

                }
            }
            Is_Add_New_Employee = false;

            
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
           //Show view ChangePass
        }

        public ICommand Cancel_Command
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new RelayingCommand<Object>(a => Cancel());
                }
                return _Cancel;
            }
        }


        public void Cancel()
        {
            using (var db = new mainEntities())
            {
                
                EMPLOYEE employee = db.EMPLOYEEs.Where(e => (e.DELETED == 0 && e.ID == ListEmployee.ElementAt(Index_CurrentEmployee).ID)).FirstOrDefault();
                ListEmployee[ListEmployee.IndexOf(ListEmployee.ElementAt(Index_CurrentEmployee))] = employee;           
            }
        }


        public ICommand Change_Pass_Employee_Command
        {
            get
            {
                if (_Change_Pass_Employee == null)
                {
                    _Change_Pass_Employee = new RelayingCommand<Object>(a => Change_Pass_Employee());
                }
                return _Change_Pass_Employee;
            }
        }

        public void Change_Pass_Employee()
        {
            using (var db = new mainEntities())
            {
                var e = db.EMPLOYEEs.Where(employee => (employee.Password == PassWord && ListEmployee.ElementAt(Index_CurrentEmployee).ID == employee.ID && ListEmployee.ElementAt(Index_CurrentEmployee).DELETED == 0)).FirstOrDefault();

                if (e == null)
                {
                    IsPassword = false;
                    return;
                }

                if (Confirm_Password != Confirm_Password)
                {
                    IsConfirmPassword = false;
                    return;
                }

                e.Password = PassWord;
                db.SaveChanges();
            }
        }

        public ICommand Click_Add_New_Employee_Command
        {
            get
            {
                if (_Click_Add_New_Employee == null)
                {
                    _Click_Add_New_Employee = new RelayingCommand<Object>(a => Is_Add_New_Employee = true); ;
                }
                return _Click_Add_New_Employee;
            }
        }

        #endregion


    }
}

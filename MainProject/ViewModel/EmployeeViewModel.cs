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
        ObservableCollection<EMPLOYEE> _ListEmployee; /*list employee hiện tại*/
        long _ID_CurrentEmployee; /* cái này ko binding, chỉ dùng để lưu lại giá trị của  employee đang đc chỉnh sửa, khi hủy thì load lại dữ liệu từ database */
        EMPLOYEE _New_Infor_Employee;  /*là cái hiển thị khi xem detail, edit, add employee*/
        bool _IsPassword;/* xác định xem password nhập vào đúng ko*/
        bool _IsConfirmPassword; /*xác định confirmpass đúng hay ko */
        bool _Is_Add_New_Employee; /*xác định hiện tại đang thực hiện add new EMPLOYEE */

        string _PassWord;  /*nhập new Password*/
        string _Confirm_Password;  /*confirm lại pasword*/

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
              /*  New_Infor_Employee = new EMPLOYEE() { DELETED = 0, Name = "", Password = "", Birthday = null, Phone = "", POSITION_EMPLOYEE = null };*/
                IsPassword = IsConfirmPassword = true;
                Is_Add_New_Employee = false;
            }
        }
        #endregion

        #region Propertity

        public ObservableCollection<EMPLOYEE> ListEmployee { get => _ListEmployee; set { if (value != _ListEmployee) { _ListEmployee = value; OnPropertyChanged(); } } }
        public long ID_CurrentEmployee { get => _ID_CurrentEmployee; set { if (_ID_CurrentEmployee != value) { _ID_CurrentEmployee = value; OnPropertyChanged(); } } }
        public EMPLOYEE New_Infor_Employee { get => _New_Infor_Employee; set { if (_New_Infor_Employee != value) { _New_Infor_Employee = value; OnPropertyChanged();  } } }
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

        public ICommand Click_Add_New_Employee_Command
        {
            get
            {
                if (_Click_Add_New_Employee == null)
                {
                    _Click_Add_New_Employee = new RelayingCommand<Object>(a => Click_Add_New_Employee());
                }
                return _Click_Add_New_Employee;
            }
        }


        public void Click_Add_New_Employee()
        {
            ID_CurrentEmployee = New_Infor_Employee.ID;

            ListEmployee.Add(new EMPLOYEE() { DELETED = 0});
            New_Infor_Employee = ListEmployee.Last();

            Is_Add_New_Employee = true;
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
                }
                    else
                    {
                        ID_CurrentEmployee = New_Infor_Employee.ID;

                        EMPLOYEE employee = db.EMPLOYEEs.Where(e => (e.DELETED == 0 && e.ID == New_Infor_Employee.ID)).FirstOrDefault();
                        employee.DELETED = 1;
                        db.EMPLOYEEs.Add(New_Infor_Employee);
                        db.SaveChanges();

                        ListEmployee[ListEmployee.IndexOf(New_Infor_Employee)] = employee;
                }
            }

            Is_Add_New_Employee = false;
            ID_CurrentEmployee = new long();

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
                EMPLOYEE employee = db.EMPLOYEEs.Where(e => (e.DELETED == 0 && e.ID == New_Infor_Employee.ID)).FirstOrDefault();
                employee.DELETED = 1;
                db.SaveChanges();
            }

            ListEmployee.Remove(New_Infor_Employee);
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
                
                EMPLOYEE employee = db.EMPLOYEEs.Where(e => (e.DELETED == 0 && e.ID == ID_CurrentEmployee)).FirstOrDefault();
                ListEmployee[ListEmployee.IndexOf(New_Infor_Employee)] = employee;

                if (Is_Add_New_Employee) ListEmployee.RemoveAt(ListEmployee.Count);
            }
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
                var e = db.EMPLOYEEs.Where(employee => (employee.Password == PassWord && New_Infor_Employee.ID == employee.ID && New_Infor_Employee.DELETED == 0)).FirstOrDefault();

                if (e == null)
                {
                    IsPassword = false;
                    return;
                }

                IsPassword = true;

                if (Confirm_Password != PassWord)
                {
                    IsConfirmPassword = false;
                    return;
                }

                IsConfirmPassword = true;

                e.Password = PassWord;
                db.SaveChanges();

                New_Infor_Employee.Password = PassWord;

                //close view changePass
            }
        }


        #endregion


    }
}

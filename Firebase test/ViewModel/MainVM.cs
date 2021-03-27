using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase_test.ViewModel;
using Firebase_test.Model;
using FirebaseAdmin.Auth;
using System.Windows.Input;
using Firebase_test.Commands;

namespace Firebase_test.ViewModel
{
    class MainVM : BaseVM
    {
        public FirebaseAccount Account { get; set;}
        public string NewPassword { get; set; }

        public MainVM()
        {
            CreateCommands();
        }


        public ICommand LoginButtonCommand { get; private set; }
        private void OnLoginButtonCommandExecuted(object p)
        {
           
        }
        private bool CanLoginButtonCommandExecute(object p) => true;

        public ICommand ChangePassButtonCommand { get; private set; }
        private void OnChangePassButtonCommandExecuted(object p)
        {

        }
        private bool CanChangePassCommandExecute(object p) => true;

        public ICommand RecoveryPassButtonCommand { get; private set; }
        private void OnRecoveryPassButtonCommandExecuted(object p)
        {

        }
        private bool CanRecoveryPassButtonCommandExecute(object p) => true;

        public ICommand DeleteAccButtonCommand { get; private set; }
        private void OnDeleteAccButtonCommandExecuted(object p)
        {

        }
        private bool CanDeleteAccButtonCommandExecute(object p) => true;


        public void CreateCommands()
        {
            LoginButtonCommand = new LambdaCommand(OnLoginButtonCommandExecuted, CanLoginButtonCommandExecute);
            ChangePassButtonCommand = new LambdaCommand(OnChangePassButtonCommandExecuted, CanChangePassCommandExecute);
            RecoveryPassButtonCommand = new LambdaCommand(OnRecoveryPassButtonCommandExecuted, CanRecoveryPassButtonCommandExecute);
            DeleteAccButtonCommand = new LambdaCommand(OnDeleteAccButtonCommandExecuted, CanDeleteAccButtonCommandExecute);
        }
    }
}

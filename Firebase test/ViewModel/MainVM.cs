using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase_test.ViewModel;
using System.Windows.Input;
using Firebase_test.Commands;
using Firebase.Auth;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Windows;

namespace Firebase_test.ViewModel
{
    class MainVM : BaseVM
    {
        private FirebaseAuthProvider authProvider;
        private string firebaseToken;
        private string _password = String.Empty;
        private string _newPassword = String.Empty;
        private string _email = String.Empty;
        private string _accountInfo = String.Empty;

        public string NewPassword
        {
            get { return _newPassword; }
            set { if (!_newPassword.Equals(value)) _newPassword = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get { return _email; }
            set { if (!_email.Equals(value)) _email = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get { return _password; }
            set { if (!_password.Equals(value)) _password = value; OnPropertyChanged(); }
        }
        public string AccountInfo
        {
            get { return _accountInfo; }
            set { if (!_accountInfo.Equals(value)) _accountInfo = value; OnPropertyChanged(); }
        }


        public MainVM()
        {
            CreateCommands();
            authProvider = PrepareAuthProvider();
        }


        public ICommand LoginButtonCommand { get; private set; }
        private void OnLoginButtonCommandExecuted(object p)
        {
          Task<FirebaseAuthLink> login = Login();
            if (login.IsFaulted) MessageBox.Show(login.Exception.Message);
            if (login.IsCompleted) 
            {
                AccountInfo = login.Result.User.ToString();
                firebaseToken = login.Result.FirebaseToken;
            } 
        }
        private bool CanLoginButtonCommandExecute(object p)
        {
            return CheckEmailAndPasswordEntered();
        }

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

        public ICommand CreateAccButtonCommand { get; private set; }
        private void OnCreateAccButtonCommandExecuted(object p)
        {
            var create = CreateAccount().ContinueWith(v => MessageBox.Show(v.Status.ToString(), v.Exception.Message.ToString()));
           // create.Wait();
            if (create.IsFaulted) MessageBox.Show(create.Exception.Message);
            if (create.IsCompleted) MessageBox.Show("Account has been created");


            
        }
        private bool CanCreateAccButtonCommandExecute(object p)
        {
            return CheckEmailAndPasswordEntered();
        }

        private FirebaseAuthProvider PrepareAuthProvider()
        {
            var serviceProvider = new ServiceCollection()
           .AddHttpClient()
           .BuildServiceProvider();

            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            return new FirebaseAuthProvider("SuperApiKey11", httpClientFactory);

        }
        public void CreateCommands()
        {
            LoginButtonCommand = new LambdaCommand(OnLoginButtonCommandExecuted, CanLoginButtonCommandExecute);
            ChangePassButtonCommand = new LambdaCommand(OnChangePassButtonCommandExecuted, CanChangePassCommandExecute);
            RecoveryPassButtonCommand = new LambdaCommand(OnRecoveryPassButtonCommandExecuted, CanRecoveryPassButtonCommandExecute);
            DeleteAccButtonCommand = new LambdaCommand(OnDeleteAccButtonCommandExecuted, CanDeleteAccButtonCommandExecute);
            CreateAccButtonCommand = new LambdaCommand(OnCreateAccButtonCommandExecuted, CanCreateAccButtonCommandExecute);
        }

        public bool CheckEmailAndPasswordEntered()
        {
            if (_email == string.Empty) return false;
            if (_password == string.Empty) return false;
            return true;
        }

        private async Task<FirebaseAuthLink> Login()
        {
            return await authProvider.SignInWithEmailAndPasswordAsync(Email, Password);
        }

        private async Task<FirebaseAuthLink> CreateAccount()
        {
            var create = await authProvider.CreateUserWithEmailAndPasswordAsync(_email, _password, "Test", true);
            
            return create;
        }

    }
}

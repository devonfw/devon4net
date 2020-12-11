using Excalibur.Shared.Observable;

namespace MyThaiStar.Core.Observable
{
    public class User : ObservableBase<int>
    {
        private string _name;
        private string _website;
        private string _phone;
        private string _email;
        private string _username;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        public string Website
        {
            get { return _website; }
            set { SetProperty(ref _website, value); }
        }

        public string Image { get { return "http://placekitten.com/202/202"; } }
    }
}

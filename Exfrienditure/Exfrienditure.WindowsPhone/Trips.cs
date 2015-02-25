using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exfrienditure
{
    public class Trips : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private string _Color = string.Empty;
        public string Color
        {
            get
            {
                return this._Color;
            }

            set
            {
                if (this._Color != value)
                {
                    this._Color = value;
                    this.OnPropertyChanged("Color");
                }
            }
        }
        private string _Name = string.Empty;
        public string Name
        {
            get
            {
                return this._Name;
            }

            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }
        private Windows.UI.Color _Colors1;
        public Windows.UI.Color Colors1
        {
            get
            {
                return this._Colors1;
            }

            set
            {
                if (this._Colors1 != value)
                {
                    this._Colors1 = value;
                    this.OnPropertyChanged("Colors1");
                }
            }
        }

    }
}

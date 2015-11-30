using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.Annotations;

namespace DesktopClient
{
    public class StatsView : INotifyPropertyChanged
    {
        private Stats _stats = new Stats();

        public Stats CurrentStats
        {
            get { return _stats; }
            set
            {
                _stats = value;
               OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

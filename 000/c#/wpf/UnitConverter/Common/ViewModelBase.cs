using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UnitConverter.Common
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void ChangePropertyValueAndNotify<T>(ref T privateVar, T value, [CallerMemberName] string propertyName = "")
        {
            if (!privateVar?.Equals(value) ?? true)
            {
                privateVar = value;
                NotifyPropertyChanged(propertyName);
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

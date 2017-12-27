using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UnitConverter.Common;
using UnitConverter.Conversion;
using UnitConverter.Model.Repositories;
using UnitConverter.Model.Entities;

namespace UnitConverter.View
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(IConversionEngine conversionEngine, IUnitsRepository unitsRepository)
        {
            unitsListFrom = new ObservableCollection<Unit>();
            unitsListTo   = new ObservableCollection<Unit>();

            ConvertCommand = new RelayCommand( o => { var _2 = ConvertNumber(o); }, CanConvertNumber);
            this.conversionEngine = conversionEngine;
            this.unitsRepository = unitsRepository;
            var _ = LoadList(unitsRepository.InputUnitsCollection(), UnitsListFrom);

            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(UnitFrom)))
            {
                var _ = LoadList(unitsRepository.OutputUnitsCollection(UnitFrom), UnitsListTo);
            }
        }

        private async Task LoadList(Task<List<Unit>> futureList, ObservableCollection<Unit> collection)
        {
            var list = await futureList;
            collection.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                collection.Add(list[i]);
            }
        }

        private async Task ConvertNumber(object obj)
        {
            ConvertTo = await conversionEngine.ConvertValue(ConvertFrom, UnitFrom, UnitTo);
        }

        private bool CanConvertNumber(object obj)
        {
            return true;
        }

        public ICommand ConvertCommand { get; }

        public decimal ConvertFrom
        {
            get => convertFrom;
            set
            {
                ChangePropertyValueAndNotify(ref convertFrom, value);
            }
        }

        public decimal ConvertTo
        {
            get => convertTo;
            set => ChangePropertyValueAndNotify(ref convertTo, value);
        }

        public Unit UnitFrom
        {
            get => unitFrom;
            set
            {
                ChangePropertyValueAndNotify(ref unitFrom, value);
            }
        }

        public ObservableCollection<Unit> UnitsListFrom => unitsListFrom;

        public Unit UnitTo
        {
            get => unitTo;
            set => ChangePropertyValueAndNotify(ref unitTo, value);
        }

        public ObservableCollection<Unit> UnitsListTo => unitsListTo;

        private decimal convertFrom;
        private decimal convertTo;
        private Unit unitFrom;
        private Unit unitTo;
        private ObservableCollection<Unit> unitsListFrom;
        private ObservableCollection<Unit> unitsListTo;
        private readonly IConversionEngine conversionEngine;
        private readonly IUnitsRepository unitsRepository;
    }
}
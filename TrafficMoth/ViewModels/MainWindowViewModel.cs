using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrafficMoth.Models;

namespace TrafficMoth.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<CapturedRequest> Requests { get; }

        private CapturedRequest _current;
        public CapturedRequest Current
        {
            get => _current;
            set => this.RaiseAndSetIfChanged(ref _current, value);
        }

        public MainWindowViewModel()
        {
            Requests = new ObservableCollection<CapturedRequest>();
        }
    }
}

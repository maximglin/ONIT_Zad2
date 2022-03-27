using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ONIT_Zad2
{
    public class MainVM : BaseVM
    {
        public IEnumerable<string> Types => new List<string> { 
            "Активные прослушиваюшие TCP-порты",
            "Активные прослушивающие UDP-порты",
            "Активные TCP-подключения"
        };

        int selIndex = 0;
        public int SelectedIndex { get => selIndex; set { 
                Set(ref selIndex, value); OnPropertyChanged(nameof(Content));
                minusCommand.OnCanExecuteChanged();
                plusCommand.OnCanExecuteChanged();
            } 
        }

        public UserControl Content {
            get
            {
                switch(SelectedIndex)
                {
                    case 0:
                        return new EndPointsView(new EndPointsVM(EndPointsVM.PortType.Tcp));
                    case 1:
                        return new EndPointsView(new EndPointsVM(EndPointsVM.PortType.Udp));
                    case 2:
                        return new ConnectionsView(new ConnectionsVM());
                    default:
                        return new EndPointsView(new EndPointsVM(EndPointsVM.PortType.Tcp));
                }
                
            }
        }
        ActionCheckCommand minusCommand, plusCommand;
        public ICommand MinusCommand => minusCommand;
        public ICommand PlusCommand => plusCommand;

        public MainVM()
        {
            minusCommand = new ActionCheckCommand(() =>
            {
                if (SelectedIndex > 0)
                    SelectedIndex--;
            },
            () => SelectedIndex > 0);
            plusCommand = new ActionCheckCommand(() =>
            {
                if (SelectedIndex < 2)
                    SelectedIndex++;
            },
            () => SelectedIndex < 2);
        }
    }
}

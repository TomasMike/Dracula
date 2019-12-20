using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dracula.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class HostWindow : Window
	{
		private List<Window> Windows = new List<Window>();

		public HostWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var s = normal();
		}

		public string normal()
		{
			//DracServRef.ServiceClient client = new DracServRef.ServiceClient();
			//var t = client.test(3);
			return "";
		}

		private void CreateNewPlayerClick(object sender, RoutedEventArgs e)
		{
	
			var v = new PlayerWindow();
			v.Show();
		}

		
	}

}

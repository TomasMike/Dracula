using Dracula.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Dracula.Client.Net
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		CommClient c = new CommClient();

		CommServer s = new CommServer();
		public MainWindow()
		{
			InitializeComponent();

			s.OnCall = (o) => 
			{
				tBl1.Text += $"Server[] {o}";
				return null;
			};
			s.Log = (s) => { tBl1.Text += $"Log:{s}"; };

			Task.Factory.StartNew(() => s.Start());
		}

		

		private void NewPlayerBtn_Click(object sender, RoutedEventArgs e)
		{
			Window1 w = new Window1();
			w.Show();
		}
	}
}

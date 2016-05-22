using System.Collections.Generic;
using System.Windows;
using RectangleConnector.ViewModel.DTO;

namespace RectangleConnectorApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new RectangleConnector.ViewModel.MainViewModel(
                new List<Rectangle>
                {
                    new Rectangle(new Coordinate(100,100), new Coordinate(150,150), Color.Black),
                    new Rectangle(new Coordinate(50,50), new Coordinate(100,175), Color.Blue),

                },
                new List<Rectangle>
                {
                        new Rectangle(new Coordinate(30,60), new Coordinate(60,300), Color.Red),
                });

            InitializeComponent();
        }
    }
}

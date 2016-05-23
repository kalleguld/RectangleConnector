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
                    new Rectangle(new Coordinate(200,200), new Coordinate(300,300),new Color(255,255,0)),
                    new Rectangle(new Coordinate(200,0), new Coordinate(300,100),new Color(255,0,255)),
                },
                new List<Rectangle>
                {
                    new Rectangle(new Coordinate(30,60), new Coordinate(60,300), Color.Red),
                    new Rectangle(new Coordinate(200,0), new Coordinate(300,100), new Color(127,127,127)),
                    new Rectangle(new Coordinate(200,200), new Coordinate(300,300), Color.Green),
                });

            InitializeComponent();
        }
    }
}

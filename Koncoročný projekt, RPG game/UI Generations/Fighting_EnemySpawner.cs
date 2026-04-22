using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Reflection.Metadata.BlobBuilder;

namespace Koncoročný_projekt__RPG_game.UI_Generations
{
    internal class Fighting_EnemySpawner : Grid
    {

        public List<(Label progLab, ProgressBar prog, Label atkLabel, Label defLabel)> stuff = new List<(Label progLab, ProgressBar prog, Label atkLabel, Label defLabel)>();

        public Fighting_EnemySpawner()
        {
            Height = 200;
            Width = 210;
            Margin = new System.Windows.Thickness(2);
            Background = System.Windows.Media.Brushes.DarkGray;



            ProgressBar progressBar = new ProgressBar()
            {
                Height = 30,
                Width = 180,
                Background = System.Windows.Media.Brushes.Gray,
                Foreground = System.Windows.Media.Brushes.Red,
                Margin = new System.Windows.Thickness(5),
                Value = 50
            };

            Label progressLabel = new Label()
            {
                Content = "50hp",
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                FontSize = 20,
                Margin = new System.Windows.Thickness(5,0,5,5),
                Foreground = System.Windows.Media.Brushes.White
            };

            Label Image_Prompt = new Label()
            {
                Height = 100,
                Width = 100,
                Background = System.Windows.Media.Brushes.Gray,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new System.Windows.Thickness(5, 5, 5, 20),
            };

            Label DefShet = new Label()
            {
                Height = 30,
                Width = 50,
                Background = System.Windows.Media.Brushes.Gray,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                Margin = new System.Windows.Thickness(5),
                FontSize = 10, 
                Foreground = System.Windows.Media.Brushes.Black,
                Content = "Defence:"
            };
            
            Label AtkShet = new Label()
            {
                Height = 30,
                Width = 50,
                Background = System.Windows.Media.Brushes.Gray,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                Margin = new System.Windows.Thickness(5,5,50,5),
                FontSize = 10,
                Foreground = System.Windows.Media.Brushes.Black,
                Content = "Attack:"
            };

            Label AtkNum = new Label()
            {
                Height = 35,
                Width = 40,
                Background = System.Windows.Media.Brushes.Gray,
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Margin = new System.Windows.Thickness(5, 5, 5, 5),
                FontSize = 15,
                Foreground = System.Windows.Media.Brushes.Black,
                Content = $"   -"
            };

            Label DefNum = new Label()
            {
                Height = 35,
                Width = 40,
                Background = System.Windows.Media.Brushes.Gray,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                Margin = new System.Windows.Thickness(60, 5, 5, 5),
                FontSize = 15,
                Foreground = System.Windows.Media.Brushes.Black,
                Content = "   -"
            };

            progressBar.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            progressLabel.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            Children.Add(DefNum);
            Children.Add(AtkNum);
            Children.Add(AtkShet);
            Children.Add(DefShet);
            Children.Add(Image_Prompt);
            Children.Add(progressBar);
            Children.Add(progressLabel);

            stuff.Add((progressLabel, progressBar, AtkNum, DefNum));
            /*
                var tempButton = new Label()
                {
                    Height = 95,
                    Width = 100,
                    FontSize = 30,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                   // Margin = new System.Windows.Thickness(110, box_position, 5, 5),
                    Background = System.Windows.Media.Brushes.DarkGray,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left
                };

                tempButton.MouseDown += (s, e) =>
                {
                  //  inventoryMovementClass.Equip_Pressed(names[current], current);
                };

                Children.Add(tempButton);
    

            */
        }
    }
}

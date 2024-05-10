using Minesweeper.Core;
using Minesweeper.Interfaces;
using Minesweeper.Services;
using Minesweeper.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Xml.Serialization;

namespace Minesweeper.Models
{
    public class UIListener : IGameListener
    {
        public IGame Game { get; set; }

        public UIListener(IGame game)
        {
            Game = game;
        }

        public void OnLeftClickOnTile() 
        {
            // MessageBox.Show("Left click from UIListener.");
        }

        public void OnRightClickOnTile() 
        {
            // MessageBox.Show("Right click from UIListener.");
        }
       
        public void OnGameLost()
        {
            MessageBox.Show("Mine clicked. Game lost!");
            Statistics statistics = new Statistics
            {
                Result = "Lost",
                EndTime = Game.EndTime,
                Difficulty = Game.DifficultyLevel.ToString(),
                ElapsedTime = (Game.EndTime - Game.StartTime).ToString()
            };
            SaveStatistics(statistics);
        }

        public void OnGameWon()
        {
            MessageBox.Show("Congrats! You won!");
            Statistics statistics = new Statistics
            {
                
                Result = "Won",
                EndTime = Game.EndTime,
                Difficulty = Game.DifficultyLevel.ToString(),
                ElapsedTime = (Game.EndTime - Game.StartTime).ToString()
            };
            SaveStatistics(statistics);
        }

        private void SaveStatistics(Statistics statistics)
        {
            string filePath = "Statistics/statistics.xml";

            List<Statistics> existingStatistics = new List<Statistics>();

            try
            {
                // If the file exists, try to deserialize it
                if (File.Exists(filePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Statistics>));
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        existingStatistics = (List<Statistics>)serializer.Deserialize(reader);
                    }
                }

                // Add the new statistic to the existing list
                existingStatistics.Add(statistics);

                // Serialize the updated list and save it back to the XML file
                XmlSerializer serializerOut = new XmlSerializer(typeof(List<Statistics>));
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    serializerOut.Serialize(writer, existingStatistics);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or display the error message
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


    }
}

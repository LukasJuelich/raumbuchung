using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Raumbuchung
{
    class Raumverwaltung
    {            
        private List<String> roomsAvailable = new List<string>();
        private List<String> roomsReserved = new List<string>();

        public Raumverwaltung()
        {
        }

        private void determineRooms(DateTime start, DateTime end)
        {
            string appPath = System.AppDomain.CurrentDomain.BaseDirectory;

            using (StreamReader csvStream = new StreamReader(appPath + "Probeaufgabe Raumbuchung Beispieldaten.csv"))
            {
                var csvContent = csvStream.ReadLine(); //   skip first line
                while ((csvContent = csvStream.ReadLine()) != null)
                {
                    var csvData = csvContent.Split(';');
                    var csvStart = DateTime.Parse(csvData[0]);
                    var csvEnd = DateTime.Parse(csvData[1]);
                    string csvRoom = csvData[2];

                    if ((csvStart <= start) && (start <= csvEnd) ||
                        (start <= csvStart) && (csvStart <= end))
                    {
                        if (!roomsReserved.Contains(csvRoom))
                            roomsReserved.Add(csvRoom);
                    }
                    else
                    {
                        if (!roomsAvailable.Contains(csvRoom))
                            roomsAvailable.Add(csvRoom);
                    }
                }
            }
        }

        private string getAvailableRooms()
        {
            foreach(string room in roomsReserved)
            {
                roomsAvailable.Remove(room);
            }

            if(roomsAvailable.Count == 0)
            {
                return "Keine verfügbaren Räume gefunden!";
            }
            else
            {
                return "Verfügbare Räume: " + string.Join(",", roomsAvailable.ToArray());
            }
        }

        public String searchRoom(DateTime start, DateTime end)
        {            
            if(end <= start)
            {
                return "Zeitparadoxon! Start- und Endzeit sind nicht möglich!";
            }

            //  create new DateTime because of milliseconds
            start = new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, 0);
            end = new DateTime(end.Year, end.Month, end.Day, end.Hour, end.Minute, 0);

            roomsAvailable.Clear();
            roomsReserved.Clear();

            determineRooms(start, end);

            return getAvailableRooms();
        }
    }
}

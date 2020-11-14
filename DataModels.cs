using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HKU_UBikeStation
{
    public class StationDetail
    {
        public int ID { get; set; }
        public string Position { get; set; }
        public string EName { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string CArea { get; set; }
        public string EArea { get; set; }
        public string CAddress { get; set; }
        public string EAddress { get; set; }
        public int AvailableCNT { get; set; }
        public int EmpCNT { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}

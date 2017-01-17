using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsScanline
{
    //represents structure for interpolating data form gouraud shading
    public struct ScanLineData
    {
        public int currentY;
        public float ndotla;
        public float ndotlb;
        public float ndotlc;
        public float ndotld;
    }
}

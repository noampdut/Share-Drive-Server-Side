﻿namespace trempApplication.Properties.Models
{
    public class MapRequest
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public List<string> Waypoints { get; set; }
    }
}

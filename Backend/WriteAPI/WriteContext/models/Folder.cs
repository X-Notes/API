﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WriteContext.models
{
    public class Folder
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Color { set; get; }
        public int UserId { set; get; }
        public User User { set; get; }
    }
}

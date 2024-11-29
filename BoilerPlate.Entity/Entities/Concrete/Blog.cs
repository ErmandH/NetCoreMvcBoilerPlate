﻿using BoilerPlate.Entity.Entities.Abstract;
using BoilerPlate.Entity.Entities.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Entity.Entities.Concrete
{
    public class Blog : BaseEntity, IEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

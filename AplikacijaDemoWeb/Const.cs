using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplikacijaDemoWeb
{
    public abstract class Const
    {
        public enum Akcije
        {
            SPREMI_PRIMATELJA = 1,
            POSLANA_PORUKA = 2,
            UPOZORENJE_PREDUGA_PORUKA = 3
        }
    }
}
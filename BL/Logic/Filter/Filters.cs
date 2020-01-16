using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class Filters
    {
        public static Filter filterName = new Filter(ent => ent.ToString());
        public static Filter filterParent = new Filter(ent => ent.Parent.ToString());
        public static Filter filterFireCabinetFault = new Filter(true,
                                    new Instruction(ent => ((FireCabinet)ent).IsDented, ent => "Поврежден; "),
                                    new Instruction(ent => !((FireCabinet)ent).IsSticker, ent => "Без наклейки; ")
                                    );
        public static Filter filterExtinguisherFault = new Filter(true,
                                    new Instruction(ent => ((Extinguisher)ent).IsDented, ent => "Поврежден; "),
                                    new Instruction(ent => !((Extinguisher)ent).IsSticker, ent => "Без наклейки; "),
                                    new Instruction(ent => !((Extinguisher)ent).IsHose, ent => "Нет шланга; "),
                                    new Instruction(ent => ((Extinguisher)ent).IsLabelDamage, ent => "Повреждена этикетка; "),
                                    new Instruction(ent => ((Extinguisher)ent).IsHandleDamage, ent => "Повреждено ЗПУ; "),
                                    new Instruction(ent => ((Extinguisher)ent).IsPaintDamage, ent => "Повреждена краска; "),
                                    new Instruction(ent => ((Extinguisher)ent).IsPressureGaugeFault, ent => "Поврежден манометр; "),
                                    new Instruction(ent => ((Extinguisher)ent).Pressure < ((Extinguisher)ent).KindExtinguisher.MinPressure, ent => "Давление менее допустимого; "),
                                    new Instruction(ent => ((Extinguisher)ent).Weight < ((Extinguisher)ent).KindExtinguisher.MinWeight, ent => "Вес менее допустимого; ")
                                    );
        public static Filter filterHoseFault = new Filter(true,
                                    new Instruction(ent => ((Hose)ent).IsRagged, ent => "Порван; "),
                                    new Instruction(ent => ((Hose)ent).DateRolling.Subtract(DateTime.Now).Days < 30, ent => "Необходима перекатка; ")
                                    );
        public static Filter filterHydrantFault = new Filter(ent => ((Hydrant)ent).IsDamage, ent => "Поврежден; ", true);
        public static Filter filterExtinguisherRecharge = new Filter(true,
            new Instruction(ent => ((Extinguisher)ent).DateRecharge.Subtract(DateTime.Now).Days < 365, ent => ((Extinguisher)ent).DateRecharge.SubtractMonths(DateTime.Now).ToString())
        );
    }
}

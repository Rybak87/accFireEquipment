using System.Collections.Generic;

namespace BL
{
    public interface INumber
    {
        int Number { get; set; }
    }
    public interface IPoint
    {
        ScalePoint Point { get; set; }
    }
    public interface ITypes
    {
        string Name { get; set; }
        string Manufacturer { get; set; }
        bool EqualsValues(EntityBase obj);
    }
    public interface ISticker
    {
        bool IsSticker { get; set; }
    }
}

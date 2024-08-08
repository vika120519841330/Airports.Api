namespace Airports.BusinessLayer.Helpers;

public static class NumberHelper
{
    public static double ConvertDegreeToRadian(this decimal degree) => (double)degree * Math.PI / 180;
}

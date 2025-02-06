using Inexto.Mappers;
using Inexto.Models;
using Inexto.Services.IntervalCalculator;
using Inexto.Validators;

var intervalCalcService = new IntervalCalculatorService(InextoMapper.GetMapper(), new IntervalValidator());

var intervalA = new Interval(1, 3);
var intervalB = new Interval(2, 4);

try
{
    Console.WriteLine(intervalCalcService.DoesOverlap(intervalA, intervalB)); // True
}
catch (ArgumentException)
{
    Console.WriteLine("The provided arguments are invalid.");
}
catch (Exception ex)
{
    Console.WriteLine("Something went wrong.");
    Console.WriteLine(ex.ToString());
}

Console.ReadKey();
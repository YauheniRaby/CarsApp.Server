using CarsServer.Bl.DTOs;
using FluentValidation;

namespace BusinessLayer.Vlidators
{
    public class CarDtoValidator : AbstractValidator<CarDto>
    {
        public CarDtoValidator()
        {
            RuleFor(car => car.Model)
                .NotEmpty()
                .MaximumLength(20);
            RuleFor(car => car.Company)
                .NotEmpty();
            RuleFor(car => car.Id)
                .NotEmpty();
            RuleFor(car => car.FileName)
                .NotEmpty();
        }
    }
}
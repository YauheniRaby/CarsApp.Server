using CarsServer.Bl.DTOs;
using FluentValidation;

namespace BusinessLayer.Vlidators
{
    public class CarCreateDtoValidator : AbstractValidator<CarCreateDto>
    {
        public CarCreateDtoValidator()
        {
            RuleFor(car => car.Model)
                .NotEmpty()
                .MaximumLength(20);
            RuleFor(car => car.Company)
                .NotEmpty();
            RuleFor(car => car.FileName)
                .NotEmpty();
        }
    }
}
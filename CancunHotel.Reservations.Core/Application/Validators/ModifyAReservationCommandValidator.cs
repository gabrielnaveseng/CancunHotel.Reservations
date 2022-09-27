using CancunHotel.Reservations.Core.Ports.In.Commands.ModifyAReservation;
using FluentValidation;

namespace CancunHotel.Reservations.Core.Application.Validators
{
    public class ModifyAReservationCommandValidator : AbstractValidator<ModifyAReservationCommand>
    {
        public ModifyAReservationCommandValidator()
        {
            RuleFor(command => command.EndReservationPeriod.Subtract(command.StartReservationPeriod))
                .LessThanOrEqualTo(TimeSpan.FromDays(3))
                .WithMessage("The reservation can’t be longer than 3 days");

            RuleFor(command => command.EndReservationPeriod.Subtract(command.UpdatedAt.Date))
                .LessThanOrEqualTo(TimeSpan.FromDays(30))
                .WithMessage("The reservation can’t be reserved more than 30 days in advance");

            RuleFor(command => command.StartReservationPeriod)
                .GreaterThanOrEqualTo(DateTime.Now.Date.AddDays(1))
                .WithMessage("All reservations need start at least the next day of booking");

            RuleFor(command => command.EndReservationPeriod)
                .GreaterThanOrEqualTo(DateTime.Now.Date)
                .WithMessage("End reservation period can't be in the past");

            RuleFor(command => command.StartReservationPeriod)
                .GreaterThanOrEqualTo(DateTime.Now.Date)
                .WithMessage("Start reservation period can't be in the past");

            RuleFor(command => command.EndReservationPeriod.Subtract(command.StartReservationPeriod))
                .GreaterThanOrEqualTo(TimeSpan.FromDays(1))
                .WithMessage("The end reservation period need to be greater than the start reservation period");
        }
    }
}

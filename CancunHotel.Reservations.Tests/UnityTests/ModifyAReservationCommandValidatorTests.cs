using CancunHotel.Reservations.Core.Application.Validators;
using CancunHotel.Reservations.Core.Ports.In.Commands.ModifyAReservation;

namespace CancunHotel.Reservations.Tests.UnityTests
{
    public class ModifyAReservationCommandValidatorTests
    {
        private readonly ModifyAReservationCommandValidator _modifyAReservationCommandValidator;

        public ModifyAReservationCommandValidatorTests()
        {
            _modifyAReservationCommandValidator = new();
        }

        [Fact]
        public void Validate_WhenPeriodIsInPast_Error()
        {
            ModifyAReservationCommand command = new(DateTime.Now.AddDays(-60), DateTime.Now.AddDays(-10));
            var result = _modifyAReservationCommandValidator.Validate(command);
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count() == 4);
        }


        [Fact]
        public void Validate_WhenPeriodIsBigherThan30DaysAndEndIsLowerThanStart_Error()
        {
            ModifyAReservationCommand command = new(DateTime.Now.AddDays(60), DateTime.Now.AddDays(50));
            var result = _modifyAReservationCommandValidator.Validate(command);
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count() == 2);
        }


        [Fact]
        public void Validate_WhenIsAValidCommand_NoError()
        {
            ModifyAReservationCommand command = new(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
            var result = _modifyAReservationCommandValidator.Validate(command);
            Assert.True(result.IsValid);
        }
    }
}

using CancunHotel.Reservations.Core.Application.Validators;
using CancunHotel.Reservations.Core.Ports.In.Commands.MakeAReservation;

namespace CancunHotel.Reservations.Tests.UnityTests
{
    public class MakeAReservationCommandValidatorTests
    {
        private readonly MakeAReservationCommandValidator _MakeAReservationCommandValidator;

        public MakeAReservationCommandValidatorTests()
        {
            _MakeAReservationCommandValidator = new();
        }

        [Fact]
        public void Validate_WhenPeriodIsInPast_Error()
        {
            MakeAReservationCommand command = new(DateTime.Now.AddDays(-60), DateTime.Now.AddDays(-10), Guid.NewGuid());
            var result = _MakeAReservationCommandValidator.Validate(command);
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count() == 4);
        }


        [Fact]
        public void Validate_WhenPeriodIsBigherThan30DaysAndEndIsLowerThanStart_Error()
        {
            MakeAReservationCommand command = new(DateTime.Now.AddDays(60), DateTime.Now.AddDays(50), Guid.NewGuid());
            var result = _MakeAReservationCommandValidator.Validate(command);
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count() == 2);
        }


        [Fact]
        public void Validate_WhenIsAValidCommand_NoError()
        {
            MakeAReservationCommand command = new(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), Guid.NewGuid());
            var result = _MakeAReservationCommandValidator.Validate(command);
            Assert.True(result.IsValid);
        }
    }
}

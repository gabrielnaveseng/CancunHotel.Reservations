using Mono.Cecil;
using NetArchTest.Rules;

namespace CancunHotel.Reservations.Tests.ArchitecturalTests
{
    public class ArchitecturalConvensionsTest
    {
        [Fact]
        public void UseCases_AreNotAbstractAndImplementPorts_Ok()
        {
            var result = Types.InCurrentDomain()
             .That().ResideInNamespace("CancunHotel.Reservations.Core.Application.UseCases")
             .And().AreClasses()
             .Should().HaveNameEndingWith("UseCase")
             .And().NotBeAbstract()
             .And().NotBeInterfaces()
             .And().BePublic()
             .And().MeetCustomRule(new UseCasesShouldImplementInPortRule())
             .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Entities_AreNotAbstract_Ok()
        {
            var result = Types.InCurrentDomain()
             .That().ResideInNamespace("CancunHotel.Reservations.Core.Domain.Entities")
             .And().AreClasses()
             .Should().NotBeAbstract()
             .And().NotBeInterfaces()
             .And().BePublic()
             .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Adapters_ShouldImplementOutPort_Ok()
        {
            var result = Types.InCurrentDomain()
             .That()
             .ResideInNamespaceContaining("CancunHotel")
             .And().HaveNameEndingWith("Adapter")
             .And().AreClasses()
             .Should().NotBeAbstract()
             .And().NotBeInterfaces()
             .And().BePublic()
             .And().MeetCustomRule(new AdapterShouldImplementOutPortRule())
             .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Controllers_ShouldOnlyKnowInPort_Ok()
        {
            var result = Types.InCurrentDomain()
             .That()
             .ResideInNamespaceContaining("CancunHotel")
             .And().HaveNameEndingWith("Controller")
             .And().AreClasses()
             .Should().NotBeAbstract()
             .And().NotBeInterfaces()
             .And().BePublic()
             .And().MeetCustomRule(new ControllerShouldOnlyKnowInPortRule())
             .GetResult();

            Assert.True(result.IsSuccessful);
        }

        public class UseCasesShouldImplementInPortRule : ICustomRule
        {
            public bool MeetsRule(TypeDefinition type)
            {
                return type.Interfaces
                    .Any(interfaceType => interfaceType.InterfaceType.Namespace.StartsWith("CancunHotel.Reservations.Core.Ports.In."));
            }
        }

        public class AdapterShouldImplementOutPortRule : ICustomRule
        {
            public bool MeetsRule(TypeDefinition type)
            {
                return type.Interfaces
                    .Any(interfaceType => interfaceType.InterfaceType.Namespace.StartsWith("CancunHotel.Reservations.Core.Ports.Out"));
            }
        }

        public class ControllerShouldOnlyKnowInPortRule : ICustomRule
        {
            public bool MeetsRule(TypeDefinition type)
            {
                return type.Fields
                    .Any(field => field.FieldType.Namespace.StartsWith("CancunHotel.Reservations.Core.Ports.In."));
            }
        }
    }
}

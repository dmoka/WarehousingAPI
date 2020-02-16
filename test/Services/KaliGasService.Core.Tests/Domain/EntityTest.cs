using KaliGasService.Core.Domain;
using NFluent;
using Xunit;

namespace KaliGasService.Core.Tests.Domain
{
    public class TestEntity : Entity
    {
        public string Name { get; set; }

        public TestEntity(string name)
        {
            Name = name;
        }
    }

    public class TestEntity2 : Entity
    {
        public string Name { get; set; }

        public TestEntity2(string name)
        {
            Name = name;
        }
    }


    public class EntityTest
    {

        [Fact]
        public void TestEqualsAndOperators()
        {
            var entity = new TestEntity("Name1");
            var entityWithSameProps = new TestEntity("Name1");
            var entity2 = new TestEntity("Name2");
            var entityWithDifferentType = new TestEntity2("Name");

            Check.That(entity.Equals(null)).IsFalse();
            Check.That(entity.Equals(entity2)).IsFalse();
            Check.That(entity.Equals(entityWithSameProps)).IsFalse();
            Check.That(entity.Equals(entityWithDifferentType)).IsFalse();

            Check.That(entity.Equals(entity)).IsTrue();

            Check.That(entity != entity2).IsTrue();
        }

    }
}
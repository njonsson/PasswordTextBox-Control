using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordTextBoxControl.ComponentModel;

namespace PasswordTextBoxControl.Test.Unit.ComponentModel
{
    [TestClass]
    public class ChangeEventArgsTest
    {
        protected ChangeEventArgs<int> ChangeEventArgs;

        [TestInitialize]
        public void TestInitialize()
        {
            ChangeEventArgs = new ChangeEventArgs<int>(123, 456);
        }

        [TestMethod]
        public void HasTheExpectedOldValue()
        {
            Assert.AreEqual(123, ChangeEventArgs.OldValue);
        }

        [TestMethod]
        public void HasTheExpectedNewValue()
        {
            Assert.AreEqual(456, ChangeEventArgs.NewValue);
        }

        [TestMethod]
        public void EqualsAnEquivalentObject()
        {
            var equivalent = new ChangeEventArgs<int>(123, 456);
            Assert.IsTrue(ChangeEventArgs.Equals(equivalent));
            Assert.IsTrue(ChangeEventArgs == equivalent);
            Assert.IsFalse(ChangeEventArgs != equivalent);
        }

        [TestMethod]
        public void DoesNotEqualAnObjectThatDiffersByOldValue()
        {
            var different = new ChangeEventArgs<int>(789, 456);
            Assert.IsFalse(ChangeEventArgs.Equals(different));
            Assert.IsTrue(ChangeEventArgs != different);
            Assert.IsFalse(ChangeEventArgs == different);
        }

        [TestMethod]
        public void DoesNotEqualAnObjectThatDiffersByNewValue()
        {
            var different = new ChangeEventArgs<int>(123, 789);
            Assert.IsFalse(ChangeEventArgs.Equals(different));
            Assert.IsTrue(ChangeEventArgs != different);
            Assert.IsFalse(ChangeEventArgs == different);
        }

        [TestMethod]
        public void HasTheSameHashCodeAsAnEquivalentObject()
        {
            var equivalent = new ChangeEventArgs<int>(123, 456);
            Assert.AreEqual(equivalent.GetHashCode(),
                            ChangeEventArgs.GetHashCode());
        }

        [TestMethod]
        public void HasAHashCodeDifferentFromAnObjectThatDiffersByOldValue()
        {
            var different = new ChangeEventArgs<int>(789, 456);
            Assert.AreNotEqual(different.GetHashCode(),
                               ChangeEventArgs.GetHashCode());
        }

        [TestMethod]
        public void HasAHashCodeDifferentFromAnObjectThatDiffersByNewValue()
        {
            var different = new ChangeEventArgs<int>(123, 789);
            Assert.AreNotEqual(different.GetHashCode(),
                               ChangeEventArgs.GetHashCode());
        }
    }
}

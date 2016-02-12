using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordTextBoxControl.ComponentModel;

namespace PasswordTextBoxControl.Test.Unit.ComponentModel
{
    [TestClass]
    public class CancelChangeEventArgsTest
    {
        protected CancelChangeEventArgs<int> CancelChangeEventArgs;

        [TestInitialize]
        public void TestInitialize()
        {
            CancelChangeEventArgs = new CancelChangeEventArgs<int>(123, 456);
        }

        [TestMethod]
        public void HasTheExpectedOldValue()
        {
            Assert.AreEqual(123, CancelChangeEventArgs.OldValue);
        }

        [TestMethod]
        public void HasTheExpectedNewValue()
        {
            Assert.AreEqual(456, CancelChangeEventArgs.NewValue);
        }

        [TestMethod]
        public void EqualsAnEquivalentObject()
        {
            var equivalent = new CancelChangeEventArgs<int>(123, 456);
            Assert.IsTrue(CancelChangeEventArgs.Equals(equivalent));
            Assert.IsTrue(CancelChangeEventArgs == equivalent);
            Assert.IsFalse(CancelChangeEventArgs != equivalent);
        }

        [TestMethod]
        public void DoesNotEqualAnObjectThatDiffersByOldValue()
        {
            var different = new CancelChangeEventArgs<int>(789, 456);
            Assert.IsFalse(CancelChangeEventArgs.Equals(different));
            Assert.IsTrue(CancelChangeEventArgs != different);
            Assert.IsFalse(CancelChangeEventArgs == different);
        }

        [TestMethod]
        public void DoesNotEqualAnObjectThatDiffersByNewValue()
        {
            var different = new CancelChangeEventArgs<int>(123, 789);
            Assert.IsFalse(CancelChangeEventArgs.Equals(different));
            Assert.IsTrue(CancelChangeEventArgs != different);
            Assert.IsFalse(CancelChangeEventArgs == different);
        }

        [TestMethod]
        public void HasTheSameHashCodeAsAnEquivalentObject()
        {
            var equivalent = new CancelChangeEventArgs<int>(123, 456);
            Assert.AreEqual(equivalent.GetHashCode(),
                            CancelChangeEventArgs.GetHashCode());
        }

        [TestMethod]
        public void HasAHashCodeDifferentFromAnObjectThatDiffersByOldValue()
        {
            var different = new CancelChangeEventArgs<int>(789, 456);
            Assert.AreNotEqual(different.GetHashCode(),
                               CancelChangeEventArgs.GetHashCode());
        }

        [TestMethod]
        public void HasAHashCodeDifferentFromAnObjectThatDiffersByNewValue()
        {
            var different = new CancelChangeEventArgs<int>(123, 789);
            Assert.AreNotEqual(different.GetHashCode(),
                               CancelChangeEventArgs.GetHashCode());
        }
    }
}

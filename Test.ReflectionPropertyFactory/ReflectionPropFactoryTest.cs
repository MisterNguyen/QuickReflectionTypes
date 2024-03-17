using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using ReflectionPropertyFactory;
using Test.ReflectionPropertyFactory;

namespace TestReflectionPropertyFactory
{
    public class ReflectionPropFactoryTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestCreateIntProperties()
        {
            var dict = TestDataHelper.CreateIntProperties();
            object o = ReflectionPropFactory.CreateNewType("TestReflectionEmit", dict, out Type newType);
            int num = 10;
            Dictionary<string, int> valueTracker = new Dictionary<string, int>();

            foreach(var kvp in dict)
            {
                valueTracker.Add(kvp.Key, num);
                newType?.GetProperty(kvp.Key).SetValue(o, num++, new object[0]);
            }

            try
            {
                foreach (var kvp in dict)
                {
                    var reflectedValue = newType?.GetProperty(kvp.Key).GetValue(o);
                    if (reflectedValue == null)
                        Assert.Fail($"The reflected property {kvp.Key} did not find a value.");

                    dynamic typedValue = Convert.ChangeType(reflectedValue, kvp.Value);
                    Assert.AreEqual(valueTracker[kvp.Key], typedValue);
                }

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Test]
        public void TestCreateStringProperties()
        {
            var dict = TestDataHelper.CreateStringProperties();
            object o = ReflectionPropFactory.CreateNewType("TestReflectionEmit", dict, out Type newType);
            int num = 1;
            Dictionary<string, string> valueTracker = new Dictionary<string, string>();
            string text = "testing ";

            foreach (var kvp in dict)
            {
                text += $"{num++}, ";
                valueTracker.Add(kvp.Key, text);
                newType?.GetProperty(kvp.Key).SetValue(o, text, new object[0]);
            }

            try
            {
                foreach (var kvp in dict)
                {
                    var reflectedValue = newType?.GetProperty(kvp.Key).GetValue(o, null);  //have to specify null for index here
                    if (reflectedValue == null)
                        Assert.Fail($"The reflected property {kvp.Key} did not find a value.");

                    dynamic typedValue = Convert.ChangeType(reflectedValue, kvp.Value);
                    Assert.AreEqual(valueTracker[kvp.Key], typedValue);
                }

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }    
        }

        [Test]
        public void TestCreateDoubleProperties()
        {
            var dict = TestDataHelper.CreateDoubleProperties();
            object o = ReflectionPropFactory.CreateNewType("TestReflectionEmit", dict, out Type newType);
            double num = 10;
            Dictionary<string, double> valueTracker = new Dictionary<string, double>();

            foreach (var kvp in dict)
            {
                valueTracker.Add(kvp.Key, num);
                newType?.GetProperty(kvp.Key).SetValue(o, num++, new object[0]);
            }

            try
            {
                foreach (var kvp in dict)
                {
                    var reflectedValue = newType?.GetProperty(kvp.Key).GetValue(o);
                    if (reflectedValue == null)
                        Assert.Fail($"The reflected property {kvp.Key} did not find a value.");

                    dynamic typedValue = Convert.ChangeType(reflectedValue, kvp.Value);
                    Assert.AreEqual(valueTracker[kvp.Key], typedValue);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Test]
        public void TestCreateMixedTypeProperties()
        {
            var dict = TestDataHelper.CreateMixedProperties();
            object o = ReflectionPropFactory.CreateNewType("TestReflectionEmit", dict, out Type newType);
            int num = 10;
            Dictionary<string, object> valueTracker = new Dictionary<string, object>();

            foreach (var kvp in dict)
            {
                if (kvp.Value == typeof(string))
                {
                    string text = "hellow world";
                    valueTracker.Add(kvp.Key, text);
                    newType?.GetProperty(kvp.Key).SetValue(o, text, new object[0]);
                }
                else if (kvp.Value == typeof(int) || kvp.Value == typeof(double))
                {
                    valueTracker.Add(kvp.Key, ++num);
                    dynamic typedValue = Convert.ChangeType(num, kvp.Value);
                    newType?.GetProperty(kvp.Key).SetValue(o, typedValue, new object[0]);
                }
                else
                    throw new Exception("unhandled prop type");
            }

            try
            {
                foreach (var kvp in dict)
                {
                    var reflectedValue = newType?.GetProperty(kvp.Key).GetValue(o);
                    if (reflectedValue == null)
                        Assert.Fail($"The reflected property {kvp.Key} did not find a value.");

                    dynamic typedValue = Convert.ChangeType(reflectedValue, kvp.Value);
                    Assert.AreEqual(valueTracker[kvp.Key], typedValue);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }
    }
}
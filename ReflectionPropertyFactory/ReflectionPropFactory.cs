using System.Reflection;
using System.Reflection.Emit;

namespace ReflectionPropertyFactory
{
    /// <summary>
    /// Intended to quickly create an object with properties that have default getters and setters.
    /// </summary>
    public static class ReflectionPropFactory
    {
        /// <summary>
        /// Takes in the name of the type and a dictionary of properties to be emitted on the type.  
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="propertyDict"></param>
        /// <returns></returns>
        public static object CreateNewType(string typeName, Dictionary<string, Type> propertyDict, out Type reflectType)
        {
            AssemblyName aname = new AssemblyName(typeName + "Assembly");
            AssemblyBuilder assemBuilder = AssemblyBuilder.DefineDynamicAssembly(aname, AssemblyBuilderAccess.Run);
            ModuleBuilder modBuilder = assemBuilder.DefineDynamicModule(typeName + "Module");
            TypeBuilder tb = modBuilder.DefineType(typeName + "Type", TypeAttributes.Public);

            foreach (var kvp in propertyDict)
            {
                AddProperty(ref tb, kvp);
            }

            reflectType = tb.CreateType();
            object? o = Activator.CreateInstance(reflectType);

            return o;
        }

        private static void AddProperty(ref TypeBuilder tb, KeyValuePair<string, Type> kvp)
        {
            string propertyName = kvp.Key;
            Type propertyType = kvp.Value;

            PropertyBuilder prop = tb.DefineProperty(propertyName, PropertyAttributes.None, propertyType, new Type[0]);
            string privatePropertyField = "_" + propertyName.ToLower();
            FieldBuilder field = tb.DefineField(privatePropertyField, propertyType, FieldAttributes.Private);


            MethodBuilder getter = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName, propertyType, new Type[0]);
            ILGenerator getGen = getter.GetILGenerator();
            getGen.Emit(OpCodes.Ldarg_0);
            getGen.Emit(OpCodes.Ldfld, field);
            getGen.Emit(OpCodes.Ret);

            MethodBuilder setter = tb.DefineMethod("set_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName, null, new Type[] { propertyType });
            ILGenerator setGen = setter.GetILGenerator();
            setGen.Emit(OpCodes.Ldarg_0);
            setGen.Emit(OpCodes.Ldarg_1);
            setGen.Emit(OpCodes.Stfld, field);
            setGen.Emit(OpCodes.Ret);

            prop.SetGetMethod(getter);
            prop.SetSetMethod(setter);
        }
    } 
}

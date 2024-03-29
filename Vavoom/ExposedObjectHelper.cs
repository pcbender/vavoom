﻿// ***********************************************************************
// Assembly         : Vavoom
// Author           : Michael Rose
// Created          : 05-20-2019
//
// Last Modified By : Michael Rose
// Last Modified On : 05-20-2019
// ***********************************************************************
// <copyright file="ExposedObjectHelper.cs" company="Heartland Payment Systems">
//     Copyright (c) Heartland Payment Systems, Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Vavoom
{
    /// <summary>
    /// Class ExposedObjectHelper.
    /// </summary>
    /// <autogeneratedoc />
    internal class ExposedObjectHelper
    {
        private static readonly Type CsharpInvokePropertyType =
            typeof(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                .Assembly
                .GetType("Microsoft.CSharp.RuntimeBinder.ICSharpInvokeOrInvokeMemberBinder");

        /// <summary>
        /// Invokes the best method.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="target">The target.</param>
        /// <param name="instanceMethods">The instance methods.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <autogeneratedoc />
        internal static bool InvokeBestMethod(object[] args, object target, List<MethodInfo> instanceMethods, out object result)
        {
            if (instanceMethods.Count == 1)
            {
                // Just one matching instance method - call it
                if (TryInvoke(instanceMethods[0], target, args, out result))
                {
                    return true;
                }
            }
            else if (instanceMethods.Count > 1)
            {
                // Find a method with best matching parameters
                MethodInfo best = null;
                Type[] bestParams = null;
                var actualParams = args.Select(p => p == null ? typeof(object) : p.GetType()).ToArray();

                bool IsAssignableFrom(Type[] a, Type[] b)
                {
                    return !a.Where((t, i) => !t.IsAssignableFrom(b[i])).Any();
                }
                
                foreach (var method in instanceMethods.Where(m => m.GetParameters().Length == args.Length))
                {
                    var mParams = method.GetParameters().Select(x => x.ParameterType).ToArray();
                    
                    if (!IsAssignableFrom(mParams, actualParams)) 
                        continue;

                    if (best != null && !IsAssignableFrom(bestParams, mParams)) 
                        continue;

                    best = method;
                    bestParams = mParams;
                }

                if (best != null && TryInvoke(best, target, args, out result))
                {
                    return true;
                }
            }

            result = null;
            return false;
        }

        /// <summary>
        /// Tries the invoke.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="target">The target.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <autogeneratedoc />
        internal static bool TryInvoke(MethodInfo methodInfo, object target, object[] args, out object result)
        {
            try
            {
                result = methodInfo.Invoke(target, args);
                return true;
            }
            catch (TargetInvocationException) { }
            catch (TargetParameterCountException) { }

            result = null;
            return false;

        }

        /// <summary>
        /// Gets the type arguments.
        /// </summary>
        /// <param name="binder">The binder.</param>
        /// <returns>Type[].</returns>
        /// <autogeneratedoc />
        internal static Type[] GetTypeArgs(InvokeMemberBinder binder)
        {
            if (CsharpInvokePropertyType.IsInstanceOfType(binder))
            {
                var typeArgsProperty = CsharpInvokePropertyType.GetProperty("TypeArguments");
                return ((IEnumerable<Type>)typeArgsProperty.GetValue(binder, null)).ToArray();
            }
            return null;
        }

    }
}



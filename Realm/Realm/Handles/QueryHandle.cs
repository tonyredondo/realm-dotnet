////////////////////////////////////////////////////////////////////////////
//
// Copyright 2016 Realm Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
////////////////////////////////////////////////////////////////////////////

using System;
using System.Runtime.InteropServices;
using Realms.Native;

namespace Realms
{
    // tables and tableviews will always return query c++ classes that must be unbound
    // all other query returning calls (on query itself) will return the same object as was called,
    // and therefore have been changed to void calls in c++ part of binding
    // so these handles always represent a qeury object that should be released when not used anymore
    // the C# binding methods on query simply return self to add the . nottation again
    // A query will be a child of whatever root its creator has as root (queries are usually created by tableviews and tables)
    internal class QueryHandle : RealmHandle
    {
        // This is a delegate type meant to represent one of the "query operator" methods such as float_less and bool_equal
        internal delegate void Operation<T>(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr propertyIndex, T value);

        private static class NativeMethods
        {
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1121 // Use built-in type alias

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_binary_equal", CallingConvention = CallingConvention.Cdecl)]
            public static extern void binary_equal(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx, IntPtr buffer, IntPtr bufferLength, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_binary_not_equal", CallingConvention = CallingConvention.Cdecl)]
            public static extern void binary_not_equal(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx, IntPtr buffer, IntPtr bufferLength, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_string_contains", CallingConvention = CallingConvention.Cdecl)]
            public static extern void string_contains(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx,
                        [MarshalAs(UnmanagedType.LPWStr)] string value, IntPtr valueLen, [MarshalAs(UnmanagedType.U1)] bool caseSensitive, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_string_starts_with", CallingConvention = CallingConvention.Cdecl)]
            public static extern void string_starts_with(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx,
                        [MarshalAs(UnmanagedType.LPWStr)] string value, IntPtr valueLen, [MarshalAs(UnmanagedType.U1)] bool caseSensitive, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_string_ends_with", CallingConvention = CallingConvention.Cdecl)]
            public static extern void string_ends_with(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx,
                        [MarshalAs(UnmanagedType.LPWStr)] string value, IntPtr valueLen, [MarshalAs(UnmanagedType.U1)] bool caseSensitive, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_string_equal", CallingConvention = CallingConvention.Cdecl)]
            public static extern void string_equal(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx,
                        [MarshalAs(UnmanagedType.LPWStr)] string value, IntPtr valueLen, [MarshalAs(UnmanagedType.U1)] bool caseSensitive, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_string_not_equal", CallingConvention = CallingConvention.Cdecl)]
            public static extern void string_not_equal(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx,
                        [MarshalAs(UnmanagedType.LPWStr)] string value, IntPtr valueLen, [MarshalAs(UnmanagedType.U1)] bool caseSensitive, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_string_like", CallingConvention = CallingConvention.Cdecl)]
            public static extern void string_like(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx,
                        [MarshalAs(UnmanagedType.LPWStr)] string value, IntPtr valueLen, [MarshalAs(UnmanagedType.U1)] bool caseSensitive, out NativeException ex);

            // primitive is IntPtr rather than PrimitiveValue due to a bug in .NET Core on Linux and Mac
            // that causes incorrect marshalling of the struct.
            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_primitive_equal", CallingConvention = CallingConvention.Cdecl)]
            public static extern void primitive_equal(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx, IntPtr primitive, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_primitive_not_equal", CallingConvention = CallingConvention.Cdecl)]
            public static extern void primitive_not_equal(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx, IntPtr primitive, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_primitive_less", CallingConvention = CallingConvention.Cdecl)]
            public static extern void primitive_less(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx, IntPtr primitive, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_primitive_less_equal", CallingConvention = CallingConvention.Cdecl)]
            public static extern void primitive_less_equal(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx, IntPtr primitive, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_primitive_greater", CallingConvention = CallingConvention.Cdecl)]
            public static extern void primitive_greater(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx, IntPtr primitive, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_primitive_greater_equal", CallingConvention = CallingConvention.Cdecl)]
            public static extern void primitive_greater_equal(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx, IntPtr primitive, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_object_equal", CallingConvention = CallingConvention.Cdecl)]
            public static extern void query_object_equal(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx, ObjectHandle objectHandle, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_null_equal", CallingConvention = CallingConvention.Cdecl)]
            public static extern void null_equal(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_null_not_equal", CallingConvention = CallingConvention.Cdecl)]
            public static extern void null_not_equal(QueryHandle queryPtr, SharedRealmHandle realm, IntPtr property_ndx, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_not", CallingConvention = CallingConvention.Cdecl)]
            public static extern void not(QueryHandle queryHandle, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_group_begin", CallingConvention = CallingConvention.Cdecl)]
            public static extern void group_begin(QueryHandle queryHandle, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_group_end", CallingConvention = CallingConvention.Cdecl)]
            public static extern void group_end(QueryHandle queryHandle, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_or", CallingConvention = CallingConvention.Cdecl)]
            public static extern void or(QueryHandle queryHandle, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_destroy", CallingConvention = CallingConvention.Cdecl)]
            public static extern void destroy(IntPtr queryHandle);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_count", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr count(QueryHandle QueryHandle, out NativeException ex);

            [DllImport(InteropConfig.DLL_NAME, EntryPoint = "query_create_results", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr create_results(QueryHandle queryPtr, SharedRealmHandle sharedRealm, SortDescriptorHandle sortDescriptor, out NativeException ex);

#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1121 // Use built-in type alias
        }

        public QueryHandle(RealmHandle root, IntPtr handle) : base(root, handle)
        {
        }

        protected override void Unbind()
        {
            NativeMethods.destroy(handle);
        }

        public void BinaryEqual(SharedRealmHandle realm, IntPtr propertyIndex, IntPtr buffer, IntPtr bufferLength)
        {
            NativeMethods.binary_equal(this, realm, propertyIndex, buffer, bufferLength, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public void BinaryNotEqual(SharedRealmHandle realm, IntPtr propertyIndex, IntPtr buffer, IntPtr bufferLength)
        {
            NativeMethods.binary_not_equal(this, realm, propertyIndex, buffer, bufferLength, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        /// <summary>
        /// If the user hasn't specified it, should be caseSensitive=true.
        /// </summary>
        public void StringContains(SharedRealmHandle realm, IntPtr propertyIndex, string value, bool caseSensitive)
        {
            NativeMethods.string_contains(this, realm, propertyIndex, value, (IntPtr)value.Length, caseSensitive, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        /// <summary>
        /// If the user hasn't specified it, should be <c>caseSensitive = true</c>.
        /// </summary>
        public void StringStartsWith(SharedRealmHandle realm, IntPtr propertyIndex, string value, bool caseSensitive)
        {
            NativeMethods.string_starts_with(this, realm, propertyIndex, value, (IntPtr)value.Length, caseSensitive, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        /// <summary>
        /// If the user hasn't specified it, should be <c>caseSensitive = true</c>.
        /// </summary>
        public void StringEndsWith(SharedRealmHandle realm, IntPtr propertyIndex, string value, bool caseSensitive)
        {
            NativeMethods.string_ends_with(this, realm, propertyIndex, value, (IntPtr)value.Length, caseSensitive, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        /// <summary>
        /// If the user hasn't specified it, should be <c>caseSensitive = true</c>.
        /// </summary>
        public void StringEqual(SharedRealmHandle realm, IntPtr propertyIndex, string value, bool caseSensitive)
        {
            NativeMethods.string_equal(this, realm, propertyIndex, value, (IntPtr)value.Length, caseSensitive, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        /// <summary>
        /// If the user hasn't specified it, should be <c>caseSensitive = true</c>.
        /// </summary>
        public void StringNotEqual(SharedRealmHandle realm, IntPtr propertyIndex, string value, bool caseSensitive)
        {
            NativeMethods.string_not_equal(this, realm, propertyIndex, value, (IntPtr)value.Length, caseSensitive, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public void StringLike(SharedRealmHandle realm, IntPtr propertyIndex, string value, bool caseSensitive)
        {
            NativeException nativeException;
            if (value == null)
            {
                NativeMethods.null_equal(this, realm, propertyIndex, out nativeException);
            }
            else
            {
                NativeMethods.string_like(this, realm, propertyIndex, value, (IntPtr)value.Length, caseSensitive, out nativeException);
            }

            nativeException.ThrowIfNecessary();
        }

        public unsafe void PrimitiveEqual(SharedRealmHandle realm, IntPtr propertyIndex, PrimitiveValue value)
        {
            PrimitiveValue* valuePtr = &value;
            NativeMethods.primitive_equal(this, realm, propertyIndex, new IntPtr(valuePtr), out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public unsafe void PrimitiveNotEqual(SharedRealmHandle realm, IntPtr propertyIndex, PrimitiveValue value)
        {
            PrimitiveValue* valuePtr = &value;
            NativeMethods.primitive_not_equal(this, realm, propertyIndex, new IntPtr(valuePtr), out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public unsafe void PrimitiveLess(SharedRealmHandle realm, IntPtr propertyIndex, PrimitiveValue value)
        {
            PrimitiveValue* valuePtr = &value;
            NativeMethods.primitive_less(this, realm, propertyIndex, new IntPtr(valuePtr), out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public unsafe void PrimitiveLessEqual(SharedRealmHandle realm, IntPtr propertyIndex, PrimitiveValue value)
        {
            PrimitiveValue* valuePtr = &value;
            NativeMethods.primitive_less_equal(this, realm, propertyIndex, new IntPtr(valuePtr), out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public unsafe void PrimitiveGreater(SharedRealmHandle realm, IntPtr propertyIndex, PrimitiveValue value)
        {
            PrimitiveValue* valuePtr = &value;
            NativeMethods.primitive_greater(this, realm, propertyIndex, new IntPtr(valuePtr), out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public unsafe void PrimitiveGreaterEqual(SharedRealmHandle realm, IntPtr propertyIndex, PrimitiveValue value)
        {
            PrimitiveValue* valuePtr = &value;
            NativeMethods.primitive_greater_equal(this, realm, propertyIndex, new IntPtr(valuePtr), out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public void ObjectEqual(SharedRealmHandle realm, IntPtr propertyIndex, ObjectHandle objectHandle)
        {
            NativeMethods.query_object_equal(this, realm, propertyIndex, objectHandle, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public void NullEqual(SharedRealmHandle realm, IntPtr propertyIndex)
        {
            NativeMethods.null_equal(this, realm, propertyIndex, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public void NullNotEqual(SharedRealmHandle realm, IntPtr propertyIndex)
        {
            NativeMethods.null_not_equal(this, realm, propertyIndex, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public void Not()
        {
            NativeMethods.not(this, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public void GroupBegin()
        {
            NativeMethods.group_begin(this, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public void GroupEnd()
        {
            NativeMethods.group_end(this, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public void Or()
        {
            NativeMethods.or(this, out var nativeException);
            nativeException.ThrowIfNecessary();
        }

        public int Count()
        {
            var result = NativeMethods.count(this, out var nativeException);
            nativeException.ThrowIfNecessary();
            return (int)result;
        }

        public ResultsHandle CreateResults(SharedRealmHandle sharedRealm, SortDescriptorHandle sortDescriptor)
        {
            var result = NativeMethods.create_results(this, sharedRealm, sortDescriptor, out var nativeException);
            nativeException.ThrowIfNecessary();
            return new ResultsHandle(sharedRealm, result);
        }
    }
}

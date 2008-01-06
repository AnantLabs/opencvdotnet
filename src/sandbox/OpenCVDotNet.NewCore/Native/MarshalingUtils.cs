using System;
using System.Drawing;
using OpenCVDotNet;
using System.Runtime.InteropServices;

namespace OpenCVDotNet
{
    
    internal static class UnmanagedArray
    {

        internal static IntPtr[] ToUnmanaged(float[][] array)
        {
            IntPtr[] secondDimArrayPointers = new IntPtr[array.Length];
            for (int i = 0; i < array.Length; ++i)
            {
                secondDimArrayPointers[i] =
                    Marshal.AllocHGlobal(Marshal.SizeOf(typeof(float)) * array[i].Length);
                Marshal.Copy(array[i], 0, secondDimArrayPointers[i], array[i].Length);
            }
            return secondDimArrayPointers;
        }
        
        internal static void ToManaged(IntPtr[] array, float[][] result)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                Marshal.Copy(array[i], result[i], 0, result[i].Length);
            }
        }

        internal static void FreeUnmanagedArray(IntPtr[] unmanaged)
        {
            foreach (IntPtr p in unmanaged)
            {
                Marshal.FreeHGlobal(p);
            }
        }


    }
}

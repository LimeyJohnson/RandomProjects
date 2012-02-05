using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WiiTest1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
                    
    
    // read/write handle to the device
        private SafeFileHandle mHandle;

// a pretty .NET stream to read/write from/to
private FileStream mStream;
bool found = false;
Guid guid;
uint index = 0;

// 1. get the GUID of the HID class
HIDImports.HidD_GetHidGuid(out guid);

// 2. get a handle to all devices that are part of the HID class
IntPtr hDevInfo = HIDImports.SetupDiGetClassDevs(ref guid, null, IntPtr.Zero, HIDImports.DIGCF_DEVICEINTERFACE);// | HIDImports.DIGCF_PRESENT);

// create a new interface data struct and initialize its size
HIDImports.SP_DEVICE_INTERFACE_DATA diData = new HIDImports.SP_DEVICE_INTERFACE_DATA();
diData.cbSize = Marshal.SizeOf(diData);

// 3. get a device interface to a single device (enumerate all devices)
while(HIDImports.SetupDiEnumDeviceInterfaces(hDevInfo, IntPtr.Zero, ref guid, index, ref diData))
{
    // create a detail struct and set its size
    HIDImports.SP_DEVICE_INTERFACE_DETAIL_DATA diDetail = new HIDImports.SP_DEVICE_INTERFACE_DETAIL_DATA();
    diDetail.cbSize = 5; //should be: (uint)Marshal.SizeOf(diDetail);, but that's the wrong size

    UInt32 size = 0;

    // get the buffer size for this device detail instance (returned in the size parameter)
    HIDImports.SetupDiGetDeviceInterfaceDetail(hDevInfo, ref diData, IntPtr.Zero, 0, out size, IntPtr.Zero);

    // actually get the detail struct
    if(HIDImports.SetupDiGetDeviceInterfaceDetail(hDevInfo, ref diData, ref diDetail, size, out size, IntPtr.Zero))
    {
        // open a read/write handle to our device using the DevicePath returned
        mHandle = HIDImports.CreateFile(diDetail.DevicePath, FileAccess.ReadWrite, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, HIDImports.EFileAttributes.Overlapped, IntPtr.Zero);

        // 4. create an attributes struct and initialize the size
        HIDImports.HIDD_ATTRIBUTES attrib = new HIDImports.HIDD_ATTRIBUTES();
        attrib.Size = Marshal.SizeOf(attrib);

        // get the attributes of the current device
        if(HIDImports.HidD_GetAttributes(mHandle.DangerousGetHandle(), ref attrib))
        {
            // if the vendor and product IDs match up
            if(attrib.VendorID == VID && attrib.ProductID == PID)
            {
                // 5. create a nice .NET FileStream wrapping the handle above
                mStream = new FileStream(mHandle, FileAccess.ReadWrite, REPORT_LENGTH, true);
            }
            else
                mHandle.Close();
        }
    }

    // move to the next device
    index++;
}

// 6. clean up our list
HIDImports.SetupDiDestroyDeviceInfoList(hDevInfo);
}
}